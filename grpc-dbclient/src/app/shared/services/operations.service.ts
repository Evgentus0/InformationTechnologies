import { Injectable } from '@angular/core';
import { grpc } from '@improbable-eng/grpc-web';
import { observable, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DeleteRequest, GetEntityReply, GetEntityRequest, InsertRequest, Row, SelectReply, SelectRequest, UnionRequest, UpdateRequest } from '../../generated/dbmsCore_pb';
import { GrpcDBService, GrpcTableService } from '../../generated/dbmsCore_pb_service';
import { EntityCollection } from '../models/entityCollection';
import { HelperService } from './helper.service';

@Injectable()
export class OperationsService{

    constructor(private helperService: HelperService){}

    select(dbName:string, tableName: string):Observable<EntityCollection> {

        let collectionResult = new EntityCollection();
        let entityRequest = new GetEntityRequest()
        entityRequest.setDbname(dbName);
        entityRequest.setTablename(tableName);
        let resultObserv = new Observable<EntityCollection>(observer=> {
            grpc.unary(GrpcDBService.GetTable, {
                request: entityRequest,
                host: environment.host, 
                onEnd: res => {
                    const { status, message } = res;
                    if (status === grpc.Code.OK && message) {
                        
                        var result = message.toObject() as GetEntityReply.AsObject;
                        collectionResult.columnNames = result.columnsList.map((item)=>item.name);
                        var selectRequest = new SelectRequest();
    
                        selectRequest.setTablename(tableName);
                        selectRequest.setDbname(dbName);
                        grpc.unary(GrpcTableService.Select, {
                            request: selectRequest,
                            host: environment.host, 
                            onEnd: res => {
                                const { status, message } = res;
                                if (status === grpc.Code.OK && message) {
                                    var result = message.toObject() as SelectReply.AsObject;
                                    collectionResult.data = result.rowsList.map((item)=> this.helperService.fromArrayToObject(item.itemsList, collectionResult.columnNames));
                                }
                                observer.next(collectionResult);
                            }
                        });
                    }
                }
            });
        });
        return resultObserv;
        
    }

    update(dbName:string, tableName:string, data:any):Observable<string>{
        let entityRequest = new UpdateRequest()
        entityRequest.setDbname(dbName);
        entityRequest.setTablename(tableName);
        let rows:Row[] = [];
        let row = new Row();
        let items:string[]=[];
        for (const [key] of Object.entries(data)) {
            items.push(data[key]);
          }
        row.setItemsList(items);
        rows.push(row);
        entityRequest.setRowsList(rows);

        let resultObserve = new Observable<string>(observe => {
            grpc.unary(GrpcTableService.Update, {
                request: entityRequest,
                host: environment.host, 
                onEnd: (res) => {
                    const { status, message } = res;
                    if (status === grpc.Code.OK && message){
                        observe.next("Rows successfully updated")
                    }
                    else{
                        observe.error("Error during row updating");
                    }
                }
            });
        });
        return resultObserve;
    }

    delete(dbName:string, tableName:string, guids:string[]): Observable<string>{
        let entityRequest = new DeleteRequest()
        entityRequest.setDbname(dbName);
        entityRequest.setTablename(tableName);
        entityRequest.setGuidsList(guids);

        let resultObserve = new Observable<string>(observe => {
            grpc.unary(GrpcTableService.Delete, {
                request: entityRequest,
                host: environment.host, 
                onEnd: (res) => {
                    const { status, message } = res;
                    if (status === grpc.Code.OK && message){
                        observe.next("Row successfully deleted")
                    }
                    else{
                        observe.error("Error during row deleting");
                    }
                }
            });
        });
        return resultObserve;
    }

    insert(dbName:string, tableName:string, data): Observable<string>{
        let entityRequest = new InsertRequest()
        entityRequest.setDbname(dbName);
        entityRequest.setTablename(tableName);
        let rows:Row[] = [];
        let row = new Row();
        let items:string[]=[];
        for (const [key] of Object.entries(data)) {
            items.push(data[key]);
          }
        row.setItemsList(items);
        rows.push(row);
        entityRequest.setRowsList(rows);

        let resultObserve = new Observable<string>(observe => {
            grpc.unary(GrpcTableService.Insert, {
                request: entityRequest,
                host: environment.host, 
                onEnd: (res) => {
                    const { status, message } = res;
                    if (status === grpc.Code.OK && message){
                        observe.next("Rows successfully added")
                    }
                    else{
                        observe.error("Error during row adding");
                    }
                }
            });
        });
        return resultObserve;
    }

    union(dbName:string, firstTableName:string, secondTableName:string):Observable<EntityCollection> {
        let collectionResult = new EntityCollection();
        let entityRequest = new UnionRequest()
        entityRequest.setDbname(dbName);
        entityRequest.setFirsttablename(firstTableName);
        entityRequest.setSecondtablename(secondTableName);
        let resultObserv = new Observable<EntityCollection>(observer=> {
            grpc.unary(GrpcTableService.Union, {
                request: entityRequest,
                host: environment.host, 
                onEnd: res => {
                    const { status, message } = res;
                    if (status === grpc.Code.OK && message) {
                        
                        const { status, message } = res;
                        if (status === grpc.Code.OK && message) {
                            var result = message.toObject() as SelectReply.AsObject;
                            if(result.rowsList.length > 0){
                                for(let i=1; i<result.rowsList[0].itemsList.length; i++){
                                    collectionResult.columnNames.push("Column" + (i));
                                }
                            }                            
                            collectionResult.data = result.rowsList.map((item)=> this.helperService.fromArrayToObject(item.itemsList, collectionResult.columnNames));
                        }
                        observer.next(collectionResult);
                    }
                    else{
                        observer.error("Can not uion these tables");
                    }
                    
                }
            });
        });
        return resultObserv;
    }
}
import { Injectable } from '@angular/core';
import { grpc } from '@improbable-eng/grpc-web';
import { observable, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GetEntityReply, GetEntityRequest, SelectReply, SelectRequest } from '../../generated/dbmsCore_pb';
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
}
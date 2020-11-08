import { Component, OnInit, ViewChild } from '@angular/core';
import {grpc} from '@improbable-eng/grpc-web';
import {GrpcTableService,GrpcDBService} from './generated/dbmsCore_pb_service';
import {SelectRequest,SelectReply, GetTableListReply, GetTableListRequest} from "./generated/dbmsCore_pb"
import { MatTableComponent } from './mat-table/mat-table.component';
import { environment } from 'src/environments/environment';

import { MatSnackBar } from '@angular/material/snack-bar';
import { OperationsService } from './shared/services/operations.service';
import { EntityCollection } from './shared/models/entityCollection';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  constructor(private _snackBar: MatSnackBar,private _operationService: OperationsService) { }
  
  title = 'dynamic-mat-table';

  tables:string[];
  tableName:string;
  dbName:string;
  dataSet:boolean;

  @ViewChild(MatTableComponent) tableComponent:MatTableComponent;

  ngOnInit(): void {
    this.tables = [];
    this.tableName="";
    this.dbName="";
    this.dataSet = false;
  }

  selectTable(table:string){
    this.tableName = table;
    this._operationService.select(this.dbName,table).subscribe((data: EntityCollection)=>{
      this.tableComponent.dbName = this.dbName;
      this.tableComponent.tableName = this.tableName;
      this.tableComponent.tableCols = data.columnNames;
      this.tableComponent.tableData = data.data;
      this.tableComponent.isReadOnly = false;
      this.dataSet = true;
      this.tableComponent.ngOnInit();
    })
    
  }

  connectToDb(value){
    var tablesRequest = new GetTableListRequest();
    tablesRequest.setDbname(value);
    grpc.unary(GrpcDBService.GetTableList, {
      request: tablesRequest,
      host: environment.host, 
      onEnd: res => {
        const { status, message } = res;
        if (status === grpc.Code.OK && message) {
          var result = message.toObject() as GetTableListReply.AsObject;
          this.tables = result.tablesList;
          this.dbName = value;
          this.openSnackBar("You have been successfully connected to database!");
        } 
      }
    });
    this.tableComponent.clearGrid();
  }

  openSnackBar(message: string) {
    let action = "Ok";
    message = message;

    this._snackBar.open(message, action, {
      duration: 10000,
    });
  }

  union(secondTableName:string){
    this._operationService.union(this.dbName, this.tableName, secondTableName).subscribe((data) => {
      this.tableComponent.tableCols = data.columnNames;
      this.tableComponent.tableData = data.data;
      this.tableComponent.isReadOnly = true;
      this.dataSet = true;
      this.tableComponent.ngOnInit();
    },
    error => this.openSnackBar(error))
  }
}
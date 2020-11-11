import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { OperationsService } from '../shared/services/operations.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EntityCollection } from '../shared/models/entityCollection';

@Component({
  selector: 'app-mat-table',
  templateUrl: './mat-table.component.html',
  styleUrls: ['./mat-table.component.css']
})
export class MatTableComponent implements OnInit {

  dbName:string='';
  tableName:string='';

  tempObject:any = {};
  isReadOnly:boolean;

  tableDataSrc: any;

  tableCols: string[] = [];
  tableData: {}[] = [];

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;


  constructor(public dialog: MatDialog, 
    private _operationService: OperationsService,
    private _snackBar: MatSnackBar) { }

  ngOnInit() {
    this.updateTempObject();

    if(!this.isReadOnly)
      this.tableCols.push("Actions");

    this.tableDataSrc = new MatTableDataSource(this.tableData);
    this.tableDataSrc.sort = this.sort;
    this.tableDataSrc.paginator = this.paginator;
  }

  updateTempObject(){
    for(var colName of this.tableCols){
      this.tempObject[colName] = "";
    }
  }

  onSearchInput(ev) {
    const searchTarget = ev.target.value;
    this.tableDataSrc.filter = searchTarget.trim().toLowerCase();
  }

  clearGrid(){
    this.tableCols = [];
    this.tableData = [];
  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '500px',
      data:obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result.event == 'Add'){
        this.addRowData(result.data);
      }
      else if(result.event == 'Update'){
        this.updateRowData(result.data);
      }
      else if(result.event == 'Delete'){
        this.deleteRowData(result.data);
      }
    });
  }

  addRowData(data){
    delete data.action;
    this._operationService.insert(this.dbName, this.tableName, data).subscribe(res =>{
      this._operationService.select(this.dbName, this.tableName).subscribe((data: EntityCollection)=>{
        this.tableData = data.data;
        this.tableCols = this.tableCols.filter(x => x != "Actions");
        this.ngOnInit();

        this.openSnackBar(res);
      })
    },
    error => {
      this.openSnackBar(error)
    })
  }
  updateRowData(data){
    delete data.action;
    this._operationService.update(this.dbName, this.tableName, data).subscribe(res =>{
      let tableDataArray = this.tableData.filter(x => x["_id_guid"] != data["_id_guid"]);
      tableDataArray.push(data);
      this.tableData = tableDataArray;
      this.tableDataSrc = new MatTableDataSource(this.tableData);
      
      this.openSnackBar(res);
    },
    error => {
      this.openSnackBar(error)
    })
  }
  deleteRowData(data){
    this._operationService.delete(this.dbName, this.tableName, [data["_id_guid"]]).subscribe(res =>{
      let tableDataArray = this.tableData.filter(x => x["_id_guid"] != data["_id_guid"]);
      this.tableData = tableDataArray;
      this.tableDataSrc = new MatTableDataSource(this.tableData);
      
      this.openSnackBar(res);
    },
    error => {
      this.openSnackBar(error)
    })
  }
  openSnackBar(message: string) {
    let action = "Ok";
    message = message;

    this._snackBar.open(message, action, {
      duration: 10000,
    });
  }
}
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import {MatButtonModule} from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { MatTableComponent } from './mat-table/mat-table.component';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { HelperService } from './shared/services/helper.service';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { OperationsService } from './shared/services/operations.service';
import { DialogBoxComponent } from './dialog-box/dialog-box.component';
import {MatInputModule} from '@angular/material/input';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    MatTableComponent,
    DialogBoxComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSnackBarModule,
    AppRoutingModule,
    MatButtonModule,
    MatDialogModule,
    FormsModule,
    MatInputModule,
  ],
  providers: [
    HelperService,
    OperationsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

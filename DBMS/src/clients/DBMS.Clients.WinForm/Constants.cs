﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Forms;

namespace DBMS.Clients.WinForm
{
    static class Constants
    {
        public static class MainForm
        {
            public const string File = "File";
            public const string About = "About";
            public const string Instruction = "Instruction";
            public const string Open = "Open DB file";
            public const string CreateNew = "Create new DataBase";
            public const string DeleteSelectedRows = "Delete selected rows";
            public const string UspdateRow = "Update selected rows";
        }

        public static class DbPanelControl
        {
            public const string AddNewTable = "Add new table";
            public const string NewTableName = "New table name";
            public const string UnionTables = "Union tables";
            public const string EnterPathForDb = "Enter db path";
        }

        public static class TableButtonControl
        {
            public const string Select = "Select top 100";
            public const string EditSchema = "Edit table schema";
            public const string EmptyTable = "Table is empty";
            public const string InsertData = "Insert rows";
            public const string DeleteData = "Delete rows";
            public const string AddConditions = "Select conditions";
            public const string UnionTables = "Union";
            public const string InsertIncorrectData = "In row number {0} in column {1} value \"{2}\" has incorrect type!";
            public const string CantOpenFile= "Can not open file";
            public const string EnterDescription = "Enter description";
            public const string UpdatedSuccess = "Rows was updated successfuly";
        }   

        public static class EnterNewDb
        {
            public const string EnterData = "Please enter the all required fields";
        }

        public static class TableSchemaForm
        {
            public const string FieldName = "Field name";
            public const string FieldType = "Field type";
            public const string FieldValidators = "Field validators";
            public const string Validators = "Validators";
            public const string DeleteField = "Delete field";
            public const string AddField = "Add field";
            public const string ChooseType = "Choose type";
        }

        public static class ValidatorsForm
        {
            public const string AddValidator = "Add validator";
            public const string Operation = "Operation";
            public const string Value = "Value";
            public const string DeleteValidator = "Delete validator";
            public const string ValidatorIsNotAvailable = "Validator for this type is not available";
        }

        public static class SelectConditions
        {
            public const string DeleteCondition = "Delete condition";
            public const string FieldName = "Field name";
        }

        public static class UnionForm
        {
            public const string DeleteTable = "Delete table";
        }
    }
}

using System;
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
        }

        public static class TableButtonControl
        {
            public const string Select = "Select top 100";
            public const string EditSchema = "Edit table schema";
        }   

        public static class EnterDbNameForm
        {
            public const string EnterData = "Please enter the all required fields";
        }
    }
}

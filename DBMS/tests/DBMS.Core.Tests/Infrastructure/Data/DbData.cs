using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;
using static DBMS.Core.Tests.Infrastructure.Data.DbData.CommonData;

namespace DBMS.Core.Tests.Infrastructure.Data
{
    public class DbData
    {
        internal static class CommonData
        {
            public const string StringField = "_StrField";
            public const string IntField = "_IntField";
            public const string IntervalField = "_IntervalString";

            public const string Server = "Server";
        }

        public static class Tables
        {
            public static class TableData1
            {
                private const int _tableIntConstant = 1;
                private const int _realInterval = 1;
                private const string _tableStringConstant = nameof(TableData1) + "str";

                public const string Name = nameof(TableData1);

                public static List<Field> Fields = new List<Field>
                {
                    new Field
                    {
                        Name = nameof(TableData1) + StringField,
                        Type = SupportedTypes.String
                    },
                    new Field
                    {
                        Name = nameof(TableData1) + IntField,
                        Type = SupportedTypes.Integer
                    },
                    new Field
                    {
                        Name = nameof(TableData1) + IntervalField,
                        Type = SupportedTypes.RealInterval
                    }
                };

                public static List<List<object>> Data = new List<List<object>>
                {
                    new List<object>{$"{_tableStringConstant}1", _tableIntConstant + 1,
                        new RealInterval{ From = _tableIntConstant + 1, To = _tableIntConstant + _realInterval + 1 } },
                    new List<object>{$"{_tableStringConstant}2", _tableIntConstant + 2,
                        new RealInterval{ From = _tableIntConstant + 2, To = _tableIntConstant + _realInterval + 2 } },
                    new List<object>{$"{_tableStringConstant}3", _tableIntConstant + 3,
                        new RealInterval{ From = _tableIntConstant + 3, To = _tableIntConstant + _realInterval + 3 } }
                };
            }

            public static class TableData2
            {
                private const int _tableIntConstant = 2;
                private const int _realInterval = 2;
                private const string _tableStringConstant = nameof(TableData2) + "str";

                public const string Name = nameof(TableData2);

                public static List<Field> Fields = new List<Field>
                {
                    new Field
                    {
                        Name = nameof(TableData2) + StringField,
                        Type = SupportedTypes.String
                    },
                    new Field
                    {
                        Name = nameof(TableData2) + IntField,
                        Type = SupportedTypes.Integer
                    },
                    new Field
                    {
                        Name = nameof(TableData2) + IntervalField,
                        Type = SupportedTypes.RealInterval
                    }
                };

                public static List<List<object>> Data = new List<List<object>>
                {
                    new List<object>{$"{_tableStringConstant}1", _tableIntConstant + 1,
                        new RealInterval{ From = _tableIntConstant + 1, To = _tableIntConstant + _realInterval + 1 } },
                    new List<object>{$"{_tableStringConstant}2", _tableIntConstant + 2,
                        new RealInterval{ From = _tableIntConstant + 2, To = _tableIntConstant + _realInterval + 2 } },
                    new List<object>{$"{_tableStringConstant}3", _tableIntConstant + 3,
                        new RealInterval{ From = _tableIntConstant + 3, To = _tableIntConstant + _realInterval + 3 } }
                };
            }
        }
        public static class DataBases
        {
            public static class DataBase1
            {
                public static string Name = nameof(DataBase1);
                public static string RootPath = $"{Server}|{Name}";
            }
            public static class DataBase2
            {
                public static string Name = nameof(DataBase2);
                public static string RootPath = $"{Server}|{Name}";
            }
        }
    }
}

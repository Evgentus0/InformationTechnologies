﻿using DBMS.SqlServerSource;
using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Sources
{
    public class MongoDbSource : BaseSource
    {
        public override string Type => typeof(MongoDbSource).AssemblyQualifiedName;

        public override long SizeInBytes => default;

        public override bool AllowMultipleSource => false;

        public MongoDbSource(IDbClientFactory dbClientFactory) : base(dbClientFactory)
        { }

        public MongoDbSource()
        { }

        protected override IDbClient DbClient
        {
            get
            {
                if (_dbClient == null)
                {
                    var data = Url.Split(Constants.Separator);
                    _dbClient = DbClientFactory.GetMongoClient(data[0], data[1], data[2]);
                }

                return _dbClient;
            }
            set
            {
                _dbClient = value;
            }
        }

        public override void SetUrl(DataBase db, Table table)
        {
            Url = $"{db.Settings.RootPath}{Constants.Separator}{db.Name}{Constants.Separator}{table.Name}";
            Create();
        }
    }
}

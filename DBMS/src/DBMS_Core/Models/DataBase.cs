using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DBMS_Core.Models
{
    public class DataBase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("settings")]
        public Settings Settings { get; set; }
        [JsonPropertyName("tables")]
        public List<Table> Tables { get; set; }

        public DataBase()
        {
            Tables = new List<Table>();
            Settings = new Settings();
        }

        public override bool Equals(object obj)
        {
            var db = (DataBase)obj;

            return Name == db.Name
                && Settings.Equals(db.Settings)
                && new Func<bool>(() =>  //Compare table
                {
                    if (Tables.Count == db.Tables.Count)
                    {
                        for (int i = 0; i < Tables.Count; i++)
                        {
                            if (!Tables[i].Equals(db.Tables[i]))
                                return false;
                        }
                        return true;
                    }
                    return false;
                }).Invoke();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

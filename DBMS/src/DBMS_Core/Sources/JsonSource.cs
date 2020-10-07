using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBMS_Core.Sources
{
    public class JsonSource: ISource
    {
        public string Url { get; set; }
        public string Type => typeof(JsonSource).AssemblyQualifiedName;
        public long SizeInBytes => new FileInfo(Url).Length;

        public async Task<List<List<object>>> GetDataAsync()
        {
            using (var data = File.OpenRead(Url))
            {
                var result = await JsonSerializer.DeserializeAsync<List<List<object>>>(data);

                return result;
            }
        }

        public List<List<object>> GetData()
        {
            var data = File.ReadAllText(Url);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<List<List<object>>>(data);

            return result;
        }

        public void WriteData(List<List<object>> data)
        {
            if (!(data == null || data.Count == 0))
            {
                var newStringData = JsonSerializer.Serialize(data);

                File.WriteAllText(Url, newStringData);
            }
        }

        public async Task WriteDataAsync(List<List<object>> data)
        {
            if (!(data == null || data.Count == 0))
            {
                using (var streamData = File.OpenWrite(Url))
                {
                    await JsonSerializer.SerializeAsync(streamData, data);
                }
            }
        }
    }
}

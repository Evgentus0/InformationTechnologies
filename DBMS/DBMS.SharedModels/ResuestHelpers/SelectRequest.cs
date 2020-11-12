using DBMS.SharedModels.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.SharedModels.ResuestHelpers
{
    public class SelectRequest
    {
        public int Offset { get; set; }
        public int Top { get; set; }
        public Dictionary<string, List<Validator>> Conditions { get; set; }
    }
}

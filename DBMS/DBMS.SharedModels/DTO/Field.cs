using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.SharedModels.DTO
{
    public class Field
    {
        public string Name { get; set; }
        public SupportedTypes Type{ get; set; }
        public List<Validator> Validators { get; set; }

    }
}

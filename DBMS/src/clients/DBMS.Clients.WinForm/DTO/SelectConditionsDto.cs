using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Clients.WinForm.DTO
{
    public class SelectConditionsDto
    {
        public int Top { get; set; }
        public int Offset { get; set; }

        public Dictionary<string, List<IValidator>> Validators{ get; set; }

        public SelectConditionsDto()
        {
            Validators = new Dictionary<string, List<IValidator>>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Models.Types
{
    public class RealInterval
    {
        public double From { get; set; }
        public double To { get; set; }

        public override bool Equals(object obj)
        {
            var otherInterval = (RealInterval)obj;

            return To == otherInterval.To
                && From == otherInterval.From;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

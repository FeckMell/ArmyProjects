using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deji.Source
{
    public class UnitsElement
    {
        private int thatID;
        private string thatName;
        private string thatMillitaryPart;

        public UnitsElement(int id_, string name_, string milpart_)
        {
            thatID = id_;
            thatName = name_;
            thatMillitaryPart = milpart_;
        }
    }
}

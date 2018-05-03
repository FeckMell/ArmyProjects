using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    public class UvalTable
    {
        private List<UvalTableElement> thatTables;

        private UvalTable() { }
        public void Init()
        {
            thatTables = new List<UvalTableElement>();
        }
    }
}

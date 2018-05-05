using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    public class UvalTableGUI
    {
        private List<UvalTableElementGUI> thatTables;

        private UvalTableGUI() { }
        public void Init()
        {
            thatTables = new List<UvalTableElementGUI>();
        }
    }
}

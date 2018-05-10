using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    class UvalTableElement
    {
        private ObservableCollection<UvalTableElementRow> thatData;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private UvalTableElement() { }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void Init()
        {
            thatData = new ObservableCollection<UvalTableElementRow>();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

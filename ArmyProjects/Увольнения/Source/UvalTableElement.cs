using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    public class UvalTableElement
    {
        private ObservableCollection<UvalTableElementRow> thatData = new ObservableCollection<UvalTableElementRow>();
        private int thatPeriodID;

        public ObservableCollection<UvalTableElementRow> ThatData { get => thatData; set => thatData = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public UvalTableElement(List<List<object>> records_)
        {
            if (records_ != null && records_.Count!=0) ThatPeriodID = Int32.Parse(records_[0][2].ToString());
            foreach(var e in records_)
            {
                UvalTableElementRow row = new UvalTableElementRow()
                {
                    ThatID = Int32.Parse(e[0].ToString()),
                    ThatManID = Int32.Parse(e[1].ToString()),
                    ThatPeriodID = Int32.Parse(e[2].ToString()),
                    ThatData = (List<int>)e[3]
                };
                ThatData.Add(row);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

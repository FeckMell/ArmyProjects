using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    public class UvalSubTable
    {
        private ObservableCollection<UvalSubTableRow> thatData = new ObservableCollection<UvalSubTableRow>();
        private int thatPeriodID;

        public ObservableCollection<UvalSubTableRow> ThatData { get => thatData; set => thatData = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public UvalSubTable(List<List<object>> records_)
        {
            foreach(var e in records_)
            {
                List<string> e_3 = (List<string>)e[3];
                List<int> e_3int = new List<int>();
                foreach(var e1 in e_3)
                {
                    e_3int.Add(Int32.Parse(e1.ToString()));
                }
                UvalSubTableRow row = new UvalSubTableRow
                {
                    ThatID = Int32.Parse(e[0].ToString()),
                    ThatManID = Int32.Parse(e[1].ToString()),
                    ThatPeriodID = Int32.Parse(e[2].ToString()),
                    ThatData = e_3int
                };
                ThatData.Add(row);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    public class UvalSubTableRow
    {
        private int thatID;
        private int thatManID;
        private int thatPeriodID;
        private List<int> thatData = new List<int>();
        private string thatColor;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManID { get => thatManID; set => thatManID = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public List<int> ThatData { get => thatData; set { thatData = value; thatData.Add(Sum()); } }
        public string ThatColor { get => thatColor; set => thatColor = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private int Sum()
        {
            int result = 0;
            foreach (var e in ThatData) result += e;
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    public class UvalTableElementRow
    {
        private int thatID;
        private string thatPeriodName;
        private List<int> thatData;
        private int thatManID;
        private int thatPeriodID;
        private string thatColor;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string ThatPeriodName { get => thatPeriodName; set => thatPeriodName = value; }
        public List<int> ThatData { get => thatData; set { thatData = value; thatData.Add(Sum()); } }
        public int ThatManID { get => thatManID; set => thatManID = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public string ThatColor { get => thatColor; set => thatColor = value; }
        public int ThatID { get => thatID; set => thatID = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private int Sum()
        {
            int result = 0;
            foreach (var e in thatData) result += e;
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

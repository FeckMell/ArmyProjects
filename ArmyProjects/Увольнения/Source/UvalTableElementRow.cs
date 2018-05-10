using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    public class UvalTableElementRow
    {
        private uint thatID;
        private string thatPeriodName;
        private List<uint> thatData;
        private uint thatManID;
        private uint thatPeriodID;
        private string thatColor;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string ThatPeriodName { get => thatPeriodName; set => thatPeriodName = value; }
        public List<uint> ThatData { get => thatData; set => thatData = value; }
        public uint ThatManID { get => thatManID; set => thatManID = value; }
        public uint ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public string ThatColor { get => thatColor; set => thatColor = value; }
        public uint ThatID { get => thatID; set => thatID = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public UvalTableElementRow(uint id_, string periodname_, List<uint> data_, uint manid_, uint periodid_)
        {
            ThatPeriodName = periodname_;
            ThatData = new List<uint>(data_.ToArray<uint>());
            ThatManID = manid_;
            ThatPeriodID = periodid_;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void EditValue(uint val_, int i_)
        {
            ThatData[i_] = val_;
            uint sum = 0;
            for (int i = 0; i < ThatData.Count - 1; ++i)
                sum += ThatData[i];
            ThatData[ThatData.Count - 1] = sum;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SetColor(string color_)
        {
            ThatColor = color_;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

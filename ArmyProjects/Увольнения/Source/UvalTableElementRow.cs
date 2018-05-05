using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    public class UvalTableElementRow
    {
        private string thatMonth;
        private string thatYear;
        private List<uint> thatData;
        private uint thatManID;
        private uint thatPeriodID;
        private string thatColor;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string ThatMonth { get => thatMonth; set => thatMonth = value; }
        public string ThatYear { get => thatYear; set => thatYear = value; }
        public List<uint> ThatData { get => thatData; set => thatData = value; }
        public uint ThatManID { get => thatManID; set => thatManID = value; }
        public uint ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public string ThatColor { get => thatColor; set => thatColor = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public UvalTableElementRow(string month_, string year_, List<uint> data_, uint manid_, uint periodid_)
        {
            ThatMonth = month_;
            ThatYear = year_;
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

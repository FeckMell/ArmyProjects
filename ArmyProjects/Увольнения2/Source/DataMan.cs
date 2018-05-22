using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    static public class DataMan
    {
        static private ObservableCollection<DataManEntry> thatData = new ObservableCollection<DataManEntry>();

        static public ObservableCollection<DataManEntry> ThatData { get => thatData; set => thatData = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void SetData(List<List<object>> data_)
        {
            foreach (var i in data_)
            {
                DataManEntry man = new DataManEntry
                {
                    ThatID = Int32.Parse(i[0].ToString()),
                    ThatManNum = Int32.Parse(i[2].ToString()),
                    ThatName = i[1].ToString(),
                    ThatPlatoon = Int32.Parse(i[3].ToString())
                };
                ThatData.Add(man);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public string GetNameByID(int id_)
        {
            string result = "";
            foreach (var e in ThatData)
            {
                if (e.ThatID == id_) result = e.ThatName;
            }
            return result;
        }
    }
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    public class DataManEntry
    {
        private int thatID;
        private int thatManNum;
        private int thatPlatoon;
        private string thatName;
        private string thatColor;

        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManNum { get => thatManNum; set => thatManNum = value; }
        public int ThatPlatoon { get => thatPlatoon; set => thatPlatoon = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public string ThatColor { get => thatColor; set => thatColor = value; }
    }
}
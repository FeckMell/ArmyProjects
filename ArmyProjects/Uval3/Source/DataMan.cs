using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Uval3.Source
{
    static public class DataMan
    {
        static private ObservableCollection<DataManEntry> thatData = new ObservableCollection<DataManEntry>();

        static public ObservableCollection<DataManEntry> ThatData { get => thatData; set => thatData = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void UpdateDataFromBD(string filter_)
        {
            List<List<object>> rawdata;
            if(string.IsNullOrEmpty(filter_))
            {
                rawdata = SQLConnector.Select("SELECT * FROM Man");
            }
            else
            {
                rawdata = SQLConnector.Select(string.Format("SELECT * FROM Man WHERE Name LIKE '%{0}%' COLLATE NOCASE", filter_));
            }
            foreach (var e in rawdata)
            {
                DataManEntry man = new DataManEntry(e);
                ThatData.Add(man);
                man.UpdateFizo();
                man.UpdateBadBoy();
                man.UpdateRecords();
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public DataManEntry FindByID(int id_)
        {
            for(var i = 0; i<ThatData.Count;++i)
            {
                if (ThatData[i].ThatID == id_) return ThatData[i];
            }
            return null;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Clear()
        {
            ThatData.Clear();
        }
    }
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
        //private int thatColor;
        private ColorEntry thatColor;

        private FizoEntry thatFizo;
        private BadBoyEntry thatBadBoy;
        private List<RecordsEntry> thatRecords = new List<RecordsEntry>();


        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManNum { get => thatManNum; set => thatManNum = value; }
        public int ThatPlatoon { get => thatPlatoon; set => thatPlatoon = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public ColorEntry ThatColor { get => thatColor; set => thatColor = value; }
        public FizoEntry ThatFizo { get => thatFizo; set => thatFizo = value; }
        public BadBoyEntry ThatBadBoy { get => thatBadBoy; set => thatBadBoy = value; }
        public List<RecordsEntry> ThatRecords { get => thatRecords; set => thatRecords = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public DataManEntry(List<object> e_)
        {
            ThatID = Int32.Parse(e_[0].ToString());
            ThatManNum = Int32.Parse(e_[2].ToString());
            ThatName = e_[1].ToString();
            ThatPlatoon = Int32.Parse(e_[3].ToString());
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void UpdateFizo()
        {
            ThatFizo = Fizo.Update(ThatID, ThatName);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void UpdateBadBoy()
        {
            ThatBadBoy = BadBoy.Update(ThatID, ThatName);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void UpdateRecords()
        {
            ThatRecords = Records.Update(ThatID);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public bool CheckFizoIsGood()
        {
            return ThatFizo.CheckIsGood();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public bool CheckBadBoyIsGood()
        {
            return ThatBadBoy.CheckIsGood();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

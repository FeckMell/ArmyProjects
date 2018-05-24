using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uval3.Source
{
    static public class Records
    {
        static private List<RecordsEntry> thatData = new List<RecordsEntry>();

        public static List<RecordsEntry> ThatData { get => thatData; set => thatData = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public List<RecordsEntry> Update(int manid_)
        {
            List<RecordsEntry> result = new List<RecordsEntry>();
            foreach(var e in Periods.ThatData)
            {
                List<List<object>> rawdata = SQLConnector.Select(string.Format("SELECT * FROM Records WHERE idMan={0} and idPeriod={1}", manid_, e.ThatPeriodID));
                if (rawdata.Count == 0) return null;

                RecordsEntry record = new RecordsEntry(rawdata[0]);
                ThatData.Add(record);
                result.Add(record);
                var a1 = Periods.GetPeriodByPeriodID(e.ThatPeriodID);
                var a2 = Periods.GetPeriodByPeriodID(e.ThatPeriodID).ThatRecords;
                Periods.GetPeriodByPeriodID(e.ThatPeriodID).ThatRecords.Add(record);
            }

            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public List<RecordsEntry> GetRecordsByPeriodID(int periodid_)
        {
            List<RecordsEntry> result = new List<RecordsEntry>();
            foreach (var e in ThatData) if (e.ThatPeriodID == periodid_) result.Add(e);
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public List<RecordsEntry> GetRecordsByManID(int manid_)
        {
            List<RecordsEntry> result = new List<RecordsEntry>();
            foreach (var e in ThatData) if (e.ThatManID == manid_) result.Add(e);
            return result;
        }
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
    public class RecordsEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged() { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatData")); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatResult")); }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private int thatID;
        private int thatManID;
        private int thatPeriodID;
        private List<string> thatData;
        private string thatResult;
        private ColorEntry thatColor;

        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManID { get => thatManID; set => thatManID = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public List<string> ThatData { get => thatData; set => thatData = value; }
        public string ThatResult { get => thatResult; set => thatResult = value; }
        public ColorEntry ThatColor { get => thatColor; set => thatColor = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public RecordsEntry(List<object> e_)
        {
            ThatID = Int32.Parse(e_[0].ToString());
            ThatManID = Int32.Parse(e_[1].ToString());
            ThatPeriodID = Int32.Parse(e_[2].ToString());
            ThatData = ParseDates(e_[3].ToString());
            ThatResult = SumData();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private List<string> ParseDates(string data_)
        {
            return new List<string>(data_.Split(','));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string SumData()
        {
            int result = 0;
            foreach (var e in ThatData) result += GetValue(e);
            return result.ToString();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private int GetValue(string e_)
        {
            if (string.IsNullOrEmpty(e_)) return 0;
            else return Int32.Parse(e_);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SaveDataToDB()
        {
            string data = "";
            for(var i=0;i<ThatData.Count;++i)
            {
                if (string.IsNullOrEmpty(ThatData[i])) data += ",";
                else data += ThatData[i] + ",";
            }
            data = data.Remove(data.Length - 1);
            SQLConnector.NoReturnQuery(string.Format("UPDATE Records SET Data='{0}' WHERE id={1}", data, ThatID));
        }
    }
}

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
        static public List<RecordsEntry> ParseForMan(object e_, DataManEntry man_)
        {
            // "PeriodID:d,d,d,d|PeriodID:d,d,d,d|PeriodID:d,d,d,d"
            List<RecordsEntry> result = new List<RecordsEntry>();
            string raw_data = e_.ToString();

            if (string.IsNullOrEmpty(raw_data)) return result;

            List<string> records_by_periods = new List<string>(raw_data.Split('|'));
            foreach (var raw_record in records_by_periods)
            {
                //RecordsEntry rec = new RecordsEntry(raw_record, manid_, color_);
                RecordsEntry rec = new RecordsEntry(raw_record, man_);
                ThatData.Add(rec);
                result.Add(rec);
                Periods.GetPeriodByID(rec.ThatPeriodID).ThatRecords.Add(rec);
            }
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
        private int thatManID;
        private int thatPeriodID;
        private List<string> thatData;
        private string thatResult;
        private ColorEntry thatColor;
        private DataManEntry thatMan;

        public int ThatManID { get => thatManID; set => thatManID = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public List<string> ThatData { get => thatData; set => thatData = value; }
        public List<string> ThatRecords { get => ThatData; set => ThatData = value; }
        public string ThatResult { get => thatResult; set => thatResult = value; }
        public ColorEntry ThatColor { get => thatColor; set => thatColor = value; }
        public DataManEntry ThatMan { get => thatMan; set => thatMan = value; }



        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public RecordsEntry(int data_amount_, int periodid_)
        {
            ThatPeriodID = periodid_;
            ThatRecords = new List<string>();
            for (int i = 0; i < data_amount_; ++i) ThatRecords.Add("");
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public RecordsEntry(string raw_record_, DataManEntry man_)
        {
            // "PeriodID:d,d,d,d"
            ThatMan = man_;
            ThatManID = man_.ThatID;
            ThatColor = man_.ThatColor;
            List<string> tmp1 = new List<string>(raw_record_.Split(':'));
            ThatPeriodID = Int32.Parse(tmp1[0]);
            ThatRecords = new List<string>(tmp1[1].Split(','));
            ThatResult = SumData();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string SumData()
        {
            int result = 0;
            foreach (var e in ThatRecords) result += GetValue(e);
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
        public string ToStringForDB()
        {
            string result = ThatPeriodID.ToString() + ":";
            for(var i=0;i<ThatRecords.Count;++i)
            {
                if (string.IsNullOrEmpty(ThatRecords[i])) result += ",";
                else result += ThatRecords[i] + ",";
            }
            result = result.Remove(result.Length - 1);

            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void ChangeWeeksAmount(int new_weeks_)
        {
            if(new_weeks_>ThatData.Count)
            {
                int dif = new_weeks_ - ThatData.Count;
                for (int i = 0; i < dif; ++i)
                {
                    ThatData.Add("");
                }
            }
            else if(new_weeks_<ThatData.Count)
            {
                int dif = ThatData.Count - new_weeks_;
                for (int i = 0; i < dif; ++i)
                {
                    ThatData.RemoveAt(ThatData.Count - 1);
                }
            }
        }
    }
}

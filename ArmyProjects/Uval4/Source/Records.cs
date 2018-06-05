using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uval4.Source
{
    public class RecordEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged() { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatData")); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatResult")); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatColor")); }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private int thatManID;
        private int thatPeriodID;
        private List<string> thatData;
        private string thatResult;
        private ColorEntry thatColor;
        private ManEntry thatMan;
        private PeriodsEntry thatPeriod;

        public int ThatManID { get => thatManID; set => thatManID = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public List<string> ThatData { get => thatData; set => thatData = value; }
        public List<string> ThatRecords { get => ThatData; set => ThatData = value; }
        public string ThatResult { get => thatResult; set => thatResult = value; }
        public ColorEntry ThatColor { get => thatColor; set => thatColor = value; }
        public ManEntry ThatMan { get => thatMan; set => thatMan = value; }
        public PeriodsEntry ThatPeriod { get => thatPeriod; set => thatPeriod = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public RecordEntry(string raw_record_, ManEntry man_)
        {
            ThatMan = man_;
            ThatManID = man_.ThatID;
            ThatColor = man_.ThatColor;
            List<string> tmp1 = new List<string>(raw_record_.Split(':'));
            ThatPeriodID = Int32.Parse(tmp1[0]);
            ThatRecords = new List<string>(tmp1[1].Split(','));
            ThatResult = SumData();

            ThatPeriod = Periods.GetPeriodByID(ThatPeriodID);
            ThatPeriod.ThatRecords.Add(this);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public RecordEntry(int period_id_, int weeks_)
        {
            ThatPeriodID = period_id_;
            ThatRecords = new List<string>();
            for (int i = 0; i < weeks_; ++i) ThatRecords.Add("");
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
            for (var i = 0; i < ThatRecords.Count; ++i)
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
            if (new_weeks_ > ThatData.Count)
            {
                int dif = new_weeks_ - ThatData.Count;
                for (int i = 0; i < dif; ++i)
                {
                    ThatData.Add("");
                }
            }
            else if (new_weeks_ < ThatData.Count)
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uval3.Source
{
    static public class Periods
    {
        static private List<PeriodsEntry> thatData = new List<PeriodsEntry>();

        public static List<PeriodsEntry> ThatData { get => thatData; set => thatData = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void UpdateDataFromBD()
        {
            List<List<object>> rawdata;
            rawdata = SQLConnector.Select("SELECT * FROM Periods");
         
            foreach (var e in rawdata)
            {
                PeriodsEntry period = new PeriodsEntry(e);
                ThatData.Add(period);
            }
            ThatData.Sort((x, y) => x.ThatPeriodID.CompareTo(y.ThatPeriodID));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public PeriodsEntry GetPeriodByPeriodID(int periodid_)
        {
            foreach (var e in ThatData) if (e.ThatPeriodID == periodid_) return e;
            return null;
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
    public class PeriodsEntry
    {
        private int thatID;
        private int thatPeriodID;
        private string thatName;
        private List<string> thatDates;
        private ObservableCollection<RecordsEntry> thatRecords = new ObservableCollection<RecordsEntry>();


        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public List<string> ThatDates { get => thatDates; set => thatDates = value; }
        public ObservableCollection<RecordsEntry> ThatRecords { get => thatRecords; set => thatRecords = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public PeriodsEntry(List<object> e_)
        {
            ThatID = Int32.Parse(e_[0].ToString());
            ThatName = e_[1].ToString();
            ThatDates = ParseDates(e_[2].ToString());
            ThatPeriodID = Int32.Parse(e_[3].ToString());
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private List<string> ParseDates(string data_)
        {
            return new List<string>(data_.Split(','));
        }
    }
}

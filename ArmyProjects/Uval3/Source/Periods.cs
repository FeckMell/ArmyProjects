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
            ThatData.Sort((x, y) => x.ThatPeriodPosition.CompareTo(y.ThatPeriodPosition));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public PeriodsEntry GetPeriodByID(int periodid_)
        {
            foreach (var e in ThatData) if (e.ThatID == periodid_) return e;
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
        private int thatPeriodPosition;
        private string thatName;
        private int thatWeeks;
        private ObservableCollection<RecordsEntry> thatRecords = new ObservableCollection<RecordsEntry>();


        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatPeriodPosition { get => thatPeriodPosition; set => thatPeriodPosition = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public int ThatWeeks { get => thatWeeks; set => thatWeeks = value; }
        public ObservableCollection<RecordsEntry> ThatRecords { get => thatRecords; set => thatRecords = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public PeriodsEntry(List<object> e_)
        {
            ThatID = Int32.Parse(e_[0].ToString());
            ThatPeriodPosition = Int32.Parse(e_[1].ToString());
            ThatName = e_[2].ToString();
            ThatWeeks = Int32.Parse(e_[3].ToString());
        }
    }
}

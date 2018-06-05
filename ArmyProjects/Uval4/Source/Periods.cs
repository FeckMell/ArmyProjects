using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uval4.Source;

namespace Uval4.Source
{
    public class Periods : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged() { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatData")); }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private Periods thatInstance = new Periods();
        static public List<PeriodsEntry> ThatData { get => thatInstance.ThatInstanceData; set => thatInstance.ThatInstanceData = value; }
        static public void UpdateDataFromBD() => thatInstance.InstanceUpdateDataFromBD();
        static public PeriodsEntry GetPeriodByID(int periodid_) => thatInstance.InstanceGetPeriodByID(periodid_);
        static public void Clear() => thatInstance.InstanceClear();
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private List<PeriodsEntry> thatData = new List<PeriodsEntry>();
        private List<PeriodsEntry> ThatInstanceData { get => thatData; set => thatData = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void InstanceUpdateDataFromBD()
        {
            List<List<object>> rawdata = SQLConnector.Select("SELECT * FROM Periods");

            foreach (var e in rawdata) ThatInstanceData.Add(new PeriodsEntry(e));

            ThatInstanceData.Sort((x, y) => x.ThatPeriodPosition.CompareTo(y.ThatPeriodPosition));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private PeriodsEntry InstanceGetPeriodByID(int periodid_)
        {
            foreach (var e in ThatInstanceData) if (e.ThatID == periodid_) return e;
            return null;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void InstanceClear()
        {
            ThatInstanceData.Clear();
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
        private ObservableCollection<RecordEntry> thatRecords = new ObservableCollection<RecordEntry>();


        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatPeriodPosition { get => thatPeriodPosition; set => thatPeriodPosition = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public int ThatWeeks { get => thatWeeks; set => thatWeeks = value; }
        public ObservableCollection<RecordEntry> ThatRecords { get => thatRecords; set => thatRecords = value; }

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
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void ChangeWeeksAmount(int new_weeks_)
        {
            foreach (var record in ThatRecords)
            {
                record.ChangeWeeksAmount(new_weeks_);
                ThatWeeks = new_weeks_;
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SaveChangesToDB()//SaveDataToDB
        {
            SQLConnector.NoReturnQuery(string.Format("UPDATE Periods SET PeriodPosition='{1}', Name='{2}', Weeks='{3}' WHERE id={0}", ThatID, ThatPeriodPosition, ThatName, ThatWeeks));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void DeleteFromDB()
        {
            Periods.ThatData.Remove(this);
            SQLConnector.NoReturnQuery(string.Format("DELETE FROM Periods WHERE id={0}", ThatID));
            foreach(var record in ThatRecords)
            {
                record.ThatMan.ThatRecords.Remove(record);
                record.ThatMan.SaveChangesToDB();
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

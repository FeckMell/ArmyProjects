using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Uval4.Source;

namespace Uval4.Source
{
    public class Man : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged() { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatData")); }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private Man thatInst = new Man();
        static public ObservableCollection<ManEntry> ThatData { get => thatInst.ThatInstData; set => thatInst.ThatInstData = value; }
        static public List<ManEntry> ThatStorage { get => thatInst.ThatInstStorage; set => thatInst.ThatInstStorage = value; }
        static public void UpdateDataFromBD() => thatInst.InstUpdateDataFromBD();
        static public void Filter(string filter_) => thatInst.InstFilter(filter_);
        static public void Clear() => thatInst.InstClear();
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private ObservableCollection<ManEntry> thatInstData = new ObservableCollection<ManEntry>();/*For filtered data*/
        private List<ManEntry> thatInstStorage = new List<ManEntry>();/*For all data*/

        private ObservableCollection<ManEntry> ThatInstData { get => thatInstData; set => thatInstData = value; }
        public List<ManEntry> ThatInstStorage { get => thatInstStorage; set => thatInstStorage = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void InstUpdateDataFromBD()
        {
            List<List<object>> rawdata = SQLConnector.Select("SELECT * FROM Man");

            foreach (var e in rawdata)
            {
                ManEntry man = new ManEntry(e);
                ThatData.Add(man);
                ThatStorage.Add(man);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void InstFilter(string filter_)
        {
            //Clear visible elements of man
            ThatData.Clear();
            //Clead periods entries
            foreach (var period in Periods.ThatData) period.ThatRecords.Clear();

            //Filter man
            foreach(var man in ThatStorage)
            {
                if (man.ThatName.ToLowerInvariant().Contains(filter_.ToLowerInvariant()))
                {
                    //Add new visible element
                    ThatData.Add(man);
                    //Add its records to periods
                    foreach (var record in man.ThatRecords) record.ThatPeriod.ThatRecords.Add(record);
                }
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void InstClear()
        {
            ThatData.Clear();
            ThatStorage.Clear();
        }
    }
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    public class ManEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int thatID;
        private int thatWDK;
        private string thatPlatoon;
        private string thatName;
        private string thatGoods;
        private string thatBads;
        private string thatSpeed;
        private string thatForce;
        private string thatStamina;
        private string thatMark;
        private string thatFreedom;
        private ColorEntry thatColor;

        private List<RecordEntry> thatRecords = new List<RecordEntry>();


        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatWDK { get => thatWDK; set => thatWDK = value; }
        public string ThatPlatoon { get => thatPlatoon; set => thatPlatoon = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public string ThatGoods { get => thatGoods; set => thatGoods = value; }
        public string ThatBads { get => thatBads; set => thatBads = value; }
        public string ThatSpeed { get => thatSpeed; set => thatSpeed = value; }
        public string ThatForce { get => thatForce; set => thatForce = value; }
        public string ThatStamina { get => thatStamina; set => thatStamina = value; }
        public string ThatMark { get => thatMark; set => thatMark = value; }
        public string ThatFreedom { get => thatFreedom; set => thatFreedom = value; }
        public ColorEntry ThatColor { get => thatColor; set => thatColor = value; }
        public List<RecordEntry> ThatRecords { get => thatRecords; set => thatRecords = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public ManEntry(List<object> e_)
        {
            //set properties
            ThatID = Int32.Parse(e_[0].ToString());
            ThatWDK = Int32.Parse(e_[1].ToString());
            ThatPlatoon = e_[2].ToString();
            ThatName = e_[3].ToString();
            ThatGoods = e_[4].ToString();
            thatBads = e_[5].ToString();
            ThatSpeed = e_[6].ToString();
            ThatForce = e_[7].ToString();
            ThatStamina = e_[8].ToString();
            ThatMark = e_[9].ToString();
            ThatFreedom = e_[10].ToString();

            //set validance color
            ThatColor = ColorControl.Analyse(this);

            //parse records
            List<string> records_by_periods = new List<string>(e_[11].ToString().Split('|'));
            foreach(var e in records_by_periods)
            {
                RecordEntry rec = new RecordEntry(e, this);
                ThatRecords.Add(rec);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatWDK"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatPlatoon"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatName"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatGoods"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatBads"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatSpeed"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatForce"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatStamina"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatMark"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatFreedom"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatColor"));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SaveChangesToDB() //SaveDataToDB
        {
            SQLConnector.NoReturnQuery(string.Format(
                "UPDATE Man SET WDK='{1}', Name='{2}', Platoon='{3}', Goods='{4}', Bads='{5}', Speed='{6}', Force='{7}', Stamina='{8}', Mark='{9}', Freedom='{10}', Records='{11}' WHERE id={0}",
                ThatID, ThatWDK, ThatName, ThatPlatoon, ThatGoods, ThatBads, ThatSpeed, ThatForce, ThatStamina, ThatMark, ThatFreedom, RecordsToString()
                ));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void DeleteFromDB()
        {
            Man.ThatData.Remove(this);
            Man.ThatStorage.Remove(this);
            SQLConnector.NoReturnQuery(string.Format("DELETE FROM Man WHERE id={0}", ThatID));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string RecordsToString()
        {
            string result = "";
            foreach (var e in ThatRecords)
            {
                result += e.ToStringForDB() + "|";
            }

            result = result.Remove(result.Length - 1);
            return result;
        }
    }
}

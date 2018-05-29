using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void DeleteDataManEntry(DataManEntry man_)
        {
            ThatData.Remove(man_);
            SQLConnector.NoReturnQuery(string.Format("DELETE FROM Man WHERE id={0}", man_.ThatID));
            MainWindow.ThatWindow.Update();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void EditDataManEntry(DataManEntry man_)
        {
            SQLConnector.NoReturnQuery(string.Format("UPDATE Man SET WDK='{1}', Name='{2}', Platoon='{3}' WHERE id={0}", man_.ThatID, man_.ThatWDK, man_.ThatName, man_.ThatPlatoon));
            MainWindow.ThatWindow.Update();
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
    public class DataManEntry : INotifyPropertyChanged
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

        private List<RecordsEntry> thatRecords;


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
        public List<RecordsEntry> ThatRecords { get => thatRecords; set => thatRecords = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public DataManEntry(List<object> e_)
        {
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

            ThatColor = ColorControl.Analyse(this);

            //ThatRecords = Records.ParseForMan(e_[11], ThatID, ThatColor);
            ThatRecords = Records.ParseForMan(e_[11], this);
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
        public void SaveDataToDB()
        {
            SQLConnector.NoReturnQuery(string.Format(
                "UPDATE Man SET WDK='{1}', Name='{2}', Platoon='{3}', Goods='{4}', Bads='{5}', Speed='{6}', Force='{7}', Stamina='{8}', Mark='{9}', Freedom='{10}', Records='{11}' WHERE id={0}",
                ThatID, ThatWDK, ThatName, ThatPlatoon, ThatGoods, ThatBads, ThatSpeed, ThatForce, ThatStamina, ThatMark, ThatFreedom,
                RecordsToString()
                ));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string RecordsToString()
        {
            string result = "";
            foreach(var e in ThatRecords)
            {
                result += e.ToStringForDB() + "|";
            }
            
            result = result.Remove(result.Length - 1);
            return result;
        }
    }
}

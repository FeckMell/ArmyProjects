using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uval3.Source
{
    static public class Fizo
    {
        static private ObservableCollection<FizoEntry> thatData = new ObservableCollection<FizoEntry>();

        public static ObservableCollection<FizoEntry> ThatData { get => thatData; set => thatData = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public FizoEntry Update(int manid_, string name_)
        {
            List<List<object>> rawdata = SQLConnector.Select(string.Format("SELECT * FROM Fizo WHERE manid={0}", manid_));
            if (rawdata.Count == 0) return null;

            FizoEntry fizo = new FizoEntry(rawdata[0], name_);
            ThatData.Add(fizo);
            return fizo;
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
    public class FizoEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatSpeed"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatForce"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatStamina"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatMark"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatFree"));
        }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private int thatID;
        private int thatManID;
        private string thatName;
        private string thatSpeed;
        private string thatForce;
        private string thatStamina;
        private string thatMark;
        private string thatFree;
        private ColorEntry thatColor;


        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManID { get => thatManID; set => thatManID = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public string ThatSpeed { get => thatSpeed; set => thatSpeed = value; }
        public string ThatForce { get => thatForce; set => thatForce = value; }
        public string ThatStamina { get => thatStamina; set => thatStamina = value; }
        public string ThatMark { get => thatMark; set => thatMark = value; }
        public string ThatFree { get => thatFree; set => thatFree = value; }
        public ColorEntry ThatColor { get => thatColor; set => thatColor = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public FizoEntry(List<object> e_, string name_)
        {
            ThatID = Int32.Parse(e_[0].ToString());
            ThatManID = Int32.Parse(e_[1].ToString());
            ThatName = name_;
            ThatSpeed = e_[2].ToString();
            ThatForce = e_[3].ToString();
            ThatStamina = e_[4].ToString();
            ThatMark = e_[5].ToString();
            ThatFree = e_[6].ToString();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public bool CheckIsGood()
        {
            if (ThatMark=="" || ThatMark==null || ThatMark == "2") return false;
            else return true;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SaveDataToDB()
        {
            SQLConnector.NoReturnQuery(string.Format("UPDATE Fizo SET Speed='{0}', Force='{1}', Stamina='{2}', Mark='{3}', Freedom='{4}' WHERE id={5}",
                        ThatSpeed, ThatForce, ThatStamina, ThatMark, ThatFree, ThatID));
        }
    }
}

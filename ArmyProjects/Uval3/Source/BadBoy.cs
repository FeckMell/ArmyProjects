using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uval3.Source
{
    static public class BadBoy
    {
        static private ObservableCollection<BadBoyEntry> thatData = new ObservableCollection<BadBoyEntry>();

        public static ObservableCollection<BadBoyEntry> ThatData { get => thatData; set => thatData = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public BadBoyEntry Update(int manid_, string name_)
        {
            List<List<object>> rawdata = SQLConnector.Select(string.Format("SELECT * FROM BadBoy WHERE manid={0}", manid_));
            if (rawdata.Count == 0) return null;

            BadBoyEntry badboy = new BadBoyEntry(rawdata[0], name_);
            ThatData.Add(badboy);
            return badboy;
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
    public class BadBoyEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatGoods"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatBads"));
        }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private int thatID;
        private int thatManID;
        private string thatName;
        private string thatGoods;
        private string thatBads;
        private ColorEntry thatColor;


        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManID { get => thatManID; set => thatManID = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public string ThatGoods { get => thatGoods; set => thatGoods = value; }
        public string ThatBads { get => thatBads; set => thatBads = value; }
        public ColorEntry ThatColor { get => thatColor; set => thatColor = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public BadBoyEntry(List<object> e_, string name_)
        {
            ThatID = Int32.Parse(e_[0].ToString());
            ThatManID = Int32.Parse(e_[1].ToString());
            ThatName = name_;
            ThatGoods = e_[2].ToString();
            ThatBads = e_[3].ToString();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public bool CheckIsGood()
        {
            if (ThatBads == "" || ThatBads == null || ThatBads == "0") return true;
            else return false;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SaveDataToDB()
        {
            SQLConnector.NoReturnQuery(string.Format("UPDATE BadBoy SET Goods='{0}', Bads='{1}' WHERE id={2}",
                        ThatGoods, ThatBads, ThatID));
        }
    }
}

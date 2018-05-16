using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Увольнения.Source
{
    static public class BadBoy
    {
        static private ObservableCollection<BadBoyEntry> thatData = new ObservableCollection<BadBoyEntry>();

        public static ObservableCollection<BadBoyEntry> ThatData { get => thatData; set => thatData = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void SetData(List<List<object>> data_)
        {
            foreach (var i in data_)
            {
                BadBoyEntry man = new BadBoyEntry
                {
                    ThatID = Int32.Parse(i[0].ToString()),
                    ThatManID = Int32.Parse(i[1].ToString()),
                    ThatName = DataMan.GetNameByID(Int32.Parse(i[1].ToString())),
                    ThatBads = i[3].ToString(),
                    ThatGoods = i[2].ToString()
                   
                };
                ThatData.Add(man);
            }
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
    public class BadBoyEntry : INotifyPropertyChanged
    {
        private int thatID;
        private int thatManID;
        private string thatName;
        private string thatGoods;
        private string thatBads;

        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManID { get => thatManID; set => thatManID = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public string ThatGoods { get => thatGoods; set => thatGoods = value; }
        public string ThatBads { get => thatBads; set => thatBads = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    public class UvalSubTable
    {
        private ObservableCollection<UvalSubTableRow> thatData = new ObservableCollection<UvalSubTableRow>();
        private int thatPeriodID;

        public ObservableCollection<UvalSubTableRow> ThatData { get => thatData; set => thatData = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public UvalSubTable(List<List<object>> records_)
        {
            foreach(var e in records_)
            {
                UvalSubTableRow row = new UvalSubTableRow
                {
                    ThatID = Int32.Parse(e[0].ToString()),
                    ThatManID = Int32.Parse(e[1].ToString()),
                    ThatPeriodID = Int32.Parse(e[2].ToString()),
                    ThatData = (List<string>)e[3]
                };
                ThatData.Add(row);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    public class UvalSubTableRow : INotifyPropertyChanged
    {
        private int thatID;
        private int thatManID;
        private int thatPeriodID;
        private List<string> thatData = new List<string>();
        private string thatColor;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManID { get => thatManID; set => thatManID = value; }
        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public List<string> ThatData { get => thatData; set { thatData = value; thatData.Add(Sum()); } }
        public string ThatColor { get => thatColor; set => thatColor = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private string Sum()
        {
            int result = 0;
            foreach (var e in ThatData) result += Int32.Parse(e);
            return result.ToString();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
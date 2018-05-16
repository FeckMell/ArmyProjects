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
        public List<string> ThatData
        {
            get => thatData;
            set
            {
                thatData = value;
                thatData.Add(Sum());
            }
        }
        public string ThatColor { get => thatColor; set => thatColor = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SumNew(string data_, int i_)
        {
            ThatData[ThatData.Count - 1] = (GetValue(ThatData.Count - 1) - GetValue(i_) + GetValue(data_)).ToString();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private string Sum()
        {
            int result = 0;
            foreach (var e in ThatData) result += GetValue(e);
            return result.ToString();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private int GetValue(int i_)
        {
            if (string.IsNullOrEmpty(ThatData[i_])) return 0;
            else return Int32.Parse(ThatData[i_]);
        }
        private int GetValue(string data_)
        {
            if (string.IsNullOrEmpty(data_)) return 0;
            else return Int32.Parse(data_);
        }
    }
}
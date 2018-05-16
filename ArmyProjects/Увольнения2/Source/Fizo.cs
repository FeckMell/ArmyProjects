using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    static public class Fizo
    {
        static private ObservableCollection<FizoEntry> thatData = new ObservableCollection<FizoEntry>();

        public static ObservableCollection<FizoEntry> ThatData { get => thatData; set => thatData = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void SetData(List<List<object>> data_)
        {
            foreach (var i in data_)
            {
                FizoEntry man = new FizoEntry
                {
                    ThatID = Int32.Parse(i[0].ToString()),
                    ThatManID = Int32.Parse(i[1].ToString()),
                    ThatName = DataMan.GetNameByID(Int32.Parse(i[1].ToString())),
                    ThatSpeed = i[2].ToString(),
                    ThatForce = i[3].ToString(),
                    ThatStamina = i[4].ToString(),
                    ThatMark = i[5].ToString(),
                    ThatFree = i[6].ToString()
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
    public class FizoEntry : INotifyPropertyChanged
    {
        private int thatID;
        private int thatManID;
        private string thatName;
        private string thatSpeed;
        private string thatForce;
        private string thatStamina;
        private string thatMark;
        private string thatFree;

        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManID { get => thatManID; set => thatManID = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public string ThatSpeed { get => thatSpeed; set => thatSpeed = value; }
        public string ThatForce { get => thatForce; set => thatForce = value; }
        public string ThatStamina { get => thatStamina; set => thatStamina = value; }
        public string ThatMark { get => thatMark; set => thatMark = value; }
        public string ThatFree { get => thatFree; set => thatFree = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

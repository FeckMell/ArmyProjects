using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deji.Source
{
    public class RecordElement
    {
        private int thatID;
        private int thatManID;
        private string thatDate;
        private string thatTime;
        private bool thatArrival;
        private string thatDrochit;
        private string thatComment;
        private Dictionary<int, string> thatDictDroch = new Dictionary<int, string>();

        public RecordElement(int id_, int manid_, string date_, string time_, bool arrival_, int droch_, string comment_)
        {
            Init();
            thatID = id_;
            thatManID = manid_;
            thatDate = date_;
            thatTime = time_;
            thatArrival = arrival_;
            thatDrochit = thatDictDroch[droch_];
            thatComment = comment_;
        }
        private void Init()
        {
            thatDictDroch.Add(0, "Нигде");
            thatDictDroch.Add(1, "На разводе");
            thatDictDroch.Add(2, "В казарме");
            thatDictDroch.Add(3, "Везде");
        }

    }
}

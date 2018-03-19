using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deji
{
    public class RecordElement
    {
        private int thatID;
        private int thatManID;
        private string thatDate;
        private string thatTime;
        private string thatArrival;
        private string thatDrochit;
        private string thatComment;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public int ThatID { get => thatID; set => thatID = value; }
        public int ThatManID { get => thatManID; set => thatManID = value; }
        public string ThatDate { get => thatDate; set => thatDate = value; }
        public string ThatTime { get => thatTime; set => thatTime = value; }
        public string ThatArrival { get => thatArrival; set => thatArrival = value; }
        public string ThatDrochit { get => thatDrochit; set => thatDrochit = value; }
        public string ThatComment { get => thatComment; set => thatComment = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public RecordElement(int id_, int manid_, string date_, string time_, string arrival_, string drochit_, string comment_)
        {
            ThatID = id_;
            ThatManID = manid_;
            ThatDate = date_;
            ThatTime = time_;
            ThatArrival = arrival_;
            ThatDrochit = drochit_;
            ThatComment = comment_;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
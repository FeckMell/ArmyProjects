using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deji
{
    public class UnitElement
    {
        private int thatID;
        private string thatName;
        private string thatRank;
        private string thatPart;
        private string thatType;

        public UnitElement(int id_, string name_, string rank_, string part_, string type_)
        {
            thatID = id_;
            thatName = name_;
            thatRank = rank_;
            thatPart = part_;
            thatType = type_;
        }

        public int ThatID { get => thatID; set => thatID = value; }
        public string ThatName { get => thatName; set => thatName = value; }
        public string ThatRank { get => thatRank; set => thatRank = value; }
        public string ThatPart { get => thatPart; set => thatPart = value; }
        public string ThatType { get => thatType; set => thatType = value; }
    }
}
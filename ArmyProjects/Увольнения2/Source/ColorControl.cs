using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    static public class ColorControl
    {
        static private List<string> thatTrueColors;
        static private List<string> thatFalseColors;

        public static List<string> ThatTrueColors { get => thatTrueColors; set => thatTrueColors = value; }
        public static List<string> ThatFalseColors { get => thatFalseColors; set => thatFalseColors = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Init()
        {
            ThatTrueColors = new List<string>
            {

            };
            ThatFalseColors = new List<string>
            {

            };
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void SetColorsToTables()
        {
            //создать список запрещенных людей.
            List<DataManEntry> forbidden = new List<DataManEntry>();
            //создать список разрешенных людей
            List<DataManEntry> allowed = new List<DataManEntry>();
            //добавить в него людей по физо.
            //добавить в него людей по взысканиям.
            //заполнить оставшимися людьми список разрешенных
            foreach(var e in DataMan.ThatData)
            {
                bool check_success = true;
                if(CheckFizoIsBad(e.ThatID)) check_success = false;
                if (CheckBadBoyIsBad(e.ThatID)) check_success = false;

                if (check_success) allowed.Add(e);
                else forbidden.Add(e);
            }
            //присвоить этим людям цвета запрета
            for (int i = 0; i < forbidden.Count; ++i)
                forbidden[i].ThatColor = ThatFalseColors[i % ThatFalseColors.Count];
            //присвоить им цвета разрешений
            for (int i = 0; i < allowed.Count; ++i)
                allowed[i].ThatColor = ThatTrueColors[i % ThatTrueColors.Count];
            //перекрасить списки

        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static bool CheckBadBoyIsBad(int id_)
        {
            foreach (var e in BadBoy.ThatData)
            {
                if (e.ThatManID == id_)
                {
                    if (string.IsNullOrEmpty(e.ThatBads) || e.ThatBads=="0") return false;
                    else return true;
                }
            }
            return false;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private bool CheckFizoIsBad(int id_)
        {
            foreach (var e in Fizo.ThatData)
            {
                if (e.ThatManID == id_)
                {
                    if (string.IsNullOrEmpty(e.ThatMark) || e.ThatMark == "2") return true;
                    else return false;
                }
            }
            return false;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

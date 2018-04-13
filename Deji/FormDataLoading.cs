using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Deji
{
    public static class FormDataLoading
    {
        public static void LoadRank(ComboBox box_)
        {
            box_.Items.Clear();
            List<List<object>> list = SQLConnector.Select("SELECT * FROM dbo.Ranks");
            foreach(List<object> e in list) box_.Items.Add(e[1]);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void LoadType(ComboBox box_)
        {
            box_.Items.Clear();
            List<List<object>> list = SQLConnector.Select("SELECT * FROM dbo.Type");
            foreach (List<object> e in list) box_.Items.Add(e[1]);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void LoadPart(ComboBox box_)
        {
            box_.Items.Clear();
            List<List<object>> list = SQLConnector.Select("SELECT * FROM dbo.Part");
            foreach (List<object> e in list) box_.Items.Add(e[1]);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void LoadWho(ComboBox box_)
        {
            box_.Items.Clear();
            List<List<object>> list = SQLConnector.Select("SELECT * FROM dbo.Units");
            foreach (List<object> e in list) box_.Items.Add(e[0].ToString() + ":" + e[2].ToString() + " " + e[1].ToString() + "(" + e[3].ToString() + ")");
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void LoadDrochit(ComboBox box_)
        {
            box_.Items.Clear();
            List<List<object>> list = SQLConnector.Select("SELECT * FROM dbo.Drochit");
            foreach (List<object> e in list) box_.Items.Add(e[1]);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

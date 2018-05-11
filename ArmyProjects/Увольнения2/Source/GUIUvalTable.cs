using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Увольнения.Source
{
    static public class GUIUvalTable
    {
        static private List<GUIUvalSubTable> thatTables = new List<GUIUvalSubTable>();
        static private StackPanel thatForm;


        public static List<GUIUvalSubTable> ThatTables { get => thatTables; set => thatTables = value; }
        public static StackPanel ThatForm { get => thatForm; set => thatForm = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Init(StackPanel panel_)
        {
            ThatForm = panel_;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Update(List<List<object>> periods_, List<List<object>> records_)
        {
            CreateSubTables(periods_, records_);
            AddToForm();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private void CreateSubTables(List<List<object>> periods_, List<List<object>> records_)
        {
            foreach (var e in periods_)
            {
                List<List<object>> tmp_records = FindPeriodRecords(e[3], records_);
                GUIUvalSubTable subtable = new GUIUvalSubTable(tmp_records)
                {
                    ThatPeriodID = Int32.Parse(e[3].ToString()),
                    ThatSuperHeader = e[1].ToString(),
                    ThatHeaders = (List<string>)e[2]
                };
                subtable.SetHeaders();
                ThatTables.Add(subtable);
            }
            ThatTables.Sort((x, y) => x.ThatPeriodID.CompareTo(y.ThatPeriodID));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static List<List<object>> FindPeriodRecords(object periodid_, List<List<object>> records_)
        {
            int periodid = Int32.Parse(periodid_.ToString());
            List<List<object>> result = new List<List<object>>();
            foreach(var e in records_)
            {
                if (periodid == Int32.Parse(e[2].ToString())) result.Add(e);
            }

            return result;
        }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private void AddToForm()
        {
            foreach(var e in ThatTables)
            {
                ThatForm.Children.Add(e.ThatStackPanel);
            }
        }
    }
}
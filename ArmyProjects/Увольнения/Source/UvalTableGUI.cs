using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Увольнения.Source
{
    static public class UvalTableGUI
    {
        static private List<UvalTableElementGUI> thatTables;
        static private StackPanel thatForm;

        public static List<UvalTableElementGUI> ThatTables { get => thatTables; set => thatTables = value; }
        public static StackPanel ThatForm { get => thatForm; set => thatForm = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Init(StackPanel panel_)
        {
            ThatTables = new List<UvalTableElementGUI>();
            ThatForm = panel_;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void FormTableGUI(List<List<object>> data_)
        {
            foreach(var e in data_)
            {
                UvalTableElementGUI table = new UvalTableElementGUI
                {
                    ThatPeriodID = Int32.Parse(e[3].ToString()),
                    ThatSuperHeader = e[1].ToString(),
                    ThatHeaders = (List<string>)e[2]
                };
                table.SetHeaders();
                ThatTables.Add(table);
            }

            ThatTables.Sort((x, y) => x.ThatPeriodID.CompareTo(y.ThatPeriodID));
            foreach (var e in ThatTables)
            {
                ThatForm.Children.Add(e.ThatStackPanel);
                var j = UvalTable.GetForBinding(e.ThatPeriodID);
                if (j!=null) e.SetBinding(UvalTable.GetForBinding(e.ThatPeriodID));
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

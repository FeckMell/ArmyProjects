using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Deji
{
    public static class UnitsStore
    {
        private static bool DEBUG = false;
        private static ObservableCollection<UnitElement> thatUnits = new ObservableCollection<UnitElement>();

        public static void Update(List<List<object>> data_)
        {
            foreach(List<object> e in data_)
            {
                thatUnits.Add(new UnitElement((int)e[0], (string)e[1], (string)e[2], (string)e[3], (string)e[4]));
            }
        }

        public static void Init(DataGrid grid_)
        {
            grid_.ItemsSource = thatUnits;
        }
    }
}
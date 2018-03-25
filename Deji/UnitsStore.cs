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
        private static TextBox thatSearchTextBox;
        private static DataGrid thatDataGrid;
        private static ObservableCollection<UnitElement> thatUnits = new ObservableCollection<UnitElement>();
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Update()
        {
            //Clear old data
            thatUnits.Clear();

            //Make query to DB depends on searchbox
            string s;
            if (string.IsNullOrEmpty(thatSearchTextBox.Text))
            {
                s = "SELECT * FROM dbo.Units";
            }
            else
            {
                string query = "SELECT * FROM dbo.Units WHERE Rank LIKE N'%{0}%' Or Name LIKE N'%{0}%' Or Part LIKE N'%{0}%' Or Type LIKE N'%{0}%'";
                s = string.Format(query, thatSearchTextBox.Text);
            }

            //Get data from DB
            List<List<object>> result = SQLConnector.Select(s);

            //Parse and add data to grid storage
            foreach (List<object> e in result)
            {
                thatUnits.Add(new UnitElement(Int32.Parse(e[0].ToString()), (string)e[1], (string)e[2], (string)e[3], (string)e[4]));
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Init(DataGrid grid_, TextBox box_)
        {
            thatDataGrid = grid_;
            thatSearchTextBox = box_;

            grid_.ItemsSource = thatUnits;
            Update();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
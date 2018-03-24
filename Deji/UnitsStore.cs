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
        public static void Update(string data_)
        {
            thatUnits.Clear();

            string s;
            if (data_ == "" || data_ == null)
            {
                s = "SELECT * FROM dbo.Units";
            }
            else
            {
                s = "SELECT * FROM dbo.Units WHERE Rank LIKE '%" + data_ + "%' Or Name LIKE '%" + data_ + "%' Or Part LIKE '%" + data_ + "%' Or Type LIKE '%" + data_ + "%'";
            }

            List<List<object>> result = SQLConnector.Select(s);
            foreach(List<object> e in result)
            {
                thatUnits.Add(new UnitElement(Int32.Parse(e[0].ToString()), (string)e[1], (string)e[2], (string)e[3], (string)e[4]));
            }
        }
        public static void Update()
        {
            thatUnits.Clear();

            string s;
            if (string.IsNullOrEmpty(thatSearchTextBox.Text))
            {
                s = "SELECT * FROM dbo.Units";
            }
            else
            {
                s = "SELECT * FROM dbo.Units WHERE Rank LIKE '%&d&%' Or Name LIKE '%&d&%' Or Part LIKE '%&d&%' Or Type LIKE '%&d&%'".Replace("&d&", thatSearchTextBox.Text);
                //s = "SELECT * FROM dbo.Units WHERE Rank LIKE '%" + thatSearchTextBox.Text + "%' Or Name LIKE '%" + thatSearchTextBox.Text + "%' Or Part LIKE '%" + thatSearchTextBox.Text + "%' Or Type LIKE '%" + thatSearchTextBox.Text + "%'";
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Init(DataGrid grid_)
        {
            grid_.ItemsSource = thatUnits;
            UnitsStore.Update(null);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Init(DataGrid grid_, TextBox box_)
        {
            thatDataGrid = grid_;
            thatSearchTextBox = box_;

            grid_.ItemsSource = thatUnits;
            UnitsStore.Update();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
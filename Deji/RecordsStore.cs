using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Deji
{
    public static class RecordsStore
    {
        private static ObservableCollection<RecordElement> thatRecords = new ObservableCollection<RecordElement>();
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Display(UnitElement unit_)
        {
            //Check that data exists in grid
            if (unit_ == null) return;

            //Make query
            string query = "SELECT * FROM dbo.Records WHERE IdMan = N'{0}'";
            string s = string.Format(query, unit_.ThatID.ToString());

            //Get data from DB and update grid
            Update(SQLConnector.Select(s));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Update(List<List<object>> data_)
        {
            //Clear old data
            thatRecords.Clear();

            //Add new data to grid storage
            foreach (List<object> e in data_)
            {
                thatRecords.Add(new RecordElement(Int32.Parse(e[0].ToString()), Int32.Parse(e[1].ToString()), (string)e[2], (string)e[3], (string)e[4], (string)e[5], (string)e[6]));
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Init(DataGrid grid_)
        {
            grid_.ItemsSource = thatRecords;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
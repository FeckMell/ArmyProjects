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
            if (unit_ == null) return;

            Update(SQLConnector.Select("SELECT * FROM dbo.Records WHERE IdMan = '" + unit_.ThatID.ToString() + "'"));
            
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Update(List<List<object>> data_)
        {
            thatRecords.Clear();
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
            //if (DEBUG)
            //{
            //    thatRecords.Add(new RecordElement(1, 1, "1993.09.12", "19:30", "Пришёл", "Нигде", "первый нах"));
            //    thatRecords.Add(new RecordElement(2, 1, "1993.10.11", "12:30", "Не Пришёл", "Везде", "второй нах"));
            //}
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
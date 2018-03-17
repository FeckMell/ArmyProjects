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
        private static bool DEBUG = true;
        private static ObservableCollection<RecordElement> thatRecords = new ObservableCollection<RecordElement>();

        public static void Display(UnitElement unit_)
        {
            if(DEBUG)
            {
                List<List<object>> resultRecords = new List<List<object>>();
                switch (unit_.ThatID)
                {
                    case 2:
                        object[] o1 = { 1, 1, "1993.09.12", "19:30", "Пришёл", "Нигде", "первый нах" };
                        resultRecords.Add(new List<object>(o1));
                        object[] o2 = { 1, 1, "1993.09.12", "12:30", "Пришёл", "Нигде", "второй нах" };
                        resultRecords.Add(new List<object>(o2));
                        break;
                    case 3:
                        object[] o3 = { 1, 1, "1994.09.12", "19:30", "Пришёл", "Нигде", "третий нах" };
                        resultRecords.Add(new List<object>(o3));
                        object[] o4 = { 1, 1, "1994.09.12", "12:30", "Не Пришёл", "Нигде", "четвертый нах" };
                        resultRecords.Add(new List<object>(o4));
                        break;
                    case 4:
                        object[] o5 = { 1, 1, "1995.09.12", "19:30", "Пришёл", "Нигде", "ййй" };
                        resultRecords.Add(new List<object>(o5));
                        object[] o6 = { 1, 1, "1995.09.12", "12:30", "Не Пришёл", "Нигде", "ццц" };
                        resultRecords.Add(new List<object>(o6));
                        break;
                    case 5:
                        object[] o7 = { 1, 1, "1996.09.12", "19:30", "Пришёл", "Нигде", "ууу" };
                        resultRecords.Add(new List<object>(o7));
                        object[] o8 = { 1, 1, "1996.09.12", "12:30", "Не Пришёл", "Нигде", "ккк" };
                        resultRecords.Add(new List<object>(o8));
                        break;
                }
                Update(resultRecords);
            }
            else
            {
                //create query for searching in table records by IDMAN
                Update(SQLConnector.Select("select by ID"));
            }
        }
        public static void Update(List<List<object>> data_)
        {
            thatRecords.Clear();
            foreach (List<object> e in data_)
            {
                thatRecords.Add(new RecordElement((int)e[0], (int)e[1], (string)e[2], (string)e[3], (string)e[4], (string)e[5], (string)e[6]));
            }
        }

        public static void Init(DataGrid grid_)
        {
            grid_.ItemsSource = thatRecords;
            if (DEBUG)
            {
                thatRecords.Add(new RecordElement(1, 1, "1993.09.12", "19:30", "Пришёл", "Нигде", "первый нах"));
                thatRecords.Add(new RecordElement(2, 1, "1993.10.11", "12:30", "Не Пришёл", "Везде", "второй нах"));
            }
        }
    }
}
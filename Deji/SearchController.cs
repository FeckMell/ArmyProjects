using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deji
{
    public static class SearchController
    {
        private static bool DEBUG = true;
        //private static SQLConnector thatSQLConnector = new SQLConnector();
        public static void Init()
        {
            //thatSQLConnector.Init("InitString");
            List<List<object>> resultUnits;

            if (DEBUG)
            {
                resultUnits = new List<List<object>>();

                object[] o1 = { 2, "Попович", "Майор", "36360", "Ответственный" };
                resultUnits.Add(new List<object>(o1));
                object[] o2 = { 3, "Сабонин", "Подполковник", "36360", "ДПЧ" };
                resultUnits.Add(new List<object>(o2));
                object[] o3 = { 4, "Номоконов", "Майор", "36360", "ПДПЧ" };
                resultUnits.Add(new List<object>(o3));
                object[] o4 = { 5, "Фролов", "Майор", "51428", "ДПЧ" };
                resultUnits.Add(new List<object>(o4));
            }
            else
            {
                resultUnits = SQLConnector.Select("select query");
            }
            
            UnitsStore.Update(resultUnits);
        }

        public static void PerformSearch(string data_)
        {
            throw new System.NotImplementedException();
        }
    }
}
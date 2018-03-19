using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Deji
{
    public static class SearchController
    {
        private static bool DEBUG = true;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Init()
        {            
            UnitsStore.Update(SQLConnector.Select("select * from dbo.Units"));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void PerformSearch(string data_)
        {
            string s;
            if (data_ == "" || data_ == null)
            {
                s = "select * from dbo.Units";
            }
            else
            {
                s = "SELECT * FROM dbo.Units WHERE Rank LIKE '%" + data_ + "%' Or Name LIKE '%" + data_ + "%' Or Part LIKE '%" + data_ + "%' Or Type LIKE '%" + data_ + "%'";
            }

            UnitsStore.Update(SQLConnector.Select(s));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
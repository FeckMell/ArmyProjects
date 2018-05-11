using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    public static class UvalDataManager
    {
        static public void GetMan()
        {
            var data = SQLConnector.Select("SELECT * FROM Man");
            DataMan.SetData(data);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void GetUvalInfo()
        {
            var periods = SQLConnector.Select("SELECT * FROM Periods");
            var records = SQLConnector.Select("SELECT * FROM Records");
            periods = ParseData(periods, 2);
            records = ParseData(records, 3);

            GUIUvalTable.Update(periods, records);

            //UvalTable.FormTableData(periods, records);
            //UvalTableGUI.FormTableGUI(periods);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static List<List<object>> ParseData(List<List<object>> data_, int i_)
        {
            foreach (var i in data_)
            {
                string tmp = i[i_].ToString();
                List<string> res = ParseLine(tmp);
                i[i_] = res;
            }
            return data_;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static List<string> ParseLine(string tmp_)
        {
            return new List<string>(tmp_.Split(','));
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}

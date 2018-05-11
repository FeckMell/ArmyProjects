using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Увольнения.Source
{
    static public class UvalTable
    {
        static private List<UvalTableElement> thatData = new List<UvalTableElement>();
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void FormTableData(List<List<object>> periods_, List<List<object>> records_)
        {
            foreach(var e in periods_)
            {
                int periodid = Int32.Parse(e[3].ToString());
                List<List<object>> records_in_month = new List<List<object>>();
                foreach(var e2 in records_)
                {
                    if (e2[2] == e[3]) records_in_month.Add(e2);
                }

                UvalTableElement table = new UvalTableElement(records_in_month);
                thatData.Add(table);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public UvalTableElement GetForBinding(int periodid_)
        {
            UvalTableElement result = null;
            foreach (var e in thatData)
            {
                if (e.ThatPeriodID == periodid_)
                {
                    result = e;
                    break;
                }
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Увольнения.Source
{
    public static class UvalDataManager
    {
        static public void GetFizo()
        {
            var data = SQLConnector.Select("SELECT * FROM Fizo");
            Fizo.SetData(data);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void GetBadBoy()
        {
            var data = SQLConnector.Select("SELECT * FROM BadBoy");
            BadBoy.SetData(data);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
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
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void SaveChangedData(object sender, RoutedEventArgs e_)
        {
            foreach(var e in GUIEventHandler.ThatChangedData)
            {
                if(e is UvalSubTableRow)
                {
                    var el = e as UvalSubTableRow;
                    string ch_data = "";
                    for (var i = 0; i < el.ThatData.Count - 1; ++i)
                    {
                        if (string.IsNullOrEmpty(el.ThatData[i])) ch_data += ",";
                        else ch_data += el.ThatData[i] + ",";
                    }
                    ch_data = ch_data.Remove(ch_data.Length - 1);
                    SQLConnector.NoReturnQuery(string.Format("UPDATE Records SET Data='{0}' WHERE id={1}", ch_data, el.ThatID));
                }
                else if (e is FizoEntry)
                {
                    var el = e as FizoEntry;
                    SQLConnector.NoReturnQuery(string.Format("UPDATE Fizo SET Speed='{0}', Force='{1}', Stamina='{2}', Mark='{3}', Freedom='{4}' WHERE id={5}",
                        el.ThatSpeed, el.ThatForce, el.ThatStamina, el.ThatMark, el.ThatFree, el.ThatID));
                }
                else if (e is BadBoyEntry)
                {
                    var el = e as BadBoyEntry;
                    SQLConnector.NoReturnQuery(string.Format("UPDATE BadBoy SET Goods='{0}', Bads='{1}' WHERE id={2}",
                        el.ThatGoods, el.ThatBads, el.ThatID));
                }
            }

            MessageBox.Show("Изменения сохранены!");
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private List<List<object>> ParseData(List<List<object>> data_, int i_)
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
        static private List<string> ParseLine(string tmp_)
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

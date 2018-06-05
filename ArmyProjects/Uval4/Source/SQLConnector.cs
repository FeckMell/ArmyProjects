using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Windows;

namespace Uval4.Source
{
    public static class SQLConnector
    {
        private static string connectionString = "Data Source=UvalDB.db;Version=3;";
        private static string dbFileName = "UvalDB.db";

        private static SQLiteCommand dbCmd;
        private static SQLiteConnection dbConnection;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static void OpenConnection()
        {
            try
            {
                if (!File.Exists(dbFileName)) throw new FileNotFoundException();
                dbConnection = new SQLiteConnection(connectionString);
                dbConnection.Open();
            }
            catch { }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static void CloseConnection()
        {
            if (dbConnection != null) dbConnection.Close();
            dbConnection = null;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void NoReturnQuery(string query)
        {
            OpenConnection();
            dbCmd = new SQLiteCommand(dbConnection) { CommandText = query };
            try { dbCmd.ExecuteNonQuery(); }
            catch (Exception e_)
            {
                MessageBox.Show(e_.ToString());
            }
            finally { CloseConnection(); }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static List<List<object>> Select(string query)
        {
            OpenConnection();
            List<List<object>> result = new List<List<object>>();
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, dbConnection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    List<object> l = new List<object>();
                    result.Add(item.ItemArray.ToList());
                }
            }
            catch { }
            finally
            {
                CloseConnection();
            }
            return result;
        }
    }
}

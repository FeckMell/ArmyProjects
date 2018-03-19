using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Deji
{
    public static class SQLConnector
    {
        private static bool DEBUG = true;
        private static SqlConnection thatConnector = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\DejiDB.mdf;Integrated Security=True");
        private static string thatInitString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\DejiDB.mdf;Integrated Security=True";
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Init()
        {
            
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static void OpenConnection()
        {
            //thatConnector = new SqlConnection(thatInitString);
            thatConnector.Open();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static void CloseConnection()
        {
            thatConnector.Close();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static bool Insert(string query_)
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static bool Update(string query_)
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static bool Delete(string query_)
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static List<List<object>> Select(string query_)
        {
            List<List<object>> result = new List<List<object>>();

            //Open connection
            OpenConnection();
            
            //Create Command
            SqlCommand cmd = new SqlCommand(query_, thatConnector);
            //Create a data reader and Execute the command
            SqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                List<object> row = new List<object>();
                for (int i = 0; i < dataReader.FieldCount; ++i) row.Add(dataReader[i].ToString());
                result.Add(row);
            }

            //close Data Reader
            dataReader.Close();

            //close Connection
            CloseConnection();
            

            //return list to be displayed
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static int Count(string query_)
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static bool BackUp(string path_)
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static bool Restore(string path_)
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
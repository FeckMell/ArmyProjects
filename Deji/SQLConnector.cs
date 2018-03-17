using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deji
{
    public static class SQLConnector
    {
        private static bool DEBUG = true;
        public static void Init(string initString_)
        {
            if (DEBUG) return;
            else throw new System.NotImplementedException();
        }

        private static bool OpenConnection()
        {
            throw new System.NotImplementedException();
        }

        private static bool CloseConnection()
        {
            throw new System.NotImplementedException();
        }

        public static bool Insert(string query_)
        {
            throw new System.NotImplementedException();
        }

        public static bool Update(string query_)
        {
            throw new System.NotImplementedException();
        }

        public static bool Delete(string query_)
        {
            throw new System.NotImplementedException();
        }

        public static List<List<object>> Select(string query_)
        {
            throw new System.NotImplementedException();
        }

        public static int Count(string query_)
        {
            throw new System.NotImplementedException();
        }

        public static bool BackUp(string path_)
        {
            throw new System.NotImplementedException();
        }

        public static bool Restore(string path_)
        {
            throw new System.NotImplementedException();
        }
    }
}
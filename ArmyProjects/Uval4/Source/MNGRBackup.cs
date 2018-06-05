using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Uval4.Source
{
    static public class MNGRBackup
    {
        static private string thatBackupFolder = "Backup";
        static private int thatHistorySize = 20;

        public static string ThatBackupFolder { get => thatBackupFolder; set => thatBackupFolder = value; }
        public static int ThatHistorySize { get => thatHistorySize; set => thatHistorySize = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Work()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(@".\" + ThatBackupFolder);
                var files = new List<FileInfo>(dir.GetFiles());
                files = files.OrderBy(o => o.Name).ToList();
                if (files.Count >= ThatHistorySize)
                {
                    File.Delete(files[0].FullName);
                    files = new List<FileInfo>(dir.GetFiles());

                    for (int i = 0; i < files.Count; ++i)
                    {
                        File.Move(files[i].FullName, string.Format("./{0}/UvalDB_{1}.db", ThatBackupFolder, i));
                    }
                    File.Copy(@"./UvalDB.db", string.Format("./{0}/UvalDB_{1}.db", ThatBackupFolder, files.Count));
                }
                else
                {
                    File.Copy(@"./UvalDB.db", string.Format("./{0}/UvalDB_{1}.db", ThatBackupFolder, files.Count));
                }
            }
            catch (Exception e_)
            {
                MessageBox.Show(e_.ToString());
            }
        }

    }
}

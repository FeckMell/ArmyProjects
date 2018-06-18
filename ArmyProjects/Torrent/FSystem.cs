using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Torrent
{
    static public class FSystem
    {
        static public string ListDirContent(string name_)
        {
            string result = "";

            if (!Directory.Exists(name_))
            {
                result ="Path does not exists";
                return result;
            }

            DirectoryInfo info = new DirectoryInfo(name_);

            var dirs = info.GetDirectories();
            foreach(var d in dirs) result += "folder:<" + d.Name + "> CreationTime:<" + d.CreationTime + "> Size:<0>" + "\n";

            var files = info.GetFiles();
            foreach(var f in files) result += "file:<" + f.Name + "> CreationTime:<" + f.CreationTime + "> Size:<" + f.Length + ">\n";
    
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------

    }
}

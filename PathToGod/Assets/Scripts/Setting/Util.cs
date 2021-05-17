using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ProjectPratice
{
    public class Util
    {
        public static string Combine(params string [] a)
        {
            return Path.Combine(a).Replace("\\","/");
        }
        public static void SimpleEncryp(byte[] _data, string _pstr)
        {
            int num = 0;
            for (int i = 0; i < _data.Length; i++)
            {
                if (num >= _pstr.Length)
                {
                    num = 0;
                }
                _data[i] ^= (byte)_pstr[num++];
            }
        }
        public static void CopyDirectory(string from, string to)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                return;
            }
            //检查是否存在目的目录  
            if (!Directory.Exists(to))
            {
                Directory.CreateDirectory(to);
            }
            if (Directory.Exists(from) == true)
            {
                //先来复制文件  
                DirectoryInfo directoryInfo = new DirectoryInfo(from);
                FileInfo[] files = directoryInfo.GetFiles();
                //复制所有文件  
                foreach (FileInfo file in files)
                {
                    string _toPath = Path.Combine(to, file.Name);
                    file.CopyTo(_toPath);
                }
                //最后复制目录  
                DirectoryInfo[] directoryInfoArray = directoryInfo.GetDirectories();
                foreach (DirectoryInfo dir in directoryInfoArray)
                {
                    CopyDirectory(Path.Combine(from, dir.Name), Path.Combine(to, dir.Name));
                }
            }
        }
        public static string GetGUID()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}

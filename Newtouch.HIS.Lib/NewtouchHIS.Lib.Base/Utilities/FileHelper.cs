using System.Text;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public class FileHelper
    {
        #region private
        private static string[] badChar = { "'", ";", "/", "\"", "@", "&", "$", "?", "#", ">", "<", "," };
        private static string[] badext = { "exe", "msi", "bat", "com", "sys", "aspx", "asax", "ashx" };

        #endregion
        #region 文件操作
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        public static FileInfo[] GetFiles(string directoryPath)
        {
            if (!IsExistDirectory(directoryPath))
            {
                throw new DirectoryNotFoundException();
            }
            var root = new DirectoryInfo(directoryPath);
            return root.GetFiles();
        }
        public static FileInfo[] GetFilesSearch(string directoryPath, string searchKey)
        {
            if (badChar.Contains(searchKey) || string.IsNullOrWhiteSpace(searchKey))
            {
                return null;
            }
            if (IsDir(directoryPath))
            {
                DirectoryInfo DirectorySearch = new DirectoryInfo(directoryPath);
                FileInfo[] filesInDir = DirectorySearch.GetFiles("*" + searchKey + "*.*", SearchOption.AllDirectories);
                //DirectoryInfo[] dirsInDir = DirectorySearch.GetDirectories("*" + searchKey + "*.*");
                return filesInDir;
            }
            return new FileInfo[] { new FileInfo(directoryPath) };


        }
        public static string ReadFile(string Path)
        {
            string s;
            if (!File.Exists(Path))
                s = "不存在相应的目录";
            else
            {
                var f2 = new StreamReader(Path, Encoding.Default);
                s = f2.ReadToEnd();
                f2.Close();
                f2.Dispose();
            }
            return s;
        }

        public static void FileMove(string OrignFile, string NewFile)
        {
            File.Move(OrignFile, NewFile);
        }
        public static void CreateDir(string dir)
        {
            if (dir.Length == 0) return;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        //删除文件
        public static bool DeleteFile(string Path)
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
            return true;
        }
        #endregion

        public static string ProExt(string ext, out string msg)
        {
            msg = "";
            if (string.IsNullOrWhiteSpace(ext)) return "";
            if (badext.Contains(ext))
            {
                msg = "危险文件";
            }
            return "";
        }

        public static string ProExt(string ext)
        {
            if (string.IsNullOrWhiteSpace(ext)) return "";
            if (badext.Contains(ext))
            {
                return null;
            }
            if (ext.First() == '.') return ext;
            return "." + ext;
        }


        /// <summary>
        /// 判断目标是文件夹还是目录(目录包括磁盘)
        /// </summary>
        /// <param name="filepath">文件名</param>
        /// <returns></returns>
        public static bool IsDir(string filepath)
        {
            //FileInfo fi = new FileInfo(filepath);
            //if ((fi.Attributes & FileAttributes.Directory) != 0)
            //    return true;
            //else
            //{
            //    return false;
            //}
            try
            {
                FileAttributes attr = File.GetAttributes(filepath);
                if (attr.HasFlag(FileAttributes.Directory))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool FileExists(string filepath)
        {
            if (File.Exists(filepath))
            {
                //存在 
                return true;
            }
            else
            {
                //不存在 
                return false;
            }
        }
    }
}

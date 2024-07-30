using drg_group;

namespace NewtouchHIS.DrgGroup.chs_drg_11
{
    //public interface IChsDrg11Group : ITransientDependency
    //{
    //    GroupResult GroupRecord(MedicalRecord record);
    //    GroupResult GroupRecord(string record);
    //    Task<List<GroupResult>> GroupRecord(string[] record);
    //    IEnumerable<string> GroupFileRead(string file, string[] cols);
    //    Task<IEnumerable<string>> GroupFileReadAsync(string file, string[] cols);
    //    string GroupFileRecord(string file, string[] cols);
    //}
    public class GroupHelper //: IChsDrg11Group
    {
        private static GroupProxy _grouper => new GroupProxy();
        public static GroupResult GroupRecord(MedicalRecord record)
        {
            var result = new GroupProxy().group_record(record.ToInputString());
            return new GroupResult
            {
                status = result.status,
                messages = result.messages,
                Index = result.Index,
                mdc = result.mdc,
                adrg = result.adrg,
                drg = result.drg,
                record = record
            };
        }
        public static GroupResult GroupRecord(string record)
        {
            //GroupProxy grouper = new GroupProxy();
            var result = _grouper.group_record(record);
            return new GroupResult
            {
                status = result.status,
                messages = result.messages,
                Index = result.Index,
                mdc = result.mdc,
                adrg = result.adrg,
                drg = result.drg,
                //record = record
            };
        }
        public static async Task<List<GroupResult>> GroupRecord(string[] record)
        {
            return await Task.Run(() => _grouper.group_record(record).Select(p => new GroupResult
            {
                status = p.status,
                messages = p.messages,
                Index = p.Index,
                mdc = p.mdc,
                adrg = p.adrg,
                drg = p.drg,
            }).ToList());
        }
        /// <summary>
        /// 文件分组结果
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static string GroupFileRecord(string file, string[] cols)
        {
            //GroupProxy grouper = new GroupProxy();
            _grouper.group_csv(file, cols);
            return file.Replace(".csv", "_csharp_result.csv");
        }
        public static IEnumerable<string> GroupFileRead(string file, string[] cols)
        {
            try
            {
                String header = (new StreamReader(File.OpenRead(file))).ReadLine();
                var cols_for_search = header.Split(',');
                var col_index = cols.Select(x => Array.IndexOf(cols_for_search, x)).ToArray();
                if (col_index.Length != cols.Length)
                {
                    Console.WriteLine("字段名称与CSV文件不匹配");
                    System.Environment.Exit(-1);
                }
                return File.ReadLines(file).Skip(1).Select(x => string.Join(',', col_index.Select(y => replace_csv(x).Split(',')[y])));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("文件读取失败");
                System.Environment.Exit(-1);
            }
            return new String[0];
        }
        public static async Task<IEnumerable<string>> GroupFileReadAsync(string file, string[] cols)
        {
            try
            {
                String header = await new StreamReader(File.OpenRead(file)).ReadLineAsync();
                var cols_for_search = header.Split(',');
                var col_index = cols.Select(x => Array.IndexOf(cols_for_search, x)).ToArray();
                if (col_index.Length != cols.Length)
                {
                    Console.WriteLine("字段名称与CSV文件不匹配");
                    System.Environment.Exit(-1);
                }
                var flines = await File.ReadAllLinesAsync(file);
                return flines.Skip(1).Select(x => string.Join(',', col_index.Select(y => replace_csv(x).Split(',')[y])));
                //return File.ReadLines(file).Skip(1).Select(x => string.Join(',', col_index.Select(y => replace_csv(x).Split(',')[y])));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("文件读取失败");
                System.Environment.Exit(-1);
            }
            return new String[0];
        }

        #region private
        private static String replace_csv(String csv)
        {
            var matches = System.Text.RegularExpressions.Regex.Matches(csv, "\"(.*?)\"");
            foreach (System.Text.RegularExpressions.Match item in matches)
            {
                csv = csv.Replace(item.Groups[0].Value, item.Groups[1].Value.Replace(",", "|"));
            }
            return csv;
        }
        #endregion
    }
}
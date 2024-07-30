namespace NewtouchHIS.Lib.Base.Model.DRG
{
    public class DrgFileInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileExtName { get; set; }
        /// <summary>
        /// 文件列名
        /// </summary>
        public string[] FileCols { get; set; }
        public bool? IsTmpFile { get; set; } = true;
    }

    public class DrgAreaFileInfo : DrgFileInfo
    {
        /// <summary>
        /// 分组器版本
        /// </summary>
        public string version { get; set; }
        public int? versionPolicy { get; set; }
    }


}

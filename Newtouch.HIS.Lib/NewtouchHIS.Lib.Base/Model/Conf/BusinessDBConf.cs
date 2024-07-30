using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Lib.Base.Model
{
    public class DBConf
    {
        /// <summary>
        /// 数据库链接名
        /// </summary>
        public string ConnId { get; set; }
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DBName { get; set; }
        public BusinessDBType DBType { get; set; }
        /// <summary>
        /// 数据库链接
        /// </summary>
        public string DBConn { get; set; }
        /// <summary>
        /// 是否启用DB
        /// </summary>
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// 开启sql输出
        /// </summary>
        public bool EnabledSqlTrace { get; set; } = false;
    }

    public class BusinessDB
    {
        /// <summary>
        /// 默认链接
        /// </summary>
        public string? DefaultConn { get; set; }
        /// <summary>
        /// 主数据库链接名
        /// </summary>
        public string MainDB { get; set; }
        /// <summary>
        /// 是否启用多库管理
        /// </summary>
        public bool EnabledMutiDB { get; set; } = false;
        public List<DBConf> DBConf { get; set; }
    }
}

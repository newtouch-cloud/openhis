using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System.Configuration;

namespace NewtouchHIS.Lib.DataBaseSvr
{
    /// <summary>
    /// 数据库上下文信息
    /// </summary>
    public class DbContextV2
    {
        /// <summary>
        /// 数据库类型。
        /// </summary>
        public DbType DbType { get; set; }
        /// <summary>
        /// 连接字符串。
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型。
        /// </summary>
        public SqlSugarScope Client { get; set; }
        public SqlSugarClient scopeClient { get; set; }

        public DbContextV2()
        {
            //默认采用配置项名
            //appSettings/DefaultDb 配置项为指定连接字符串的name
            var dbConfigName = ConfigurationManager.AppSettings["Default"];
            Init(dbConfigName);
        }

        public DbContextV2(string dbConfigName)
        {
            Init(dbConfigName);
        }
        public void Init(string dbConfigName)
        {
            var DbConfig = ConfigInitHelper.DbConfig.DBConf.Where(p=>p.ConnId== ConfigInitHelper.DbConfig.MainDB).FirstOrDefault();
            Client = new SqlSugarScope(new ConnectionConfig()
            {
                DbType = (DbType)DbConfig!.DBType,
                ConnectionString = ConnectionString,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
            });
        }

        public void InitClient(string dbConfigName)
        {
            var DbConfig = ConfigInitHelper.DbConfig;
            var configConnection = new List<ConnectionConfig>();
            DbConfig.DBConf.ForEach(option =>
            {
                if (string.IsNullOrWhiteSpace(option.DBConn) && !string.IsNullOrWhiteSpace(DbConfig.DefaultConn) && !string.IsNullOrWhiteSpace(option.DBName))
                {
                    option.DBConn = string.Format(DbConfig.DefaultConn, option.DBName);
                }
                if (!string.IsNullOrWhiteSpace(option.DBConn))
                {
                    configConnection.Add(new ConnectionConfig()
                    {
                        ConfigId = option.ConnId/*.ToLower()*/,
                        ConnectionString = option.DBConn,
                        InitKeyType = InitKeyType.Attribute,
                        DbType = (DbType)option.DBType,
                        IsAutoCloseConnection = true,
                        MoreSettings = new ConnMoreSettings()
                        {
                            IsAutoRemoveDataCache = true,
                            IsWithNoLockQuery = true,
                            DisableNvarchar = true,
                        },
                        AopEvents=new AopEvents()
                        {
                            OnLogExecuting = (sql, p) =>
                            {

                            }
                        }
                        //InitKeyType = InitKeyType.SystemTable
                    });
                }
                scopeClient = new SqlSugarClient(configConnection);
            });
        }

        /// <summary>
        /// 根据链接字符串的providerName决定那种数据库类型
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        private DbType GetSugarDbType(ConnectionStringSettings setting)
        {
            DbType dbType = DbType.SqlServer; //默认值
            var providerName = setting.ProviderName;
            if (providerName != null)
            {
                //数据库providerName：SqlClient MySqlClient SQLite OracleManaged/OracleClient Npgsql
                if (providerName.EndsWith(".SqlClient", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.SqlServer;
                }
                else if (providerName.EndsWith(".MySqlClient", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.MySql;
                }
                else if (providerName.EndsWith(".SQLite", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Sqlite;
                }
                else if (providerName.EndsWith("OracleManaged", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Oracle;
                }
                else if (providerName.EndsWith(".OracleClient", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Oracle;
                }
                else if (providerName.EndsWith("Npgsql", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.PostgreSQL;
                }
                else if (providerName.EndsWith("Dm", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Dm;
                }
            }
            return dbType;
        }
    }
}

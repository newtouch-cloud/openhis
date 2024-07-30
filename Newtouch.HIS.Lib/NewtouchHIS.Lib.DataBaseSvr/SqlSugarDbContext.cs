using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;


namespace NewtouchHIS.Lib.DataBaseSvr
{
    public class SqlSugarDbContext
    {
        //private static int i = 0;
        private readonly static BusinessDB DbConfig = ConfigInitHelper.DbConfig ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig");
        public static string DefaultDb => ConfigInitHelper.DbConfig.MainDB;
        public static SqlSugarScope Db = new SqlSugarScope(ConnectionConfigInit(),
            db =>
            {
                DbConfig.DBConf.Where(p => p.Enabled).ToList()!.ForEach(option =>
                {

                    if (db.IsAnyConnection(option.ConnId))
                    {
                        db.GetConnectionScope(option.ConnId).Aop.OnLogExecuting = (sql, pars) =>
                        {
                            if (option.EnabledSqlTrace)
                            {
                                Console.WriteLine(sql, pars);
                            }
                        };
                    }
                });
            });

        private static List<ConnectionConfig> ConnectionConfigInit()
        {
            var configConnection = new List<ConnectionConfig>();
            DbConfig.DBConf.Where(p => p.Enabled).ToList()!.ForEach(option =>
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
                        DbType = (DbType)option.DBType,
                        IsAutoCloseConnection = true,
                        MoreSettings = new ConnMoreSettings()
                        {
                            IsAutoRemoveDataCache = true,
                            IsWithNoLockQuery = true,
                            DisableNvarchar = true,
                        }
                        //InitKeyType = InitKeyType.SystemTable
                    });
                }

            });
            return configConnection;
        }
    }
}

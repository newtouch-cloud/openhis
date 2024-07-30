using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Extension;
using SqlSugar;

namespace NewtouchHIS.Lib.DataBaseSvr
{
    public class DbContext<T> where T : class, new()
    {
        public DbContext()
        {
            var DbConfig = ConfigInitHelper.DbConfig;
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
            Db = new SqlSugarClient(configConnection, db =>
            {
                db.GetConnectionScope(DBEnum.MrmsDb).Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine($"{DBEnum.MrmsDb.GetDescription()}======> {sql}");
                    Console.WriteLine();
                };

            });
            //Db = new SqlSugarClient(new ConnectionConfig()
            //{
            //    ConnectionString = DbConfig.DBConf,
            //    DbType = DbType.SqlServer,
            //    InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
            //    IsAutoCloseConnection = true, 

            //});
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据

        //public SqlSugarScopeProvider dbProvider(string ConnId) => Db.GetConnectionScope(ConnId);
        //public SqlSugarScopeProvider dbProviderAttr => Db.GetConnectionScopeWithAttr<T>();
    }
}

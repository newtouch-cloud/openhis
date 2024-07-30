using SqlSugar;

namespace NewtouchHIS.Lib.Base.SQLSugar
{
    public class SqlSugarHelper
    {
        /// <summary>
        /// 初始化SqlSugarClient
        /// </summary>
        /// <returns>返回SqlSugarClient对象</returns>
        public static SqlSugarClient GetInstance(DbType dbType, string dbName)
        {
            var strConnectionString = string.Empty;
            switch (dbType)
            {
                case DbType.SqlServer:
                    strConnectionString = GetSqlDBconnection(dbName);
                    //strConnectionString = @"Data Source=61.172.179.30,18040;Initial Catalog=NewtouchHIS_Base;User ID=sa;Password=1qazxsw2#;";
                    //strConnectionString = @"server = IP; database =ERP_PROD; uid = dbadmin; pwd = cltest22; Connect Timeout = 1200;";
                    break;
                case DbType.Oracle:
                    strConnectionString = @"Data Source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =IP)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME =LABELING)));User ID=label;Password=label20221123;";
                    //strConnectionString = "Data Source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME =ORCL)));User ID=TEST;Password=TEST;";
                    break;
                case DbType.MySql:
                    strConnectionString = @"Server=DESKTOP-9GRFFRR;database=Test_Demo;Trusted_Connection=True;MultipleActiveResultSets=True;";
                    break;
            }
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = strConnectionString,
                DbType = dbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,

            });
            return db;
        }

        private static string GetSqlDBconnection(string dbName)
        {
            string strConnectionString = @"Data Source=61.172.179.73,41108;Initial Catalog={0};User ID=sa;Password=a1+b2*c3=d4;";
            switch (dbName)
            {
                case "sett":
                    return string.Format(strConnectionString, "NewtouchHIS_Sett");
                case "base":
                    return string.Format(strConnectionString, "NewtouchHIS_Base");
                case "mrms":
                    return string.Format(strConnectionString, "Newtouch_MRMS");
                case "cis":
                    return string.Format(strConnectionString, "Newtouch_CIS");
            }
            return strConnectionString;
        }


        /// <summary>
        /// 根据表生产实体
        /// </summary>
        /// <param name="path"></param>
        public static void DbFirst(DbType dbType, string path, Dictionary<string, string> tabledic, string dbName)
        {
            List<string> basename = tabledic.Keys.ToList();
            List<string> tablename = tabledic.Values.ToList();
            //db.DbFirst.IsCreateAttribute().CreateClassFile(path);
            var db = GetInstance(dbType, dbName);
            //ALL所有表  
            //List<DbTableInfo> list = db.DbMaintenance.GetTableInfoList().Where(x => x.Name.Contains("mr")).ToList();
            List<DbTableInfo> list = db.DbMaintenance.GetTableInfoList().Where(x => tablename.Contains("ALL") ? x.Name.IsNormalized() : tablename.Contains(x.Name.Trim())).ToList();
            //所有视图
            List<DbTableInfo> viewList = db.DbMaintenance.GetViewInfoList().Where(p => tablename.Contains(p.Name.Trim())).ToList();

            var allList = list.Concat(viewList);

            //循环所有的表和视图 他们属于同一个类 DbTableInfo
            foreach (DbTableInfo table in allList)
            {
                //首字母转大写 
                string table_name = table.Name.Substring(0, 1).ToUpper() + table.Name.Substring(1).ToLower().Replace("_", "");
                //映射表增加 实体名称 和表名称
                db.MappingTables.Add(table_name, table.Name);
                //根据表名 获取所有表字段
                List<DbColumnInfo> dd = db.DbMaintenance.GetColumnInfosByTableName(table.Name);
                var @base = tabledic.FirstOrDefault(q => tablename.Contains("ALL") ? q.Value == "ALL" : q.Value == table.Name).Key;
                foreach (DbColumnInfo item in dd)
                {
                    string columnname = item.DbColumnName.ToLower();
                    //映射字段添加 （字段名，字段名，表名）
                    db.MappingColumns.Add(columnname, columnname, table_name);
                }
                db.DbFirst.
                SettingClassTemplate(old =>
                {
                    string snp = "\r\n    ";
                    var sugartable = GetClassTemplate().Replace("{SugarTable}", @$"{snp}[Tenant(""{dbName}"")]{snp}[SugarTable(""{table.Name}"",""{@base}"")]");
                    return sugartable;
                })
                .SettingNamespaceTemplate(old =>
                {
                    return old;
                })
                .SettingPropertyDescriptionTemplate(old =>
                {
                    //自定义的模板
                    return old;//GetPropertyDescriptionTemplate();
                })
                .SettingPropertyTemplate((columns, temp, type) =>
                {
                    string comp = "\r\n           ";
                    var columnattribute = $"{comp}[SugarColumn({{0}})]";
                    List<string> attributes = new List<string>();
                    if (columns.IsPrimarykey)
                        attributes.Add("IsPrimaryKey=true");
                    if (columns.IsIdentity)
                        attributes.Add("IsIdentity=true");
                    if (attributes.Count == 0)
                        columnattribute = string.Empty;
                    List<string> customattributes = new List<string>();
                    if (!columns.IsNullable)
                        customattributes.Add($@"{comp}[Required(ErrorMessage =""{string.Format("{0}不可为空", columns.DbColumnName)}"" )]");
                    if (!columns.DataType.ToLower().Equals("int"))
                        customattributes.Add($@"{comp}[StringLength({columns.Length}, ErrorMessage =""{string.Format("{0}长度限制为{1}", columns.DbColumnName, columns.Length)}"" )]");

                    var t = temp.Replace("{PropertyType}", type)
                            .Replace("{PropertyName}", columns.DbColumnName)
                            .Replace("{SugarColumn}", attributes.Count > 0 ? string.Format(columnattribute, string.Join(",", attributes)) : string.Join("", customattributes));
                    return t;
                })
                .SettingConstructorTemplate(old =>
                {
                    return old;
                }).IsCreateAttribute().Where(table.Name).CreateClassFile(path);
            }
        }

        /// <summary>
        /// 命名空间的模板
        /// </summary>
        /// <returns></returns>
        public static string GetClassTemplate()
        {
            return @"using System.ComponentModel.DataAnnotations;
{using}
namespace {Namespace}
{
{ClassDescription}{SugarTable}
    public partial class {ClassName}
    {
           public {ClassName}() { }

{Constructor}{PropertyName}
    }
}
";
        }

        /// <summary>
        /// 字段的模板
        /// </summary>
        /// <returns></returns>
        public static string GetPropertyDescriptionTemplate()
        {
            return @"
   /// <summary>
   /// Remark:{PropertyDescription}
   /// Default:{DefaultValue}
   /// Nullable:{IsNullable}
   /// </summary>";
        }


        #region 通用方法
        /// <summary>
        /// 拼接出完整的Sql，方便查看
        /// </summary>
        public class ToSqlExplain
        {
            public static string GetSql(KeyValuePair<string, SugarParameter[]> queryString)
            {
                var sql = queryString.Key;//sql语句
                var par = queryString.Value;//参数

                //字符串替换MethodConst1x会替换掉MethodConst1所有要从后往前替换,不能用foreach,后续可以优化
                for (int i = par.Length - 1; i >= 0; i--)
                {
                    if (par[i].ParameterName.StartsWith("@") && par[i].ParameterName.Contains("UnionAll"))
                    {
                        sql = sql.Replace(par[i].ParameterName, par[i].Value.ToString());
                    }
                }

                for (int i = par.Length - 1; i >= 0; i--)
                {
                    if (par[i].ParameterName.StartsWith("@Method"))
                    {
                        sql = sql.Replace(par[i].ParameterName, "'" + par[i].Value.ToString() + "'");
                    }
                }
                for (int i = par.Length - 1; i >= 0; i--)
                {
                    if (par[i].ParameterName.StartsWith("@Const"))
                    {
                        sql = sql.Replace(par[i].ParameterName, par[i].Value.ToString());
                    }
                }
                for (int i = par.Length - 1; i >= 0; i--)
                {
                    if (par[i].ParameterName.StartsWith("@"))
                    {
                        //值拼接单引号 拿出来的sql不会报错
                        sql = sql.Replace(par[i].ParameterName, "'" + Convert.ToString(par[i].Value) + "'");
                    }
                }
                return sql;
            }
        }

        #endregion
    }
}

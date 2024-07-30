using System.ComponentModel;

namespace NewtouchHIS.Lib.Base.EnumExtend
{
    /// <summary>
    /// 多租户
    /// </summary>
    public enum DBEnum
    {
        [Description("后台管理数据库")]
        BaseDb = 1,
        [Description("结算系统数据库")]
        SettDb = 2,
        [Description("医护协同数据库")]
        CisDb = 3,
        [Description("药房药库数据库")]
        PdsDb = 4,
        [Description("电子病历系统数据库")]
        EmrDb = 5,
        [Description("病案系统数据库")]
        MrmsDb = 6,
        [Description("质控系统数据库")]
        MrQcDb = 7,
        [Description("统一平台数据库")]
        UnionDb = 8,
        [Description("手术管理系统")]
        OrDb = 9,
        [Description("物资耗材管理系统")]
        HerpDb = 10,
        [Description("接口数据库")]
        InterfaceDb = 11,
    }


    public enum JoinTypeEnum
    {
        Inner,
        Left,
        Right,
        Full
    }

    /// <summary>
    /// SqlSugar.DbType
    /// </summary>
    public enum BusinessDBType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4,
        Dm = 5,
        Kdbndp = 6,
        Oscar = 7,
        MySqlConnector = 8,
        Access = 9,
        OpenGauss = 10,
        QuestDB = 11,
        HG = 12,
        ClickHouse = 13,
        GBase = 14,
        Odbc = 0xF,
        OceanBaseForOracle = 0x10,
        TDengine = 17,
        GaussDB = 18,
        OceanBase = 19,
        Tidb = 20,
        Custom = 900
    }

    public enum JoinKeyTypeEnum
    {
        Inner=0,
        Left=1, 
        Right=2, 
        Full=3
    }

    public enum OperatorEnum
    {
        And = 1,
        AndIF = 2,
        Or = 3,
        OrIF = 4
    }
}

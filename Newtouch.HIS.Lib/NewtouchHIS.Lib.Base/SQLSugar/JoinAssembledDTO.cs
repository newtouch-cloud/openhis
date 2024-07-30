namespace NewtouchHIS.Lib.Base.SQLSugar
{
    /// <summary>
    /// 组装连表查询
    /// </summary>
    public class JoinAssembledDTO
    {
        public string Table { get; set; }
        public string TableAs { get; set; }
        List<TableJoinInfo>? JoinInfos { get; set; }
    }

    public class TableJoinInfo
    {
        public string JoinTable { get; set; }
        public string JoinTableAs { get; set; }
        public string JoinFunc { get; set; }
        public string JoinFuncParameters { get; set; }
        public string JoinType { get; set; }
    }
}

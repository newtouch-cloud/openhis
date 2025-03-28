using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    //病区病人配药操作
    public class DispenseBQInfoVO
    {
        public int zxId { get; set; }//医嘱执行Id
        public string zyh { get; set; }//住院号
        public Decimal sl { get; set; }//数量
        public string lyyf { get; set; }//类要药房
        public string ypCode { get; set; }//药品code
        public short cls { get; set; }// 拆量数
        public DateTime jdrq { get; set; }//提档日期、操作日期,建档日期
    }
    /// <summary>
    /// 获取需要发药的用户的详细信息批次、批号、数量
    /// </summary>
    public class DispenseBQInfoZYFYVO
    {// 
        public string ypCode { get; set; }//药品编号
        public string ypph { get; set; }//批号
        public string yppc { get; set; }//批次
        public string ypyjbm { get; set; }//医嘱编码
        public string ypsl { get; set; }//数量
    }
}

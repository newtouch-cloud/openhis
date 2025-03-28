namespace Newtouch.HIS.Domain.DO.Book
{
    /// <summary>
    /// 排药失败处方明细
    /// </summary>
    public class PyFailCfmxDO
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }


        /// <summary>
        /// 错误信息
        /// </summary>
        public string errorMsg { get; set; }
    }
}

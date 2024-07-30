namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 系统 药品 出入库方式
    /// </summary>
    public class SysMedicineCrkfsVEntity
    {
        /// <summary>
        /// 出入库方式ID
        /// </summary>
        public int crkfsId { get; set; }

        /// <summary>
        /// 出入库方式代码
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// 出入库方式名称
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// 出入库标志  0：入库  1：出库
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// 状态  1：有效 0：无效
        /// </summary>
        public string zt { get; set; }
    }
}

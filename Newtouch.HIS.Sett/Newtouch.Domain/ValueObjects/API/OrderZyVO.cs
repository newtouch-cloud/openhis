
namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OrderZyVO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 收费大类
        /// </summary>
        public string dlcode { get; set; }
        /// <summary>
        /// 收费大类名称
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 处方收费项目
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? sl { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// 计费单位
        /// </summary>
        public string jfdw { get; set; }
        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// 转自费标识
        /// </summary>
        public string zzfbz { get; set; }
    }

    public class OrderZyBaseVO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 收费大类
        /// </summary>
        public string dlcode { get; set; }
        /// <summary>
        /// 收费大类名称
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? je { get; set; }

    }


}

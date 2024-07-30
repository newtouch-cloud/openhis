namespace Newtouch.Herp.Domain.ValueObjects
{
    /// <summary>
    /// supplier contact vo
    /// </summary>
    public class SupplierContactVO
    {
        /// <summary>
        /// gys_contacts Id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string contactName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// 职责
        /// </summary>
        public string duties { get; set; }

        /// <summary>
        /// 固话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string keyWord { get; set; }

    }
}

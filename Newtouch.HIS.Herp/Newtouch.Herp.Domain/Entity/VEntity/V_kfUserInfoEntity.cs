namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 库房用户信息
    /// </summary>
    public class VKfUserInfoEntity
    {

        /// <summary>
        /// 工号
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 员工名称
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// 库房ID
        /// </summary>
        public string kfId { get; set; }

        /// <summary>
        /// 库房名称
        /// </summary>
        public string kfName { get; set; }

        /// <summary>
        /// 库房等级
        /// </summary>
        public int kfLeve { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string dutyName { get; set; }
    }
}

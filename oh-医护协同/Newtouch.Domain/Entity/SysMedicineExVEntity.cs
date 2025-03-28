using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 扩展药品信息
    /// </summary>
    public class SysMedicineExVEntity: SysMedicineVEntity
    {
        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }
    }
}
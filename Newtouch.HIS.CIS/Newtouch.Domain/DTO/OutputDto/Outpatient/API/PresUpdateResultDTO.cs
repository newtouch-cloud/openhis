using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// 通过API更新处方的返回结果
    /// </summary>
    public class PresUpdateResultDTO
    {
        /// <summary>
        /// 已结处方 处方号 List（哪些已结了 还请求更新）
        /// </summary>
        public IList<string> settledCfhList { get; set; }
    }
}

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// 处方状态
    /// </summary>
    public class PrescriptionStatusDTO
    {
        /// <summary>
        /// 1 0
        /// </summary>
        public string cfzt { get; set; }

        /// <summary>
        /// 1正常 0已作废
        /// </summary>
        public string cfztValue { get; set; }

        /// <summary>
        /// 0 1
        /// </summary>
        public string jszt { get; set; }

        /// <summary>
        /// 0未结 1已结
        /// </summary>
        public string jsztValue { get; set; }


    }
}

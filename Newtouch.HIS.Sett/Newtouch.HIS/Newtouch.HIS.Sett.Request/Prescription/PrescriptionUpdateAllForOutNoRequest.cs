namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 门诊号下的处方信息全部重写
    /// </summary>
    public class PrescriptionUpdateAllForOutNoRequest : PrescriptionAddOrUpdateRequest
    {
        /// <summary>
        /// 逗号分割的cf（若未指定，作废所有的未收费处方）
        /// </summary>
        public string AheadCancelCf { get; set; }

    }
}

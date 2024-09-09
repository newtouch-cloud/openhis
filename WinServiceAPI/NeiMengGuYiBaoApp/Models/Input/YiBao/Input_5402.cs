namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_5402 : InputBase
    {
        public Rptdetailinfo Rptdetailinfo { get; set; }
    }

    public class Rptdetailinfo
    {
        /// <summary>
        /// 人员编号 (必填, 长度: 30) | Y
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 报告单号 (长度: 30) | Y
        /// </summary>
        public string rpotc_no { get; set; }

        /// <summary>
        /// 机构编号 (长度: 20) | Y
        /// </summary>
        public string fixmedins_code { get; set; }

    }

}

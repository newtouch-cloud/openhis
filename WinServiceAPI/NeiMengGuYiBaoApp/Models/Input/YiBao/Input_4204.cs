using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    //1、交易输入费用明细信息为单行数据，费用明细流水信息为多行数据； 
    //2、每次接口调用只删除一位患者的住院费用明细。
    //3、若费用明细流水信息不传，则删除自费病人住院全部费用明细。
    public class Input_4204 : InputBase
    {
        // 自费病人费用明细信息
        public FeeDetail feedetail { get; set; }
        // 自费病人费用明细流水信息
        public List<FeeDetl> feedetl { get; set; }
    }

    public class FeeDetail
    {
        /// <summary>
        /// 医药机构就诊 ID (长度: 30)
        /// </summary>
        public string fixmedins_mdt_rt_id { get; set; }

        /// <summary>
        /// 定点医药机构编号 (长度: 30)
        /// </summary>
        public string fixmedins_code { get; set; }
    }

    /// <summary>
    /// 自费病人费用明细流水信息（节点标识：feedetl）
    /// </summary>
    public class FeeDetl
    {
        /// <summary>
        /// 记账流水号 (长度: 30)
        /// </summary>
        public string bkkp_sn { get; set; }
    }

}

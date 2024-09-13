namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    //交易输入就诊信息为单行数据；
    //每次接口调用只修改一位患者的就诊信息；
    //可以将状态由完成改为未完成，也可以将未完成改为完成。
    public class Input_4203 : InputBase
    {
        public string fixmedins_mdtrt_id { get; set; }  // 医药机构就诊 ID (必填, 字符型 30) 机构生成内唯一就诊流水
        public string fixmedins_code { get; set; }  // 定点医药机构编号(必填, 字符型 30)
        public string cplt_flag { get; set; }  // 完成标志 (必填, 字符型 6) 费用明细和就诊信息上传完成后修改完成标志
    }
}

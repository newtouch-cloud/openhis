namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4203 : InputBase
    {
        // 自费病人就诊信息
        public Input4203 input { get; set; }
    }

    public class Input4203
    {
        public string fixmedins_mdtrt_id { get; set; }  // 医药机构就诊 ID (必填, 字符型 30) 机构生成内唯一就诊流水
        public string fixmedins_code { get; set; }  // 定点医药机构编号(必填, 字符型 30)
        public string cplt_flag { get; set; }  // 完成标志 (必填, 字符型 6) 费用明细和就诊信息上传完成后修改完成标志
    }
}

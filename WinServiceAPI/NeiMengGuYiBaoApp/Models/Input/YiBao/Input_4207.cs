namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4207 : InputBase
    {
        // 自费病人就诊信息
        public Input4207 input { get; set; }
    }

    public class Input4207
    {
        public string fixmedins_mdtrt_id { get; set; }  // 医药机构就诊 ID (必填, 字符型 30) 机构生成内唯一就诊流水
        public string fixmedins_code { get; set; }  // 定点医药机构编号(必填, 字符型 30)
        public string page_num { get; set; }  // 当前页数 (必填, 数值型 4)
        public string page_size { get; set; }  // 本页数据量(必填, 数值型 4)
    }
}

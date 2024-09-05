using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3502 : InputBase
    {
        public Invinfo3502 invinfo { get; set; }
    }
    public class Invinfo3502
    {
        /// <summary>
        /// 医疗目录编码 字符型 50 Y
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 库存变更类型 字符型 6 Y Y
        /// </summary>
        public string inv_chg_type { get; set; }

        /// <summary>
        /// 定点医药机构目录编号 字符型 30 Y
        /// </summary>
        public string fixmedins_hilist_id { get; set; }

        /// <summary>
        /// 定点医药机构目录名称 字符型 200 Y
        /// </summary>
        public string fixmedins_hilist_name { get; set; }

        /// <summary>
        /// 定点医药机构批次流水号 字符型 30 Y
        /// </summary>
        public string fixmedins_bchno { get; set; }

        /// <summary>
        /// 单价 数值型 16,6 Y
        /// </summary>
        public decimal pric { get; set; }

        /// <summary>
        /// 数量 数值型 16,4 Y 按最小计价包装单位
        /// </summary>
        public decimal cnt { get; set; }

        /// <summary>
        /// 处方药标志 字符型 3 Y Y
        /// </summary>
        public string rx_flag { get; set; }

        /// <summary>
        /// 库存变更时间 日期时间型 Y yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string inv_chg_time { get; set; }

        /// <summary>
        /// 库存变更经办人姓名 字符型 50
        /// </summary>
        public string inv_chg_opter_name { get; set; }

        /// <summary>
        /// 备注 字符型 500
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 拆零标志 字符型 2 Y
        /// </summary>
        public string trdn_flag { get; set; }

        /// <summary>
        /// 溯源码节点信息
        /// </summary>
        public List<Drugtracinfo> drugtracinfo { get; set; }
    }
}

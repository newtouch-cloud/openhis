using System;
using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_4209 : OutputBase
    {
        public List<Output4209> data { get; set; }
    }

    /// <summary>
    /// 诊断信息
    /// </summary>
    public class Output4209
    {
        /// <summary>
        /// 诊断信息 ID (长度: 30)
        /// </summary>
        public string diagInfoId { get; set; }

        /// <summary>
        /// 医药机构就诊 ID (长度: 30)
        /// </summary>
        public string fixmedinsMdtrtId { get; set; }

        /// <summary>
        /// 定点医药机构编号 (长度: 30)
        /// </summary>
        public string fixmedinsCode { get; set; }

        /// <summary>
        /// 出入院诊断类别 (长度: 6)
        /// </summary>
        public string inoutDiagType { get; set; }

        /// <summary>
        /// 诊断类别 (长度: 3)
        /// </summary>
        public string diagType { get; set; }

        /// <summary>
        /// 主诊断标志 (长度: 3)
        /// </summary>
        public string maindiagFlag { get; set; }

        /// <summary>
        /// 诊断排序号 (长度: 2)
        /// </summary>
        public string diagSrtNo { get; set; }

        /// <summary>
        /// 诊断代码 (长度: 20)
        /// </summary>
        public string diagCode { get; set; }

        /// <summary>
        /// 诊断名称 (长度: 255)
        /// </summary>
        public string diagName { get; set; }

        /// <summary>
        /// 诊断科室 (长度: 50)
        /// </summary>
        public string diagDept { get; set; }

        /// <summary>
        /// 诊断医师代码 (长度: 30)
        /// </summary>
        public string diagDrCode { get; set; }

        /// <summary>
        /// 诊断医师姓名 (长度: 50)
        /// </summary>
        public string diagDrName { get; set; }

        /// <summary>
        /// 诊断时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime diagTime { get; set; }

        /// <summary>
        /// 入院病情 (长度: 500)
        /// </summary>
        public string admCond { get; set; }

        /// <summary>
        /// 有效标志 (长度: 3)
        /// </summary>
        public string valiFlag { get; set; }

        /// <summary>
        /// 经办人 ID (长度: 20)
        /// </summary>
        public string opterId { get; set; }

        /// <summary>
        /// 经办人姓名 (长度: 50)
        /// </summary>
        public string opterName { get; set; }

        /// <summary>
        /// 经办时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime optTime { get; set; }

        /// <summary>
        /// 唯一记录号 (长度: 30)
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updtTime { get; set; }

        /// <summary>
        /// 创建人 (长度: 20)
        /// </summary>
        public string crterId { get; set; }

        /// <summary>
        /// 创建人姓名 (长度: 50)
        /// </summary>
        public string crterName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime crteTime { get; set; }

        /// <summary>
        /// 创建机构 (长度: 20)
        /// </summary>
        public string crteOptinsNo { get; set; }

        /// <summary>
        /// 经办机构 (长度: 20)
        /// </summary>
        public string optinsNo { get; set; }

        /// <summary>
        /// 统筹区编码 (长度: 10)
        /// </summary>
        public string poolareaNO { get; set; }
    }


}

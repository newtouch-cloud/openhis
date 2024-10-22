using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class PrescriptionDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// 枚举 1 西药处方 2 中药处方 3 康复处方 4 检验处方 5 检查处方 6 输液处方
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 贴数
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// 临床原因
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// 申请备注
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// 处方明细
        /// </summary>
        public List<PrescriptionDetailVO> cfmxList { get; set; }

        /// <summary>
        /// 处方标签 JI 精I JII 精II MZ 麻醉
        /// </summary>
        public string cftag { get; set; }
        public string jzId { get; set; }


        public string isdzcf { get; set; }//电子处方 1 是  0 否
        public string gjybdm { get; set; }

        /// <summary>
        /// 代煎方式
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// 代煎付数
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// 嘱托
        /// </summary>
        public string cfzt { get; set; }

		/// <summary>
		/// 同步标志
		/// </summary>
		public bool? tbbz { get; set; }

		/// <summary>
		/// 退标志
		/// </summary>
		public bool? tbz { get; set; }

		/// <summary>
		/// 申请单号
		/// </summary>
		public string sqdh { get; set; }

		/// <summary>
		/// 远程医疗上传状态
		/// </summary>
		public string SyncStatus { get; set; }


		/// <summary>
		/// 处方整剂医嘱说明
		/// </summary>
		public string rxDrordDscr { get; set; }

		/// <summary>
		/// 处方有效天数
		/// </summary>
		public int? valiDays { get; set; }

		/// <summary>
		/// 复用(多次)使用标志
		/// </summary>
		public string reptFlag { get; set; }

		/// <summary>
		/// 最大使用次数
		/// </summary>
		public int? maxReptCnt { get; set; }

		/// <summary>
		/// 使用最小间隔(天数)
		/// </summary>
		public int? minInrvDays { get; set; }

		/// <summary>
		/// 续方标志
		/// </summary>
		public string rxCotnFlag { get; set; }

		/// <summary>
		/// 长期处方标志
		/// </summary>
		public string longRxFlag { get; set; }

		/// <summary>
		/// 处方追溯码
		/// </summary>
		public string rxTraceCode { get; set; }

		/// <summary>
		/// 医保处方编号
		/// </summary>
		public string hiRxno { get; set; }
	}
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2020-03-13 11:29
    /// 描 述：病案首页-手术记录
    /// </summary>
    [Table("mr_basy_ss")]
    public class MrbasyssEntity : IEntity<MrbasyssEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// BAH
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }

        /// <summary>
        /// SSOrder
        /// </summary>
        /// <returns></returns>
        public int SSOrder { get; set; }

        /// <summary>
        /// 手术及操作编码
        /// </summary>
        /// <returns></returns>
        public string SSJCZBM { get; set; }

        /// <summary>
        /// 手术及操作日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SSJCZRQ { get; set; }

        /// <summary>
        /// 手术级别
        /// </summary>
        /// <returns></returns>
        public string SSJB { get; set; }

        /// <summary>
        /// 手术及操作名称
        /// </summary>
        /// <returns></returns>
        public string SSJCZMC { get; set; }

        /// <summary>
        /// 术者
        /// </summary>
        /// <returns></returns>
        public string SZ { get; set; }

        /// <summary>
        /// I助
        /// </summary>
        /// <returns></returns>
        public string YZ { get; set; }

        /// <summary>
        /// II助
        /// </summary>
        /// <returns></returns>
        public string EZ { get; set; }

        /// <summary>
        /// 切口等级
        /// </summary>
        /// <returns></returns>
        public string QKDJ { get; set; }

        /// <summary>
        /// 切口愈合类别
        /// </summary>
        /// <returns></returns>
        public string QKYHLB { get; set; }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public string MZFS { get; set; }

        /// <summary>
        /// 麻醉医师
        /// </summary>
        /// <returns></returns>
        public string MZYS { get; set; }

        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// QKYHDJ
        /// </summary>
        /// <returns></returns>
        public string QKYHDJ { get; set; }


		/// <summary>
		/// 是否急诊手术 0：否，1：是
		/// </summary>
		public int? SSLX { get; set; }

		/// <summary>
		/// 手术开始日期
		/// </summary>
		public DateTime? SSKSSJ { get; set; }

		/// <summary>
		/// 手术结束日期
		/// </summary>
		public DateTime? SSJSSJ { get; set; }

		/// <summary>
		/// 术前准备天数
		/// </summary>
		public decimal? SQZBSJT { get; set; }

		/// <summary>
		/// 术前预防性抗菌药物给药时间
		/// </summary>
		public DateTime? SQYFKJGYS { get; set; }

		/// <summary>
		/// 麻醉开始时间
		/// </summary>
		public DateTime? MZKSSJNYR { get; set; }


		/// <summary>
		/// ASA麻醉分级
		/// </summary>
		public string MZFJ_ASA { get; set; }

		/// <summary>
		/// 切口部位
		/// </summary>
		public string QKBW { get; set; }

		/// <summary>
		/// 手术切口感染
		/// </summary>
		public string SSQKGR { get; set; }

		/// <summary>
		/// 手术并发症
		/// </summary>
		public string SSBFZ { get; set; }

		/// <summary>
		/// 手术并发症名称
		/// </summary>
		public string SSBFZMC { get; set; }

	}
}
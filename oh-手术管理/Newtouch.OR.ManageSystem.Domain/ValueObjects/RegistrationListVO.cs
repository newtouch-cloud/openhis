using Newtouch.OR.ManageSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.ValueObjects
{
    public class RegistrationListVO
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 登记编号
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 安排编号
        /// </summary>
        /// <returns></returns>
        public string arrangeId { get; set; }
        /// <summary>
        /// 申请编号
        /// </summary>
        /// <returns></returns>
        public string applyId { get; set; }
        /// <summary>
        /// 申请代码
        /// </summary>
        public string applyno { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string xm { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public string xb { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        /// <returns></returns>
        public string nl { get; set; }
        /// <summary>
        /// 手术登记号
        /// </summary>
        /// <returns></returns>
        public string ssxh { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        /// <returns></returns>
        public string bq { get; set; }
        /// <summary>
        /// 床号
        /// </summary>
        /// <returns></returns>
        public string ch { get; set; }
        /// <summary>
        /// 诊断
        /// </summary>
        /// <returns></returns>
        public string zd { get; set; }
        /// <summary>
        /// 入院诊断
        /// </summary>
        /// <returns></returns>
        public string ryzd { get; set; }
        /// <summary>
        /// 入院诊断名称
        /// </summary>
        /// <returns></returns>
        public string ryzdmc { get; set; }
        /// <summary>
        /// 术后诊断
        /// </summary>
        /// <returns></returns>
        public string sszd { get; set; }
        /// <summary>
        /// 术后诊断名称
        /// </summary>
        /// <returns></returns>
        public string sszdmc { get; set; }
        /// <summary>
        /// 病情
        /// </summary>
        /// <returns></returns>
        public string shbq { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }
        /// <summary>
        /// 手术级别
        /// </summary>
        /// <returns></returns>
        public string ssjb { get; set; }
        /// <summary>
        /// 手术名称
        /// </summary>
        /// <returns></returns>
        public string ssmc { get; set; }
        /// <summary>
        /// 手术代码
        /// </summary>
        public string ssdm { get; set; }
        /// <summary>
        /// 手术申请时间
        /// </summary>
        /// <returns></returns>
        public DateTime? sssqsj { get; set; }
        /// <summary>
        /// 手术安排时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ssapsj { get; set; }
        /// <summary>
        /// 手术开始时间
        /// </summary>
        /// <returns></returns>
        public DateTime? sskssj { get; set; }
        /// <summary>
        /// 手术结束时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ssjssj { get; set; }
        /// <summary>
        /// 手术室
        /// </summary>
        /// <returns></returns>
        public string oproom { get; set; }
        /// <summary>
        /// 台次
        /// </summary>
        /// <returns></returns>
        public string oporder { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public string AnesCode { get; set; }
        /// <summary>
        /// 手术部位
        /// </summary>
        /// <returns></returns>
        public string ssbw { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        /// <returns></returns>
        public string qkdj { get; set; }
        /// <summary>
        /// 是否隔离 0 否 1 是
        /// </summary>
        /// <returns></returns>
        public string isgl { get; set; }
        /// <summary>
        /// 是否有菌 0 否 1 是
        /// </summary>
        /// <returns></returns>
        public string isjun { get; set; }
        /// <summary>
        /// 输血量
        /// </summary>
        /// <returns></returns>
        public decimal? shuxl { get; set; }
        /// <summary>
        /// 失血量
        /// </summary>
        /// <returns></returns>
        public decimal? shixl { get; set; }
        /// <summary>
        /// 总入量
        /// </summary>
        /// <returns></returns>
        public decimal? zrxl { get; set; }
        /// <summary>
        /// 总出量
        /// </summary>
        /// <returns></returns>
        public decimal? zcxl { get; set; }
        /// <summary>
        /// 主刀医生
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// 助理医生1
        /// </summary>
        /// <returns></returns>
        public string zlys1 { get; set; }
        /// <summary>
        /// 助理医生2
        /// </summary>
        /// <returns></returns>
        public string zlys2 { get; set; }
        /// <summary>
        /// 巡回护士
        /// </summary>
        /// <returns></returns>
        public string xhhs { get; set; }
        /// <summary>
        /// 洗手护士
        /// </summary>
        /// <returns></returns>
        public string xshs { get; set; }
        /// <summary>
        /// 麻醉医师
        /// </summary>
        /// <returns></returns>
        public string mzys { get; set; }
		/// <summary>
		/// 医生姓名
		/// </summary>
		public string ysxm { get; set; }
		/// <summary>
		/// 助理医生1名字
		/// </summary>
		public string zlys1name { get; set; }
		/// <summary>
		/// 助理医生2名字
		/// </summary>
		public string zlys2name { get; set; }
		/// <summary>
		/// 巡回护士名字
		/// </summary>
		public string xhhsname { get; set; }
		/// <summary>
		/// 洗手护士名字
		/// </summary>
		public string xshsname { get; set; }
		/// <summary>
		/// 麻醉医师名字
		/// </summary>
		public string mzysname { get; set; }

		/// <summary>
		/// 手术
		/// </summary>
		public List<ORApplyInfoExpandEntity> ss { get; set; }
	}
}

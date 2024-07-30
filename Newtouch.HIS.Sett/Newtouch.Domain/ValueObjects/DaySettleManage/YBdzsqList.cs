using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.DaySettleManage
{
	public class YBdzsqList
	{

	}
	public class ybzdinfo
	{
		/// <summary>
		/// 清算类型名称
		/// </summary>
		public string label { get; set; }
		/// <summary>
		/// 清算类型编码
		/// </summary>
		public string value { get; set; }
	}

	/// <summary>
	/// 清算对总帐
	/// </summary>
	public class Qsdzz
	{
		/// <summary>
		/// 清算类别
		/// </summary>
		public string qslb { get; set; }

		public string qslbmc { get; set; }
		/// <summary>
		/// 险种
		/// </summary>
		public string xz { get; set; }
		public string xzmc { get; set; }
		/// <summary>
		/// 是否异地
		/// </summary>
		public string sfyd { get; set; }
		public DateTime ksrq { get; set; }
		public DateTime jsrq { get; set; }
		public decimal zfy { get; set; }
		public decimal jjzfje { get; set; }
		public decimal grzh { get; set; }
		public decimal xjzfje { get; set; }
		public int jsbs { get; set; }
		public string sftf { get; set; }

		public decimal czzgtcjj { get; set; }/*城镇职工基本医疗保险统筹基金*/
		public int czzgtcjjrc { get; set; }/*城镇职工基本医疗保险统筹基金人次*/
		public decimal czzgzhjj { get; set; }/*城镇职工基本医疗保险个人账户基金*/
		public int czzgzhjjrc { get; set; }/*城镇职工基本医疗保险个人账户基金*/
		public decimal gywbzjj { get; set; }/*公务员医疗补助基金*/
		public int gywbzjjrc { get; set; }/*公务员医疗补助基金*/
		public decimal deylbzjj { get; set; }/*大额医疗费用补助基金*/
		public int deylbzjjrc { get; set; }/*大额医疗费用补助基金*/
		public decimal lxrybzjj { get; set; }/*离休人员医疗保障基金*/
		public int lxrybzjjrc { get; set; }/*离休人员医疗保障基金*/
		public decimal cjbzjj { get; set; }/*一至六级残疾军人医疗补助基金*/
		public int cjbzjjrc { get; set; }/*一至六级残疾军人医疗补助基金*/
		public decimal qybzyljj { get; set; }/*企业补充医疗保险基金*/
		public int qybzyljjrc { get; set; }/*企业补充医疗保险基金*/
		public decimal cxjmjbyljj { get; set; }/*城乡居民基本医疗保险基金*/
		public int cxjmjbyljjrc { get; set; }/*城乡居民基本医疗保险基金*/
		public decimal cxjmdbyljj { get; set; }/*城乡居民大病医疗保险基金*/
		public int cxjmdbyljjrc { get; set; }/*城乡居民大病医疗保险基金*/
		public decimal yljzjj { get; set; }/*医疗救助基金*/
		public int yljzjjrc { get; set; }/*医疗救助基金人次*/
		public decimal yfjj { get; set; }/*优抚基金*/
		public int yfjjrc { get; set; }/*优抚基金人次*/
		public decimal syjj { get; set; }/*生育基金*/
		public int syjjrc { get; set; }/*生育基金人次*/
		public decimal qtjj { get; set; }/*其他基金*/
		public int qtjjrc { get; set; }/*其他基金人次*/
		public decimal eyjj { get; set; }/*二乙基金*/
		public int eyjjrc { get; set; }/*二乙基金人次*/
	}

	/// <summary>
	/// 清算明细（上半部分）
	/// </summary>
	public class Qsmx
	{
		/// <summary>
		/// 个人编码
		/// </summary>
		public string grbm { get; set; }
		/// <summary>
		/// 就诊ID
		/// </summary>
		public string jzid { get; set; }
		/// <summary>
		/// 结算ID
		/// </summary>
		public string jsid { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		public string xm { get; set; }
		/// <summary>
		/// 医保结算日期
		/// </summary>
		public DateTime ybjsrq { get; set; }
		/// <summary>
		/// 总费用
		/// </summary>
		public decimal zfy { get; set; }
		/// <summary>
		/// 基金支付金额
		/// </summary>
		public decimal jjzfje { get; set; }

		/// <summary>
		/// 共济支付金额
		/// </summary>
		public decimal gjje { get; set; }

		/// <summary>
		/// 是否退费
		/// </summary>
		public string sftf { get; set; }
		/// <summary>
		/// 报销类型
		/// </summary>
		public string bxlx { get; set; }
		/// <summary>
		/// 身份证
		/// </summary>
		public string sfz { get; set; }
		/// <summary>
		/// 收据编号
		/// </summary>
		public string sjh { get; set; }
		/// <summary>
		/// 清算类别
		/// </summary>
		public string qslb { get; set; }
		/// <summary>
		/// 险种
		/// </summary>
		public string xz { get; set; }
		/// <summary>
		/// 开始日期
		/// </summary>
		public DateTime ksrq { get; set; }
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime jsrq { get; set; }
	}

	/// <summary>
	/// 清算明细(下半部分)
	/// </summary>
	public class Qsmx_1
	{
		/// <summary>
		/// 人员ID
		/// </summary>
		public string ryid { get; set; }
		/// <summary>
		/// 就诊ID
		/// </summary>
		public string jzid { get; set; }
		/// <summary>
		/// 结算ID
		/// </summary>
		public string jsid { get; set; }
		/// <summary>
		/// 对账结果
		/// </summary>
		public string dzjg { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string bz { get; set; }
		/// <summary>
		/// 医疗费总额
		/// </summary>
		public decimal ylfze { get; set; }
		/// <summary>
		/// 基金支付总额
		/// </summary>
		public decimal jjzfze { get; set; }
		/// <summary>
		/// 个人账户支付总额
		/// </summary>
		public decimal grzhzfze { get; set; }
		/// <summary>
		/// 发送方报文ID
		/// </summary>
		public string fsfbwid { get; set; }
		/// <summary>
		/// 退费结算标志
		/// </summary>
		public string tfjsbz { get; set; }
		/// <summary>
		/// 开始日期
		/// </summary>
		public DateTime ksrq { get; set; }
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime jsrq { get; set; }
	}

	/// <summary>
	/// 清算申请
	/// </summary>
	public class Qssq
	{

		/// <summary>
		/// 申请状态
		/// </summary>
		public string sqzt { get; set; }
		/// <summary>
		/// 清算类别
		/// </summary>
		public string qslb { get; set; }

		public string qslbmc { get; set; }
		/// <summary>
		/// 险种类型
		/// </summary>
		public string xzlx { get; set; }
		/// <summary>
		/// 是否异地
		/// </summary>
		public string sfyd { get; set; }
		/// <summary>
		/// 开始日期
		/// </summary>
		public DateTime ksrq { get; set; }
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime jsrq { get; set; }
		/// <summary>
		/// 总费用
		/// </summary>
		public decimal zfy { get; set; }
		/// <summary>
		/// 基金支付总额
		/// </summary>
		public decimal jjzfze { get; set; }
		/// <summary>
		/// 个账支付
		/// </summary>
		public decimal gzzf { get; set; }
		/// <summary>
		/// 医保认可费用总额
		/// </summary>
		public decimal ybrkfyze { get; set; }
		/// <summary>
		/// 现金支付总额
		/// </summary>
		public decimal xjzfze { get; set; }
		/// <summary>
		/// 清算人次
		/// </summary>
		public int qsrc { get; set; }
		/// <summary>
		/// 清算年月
		/// </summary>
		public string qsny { get; set; }
		/// <summary>
		/// 门诊申报金额
		/// </summary>
		public decimal mzsbje { get; set; }
		/// <summary>
		/// 住院申报金额
		/// </summary>
		public decimal zysbje { get; set; }



		public decimal czzgtcjj { get; set; }/*城镇职工基本医疗保险统筹基金*/
		public int czzgtcjjrc { get; set; }/*城镇职工基本医疗保险统筹基金人次*/
		public decimal czzgzhjj { get; set; }/*城镇职工基本医疗保险个人账户基金*/
		public int czzgzhjjrc { get; set; }/*城镇职工基本医疗保险个人账户基金*/
		public decimal gywbzjj { get; set; }/*公务员医疗补助基金*/
		public int gywbzjjrc { get; set; }/*公务员医疗补助基金*/
		public decimal deylbzjj { get; set; }/*大额医疗费用补助基金*/
		public int deylbzjjrc { get; set; }/*大额医疗费用补助基金*/
		public decimal lxrybzjj { get; set; }/*离休人员医疗保障基金*/
		public int lxrybzjjrc { get; set; }/*离休人员医疗保障基金*/
		public decimal cjbzjj { get; set; }/*一至六级残疾军人医疗补助基金*/
		public int cjbzjjrc { get; set; }/*一至六级残疾军人医疗补助基金*/
		public decimal qybzyljj { get; set; }/*企业补充医疗保险基金*/
		public int qybzyljjrc { get; set; }/*企业补充医疗保险基金*/
		public decimal cxjmjbyljj { get; set; }/*城乡居民基本医疗保险基金*/
		public int cxjmjbyljjrc { get; set; }/*城乡居民基本医疗保险基金*/
		public decimal cxjmdbyljj { get; set; }/*城乡居民大病医疗保险基金*/
		public int cxjmdbyljjrc { get; set; }/*城乡居民大病医疗保险基金*/
		public decimal yljzjj { get; set; }/*医疗救助基金*/
		public int yljzjjrc { get; set; }/*医疗救助基金人次*/
		public decimal yfjj { get; set; }/*优抚基金*/
		public int yfjjrc { get; set; }/*优抚基金人次*/
		public decimal syjj { get; set; }/*生育基金*/
		public int syjjrc { get; set; }/*生育基金人次*/
		public decimal qtjj { get; set; }/*其他基金*/
		public int qtjjrc { get; set; }/*其他基金人次*/
		public decimal eyjj { get; set; }/*二乙基金*/
		public int eyjjrc { get; set; }/*二乙基金人次*/

	}

	/// <summary>
	/// 清算回退
	/// </summary>
	public class Qsht
	{
		/// <summary>
		/// 清算期号
		/// </summary>
		public string qsqh { get; set; }
		/// <summary>
		/// 清算类别
		/// </summary>
		public string qslb { get; set; }
		/// <summary>
		/// 清算类别名称
		/// </summary>
		public string qslbmc { get; set; }
		/// <summary>
		/// 险种类型
		/// </summary>
		public string xzlx { get; set; }
		/// <summary>
		/// 是否异地
		/// </summary>
		public string sfyd { get; set; }
		/// <summary>
		/// 清算流水号
		/// </summary>
		public string qslsh { get; set; }
		/// <summary>
		/// 总费用
		/// </summary>
		public decimal zfy { get; set; }
		/// <summary>
		/// 基金申报总额
		/// </summary>
		public decimal jjsbze { get; set; }
		/// <summary>
		/// 个账支付总额
		/// </summary>
		public decimal gzzfze { get; set; }
		/// <summary>
		/// 个人现金支付总额
		/// </summary>
		public decimal grxjzfze { get; set; }
		/// <summary>
		/// 清算人次
		/// </summary>
		public int qsrc { get; set; }
		/// <summary>
		/// 开始日期
		/// </summary>
		public DateTime ksrq { get; set; }
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime jsrq { get; set; }
		/// <summary>
		/// 清算申请日期
		/// </summary>
		public DateTime qssqrq { get; set; }
		/// <summary>
		/// 清算申请人
		/// </summary>
		public string qssqr { get; set; }
		/// <summary>
		/// 医疗机构代码
		/// </summary>
		public string yljgdm { get; set; }
		/// <summary>
		/// 医疗机构名称
		/// </summary>
		public string yljgmc { get; set; }
		/// <summary>
		/// 清算经办机构名称
		/// </summary>
		public string qsjbjgmc { get; set; }
		/// <summary>
		/// 清算经办机构编码
		/// </summary>
		public string qsjbjgbm { get; set; }
		/// <summary>
		/// 门诊人次
		/// </summary>
		public int mzrc { get; set; }
		/// <summary>
		/// 住院人次
		/// </summary>
		public int zyrc { get; set; }
		/// <summary>
		/// 门诊总费用
		/// </summary>
		public decimal mzzfy { get; set; }
		/// <summary>
		/// 住院总费用
		/// </summary>
		public decimal zyzfy { get; set; }
		/// <summary>
		/// 门诊申报金额
		/// </summary>
		public decimal mzsbje { get; set; }
		/// <summary>
		/// 住院申报金额
		/// </summary>
		public decimal zysbje { get; set; }



		public decimal czzgtcjj { get; set; }/*城镇职工基本医疗保险统筹基金*/
		public int czzgtcjjrc { get; set; }/*城镇职工基本医疗保险统筹基金人次*/
		public decimal czzgzhjj { get; set; }/*城镇职工基本医疗保险个人账户基金*/
		public int czzgzhjjrc { get; set; }/*城镇职工基本医疗保险个人账户基金*/
		public decimal gywbzjj { get; set; }/*公务员医疗补助基金*/
		public int gywbzjjrc { get; set; }/*公务员医疗补助基金*/
		public decimal deylbzjj { get; set; }/*大额医疗费用补助基金*/
		public int deylbzjjrc { get; set; }/*大额医疗费用补助基金*/
		public decimal lxrybzjj { get; set; }/*离休人员医疗保障基金*/
		public int lxrybzjjrc { get; set; }/*离休人员医疗保障基金*/
		public decimal cjbzjj { get; set; }/*一至六级残疾军人医疗补助基金*/
		public int cjbzjjrc { get; set; }/*一至六级残疾军人医疗补助基金*/
		public decimal qybzyljj { get; set; }/*企业补充医疗保险基金*/
		public int qybzyljjrc { get; set; }/*企业补充医疗保险基金*/
		public decimal cxjmjbyljj { get; set; }/*城乡居民基本医疗保险基金*/
		public int cxjmjbyljjrc { get; set; }/*城乡居民基本医疗保险基金*/
		public decimal cxjmdbyljj { get; set; }/*城乡居民大病医疗保险基金*/
		public int cxjmdbyljjrc { get; set; }/*城乡居民大病医疗保险基金*/
		public decimal yljzjj { get; set; }/*医疗救助基金*/
		public int yljzjjrc { get; set; }/*医疗救助基金人次*/
		public decimal yfjj { get; set; }/*优抚基金*/
		public int yfjjrc { get; set; }/*优抚基金人次*/
		public decimal syjj { get; set; }/*生育基金*/
		public int syjjrc { get; set; }/*生育基金人次*/
		public decimal qtjj { get; set; }/*其他基金*/
		public int qtjjrc { get; set; }/*其他基金人次*/
		public decimal eyjj { get; set; }/*二乙基金*/
		public int eyjjrc { get; set; }/*二乙基金人次*/
									   /// <summary>
									   /// 各项基金总和
									   /// </summary>
		public decimal gxjjzh { get; set; }
	}

	public class RdrlsList
	{
		/// <summary>
		/// 对账时间
		/// </summary>
		public DateTime? czrq { get; set; }
		/// <summary>
		/// 对帐日 日期格式 8 非空
		/// </summary>
		public string daycollate { get; set; }
		/// <summary>
		/// 对帐日中心流水号数量 数字格式 B 非空
		/// </summary>
		public int daycount { get; set; }
		/// <summary>
		/// 当年帐户支付总额 数字格式 A 非空
		/// </summary>
		public decimal totalcuraccpay { get; set; }
		/// <summary>
		/// 历年帐户支付总额 数字格式 A 非空
		/// </summary>
		public decimal totalhisaccpay { get; set; }
		/// <summary>
		/// 现金自负总额 数字格式 A 非空
		/// </summary>
		public decimal totalcashpay { get; set; }
		/// <summary>
		/// 统筹支付总额 数字格式 A 非空
		/// </summary>
		public decimal totaltcpay { get; set; }
		/// <summary>
		/// 附加支付总额 数字格式 A 非空
		/// </summary>
		public decimal totaldffjpay { get; set; }
		/// <summary>
		/// 分类自负总额 数字格式 A 非空
		/// </summary>
		public decimal totalflzf { get; set; }
		/// <summary>
		/// 非医保结算范围费用总额总额
		/// </summary>
		public decimal totalfybjsfw { get; set; }

		/// <summary>
		/// 返回对账结果
		/// </summary>
		public string resultcollate { get; set; }
		/// <summary>
		/// 返回对账日
		/// </summary>
		public string outdaycollate { get; set; }
	}

	public class RdrNewList
	{
		/// <summary>
		/// 类型 mz：门诊数据 zy：住院数据 all：全部合计数据
		/// </summary>
		public string typetext { get; set; }
		/// <summary>
		/// 对帐日 日期格式 8 非空
		/// </summary>
		public string daycollate { get; set; }
		/// <summary>
		/// 对帐日中心流水号数量 数字格式 B 非空
		/// </summary>
		public int daycount { get; set; }
		/// <summary>
		/// 当年帐户支付总额 数字格式 A 非空
		/// </summary>
		public decimal totalcuraccpay { get; set; }
		/// <summary>
		/// 历年帐户支付总额 数字格式 A 非空
		/// </summary>
		public decimal totalhisaccpay { get; set; }
		/// <summary>
		/// 现金自负总额 数字格式 A 非空
		/// </summary>
		public decimal totalcashpay { get; set; }
		/// <summary>
		/// 统筹支付总额 数字格式 A 非空
		/// </summary>
		public decimal totaltcpay { get; set; }
		/// <summary>
		/// 附加支付总额 数字格式 A 非空
		/// </summary>
		public decimal totaldffjpay { get; set; }
		/// <summary>
		/// 分类自负总额 数字格式 A 非空
		/// </summary>
		public decimal totalflzf { get; set; }
		/// <summary>
		/// 非医保结算范围费用总额总额
		/// </summary>
		public decimal totalfybjsfw { get; set; }
	}
}

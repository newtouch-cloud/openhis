using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Infrastructure.EnumMR
{
	/// <summary>
	/// 在院标志
	/// </summary>
	public enum EnumZYBZ
	{
		/// <summary>
		/// 入院登记
		/// </summary>
		Xry = 0,
		/// <summary>
		/// 病区中
		/// </summary>
		Bqz = 1,
		/// <summary>
		/// 病区出院（出病区）（待结账）
		/// </summary>
		Djz = 2,
		/// <summary>
		/// 已出院（出院结算）
		/// </summary>
		Ycy = 3,
		/// <summary>
		/// 转区
		/// </summary>
		Zq = 7,

		/// <summary>
		/// 作废记录/取消入院
		/// </summary>
		Wry = 9,
	}
	public enum EnumMRSex
	{
		[Description("男")]
		M = 1,
		[Description("女")]
		F = 2
	}
	/// <summary>
	/// 病案状态
	/// </summary>
	public enum Enumbazt
	{
		[Description("待录入")]
		dlr = 1,
		[Description("已录入")]
		lrz = 2,
		//[Description("已归档")]
		//gd = 3
	}

	///// <summary>
	///// 病历状态 与EMR通用
	///// </summary>
	//public enum EnumRecordStu
	//{
	//    [Description("未提交")]
	//    wtj = 0,
	//    [Description("已提交")]
	//    ytj = 1,
	//    [Description("退回")]
	//    th = 2,
	//    [Description("已签收")]
	//    yqs = 3,
	//    //[Description("病案归档")]
	//    //bagd = 4
	//}
	/// <summary>
	/// 血型
	/// </summary>
	public enum EnumBloodType
	{
		[Description("A")]
		A = 1,
		[Description("B")]
		B = 2,
		[Description("O")]
		O = 3,
		[Description("AB")]
		AB = 4,
		[Description("不详")]
		BX = 5,
		[Description("未查")]
		WC = 6
	}

	/// <summary>
	/// 死亡患者尸检
	/// </summary>
	public enum EnumSwhzsj
	{
		[Description("是")]
		Y = 1,
		[Description("否")]
		N = 2
	}

	/// <summary>
	/// RH
	/// </summary>
	public enum EnumBloodTypeRH
	{
		[Description("阴")]
		yin = 1,
		[Description("阳")]
		yang = 2,
		[Description("不详")]
		BX = 3,
		[Description("未查")]
		WC = 4
	}

	//public enum EnumRytj
	//{
	//    [Description("急诊")]
	//    JZ = 1,
	//    [Description("门诊")]
	//    MZ = 2,
	//    [Description("其他医疗机构转入")]
	//    QTOrg = 3,
	//    [Description("其他")]
	//    QT = 9
	//}
	/// <summary>
	/// 关系
	/// </summary>
	public enum EnumGx
	{
		[Description("本人或户主")]
		br = 1,
		[Description("配偶")]
		fq = 2,
		[Description("子")]
		z = 3,
		[Description("女")]
		n,
		[Description("孙子、孙女或外孙子、外孙女")]
		sun,
		[Description("父母")]
		fm,
		[Description("祖父母或外祖父母")]
		zfm,
		[Description("兄、弟、姐、妹")]
		xdjm,
		[Description("其他")]
		qt
	}
	/// <summary>
	/// 是或否
	/// </summary>
	public enum EnumYorN
	{
		[Description("否")]
		N = 1,
		[Description("是")]
		Y = 2
	}


	/// <summary>
	/// 是或否
	/// </summary>
	public enum EnumNewYorN
	{
		[Description("是")]
		Y = 1,
		[Description("否")]
		N = 2
	}

	public enum EnumYork
	{
		[Description("急诊")]
		y = 1,
		[Description("门诊")]
		lcwqd = 2,
		[Description("其他医疗机构转入")]
		qkbm = 3,
		[Description("其他")]
		w = 4
	}
	/// <summary>
	/// 否或是
	/// </summary>
	public enum EnumNorY
	{
		[Description("否")]
		N = 1,
		[Description("是")]
		Y = 2
	}
	/// <summary>
	/// 有或无
	/// </summary>
	public enum EnumHorN
	{
		[Description("无")]
		Non = 1,
		[Description("有")]
		Has = 2
	}
	/// <summary>
	/// 输血反应
	/// 1.是  2.否  3.未输
	/// </summary>
	public enum EumSYFY
	{
		[Description("是")]
		y = 1,
		[Description("否")]
		n = 2,
		[Description("未输")]
		Non = 3
	}
	/// <summary>
	///入院病情 1	有 2	临床未确定 3	情况不明 4	无
	/// </summary>
	public enum EnumRybq
	{
		[Description("有")]
		y = 1,
		[Description("临床未确定")]
		lcwqd = 2,
		[Description("情况不明")]
		qkbm = 3,
		[Description("无")]
		w = 4
	}

	/// <summary>
	/// 出院情况
	/// 1	治愈 2	好转 3	未愈4	死亡5	其他
	/// </summary>
	public enum EnumCyqk
	{
		[Description("治愈")]
		zy = 1,
		[Description("好转")]
		hz = 2,
		[Description("未愈")]
		wy = 3,
		[Description("死亡")]
		sw = 4,
		[Description("其他")]
		qt = 5
	}
	/// <summary>
	/// 诊断类型-主从
	/// </summary>
	public enum EnumZdlxbs
	{
        [Description("主病")]
        zb = 3,
        [Description("主证")]
        zz = 4,
        [Description("主要诊断")]
		zy = 1,
		[Description("次要诊断")]
		cy = 2
        
    }


	/// <summary>
	/// 入院途径
	/// </summary>
	public enum EnumRYTJ
	{
		/// <summary>
		/// 门诊
		/// </summary>
		[Description("门诊")]
		mz = 1,
		/// <summary>
		/// 急诊
		/// </summary>
		[Description("急诊")]
		jz = 2,
		/// <summary>
		/// 其他医疗机构转入
		/// </summary>
		[Description("其他医疗机构转入")]
		qtyljg = 3,
		/// <summary>
		/// 120
		/// </summary>
		//[Description("120")]
		//jj120,
		/// <summary>
		/// 其他
		/// </summary>
		[Description("其他")]
		Else = 9,
	}
	///// <summary>
	///// 病历类型
	///// </summary>
	//public enum EnumBllx
	//{
	//    /// <summary>
	//    /// 住院病历
	//    /// </summary>
	//    [Description("住院病历")]
	//    zybl = 1,

	//    /// <summary>
	//    /// 病程记录
	//    /// </summary>
	//    [Description("病程记录")]
	//    bcjl = 2,

	//    /// <summary>
	//    /// 医疗文书
	//    /// </summary>
	//    [Description("医疗文书")]
	//    ylws = 3,

	//    /// <summary>
	//    /// 护理记录
	//    /// </summary>
	//    [Description("护理记录")]
	//    hljl = 4,

	//    /// <summary>
	//    /// 病案首页
	//    /// </summary>
	//    /// 
	//    [Description("病案首页")]
	//    basy = 5
	//}
	/// <summary>
	/// 诊断类型
	/// </summary>
	//public enum EnumZdlx2
	//{
	//    [Description("西医诊断")]
	//    WM = 1,
	//    [Description("中医诊断")]
	//    TCM = 2

	//}
	/// <summary>
	/// 病案质量
	/// </summary>
	public enum EnumBazl
	{
		[Description("甲")]
		I = 1,
		[Description("乙")]
		II,
		[Description("丙")]
		III
	}
	/// <summary>
	/// 离院方式
	/// </summary>
	public enum EnumLyfs
	{
		[Description("医嘱离院")]
		yzly = 1,
		/// <summary>
		/// 急诊
		/// </summary>
		[Description("医嘱转院")]
		yzzy,
		[Description("医嘱转社区卫生服务机构/乡镇卫生院")]
		yzzsq,
		[Description("非医嘱离院")]
		fyz,
		[Description("死亡")]
		sw,
		[Description("其他")]
		other = 9,
	}
	/// <summary>
	/// 病情分型
	/// </summary>
	public enum EnumBqfx
	{
		[Description("病危")]
		bw = 1,
		[Description("病重")]
		bz,
		[Description("疑难")]
		yn,
		[Description("抢救")]
		qj,
		[Description("一般")]
		yb
	}
	/// <summary>
	/// 临床路径变异原因
	/// </summary>
	public enum EnumBYYY
	{
		[Description("正变异")]
		zby = 1,
		[Description("负变异")]
		fby,
		[Description("系统原因")]
		xtyy
	}
	/// <summary>
	/// 检查情况
	/// </summary>
	public enum EnumJBQK
	{
		[Description("CT")]
		CT = 1,
		[Description("PETCT")]
		PETCT = 2,
		[Description("双源")]
		SY = 3,
		[Description("B超")]
		Bc = 4,
		[Description("X片")]
		Xp = 5,
		[Description("超声心动图")]
		CSXDT = 6,
		[Description("MRI")]
		MRI = 7
	}
	/// <summary>
	/// 检查情况
	/// </summary>
	public enum EnumJCQK
	{
		[Description("阳性")]
		Yang = 1,
		[Description("阴性")]
		Non = 2,
		[Description("未做")]
		Yin = 3,


	}

	/// <summary>
	///抗菌药物使用情况 
	/// </summary>
	public enum EnumKJYWSYQK
	{
		[Description("Ⅰ种")]
		first = 1,
		[Description("Ⅱ联")]
		second = 2,
		[Description("Ⅲ联")]
		third = 3,
		[Description("Ⅳ联")]
		forth = 4,
		[Description(">Ⅳ联")]
		more = 5
	}

	/// <summary>
	///输液反映 0未输 2无 3有
	/// </summary>
	public enum EnumSYFY
	{
		[Description("未输")]
		first = 0,
		[Description("无")]
		second = 1,
		[Description("有")]
		third = 2
	}

	/// <summary>
	///输血反映 0未输 2无 3有
	/// </summary>
	public enum EnumSXFY
	{
		[Description("是")]
		Y = 1,
		[Description("否")]
		N = 2,
		[Description("未输")]
		none = 3
	}

	/// <summary>
	///住院跌倒伤害程度 0未输 2无 3有
	/// </summary>
	public enum EnumZYDD_SHCD
	{
		[Description("一级")]
		first = 1,
		[Description("二级")]
		second = 2,
		[Description("三级")]
		third = 3,
		[Description("未造成伤害")]
		none = 4
	}

	/// <summary>
	///跌倒或坠床的原因
	/// </summary>
	public enum EnumZYDD_YY
	{
		[Description("健康原因")]
		jkyy = 1,
		[Description("治疗，药物，麻醉原因")]
		zlywmzyy = 2,
		[Description("环境因素")]
		hjys = 3,
		[Description("其他原因")]
		qtyy = 4
	}

	/// <summary>
	///压疮分期
	/// </summary>
	public enum EnumYCFQ
	{
		[Description("1期")]
		first = 1,
		[Description("2期")]
		second = 2,
		[Description("3期")]
		third = 3,
		[Description("4期")]
		forth = 4
	}

}

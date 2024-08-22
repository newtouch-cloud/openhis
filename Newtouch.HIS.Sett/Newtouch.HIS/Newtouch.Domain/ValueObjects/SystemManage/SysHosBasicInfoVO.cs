using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysHosBasicInfoVO
    {
        /// <summary>
        /// 卡类型 对应枚举EnumCardType
        /// </summary>
        public string cardtype { get; set; }
        public string CardTypeName { get; set; }
        /// <summary>
        /// 病人内码
        /// </summary>
        public int? patid { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh2 { get; set; }

        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 大病  add by caishanshan  暂加
        /// </summary>
        public string db { get; set; }

        /// <summary>
        /// 大病诊断  add by caishanshan  暂加
        /// </summary>
        public string dbzd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? brxzbh { get; set; }

        public string brxzmc { get; set; }

        public decimal wjje { get; set; }

        #region 病人基本信息
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public string csny { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public short? nl { get; set; }

        public string zjlx { get; set; }
        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string dh { get; set; }
        /// <summary>
        /// 地域
        /// </summary>
        public string dy { get; set; }

        public int? dybh { get; set; }

        /// <summary>
        /// 地域
        /// </summary>
        public string dymc { get; set; }
        /// <summary>
        /// 婚否
        /// </summary>
        public byte? hf { get; set; }

        public string wechat { get; set; }

        public string zjlxfs { get; set; }
        public string yddh { get; set; }
        public string email { get; set; }

        public string py { get; set; }

        public string xl { get; set; }

        public string gj2 { get; set; }

        public string mz2 { get; set; }

        public string jsr { get; set; }

        public string phone { get; set; }
        public string brly { get; set; }

        public string jjlldh { get; set; }
        public string jjllr { get; set; }
        public string jjllrgx { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string cs_yb { get; set; }
        public string hu_yb { get; set; }
        public string xian_yb { get; set; }
        public string dwyb { get; set; }
        /// <summary>
        /// 疾病史
        /// </summary>
        public string jbs { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string jg { get; set; }

        #endregion

        #region 住院基本信息
        /// <summary>
        /// 健康教练
        /// </summary>
        public string jkjl { get; set; }

        /// <summary>
        /// 健康教练名称
        /// </summary>
        public string jkjlmc { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        public string doctor { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string doctormc { get; set; }

        /// <summary>
        /// 床位
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 因为入院时不确定床位，所以先保存病区，以使本区护士可以操作此病人
        /// </summary>
        public string bq { get; set; }

        public string bqmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ryrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cyrq { get; set; }

        /// <summary>
        /// 入院途径 枚举EnumRYTJ
        /// </summary>
        public string rytj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zy { get; set; }

        public string mzmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? mz { get; set; }

        public string mzCode { get; set; }

        public string gjCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? gj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gjmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_xian { get; set; }
        public string cs_dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_dz { get; set; }
        public string jjlxr_sheng { get; set; }
        public string jjlxr_shi { get; set; }
        public string jjlxr_xian { get; set; }
        public string jjlxr_dz { get; set; }

        /// <summary>
        /// 枚举EnumHF
        /// </summary>
        public int? hy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? bje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrgx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrdh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrdz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrjtdh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrWebchat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxr2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrgx2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxryddh2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrjtdh2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrWebchat2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrEmail2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrdz2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gms { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        public string zdmc { get; set; }
        public string zdmc1 { get; set; }
        public string zdmc2 { get; set; }
        public string zdmc3 { get; set; }

        public string ryzd1 { get; set; }
        public string ryzd2 { get; set; }
        public string ryzd3 { get; set; }

        public string rybq { get; set; }

        public decimal zhye { get; set; }

        public string zybz { get; set; }

        public string dwmc { get; set; }
        public string dwdz { get; set; }
        public string dwdh { get; set; }
        public string bz { get; set; }
        #endregion

        /// <summary>
        /// 社保编号
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// 参保地编码
        /// </summary>
        public string cbdbm { get; set; }

        public string zxsbbf { get; set; }
        /// <summary>
        /// 新农合个人编码
        /// </summary>
        public string xnhgrbm { get; set; }

        public string xnhylzh { get; set; }
        /// <summary>
        /// 新农合返回的住院补偿序号
        /// </summary>
        public string inpId { get; set; }
		/// <summary>
		/// 参保类别
		/// </summary>
	    public string cblb { get; set; }
		/// <summary>
		/// 转出医院
		/// </summary>
		public string zcyy { get; set; }
        public string dwbh { get; set; }
        public string grbh { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string xzlx { get; set; }
        public string jzlx { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string ssczdm { get; set; }
        public string ssczmc { get; set; }
        public string syfwzh { get; set; }
        public string sylb { get; set; }
        public string sysslb { get; set; }
        public string wybz { get; set; }
        public string yzs { get; set; }
        public string tc { get; set; }
        public string tes { get; set; }
        public string zcbz { get; set; }
        public DateTime? syrq { get; set; }
        /// <summary>
        /// yktmodify 修改一卡通信息/ yktregister 新建一卡通信息
        /// /yktcardregister 卡信息新建
        /// </summary>
        public string yktbz { get; set; }
        /// <summary>
        /// 是否第三方健卡 Y:是 N:否
        /// </summary>
        public string isdsfly { get; set; }
    }
}

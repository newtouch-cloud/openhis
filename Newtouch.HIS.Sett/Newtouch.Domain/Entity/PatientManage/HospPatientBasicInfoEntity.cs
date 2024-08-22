using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("zy_brjbxx")]
    public class HospPatientBasicInfoEntity : IEntity<HospPatientBasicInfoEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int syxh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 枚举EnumZYBZ
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// （入院登记时初步选择的病区）因为入院时不确定床位，所以先保存病区，以使本区护士可以操作此病人
        /// </summary>
        public string bq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ryrq { get; set; }

        /// <summary>
        /// 入院途径 枚举EnumRYTJ
        /// </summary>
        public string rytj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string rqry { get; set; }

        /// <summary>
        /// 病人进入病区的日期，入区时还更新在院状态＝2   床位费等费用也是从入区日???开始计算
        /// </summary>
        public DateTime? rqrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gj { get; set; }

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

        /// <summary>
        /// 婚否 枚举 EnumHF
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
        public string cyjdry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? cyjdrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? cyrq { get; set; }

        /// <summary>
        /// xt_cyzd 的编号
        /// </summary>
        public string cyzd { get; set; }

        /// <summary>
        /// 出院病情？出院病区？
        /// </summary>
        public string cybq { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string doctor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 原来卡号可以为空，且不是唯一索引。   2006.09.15将此字段定义为唯一索引，且不可为空，需要按此字段检索病人基本信息
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CardTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jkjl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 入院病情
        /// </summary>
        public string rybq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? ryzd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjlx { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public short? nl{ get; set; }

        public string brly { get; set; }

        /// <summary>
        /// 年龄文本显示
        /// </summary>
        public string nlshow { get; set; }
		/// <summary>
		/// 转出医院
		/// </summary>
		public string zcyy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jjlxr_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjlxr_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjlxr_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjlxr_dz { get; set; }

        public string jzid { get; set; }
        public string jzlx { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string jzh { get; set; }
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
       // public string BedNo { get; set; }
    }
}

using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.PatientCenter
{
    [Tenant(DBEnum.SettDb)]
    [SugarTable("mz_gh", "OutpatientRegistEntity")]
    public partial class OutpatientRegistEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]

        public int ghnm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 0 -窗口  1-预约
        /// </summary>
        public string ghly { get; set; }

        /// <summary>
        /// 1：门诊 2：急诊 专家
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 按科室、挂号类型产生的排队号 from mz_jzxh
        /// </summary>
        public Int16? jzxh { get; set; }

        /// <summary>
        /// 挂号费 挂号项目 from xt_sfxm where dl = 挂号费 and zt = '1'
        /// </summary>
        public string ghlx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ghf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zlf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ckf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal gbf { get; set; }

        /// <summary>
        /// 0 待结 1 已结 2 已退   --2006-06-17 启用,作为便于识别结算情况的冗余字段      --2006.06.13 ??时不用此字??   好像可以通过结算内码＝0判断未结，>0判断已结，撤销结算内码>0判断已退？暂时不用此字段
        /// </summary>
        public string ghzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// 就诊标志（就诊状态） 枚举EnumOutpatientJzbz
        /// </summary>
        public string jzbz { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime? jzrq { get; set; }

        /// <summary>
        /// 0-普通挂号   1-空挂号   2-家床挂号   3-自费转医保挂号         ---------------------------------------------------   0 待挂号   1 挂号   2 退号 (也可通过判断此记录的jsnm是否被其他记录作为cxjsnm，得知是否退号，但是这样效率高)
        /// </summary>
        public string ghxz { get; set; }

        /// <summary>
        /// 1.复诊 0.首诊
        /// </summary>
        public byte? fzbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dbxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gsrdh { get; set; }

        /// <summary>
        /// 对应挂号排班表中专病项目 from mz_ghpb
        /// </summary>
        public int? ghzbbh { get; set; }

        /// <summary>
        /// 对应挂号排班表中专病项目 from mz_ghpb
        /// </summary>
        public string ghzb { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool zzjm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdicd10 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mzh { get; set; }

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
        public DateTime? csny { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CardTypeName { get; set; }

        public string brly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? xlbrbz { get; set; }

        public DateTime? ghrq { get; set; }
        public string lxr { get; set; }
        public string lxrgx { get; set; }
        public string lxrdh { get; set; }

        /// <summary>
        /// 医保结算号（前置）
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// 就诊原因
        /// </summary>
        public string jzyy { get; set; }
        /// <summary>
        /// 推荐人
        /// </summary>
        public string tjr { get; set; }

        /// <summary>
        /// 门诊预约挂号ID 对应Newtouch_CIS.dbo.mz_yygh.Id
        /// </summary>
        public string yyghId { get; set; }

        /// <summary>
        /// 年龄文本显示
        /// </summary>
        public string nlshow { get; set; }

        /// <summary>
        /// 门诊补偿序号 贵州新农合用
        /// </summary>
        public string outpId { get; set; }
        /// <summary>
        /// 挂号来源辅助标识 区分同一来源的不同标记患者
        /// 体检用
        /// </summary>
        public string ghlybz { get; set; }
        /// <summary>
        /// 挂号排班Id
        /// </summary>
        public decimal? ScheduId { get; set; }
        /// <summary>
        /// 就诊ID
        /// </summary>
        public string jzid { get; set; }
        public string jzpzlx { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }

        public string ecToken { get; set; }
    }
}

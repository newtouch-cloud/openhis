using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊项目
    /// </summary>
    [Table("mz_xm")]
    public class OutpatientItemEntity : IEntity<OutpatientItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int xmnm { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// 同本次挂号的病人内码－提高性能之冗余字段
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 同本次挂号的病人性质－提高性能之冗余字段
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 1：门诊 2：急诊
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 计价数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0 待结 1 已结 2 已退
        /// （不管是否关联处方，其都要赋值）
        /// </summary>
        public string xmzt { get; set; }

        /// <summary>
        /// 0 收费处 1 医生站
        /// （不管是否关联处方，其都要赋值）
        /// </summary>
        public string xmly { get; set; }

        /// <summary>
        /// 0 待实施 1 已实施（已结束实施/不再实施） 2无需实施 9实施过程中
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ssry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ssrq { get; set; }

        /// <summary>
        /// 结算内码（多次关联结算、退结算 所以赋值没啥意义）
        /// </summary>
        public int? jsnm { get; set; }

        /// <summary>
        /// 结算日期（多次关联结算、退结算 所以赋值没啥意义）
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// 减免比例
        /// </summary>
        private decimal? _jmbl { get; set; }
        /// <summary>
        /// 减免比例
        /// </summary>
        public decimal? jmbl
        {
            get
            {
                return _jmbl ?? 0;
            }
            set
            {
                _jmbl = value;
            }
        }

        /// <summary>
        /// 减免金额
        /// </summary>
        private decimal? _jmje { get; set; }
        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal? jmje
        {
            get
            {
                return _jmje ?? 0;
            }
            set
            {
                _jmje = value;
            }
        }

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
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        public string ysmc { get; set; }
        public string ksmc { get; set; }

        public string jzjhmxId { get; set; }
        public string kflb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? ttbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 总治疗量
        /// </summary>
        public int? zzll { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 处方内码
        /// 20180326项目也开始关联处方主表
        /// </summary>
        public int? cfnm { get; set; }

        /// <summary>
        /// 收费日期 20180329加
        /// </summary>
        public DateTime? sfrq { get; set; }
        /// <summary>
        /// 备注 20180408加
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 单次治疗量（在由项目生成计划的时候需要）
        /// </summary>
        public int? dczll { get; set; }

        /// <summary>
        /// 执行次数（在由项目生成计划的时候需要）
        /// </summary>
        public int? zxcs { get; set; }
        /// <summary>
        /// 转自费标志
        /// </summary>
        public int? zzfbz { get; set; }

        public string ztId { get; set; }
        public string ztmc { get; set; }
        public int? ztsl { get; set; }
    }
}

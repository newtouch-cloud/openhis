using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class PatMedicalRecordVO : OutpMedicalRecordVO
    {
        public string patid { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }
    }

    // </summary> 
    public class OutpMedicalRecordVO
    {
        public string blId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        public string mzh { get; set; }

        /// <summary>
        /// 冗余挂号信息  枚举 1 普通门诊 2 急诊 3专家
        /// </summary>
        public int? mjzbz { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }
        #region 一般检查
        /// <summary>
        /// 体重
        /// </summary>
        public decimal? tizhong { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        public decimal? tiwen { get; set; }

        /// <summary>
        /// 脉搏
        /// </summary>
        public decimal? maibo { get; set; }

        /// <summary>
        /// 血糖测量方式 餐后 空腹 随机 
        /// EnumXtclfs
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// 血糖
        /// </summary>
        public decimal? xuetang { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// 收缩压
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// 舒张压
        /// </summary>
        public decimal? shuzhangya { get; set; }
        /// <summary>
        /// 呼吸
        /// </summary>
        public decimal? huxi { get; set; }
        /// <summary>
        /// 婚姻状况 EnumMarriageStu
        /// </summary>
        public string hy { get; set; }
        #endregion

        /// <summary>
        /// 主诉
        /// </summary>
        public string zs { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime? fbsj { get; set; }

        /// <summary>
        /// 现病史
        /// </summary>
        public string xbs { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public string jws { get; set; }

        /// <summary>
        /// 查体
        /// </summary>
        public string ct { get; set; }
        /// <summary>
        /// 处理
        /// </summary>
        public string clfa { get; set; }
        /// <summary>
        /// 辅助检查
        /// </summary>
        public string fzjc { get; set; }
        /// <summary>
        /// 月经史
        /// </summary>
        public string yjs { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string gms { get; set; }
        public List<WMDiagnosisVO> xyzd { get; set; }
        public List<TCMDiagnosisVO> zyzd { get; set; }
    }

    public class WMDiagnosisVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string zdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int zdlx { get; set; }
        /// <summary>
        /// 疑似标志
        /// </summary>
        public bool? ysbz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// 西医诊断备注
        /// </summary>
        public string zdbz { get; set; }
    }
    /// <summary>
    /// 中医诊断
    /// </summary>
    public class TCMDiagnosisVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string zdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int zdlx { get; set; }
        /// <summary>
        /// 疑似标志
        /// </summary>
        public bool? ysbz { get; set; }

        /// <summary>
        /// 症候编码
        /// </summary>
        public string zhCode { get; set; }

        /// <summary>
        /// 症候名称
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// 中医诊断备注
        /// </summary>
        public string zdbz { get; set; }
    }
}

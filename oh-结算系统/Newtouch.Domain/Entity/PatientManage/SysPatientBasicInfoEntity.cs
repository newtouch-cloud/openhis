using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_brjbxx")]
    public class SysPatientBasicInfoEntity : IEntity<SysPatientBasicInfoEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int patid { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 本院磁卡号（包括医保离休病人），或者医保卡号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string zjh { get; set; }

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
        /// 邮编
        /// </summary>
        public string cs_yb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hu_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_shi { get; set; }

        /// <summary>
        /// 户籍县
        /// </summary>
        public string hu_xian { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string hu_dz { get; set; }
        public string hu_yb { get; set; }

        /// <summary>
        /// 现住址省
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
        public string xian_yb { get; set; }
        /// <summary>
        /// 紧急联系人省
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

        /// <summary>
        /// 紧急联系人关系
        /// </summary>
        public string jjllrgx { get; set; }
        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string jjllr{ get; set; }
        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string jjlldh { get; set; }
        /// <summary>
        /// 是否异地
        /// </summary>
        public int? dybh { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>
        public string dwdz { get; set; }
        public string dwdh { get; set; }
        /// <summary>
        /// 单位邮编
        /// </summary>
        public string dwyb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xl { get; set; }

        /// <summary>
        /// xt_gj.gj
        /// </summary>
        public string gj { get; set; }

        /// <summary>
        /// 枚举EnumHF
        /// </summary>
        public byte? hf { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string mz { get; set; }
        public string jg { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string zy { get; set; }

        /// <summary>
        /// 最佳联系方式
        /// </summary>
        public string zjlxfs { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string dh { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wechat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 病人来源
        /// </summary>
        public string brly { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// 疾病史
        /// </summary>
        public string jbs { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }


        /// <summary>
        /// 0 无效 1 有效
        /// </summary>
        public string zt { get; set; }

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

    }
}

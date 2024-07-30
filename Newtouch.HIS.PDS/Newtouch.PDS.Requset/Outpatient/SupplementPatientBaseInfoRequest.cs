using System;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// 补充患者基本信息
    /// </summary>
    public class SupplementPatientBaseInfoRequest : RequestBase
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 病人最近的性质xt_brxz.brxz
        /// </summary>
        public string brxz { get; set; }

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
        /// 
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dh { get; set; }

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
        /// 
        /// </summary>
        public string qx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yb { get; set; }

        /// <summary>
        /// from xtdy
        /// </summary>
        public int? dybh { get; set; }

        /// <summary>
        /// 单位代码
        /// </summary>
        public string dwdm { get; set; }

        /// <summary>
        /// 区县代码
        /// </summary>
        public string qxdm { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// 离休凭证
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// 医疗项目
        /// </summary>
        public string ylxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? pzksrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? pzzzrq { get; set; }

        /// <summary>
        /// 凭证诊断
        /// </summary>
        public string pzzd { get; set; }

        /// <summary>
        /// --2006-07-9 取消???帐户规则，家床结算，在变量传递时取负数，不修改表      --2006.07.09 不允许自动注销帐户，为了不和家床撤床失败混淆   负帐户只有一个含义，就是撤床失败      单位特约帐号、个人特约帐号 from xtzh   当帐户为负数时，表示此帐户已被系统自动注销(有效期已过、或家床交易成功，但是撤床失败。)
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 0 无效 1 有效
        /// </summary>
        public string zt { get; set; }

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

        /// <summary>
        /// 职业
        /// </summary>
        public string zy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ywxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yddh { get; set; }

        /// <summary>
        /// 0.其他 1.住宅 2.公司
        /// </summary>
        public byte? dzxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gzdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gzdz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjllr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjlldh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gzdwdh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? bxgs { get; set; }

        /// <summary>
        /// 医保卡返回的标志位，非医保卡为空字符
        /// </summary>
        public string zhbz { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public string zjlxfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjlx { get; set; }

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

        public string Jsr { get; set; }
        public string brly { get; set; }

        public string jjllrgx { get; set; }

        /// <summary>
        /// 个人社保编号
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// 参保地编码（181019医保参保地编码）
        /// </summary>
        public string cbdbm { get; set; }

        /// <summary>
        /// 执行社会保险办法
        /// </summary>
        public string zxsbbf { get; set; }
        /// <summary>
        /// 新农合个人编码
        /// </summary>
        public string xnhgrbm { get; set; }
        /// <summary>
        /// 新农合医疗证号
        /// </summary>
        public string xnhylzh { get; set; }
    }
}
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// 系统岗位
    ///</summary>
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_brxz", "病人信息")]
    public partial class SysPatientNatureEntity : ISysEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brxzbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 0 所有卡都可选的类型   1 医保卡可选的类型
        /// </summary>
        public string brxzlb { get; set; }

        /// <summary>
        /// 0 不交易 1 普通交易 2 大病交易 3 家床交易
        /// </summary>
        public string ybjylx { get; set; }

        /// <summary>
        /// 0：普通  1：离休  2：伤残   这里的设置只影响到新增病人时的“病人性质”过滤，所有交易费用的范围确定使用“交易费用范围”来控制      --此字段含义存在的问题！！！！！！   当“伤残”病人时，无法选择自费属性
        /// </summary>
        public string ybtsdy { get; set; }

        /// <summary>
        /// 医保交易的费用范围   0 可记帐部分参与计算（普通医保）   1 可记帐＋分类自负进行交易（伤残）   2 可记帐＋分类自负＋自理进行交易（暂无）      ----cms作废字段
        /// </summary>
        public string jyfyfw { get; set; }

        /// <summary>
        /// 0 不使用凭证   1 使用凭证
        /// </summary>
        public string pzbz { get; set; }

        /// <summary>
        /// 0 通用 1 仅门诊 2 仅住院    3 系统（门诊住院都不可用）例如：家床
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// 0 不享受减免（诊疗费）   1 享受减免
        /// </summary>
        public string sqjmbz { get; set; }

        /// <summary>
        /// from xt_zhxz   如果有设置：则在结算时，优先扣除设置之账户   （例如：帮困病人此字段设置为4，当结算时，先扣除帮困账户）
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// 0 无效 1 有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 0：城保 1：个体医保 2：小城镇 3：小时工   A:民政医疗帮困   B：老年遗嘱 C:五保老人 D：重残人员   E:中小学生和婴幼儿  F： 其他居民   G：医疗互助帮困对象   
        /// </summary>
        public string syybbf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxxpz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fpdymb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fpdyghmb { get; set; }

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
        public string bz { get; set; }

        /// <summary>
        /// 关联主键brxzbh
        /// </summary>
        public int? ParentId { get; set; }
        public string insutype { get; set; }
    }
}

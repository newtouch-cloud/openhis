using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class BlbasyVO
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 电子病历ID
        /// </summary>
        /// <returns></returns>
        public decimal? dzbl_id { get; set; }
        /// <summary>
        /// 住院登记ID
        /// </summary>
        /// <returns></returns>
        public decimal? zydj_id { get; set; }
        /// <summary>
        /// 医保登记ID
        /// </summary>
        /// <returns></returns>
        public decimal? ybdj_id { get; set; }
        /// <summary>
        /// yebh
        /// </summary>
        /// <returns></returns>
        public int? yebh { get; set; }
        /// <summary>
        /// 登记序号
        /// </summary>
        /// <returns></returns>
        public int? djxh { get; set; }
        /// <summary>
        /// 病历凭证号码
        /// </summary>
        /// <returns></returns>
        public decimal pzhm_bl { get; set; }
        /// <summary>
        /// 模板编号
        /// </summary>
        /// <returns></returns>
        public string mbbh { get; set; }
        /// <summary>
        /// 病历系统储放目录
        /// </summary>
        /// <returns></returns>
        public string blxtml { get; set; }
        /// <summary>
        /// 病历系统名称编号_原件
        /// </summary>
        /// <returns></returns>
        public string blxtmc_yj { get; set; }
        /// <summary>
        /// 病历系统名称编号_痕迹
        /// </summary>
        /// <returns></returns>
        public string blxtmc_hj { get; set; }
        /// <summary>
        /// 病历系统名称编号_xml
        /// </summary>
        /// <returns></returns>
        public string blxtmc_xml { get; set; }
        /// <summary>
        /// ksmc
        /// </summary>
        /// <returns></returns>
        public string ksmc { get; set; }
        /// <summary>
        /// 科室代码
        /// </summary>
        /// <returns></returns>
        public string ksdm { get; set; }
        /// <summary>
        /// zgysmc
        /// </summary>
        /// <returns></returns>
        public string zgysmc { get; set; }
        /// <summary>
        /// 主管理医生
        /// </summary>
        /// <returns></returns>
        public string czydm_zgys { get; set; }
        /// <summary>
        /// zzysmc
        /// </summary>
        /// <returns></returns>
        public string zzysmc { get; set; }
        /// <summary>
        /// 主治医生(录入人)
        /// </summary>
        /// <returns></returns>
        public string czydm_zzys { get; set; }
        /// <summary>
        /// 病历日期
        /// </summary>
        /// <returns></returns>
        public DateTime? blrq { get; set; }
        /// <summary>
        /// 病历系统日期
        /// </summary>
        /// <returns></returns>
        public DateTime? blxtrq { get; set; }
        /// <summary>
        /// shrmc
        /// </summary>
        /// <returns></returns>
        public string shrmc { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        /// <returns></returns>
        public string czydm_shr { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        /// <returns></returns>
        public DateTime? shrq { get; set; }
        /// <summary>
        /// kzrmc
        /// </summary>
        /// <returns></returns>
        public string kzrmc { get; set; }
        /// <summary>
        /// 科主任审核
        /// </summary>
        /// <returns></returns>
        public string czydm_kzr { get; set; }
        /// <summary>
        /// 科主任审核日期
        /// </summary>
        /// <returns></returns>
        public DateTime? shrq_kzr { get; set; }
        /// <summary>
        /// 作废日期
        /// </summary>
        /// <returns></returns>
        public DateTime? zfrq { get; set; }
        /// <summary>
        /// zfrmc
        /// </summary>
        /// <returns></returns>
        public string zfrmc { get; set; }
        /// <summary>
        /// 作废人
        /// </summary>
        /// <returns></returns>
        public string czydm_zfr { get; set; }
        /// <summary>
        /// czrmc
        /// </summary>
        /// <returns></returns>
        public string czrmc { get; set; }
        /// <summary>
        /// 当前记录操作人(只有获得操作此条记录的人,才允许存盘、修改操作,否则为可读权限)
        /// </summary>
        /// <returns></returns>
        public string czydm_czr { get; set; }
        /// <summary>
        /// sxysmc
        /// </summary>
        /// <returns></returns>
        public string sxysmc { get; set; }
        /// <summary>
        /// 实习医生(由于实习医生、进修医生都有老师带)
        /// </summary>
        /// <returns></returns>
        public string czydm_sxys { get; set; }
        /// <summary>
        /// sxysxm
        /// </summary>
        /// <returns></returns>
        public string sxysxm { get; set; }
        /// <summary>
        /// 实习医生(打印就用这个名字)
        /// </summary>
        /// <returns></returns>
        public string czyxm_sxys { get; set; }
        /// <summary>
        /// blbxzt
        /// </summary>
        /// <returns></returns>
        public int? blbxzt { get; set; }
        /// <summary>
        /// blbczt
        /// </summary>
        /// <returns></returns>
        public bool? blbczt { get; set; }
        /// <summary>
        /// qmrq
        /// </summary>
        /// <returns></returns>
        public DateTime? qmrq { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        public Nullable<int> IsLock { get; set; }
    }
}

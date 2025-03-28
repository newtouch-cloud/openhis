using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.ReportTemplateVO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.PatientManage
{
    public interface IG_yb_daysrg_trt_list_bRepo : IRepositoryBase<G_yb_daysrg_trt_list_bEntity>
    {
        /// <summary>
        /// 医保日间手术治疗目录表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_daysrg_trt_list_bVO> G_yb_daysrg_trt_list_b(string tbname);
        /// <summary>
        /// 诊断目录表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_diag_list_bVO> G_yb_diag_list_b(string tbname);
        /// <summary>
        /// 医保按病种结算目录表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_dise_setl_list_bVO> G_yb_dise_setl_list_b(string tbname);
        /// <summary>
        /// 耗材信息表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_mcs_info_bVO> G_yb_mcs_info_b(string tbname);
        /// <summary>
        /// 手术操作分类和代码表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_oprn_std_bVO> G_yb_oprn_std_b(string tbname);
        /// <summary>
        /// 医保门慢门特病种目录表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_opsp_dise_list_bVO> G_yb_opsp_dise_list_b(string tbname);
        /// <summary>
        /// 自制剂信息表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_selfprep_info_bVO> G_yb_selfprep_info_b(string tbname);
        /// <summary>
        /// 中医诊断表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_tcm_diag_bVO> G_yb_tcm_diag_b(string tbname);
        /// <summary>
        /// 中草药信息表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_tcmherb_info_bVO> G_yb_tcmherb_info_b(string tbname);
        /// <summary>
        /// 中医证候分类表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_tcmsymp_type_bVO> G_yb_tcmsymp_type_b(string tbname);
        /// <summary>
        /// 肿瘤形态学表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_tmor_mpy_bVO> G_yb_tmor_mpy_b(string tbname);
        /// <summary>
        /// 医疗服务项目表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_trt_serv_bVO> G_yb_trt_serv_b(string tbname);
        /// <summary>
        /// 西药中成药信息表
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<G_yb_wm_tcmpat_info_bVO> G_yb_wm_tcmpat_info_b(Pagination pagination, string tbname);

        /// <summary>
        /// 目录查询公共方法
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="mlbh"></param>
        /// <returns></returns>
        IList<G_yb_mluCommon_Info> Get_G_yb_mluCommon_Info(Pagination pagination, string mlbh,string key);

        string Header(string tbname);

        void DataListSQl(string tbname, string path);
        List<MedinsDaySetlResult> DataListSQl_V2(string tbname, string path);

    }
}

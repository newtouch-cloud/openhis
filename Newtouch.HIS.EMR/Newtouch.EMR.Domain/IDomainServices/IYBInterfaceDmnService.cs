using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IYBInterfaceDmnService
    {
        /// <summary>
        /// 入院病历医保接口
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        YB_7100 GetRyblInfo(string orgId, string dzblId);
        /// <summary>
        /// 入院病历诊断
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        IList<YB_7110> GetRyblZdInfo(string orgId, string dzblId);
        /// <summary>
        /// 病程记录
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        YB_7200 GetBcjlInfo(string orgId, string dzblId);
        /// <summary>
        /// 出院小结
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        YB_7500 GetCyxjInfo(string orgId, string dzblId);
        /// <summary>
        /// 出院小结
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <returns></returns>
        IList<YB_7510> GetCyxjZdInfo(string orgId, string dzblId);
        /// <summary>
        /// 病案首页诊断
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<YB_7610> GetBasyZdInfo(string orgId, string zyh);
        /// <summary>
        /// 入院诊断保存
        /// </summary>
        /// <param name="zdlist"></param>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <param name="zyh"></param>
        string YBInhosDiagInfoSave(List<DiagnoseDocDTO> zdlist, string orgId, string dzblId, string zyh);
        /// <summary>
        /// 出院诊断保存
        /// </summary>
        /// <param name="zdlist"></param>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <param name="zyh"></param>
        string YBOuthosDiagInfoSave(List<DiagnoseDocDTO> zdlist, string orgId, string dzblId, string zyh);

    }
}

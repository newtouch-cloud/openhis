using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain.DTO;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.IDomainServices
{
    public interface IOPApplyDmnService
    {
        /// <summary>
        /// 获取在院患者
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="zyh"></param>
        /// <param name="ysgh"></param>
        /// <param name="OrgId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<PatListVO> GetPatList(string keyword, string zyh,string bq, string ysgh, string OrgId, int type);
        /// <summary>
        /// 手术申请列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ORApplyInfoEntity> GetOpApplybyzyh(string zyh,string sqlx, string orgId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applyNo"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OpApplySubmitDTO GetOpApplybyApplyNo(string applyNo, string orgId);
        /// <summary>
        /// 手术申请提交
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="keyValue"></param>
        string SubmitApplyForm(OpApplyDTO dto, string keyValue,string datas);
        string SubmitApplyForm(OpApplySubmitDTO dto, string keyValue);
    }
}

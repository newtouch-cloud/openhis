using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.IDomainServices
{

    public interface ICommonDmnService
    {
        /// <summary>
        /// 获取病区信息
        /// </summary>
        IList<SysBqListVO> GetBqlist(string bqcode, string orgId,string staffgh);
        /// <summary>
        /// 手术字典列表
        /// </summary>
        /// <param name="ssdm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OROperationEntity> GetOplist(string ssdm, string orgId);
        IList<OperationDicVO> GetOperations(string ssdm, string orgId);
        IList<OperationDicVO> GetOperations(string[] ssdm, string orgId);
        /// <summary>
        /// 院内职工
        /// </summary>
        /// <param name="rygh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<StaffListVO> GetStafflist(string rygh, string orgId);
        //IList<ORStaffEntity> GetStafflist2(string Code, string orgId);
        /// <summary>
        /// 麻醉列表
        /// </summary>
        /// <param name="AnesCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ORAnesthesiaEntity> GetAneslistlist(string AnesCode, string orgId);
        /// <summary>
        /// 手术室
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ORRoomEntity> GetRoomlist(string Code, string orgId);
        /// <summary>
        /// 切口等级
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ORNotchGradeEntity> GetNotchGradelist(string Code, string orgId);
        /// <summary>
        /// 系统错误代码
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        IList<SysFailedCodeMessageMappVO> FailMessage(string orgId, string appId);

    }
}

using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.DTO;
using Newtouch.MR.ManageSystem.Domain.DTO.OutputDto;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.MR.ManageSystem.Domain.Entity;
#pragma warning disable CS0105 // “Newtouch.MR.ManageSystem.Domain.ValueObjects”的 using 指令以前在此命名空间中出现过
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
#pragma warning restore CS0105 // “Newtouch.MR.ManageSystem.Domain.ValueObjects”的 using 指令以前在此命名空间中出现过
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.ValueObjects;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface ICommonDmnService
    {
        IList<SysZdListDto> ZdList(string orgId, string keyword);
        IList<SysOpListDto> OpList(string orgId, string keyword, bool type = true);
        IList<SysAnesListDto> AnesList(string orgId, string keyword);
        /// <summary>
        /// 愈合等级
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> NotchGradeList(string orgId, string keyword);
        /// <summary>
        /// 病案通用字典
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="Rlue"></param>
        /// <returns></returns>
        IList<SysDicDto> DicCommonList(string orgId, string keyword, string Rlue, string code = null);
        /// <summary>
        /// 系统病人性质
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysBrxzVO> BrxzList(string orgId, string keyword);
        /// <summary>
        /// 系统-国籍
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> DicNationalityList(string orgId, string keyword);
        /// <summary>
        /// 系统-民族
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDicDto> DicNationsList(string orgId, string keyword);
        /// <summary>
        /// 获取病历类型对应表
        /// </summary>
        /// <param name="bllxId"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        string GetBllxTB(string bllx);
        /// <summary>
        /// 人员（岗位）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null);
        /// <summary>
        /// 获取his科室列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysDicDto> GetDeptList(string orgId, string code);

        /// <summary>
        /// 获取his收费大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<HisSfdlVO> GetHisSfdl(string orgId, string keyword);
    }
}

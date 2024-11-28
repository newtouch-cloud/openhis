
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.DTO.InputDto.MRHomePage;
using Newtouch.EMR.Domain.DTO.OutputDto.MRHomePage;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using System.Collections.Generic;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface ICommonDmnService
    {
        /// <summary>
        /// 根据病历类型Id获取病历大类名称标识
        /// </summary>
        /// <param name="bllxId"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        string GetBllxmcFlag(string bllxId, string OrgId);
        /// <summary>
        /// 获取病历类型对应表
        /// </summary>
        /// <param name="bllxId"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        string GetBllxTB(int bllx);
        /// <summary>
        /// 通过bllx获取病历大类相关信息
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="Code"></param>
        /// <param name="bllx">EnumBllx</param>
        /// <returns></returns>
        IList<MedRecordTypeVO> GetSysItemDicBybllx(string Code, int bllx);
        /// <summary>
        /// 获取模板对应岗位信息
        /// </summary>
        /// <returns></returns>
        IList<BlmbqxkzEntity> GetblmbDuty(string blmbId, OperatorModel user);
        /// <summary>
        /// 判断是否为模板管理员
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool GetIsblManager(OperatorModel user);
        /// <summary>
        /// 诊断
        /// </summary>  
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<DiagnoseDTO> GetList(string orgId);
        IList<DiagnoseDTO> GetList(Pagination pagination,string orgId, string keyword,int? zdlx, string ybnhlx = "");

        IList<SysZdListDto> ZdList(string orgId, string keyword, string zdlx = "", string ybnhlx = "");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="type">是否优化查询 1 是 0 否</param>
        /// <returns></returns>
        IList<SysOpListDto> OpList(string orgId, string keyword, bool type = true);
        /// <summary>
        /// 分页手术列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysOpListDto> OpListPage(Pagination pagination, string orgId, string keyword);
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
        ///// <summary>
        ///// 系统病人性质
        ///// </summary>
        ///// <param name="orgId"></param>
        ///// <param name="keyword"></param>
        ///// <returns></returns>
        //IList<SysBrxzVO> BrxzList(string orgId, string keyword);
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
        /// 获取病区列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysDicDto> GetWardList(string orgId, string code);
        IList<MedRecordTypeVO> GetSysItemDic(string OrgId, string Code, string bllxId, string DetailCode = null);
        /// <summary>
        /// 获取病历大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysDicMarkDto> GetBllxList(string orgId, string code, string mzbz = null);
        IList<SysDicDto> GetDutyList(string orgId, string code);
        IList<SysDicMarkDto> GetDutyListRelbllx(string orgId, string bllx, string dutycode);
        /// <summary>
        /// 获取病历类型
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bllx">Id or bllx</param>
        /// <param name="keyword"></param>
        /// <param name="type">1 root 2 子目录</param>
        /// <returns></returns>
        IList<BaseBllxVO> GetBllxListDetail(string orgId,string bllx=null,string keyword=null,string type=null);
        /// <summary>
        /// 获取员工信息（签名）
        /// </summary>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<StaffVO> StaffInfo(string user, string orgId);

        /// <summary>
        /// 病区
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<xt_bq> GetInpatientArea(string orgId, string keyword);
        /// <summary>
        /// 中医症候
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDiagZyzh> GetZyzhList(string orgId, string keyword);

        #region 医嘱打印
        /// <summary>
        /// 长期医嘱打印
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="isSign"></param>
        /// <returns></returns>
        IList<CqyzPrintVo> GetPrintCqyzData(string zyh, string orgId, string isSign);
        IList<LsyzPrintVo> GetPrintLsyzData(string zyh, string orgId, string isSign);
        PatidInfoVo GetInpatInfo(string zyh, string orgId);
        #endregion

        #region 护理单打印
        IList<HljlDataPrintVo> GetPrintHljlData(string zyh, string orgId, string hljb);

        #endregion
    }
}

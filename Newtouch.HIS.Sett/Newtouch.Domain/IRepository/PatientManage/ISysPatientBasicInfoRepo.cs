using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysPatientBasicInfoRepo : IRepositoryBase<SysPatientBasicInfoEntity>
    {
        void SubmitForm(SysPatientBasicInfoEntity sysPatBasicInfoEntity, string keyValue);

        /// <summary>
        /// （病历号由系统自动生成）获取最新病历号
        /// </summary>
        /// <returns></returns>
        string Getblh(string orgId);

        /// <summary>
        /// 根据证件号，姓名获取病人基本信息
        /// </summary>
        /// <param name="zjh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetBasicInfoByZjh(string zjh, string orgId);

        /// <summary>
        /// 根据patid获取病人基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        SysPatientBasicInfoEntity GetInfoByPatid(string patid, string orgId);

        /// <summary>
        /// 根据姓名获取病人基本信息
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<SysPatientBasicInfoEntity> GetBasicInfoByxm(string xm, string orgId);

        /// <summary>
        /// 根据病历号获取信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="blh"></param>
        /// <returns></returns>
        SysPatientBasicInfoEntity GetInfoByblh(string orgId, string blh);

        /// <summary>
        /// 根据病历号查询
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        IList<PatOnlyBlhSearchVO> GetPatOnlyBlhSearchList(Pagination pagination, string orgId, string blh, string xm);

        /// <summary>
        /// 更新紧急联系人相关信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <param name="lxr"></param>
        /// <param name="lxrgx"></param>
        /// <param name="lxrdh"></param>
        /// <param name="userCode"></param>
        void UpdatelxrInfo(string patid, string orgId, string lxr, string lxrgx, string lxrdh, string userCode);

        void Updateybxx(string kh, string zjh, string cbdbm, string cblb, string grbh, string xzlx, string orgId, string userCode);
        void UpdateZt(string patid, string orgId, string zt, string userCode);

        #region 一卡通日志
        IList<SysPatientBasiInfoLOGVO> GetModifyLog(Pagination pagination, string orgId, string xm);
        IList<SysPatientBasiInfoLOGVO> GetDetailsData(string Id, string orgId);
        #endregion
        #region 一卡通管理
        IList<SysCardEntity> GetCardNoInfo(string patId, string orgId, string cartype);
        void SubmitCard(SysHosBasicInfoVO vo, string orgId);
        void CardVoids(string CardId, string orgId, string upcode);
        #endregion
    }
}

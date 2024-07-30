
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 门诊挂号
    /// </summary>
    public interface IOutpatientRegistRepo : IRepositoryBase<OutpatientRegistEntity>
    {
        OutpatientRegistEntity SelectOutPatientReg(int ghnm, string orgId);
        OutpatientRegistEntity SelectOutPatientReg(string mzh, string orgId);

        /// <summary>
        /// 根据mzh获取挂号实体
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutpatientRegistEntity SelectData(string mzh, string orgId);
        /// <summary>
        /// 根据门诊号获取患者信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutpatAccInfoDto GetBasicInfoPatInfoInRegister(string mzh, string orgId);

        List<OutpatientRegistEntity> SelectOutPatientRegList(List<int> ghnmList, string orgId);

        /// <summary>
        /// 根据卡号和组织机构获取挂号信息
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutpatientRegistEntity SelectCFZ(string kh, string orgId);

        /// <summary>
        /// 获取当前卡号当天挂号信息
        /// add HLF
        /// </summary>
        /// <param name="kh">卡号</param>
        /// <returns></returns>
        List<OutpatientRegistEntity> GetOutPatRegEntityList(string kh, string orgId);

        #region GRS门诊挂号
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        string SubmitForm(OutpatientRegistEntity OutpatientRegistEntity, int? keyValue, string pzzy, string orgId, string curUserCode, out string mzh);

        /// <summary>
        /// 病人今天是否已挂号
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool AllowRegh(string blh, string orgId);
        /// <summary>
        /// 取消挂号
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ghnm"></param>
        void SaveCancelRegister(string orgId, int ghnm);

        #endregion

        /// <summary>
        /// 更新门诊挂号 就诊状态、就诊医生
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        void UpdateConsultationStatus(string orgId, string mzh, string jzbz, string jzys);

        /// <summary>
        /// 更新门诊挂号 诊断信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="zdicd10"></param>
        /// <param name="zdmc"></param>
        void UpdateDiagnosis(string orgId, string mzh, string zdicd10, string zdmc);
        /// <summary>
        /// 更新患者性质
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="brxz"></param>
        void updatePatBrxzInfo(string orgId, string mzh, string brxz);

        /// <summary>
        /// 记录补偿序号
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId">补偿序号</param>
        /// <param name="userCode">修改人</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int RecordOutpId(string mzh, string outpId, string userCode, string organizeId);
        int UpdatePatPhone(string patid, string phone, string userCode, string organizeId);
    }
}

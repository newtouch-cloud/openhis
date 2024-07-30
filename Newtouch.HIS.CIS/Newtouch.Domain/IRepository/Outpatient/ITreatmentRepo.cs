using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.API;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITreatmentRepo : IRepositoryBase<TreatmentEntity>
    {
        /// <summary>
        /// 已就诊 就诊中 列表( 已就诊： 只能查看自己名下的患者)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="jzzt"></param>
        /// <param name="mjzbz"></param>
        /// <param name="rygh"></param>
        /// <param name="kzrq"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TreatEntityObj> GetTreatingOrTreatedList(Pagination pagination, string orgId, int jzzt, int mjzbz, string rygh, DateTime? kzrq, string keyword, bool? zzhz);

        IList<PatientRegInfoDTO> GetdjzList(Pagination pagination, string keyword, int mjzbz);

        IList<RegisteredInfoRespVO> GetghList(string xm, string blh, string orgid);

        /// <summary>
        /// 恢复就诊
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jzId"></param>
        void ResumeTreat(string orgId, string jzId);

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        TreatmentEntity SelectData(string jzId, string organizeId);

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<TreatmentEntity> SelectDataByMzh(string mzh, string organizeId);

        /// <summary>
        ///修改CmmPatId
        /// </summary>
        /// <param name="cmmPatId"></param>
        /// <param name="jzId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="updateTime"></param>
        /// <returns></returns>
        int UpdateCmmPatId(string cmmPatId, string jzId, string organizeId, string userCode, DateTime updateTime);
    }
}

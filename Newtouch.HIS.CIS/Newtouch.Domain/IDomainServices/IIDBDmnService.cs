using Newtouch.Core.Common;
using Newtouch.Domain.DTO;
using Newtouch.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 中间库相关
    /// </summary>
    public interface IIDBDmnService
    {
        /// <summary>
        /// 查询患者挂号信息
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh">//筛选专家号 仅看挂自己号的</param>
        /// <param name="mjzbz">//1普通门诊 2急诊 3专家门诊</param>
        /// <param name="jiuzhenbz"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TreatmentEntity> GetRegistrationInfoList(Pagination pagination, string orgId, string ksCode, string ysgh, string mjzbz, int jiuzhenbz, string keyword);

        /// <summary>
        /// 将mzh的未收费处方+就诊标志 等信息 同步至中间库
        /// （且同步处方成功后要回写处方同步状态）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        void SyncTo(string orgId, string mzh, string cfId = null);

        /// <summary>
        /// 作废his单次就诊的所有处方
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="orgId"></param>
        void ObsoleteAllPresToHIS(string jzId, string orgId);

        /// <summary>
        /// 作废HIS单张处方
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        /// <param name="apicflx"></param>
        /// <param name="apicfh"></param>
        void cancelSinglePresToHIS(string orgId, string mzh, string cfId
            , int apicflx, string apicfh);


        /// <summary>
        /// CIS处方数据
        /// </summary>
        /// <param name="Opt"></param>
        /// <param name="Brwym"></param>
        /// <param name="Zdlx"></param>
        /// <param name="Brxz"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
         List<PrescriptionDto> GetCisPrescription(string Opt, string Brwym, string Zdlx, string Brxz, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 中间库 同步处方收费状态
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
         void Refreshcfsfbz(string blh, string orgId);
    }
}

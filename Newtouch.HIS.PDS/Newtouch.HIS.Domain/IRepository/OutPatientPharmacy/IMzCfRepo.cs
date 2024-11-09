using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Domain.VO;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 门诊处方
    /// </summary>
    public interface IMzCfRepo : IRepositoryBase<MzCfEntity>
    {
        /// <summary>
        /// 修改发药标致 未发=>已排
        /// </summary>
        /// <param name="cfhs"></param>
        /// <returns></returns>
        int UpdateFybzByCfh(List<string> cfhs);

        /// <summary>
        /// 修改发药标致
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int UpdateFybzByCfh(string cfh, EnumFybz fybz, string organizeId);

        /// <summary>
        /// 修改发药标致 已排=>未发
        /// </summary>
        /// <param name="cfhs"></param>
        /// <returns></returns>
        int UpdateFybzToWFByCfh(List<string> cfhs, string OrganizeId);

        /// <summary>
        /// 获取未排药的处方
        /// </summary>
        /// <returns></returns>
        List<MzCfEntity> GetNoArrangedCfList(DateTime? bT = null, DateTime? eT = null);

        /// <summary>
        /// 获取未排药的处方
        /// </summary>
        /// <returns></returns>
        IList<MzCfEntity> GetCfsByKeyword(string keyword, int fybz = (int)EnumFybz.Yp, Pagination pagination = null);

        /// <summary>
        /// 根据卡号和姓名获取处方信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        cfInfoVo GetCfInfo(string cardNo, string xm, int fybz = 0, string organizeId = "");

        /// <summary>
        /// 根据卡号和姓名获取处方信息
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> GetRpInfo(string yfbmCode, string cardNo, string xm, EnumFybz fybz = EnumFybz.Yp, string organizeId = "");

        /// <summary>
        /// 根据卡号和姓名获取处方信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> GetCfs(string cardNo, string xm, int fybz = 0, string organizeId = "");

        /// <summary>
        /// 根据卡号和姓名获取处方信息
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="ispay"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> GetCf(string yfbmCode, string cardNo, string xm, EnumFybz fybz, bool ispay, string organizeId);

        /// <summary>
        /// 通过处方号获取组号
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="ispay"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<GetMzCfZt> GetZhInOutpatient(string cfh,string type);
        /// <summary>
        /// 批量插入处方信息 返回受影响行
        /// </summary>
        /// <param name="mzCfs"></param>
        /// <returns></returns>
        int InsertBatch(List<MzCfEntity> mzCfs);

        /// <summary>
        /// 删除老的处方信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        int DeleteCf(string cfh, long cfnm);

        /// <summary>
        /// 作废处方，退费用
        /// </summary>
        /// <param name="cfh">处方号</param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        int DeleteCf(string cfh, string OrganizeId);

        /// <summary>
        /// 返回去重后的姓名和收费时间
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        IList<patientInfoVO> GetXmAndCardNo(Pagination pagination, string yfbmCode, string organizeId, string keyword = "", EnumFybz fybz = EnumFybz.Wp);

        /// <summary>
        /// 查询已发或者已退处方 默认显示前500条
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IList<CfxxVO> SelectDispensedRpList(searchFyInfoReqVO param);

        /// <summary>
        /// 根据处方号获取有效的处方
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> SelectRpList(string cfh, string organizeId);

        /// <summary>
        /// 更新性别
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="xb"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        int UpdateGender(string cfh, string xb, string orgId);

        List<MzCfEntity> SelectTfRpList(string cfh, string organizeId);

        #region  医保电子处方
        /// <summary>
        /// 返回去重后的姓名和收费时间
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        IList<patientInfoVO> GetElectronicPrescription(Pagination pagination, string organizeId, string isysh, string keyword = "");
        /// <summary>
        /// 根绝处方号和姓名获取处方明细信息
        /// </summary>
        /// <returns></returns>
        List<DzcfmxVO> QueryElectronicPrescriptionCfmx(string cfh, string xm, string organizeId);
        Input_2203A GetCQjzdjInfo(string mzh, string orgId);
        int UpdateCfYsshyj(string cfh, string ysshyj, string orgId);

        #endregion
    }
}
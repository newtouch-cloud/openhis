using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Sett.Request.OutPatientPharmacy;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.OutpatientManage
{
    public interface IOutpatientPharmacyAPIDmnService
    {
        /// <summary>
        /// 加载排药合计信息
        /// </summary>
        /// <param name="ksdm">科室代码</param>
        /// <param name="fysjs"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        OutPatientpyInfoDTO GetFyhjInfo(string ksdm, DateTime fysjs, int fybz);

        /// <summary>
        /// 查询排药信息
        /// </summary>
        /// <param name="ksdm"></param>
        /// <param name="yxq"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutPatientpyListDTO> GetFyList(string ksdm, int yxq, string orgId);


        /// <summary>
        /// 补打发药单 查询发药信息
        /// </summary>
        /// <param name="ksdm"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="kh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<OutPatientpyListDTO> GetFyListOnPrint(string ksdm, string fph, string xm, string kh, DateTime kssj, DateTime jssj, string organizeId);

        /// <summary>
        /// 查询排药详细药品信息
        /// </summary>
        /// <param name="cfh">处方号</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutPatientpyDetailListDTO> GetFyDetailList(string cfh, string orgId);

        /// <summary>
        /// 根据处方内码获取详细的处方药品信息
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<YP_XT_OP_PYListDTO> GetpyParList(string cfnm, string organizeId);

        /// <summary>
        /// 获取处方金额，领药药房信息
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<GetcfInfoDTO> GetcfInfo(string cfnm, string organizeId);

        /// <summary>
        /// 更新发药标志
        /// </summary>
        /// <param name="par"></param>
        bool Updatecfzt(UpdatefyztRequest par);

        /// <summary>
        /// 门诊发药显示主表信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<fyMainListRequest> GetfyMainInfoList(string keyword, string orgId);

        /// <summary>
        /// 门诊发药显示处方详细信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<fyDetailListRequest> GetfyDetailInfoList(string cfh, string organizeId);

        /// <summary>
        /// 门诊发药，发药时获取处方信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="lyyf"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<fyCfYpInfo> GetfyDetailCFInfo(string cfh, string lyyf, string organizeId);

        /// <summary>
        /// 发药完成后更新处方表的发药标志
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="user_code"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        bool UpdatecfztByFY(string cfnm, string user_code, string zt);


        /// <summary>
        /// 门诊退药显示主表信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<tyCFMainInfo> GetTyMainInfoList(tyCFYpInfoRequest req, string orgId);

        /// <summary>
        /// 门诊退药显示处方详细信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<tyDetailListRequest> GettyDetailInfoList(string cfh, string orgId);

        /// <summary>
        /// 门诊发药查询页面 查询主表信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<fyQueryMainInfo> GetfyQueryInfoList(searchFyInfoReqVO req, string orgId);

        /// <summary>
        /// 门诊发药查询显示药品详细信息
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<fyDetailListRequest> GetfyQueryDetailInfoList(string cfnm, string orgId);
        /// <summary>
        /// 已结算处方明细查询
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <param name="yfCode">用法代码</param>
        /// <returns></returns>
        List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(string organizeId, DateTime kssj, DateTime jssj, string fph, string kh, string yfCode,string mzh);

        /// <summary>
        /// 获取已结算药品
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="cfnm"></param>
        /// <param name="yp"></param>
        /// <param name="sl"></param>
        /// <param name="czh">成组号</param>
        void OutpatientDrugWithdrawalNotify(string organizeId, int cfnm, string yp, decimal sl, string czh);
    }
}

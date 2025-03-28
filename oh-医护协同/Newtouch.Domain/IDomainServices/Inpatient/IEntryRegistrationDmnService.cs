using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices
{
    public interface IEntryRegistrationDmnService
    {
        /// <summary>
        /// 获取床位信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<BedInfoViewResponseVO> GetCwInfoViewList(BedInfoViewRequestVO req);
        /// <summary>
        /// 患者入区
        /// </summary>
        /// <param name="inareaReq"></param>
        /// <returns></returns>
        string SavePatInArea(patInAreaRequestDto inareaReq);
        /// <summary>
        /// 患者转区患者入区信息修改
        /// </summary>
        /// <param name="patChangeAreaRequestDto"></param>
        string SaveChangeAreaPatInfo(patChangeAreaRequestDto patChangeAreaRequestDto);
        string UpdatePatInfo(string zyh,string wzjb,string orgId);
        /// <summary>
        /// 保存入区时患者信息
        /// </summary>
        /// <param name="patInfoRes"></param>
        void SavePatInfo(InPatientDetailQueryDto patInfoRes);
        /// <summary>
        /// 入区时收取当天床位绑定费用
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string AddPatBedItemFee(patBedFeeRequestDto req);
        /// <summary>
        /// 获取转区患者
        /// </summary>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        List<NewPatInfoVO> GetPatChangeArea(string bqdm);
        /// <summary>
        /// 获取需插入到床位使用记录表的数据
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        patBedSyjlInfoVO GetPatBedSyInfo(string zyh);
        /// <summary>
        /// 保存床位使用记录
        /// </summary>
        /// <param name="patBedSyInfo"></param>
        void SavePatBedSYInfo(patBedSyjlInfoVO patBedSyInfo);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">关键字</param>
        /// <param name="ksrq">开始日期</param>
        /// <param name="jsrq">结束日期</param>
        /// <param name="bqdm">病区代码</param>
        /// <returns></returns>
        IList<OutAreapatientInfo> GetOutAreaPatlist(Pagination pagination, string keyword, DateTime? ksrq, DateTime? jsrq, string bqdm, string orgId);
        /// <summary>
        /// 出区召回
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="bqdm">病区代码</param>
        string SaveRecallOutArea(string zyh, string bqdm);
        /// <summary>
        /// 判断患者能否取消入区
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string GetPatIsCancelInArea(patRequestDto req);

        string PatCancelInArea(patRequestDto req);

        string GetPatIsChangeArea(patRequestDto req);

        string SaveChangeArea(patRequestDto req, string bqdm);
       
        /// <summary>
        /// 转床
        /// </summary>
        /// <param name="reqDto"></param>
        string ChangeBed(patBedRequestDto reqDto);
        /// <summary>
        /// 在病区病人信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="bqdm"></param>
        /// <returns></returns>
        IList<InAreapatientInfo> GetINAreaPatlist(Pagination pagination, string keyword, string bqdm,string orgId);

        /// <summary>
        /// 新入区、转区 床位是否被占用
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="cwCode"></param>
        /// <param name="bqCode"></param>
        /// <returns></returns>
        bool ValidationBedIsUse(string OrganizeId,string cwCode,string bqCode);
    }
}

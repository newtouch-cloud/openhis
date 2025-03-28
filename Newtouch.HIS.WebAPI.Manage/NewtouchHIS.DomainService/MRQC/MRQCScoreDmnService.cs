using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.IDomainService.EMR;
using NewtouchHIS.Domain.IDomainService.MRQC;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Domain.InterfaceObjets.MRQC;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtouchHIS.Lib.Base.EnumExtend;
using Microsoft.Data.SqlClient;

namespace NewtouchHIS.DomainService.MRQC
{
    /// <summary>
    /// 病历质控
    /// </summary>
    public class MRQCScoreDmnService : BaseServices<MRQCScoreVO>, IMRQCScoreDmnService
    {

        /// <summary>
        /// 病历质控达标情况
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<List<MRQCScoreVO>> GetMrqcScore(string orgId, string zyh,string bllxId,DBEnum db = DBEnum.MrQcDb)
        {
            //            var data = await baseDal.GetListBySqlQuery<MRQCScoreVO>(db.ToString(), @"select a.* ,b.* , c.bllxmc ,Convert(decimal(18,1),Score * sl) TotalScore 
            //from [dbo].[Mr_Qc_Score] a
            //left join [dbo].[MR_Qc_ItemDetail] b on a.ScoreCode=b.Code and a.organizeId=b.organizeId and a.zt=b.zt 
            //left join [Newtouch_EMR].[dbo].[bl_bllx] c on a.bllxId=c.bllx and a.OrganizeId=c.OrganizeId and a.zt=c.zt 
            //where zyh=@zyh and bllxId= @bllxId and a.organizeid=@orgId and a.zt='1' "
            var data = await baseDal.GetListBySqlQuery<MRQCScoreVO>(db.ToString(), @"
select 
a.zyh,a.sl,c.bllxmc,
b.*,
Convert(decimal(18,1),b.Score * sl) TotalScore
from [dbo].[Mr_Qc_Score] a
left join  [dbo].[MR_Qc_ItemData] b on a.BllxId=b.blmbId and  a.ScoreCode=b.Code and a.organizeId=b.organizeId and a.zt=b.zt 
left join [Newtouch_EMR].[dbo].[bl_bllx] c on a.bllxId=c.bllx and a.OrganizeId=c.OrganizeId and a.zt=c.zt
--left join [Newtouch_EMR].[dbo].[bl_mblb] d on d.bllxId=c.Id and d.mbbm= a.blmbId and d.OrganizeId=c.OrganizeId and d.zt=c.zt
where a.zyh=@zyh and a.bllxId= @bllxId and a.organizeid=@orgId and a.zt='1'"
, new List<DbParameter> {
                new SqlParameter("@orgId",orgId),
                  new SqlParameter("@zyh",zyh),
                    new SqlParameter("@bllxId",bllxId)
            });

            //MRQCScoreVO list = new MRQCScoreVO();
            //if (data != null && data.Count > 0)
            //{
            //    list.MRQCScore = data;
            //}
            //return bllxList;
            return data;
        }
    }
}

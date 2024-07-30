using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using Newtouch.Tools;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Common.Operator;
using System.Data.SqlClient;
using System.Data.Common;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Common.Model;

namespace Newtouch.HIS.DomainServices.TherapeutistCompleteManage
{
    public class TherapistSuggestionDmnService : DmnServiceBase, ITherapistSuggestionDmnService
    {
        public TherapistSuggestionDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 保存治疗建议
        /// </summary>
        /// <param name="itemList"></param>
        public void SaveData(IList<TherapistSuggestionEntity> itemList, IList<string> delIds, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (delIds != null && delIds.Count > 0)
                {
                    foreach (var delitem in db.IQueryable<TherapistSuggestionEntity>(p => delIds.Contains(p.jyId)).ToList())
                    {
                        delitem.zt = "0";
                        delitem.Modify();
                        db.Update(delitem);
                    }
                }
                if (itemList != null && itemList.Count>0)
                {
                    foreach (var item in itemList)
                    {
                        TherapistSuggestionEntity tsentity = null;
                        if (string.IsNullOrEmpty(item.jyId))
                        {
                            tsentity = new TherapistSuggestionEntity()
                            {
                                jyId = Comm.GuId(),
                                OrganizeId = orgId,
                                blh = item.blh,
                                brlx = item.brlx,
                                mzzyh = item.mzzyh,
                                itemCode = item.itemCode,
                                mczll = item.mczll,
                                sl = item.sl,
                                pc = item.pc,
                                zxksdm = item.zxksdm,
                                bz = item.bz,
                                zhbz = "0",
                                CreateTime = DateTime.Now,
                                CreatorCode = OperatorProvider.GetCurrent().UserCode,
                                LastModifierCode = OperatorProvider.GetCurrent().UserCode,
                                LastModifyTime = DateTime.Now,
                                zt = "1",
                                bw = item.bw
                            };
                            db.Insert(tsentity);
                        }
                        else
                        {
                            var uptsentity = db.IQueryable<TherapistSuggestionEntity>(p => p.jyId == item.jyId && p.zt == "1").FirstOrDefault();
                            if (uptsentity != null)
                            {
                                uptsentity.itemCode = item.itemCode;
                                uptsentity.mczll = item.mczll;
                                uptsentity.sl = item.sl;
                                uptsentity.pc = item.pc;
                                uptsentity.zxksdm = item.zxksdm;
                                uptsentity.bz = item.bz;
                                uptsentity.bw = item.bw;
                                uptsentity.Modify();
                                db.Update(uptsentity);
                            }
                            else
                            {
                                throw new FailedException("数据异常，未查询到该治疗建议信息");
                            }

                        }
                    }
                }

                db.Commit();
            }
        }

        /// <summary>
        /// 根据门诊或住院号模糊查询病人信息（治疗师建议）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<MzZyPatInfoDto> GetMzZyPatInfo(string mzzyh, string orgId, string brlx)
        {
            List<MzZyPatInfoDto> MzZyPatInfoDto = null;
            StringBuilder strSql = new StringBuilder();
            if (brlx == "1")
            {
                strSql.Append(@"SELECT  a.blh ,
                                        A.xm ,
                                        A.xb ,
                                        A.csny ,
                                        A.zjh ,
                                        CAST( FLOOR(datediff(DY,a.csny,getdate())/365.25) as int) nl,
                                        gh.mzh mzzyh,
                                        A.phone,
                                        null ryrq,
                                        '' ryzd
                                FROM    [dbo].[xt_brjbxx] AS A WITH ( NOLOCK )
                                        RIGHT JOIN dbo.mz_gh gh ON gh.patid=a.patid 
		                                AND gh.OrganizeId=a.OrganizeId
                                WHERE   gh.mzh=@mzzyh AND a.OrganizeId = @orgId");
            }
            if (brlx == "2")
            {
                strSql.Append(@"
                --第一诊断
                DECLARE @ryzdmc varchar(20) DECLARE @ryzdicd10 varchar(20)
                SELECT top 1 @ryzdmc = n.zdmc,
                         @ryzdicd10 = n.icd10
                FROM zy_rydzd m with(nolock)
                LEFT JOIN [NewtouchHIS_Base]..V_S_xt_zd n
                    ON m.zdCode = n.zdCode
                        AND (n.OrganizeId=@orgId
                        OR n.OrganizeId = '*')
                WHERE m.zyh = @mzzyh
                        AND m.OrganizeId=@orgId
                ORDER BY  m.zdpx

                SELECT zybrjbxx.zyh mzzyh,
                         brjbxx.xm,
                         brjbxx.csny,
                         CAST( FLOOR(datediff(DY,brjbxx.csny,getdate())/365.25) as int) nl,
                         zybrjbxx.zjh,
                         zybrjbxx.ryrq,
                         brjbxx.blh,
                         brjbxx.xb,
                         brjbxx.phone,
		                 @ryzdmc as ryzdmc
                FROM zy_brjbxx zybrjbxx with(nolock)
                LEFT JOIN xt_brjbxx brjbxx
                    ON zybrjbxx.patid = brjbxx.patid
                        AND brjbxx.OrganizeId=@orgId
                LEFT JOIN xt_brxz brxz
                    ON brxz.brxz = zybrjbxx.brxz
                        AND brxz.OrganizeId=@orgId
                LEFT JOIN xt_brzh brzh
                    ON zybrjbxx.zyh = brzh.zyh and brzh.OrganizeId=@orgId
                LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Department Department
                    ON Department.Code = zybrjbxx.ks
                        AND Department.OrganizeId=@orgId
                WHERE zybrjbxx.zyh=@mzzyh
                        AND zybrjbxx.OrganizeId=@orgId and zybrjbxx.zt=1
                        ");
            }

            SqlParameter[] param =
                {
                    new SqlParameter("@mzzyh",mzzyh),
                    new SqlParameter("@orgId",orgId)
                };
            MzZyPatInfoDto = FindList<MzZyPatInfoDto>(strSql.ToString(), param);
            if (MzZyPatInfoDto == null)
            {
                throw new FailedCodeException("OUTPAT_REGIST_ISINVALID");
            }
            return MzZyPatInfoDto;
        }


        public List<TherapistAdviceDto> GetZLJYList(string mzzyh, string orgId, string brlx)
        {
            List<TherapistAdviceDto> TherapistAdviceDto = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.jyId,a.blh,a.itemCode,b.sfxmmc itemName, a.mczll,a.sl,a.pc,a.zxksdm,d.Name  zxksmc,a.bz,a.zhbz,c.yzpcmc pcmc,a.bw
FROM [NewtouchHIS_Sett].[dbo].[zls_zljy] AS A WITH ( NOLOCK )
                                    LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm  b ON a.itemCode = b.sfxmCode AND a.OrganizeId=b.OrganizeId  AND b.zt='1'
                              left join [NewtouchHIS_Base]..[V_S_xt_yzpc] c on a.pc=c.yzpcCode AND a.OrganizeId=c.OrganizeId  AND c.zt='1'
                             left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] d ON a.zxksdm=d.Code AND a.OrganizeId=d.OrganizeId  AND d.zt='1'
                            WHERE   a.mzzyh=@mzzyh AND a.zt='1' AND a.zhbz<>1 AND a.brlx=@brlx AND a.OrganizeId = @orgId");
            SqlParameter[] param =
                {
                    new SqlParameter("@mzzyh",mzzyh),
                    new SqlParameter("@brlx",brlx),
                    new SqlParameter("@orgId",orgId)
                };
            TherapistAdviceDto = FindList<TherapistAdviceDto>(strSql.ToString(), param);
            if (TherapistAdviceDto == null)
            {
                throw new FailedCodeException("OUTPAT_REGIST_ISINVALID");
            }
            return TherapistAdviceDto;
        }

        /// <summary>
        /// 获取治疗建议列表（API用）
        /// </summary>
        /// <param name="mzzyh"></param>
        /// <param name="orgId"></param>
        /// <param name="brlx"></param>
        /// <param name="startTime">开立起始时间</param>
        /// <returns></returns>
        public IList<TherapistSuggestionVO> GetTherapistSuggestionList(string mzzyh, string orgId, string brlx, DateTime startTime)
        {
            var sql = @"select jyId, sfxm.sfxmCode sfxmCode, sfxm.sfxmmc sfxmmc,yzpc.zxcs,yzpc.zxzq,yzpc.zxzqdw
,isnull(zljy.mczll,0) mczll, isnull(zljy.sl,0) sl
,yzpc.yzpcCode pc, yzpc.yzpcmc pcmc
,sysdept.Code zxks, sysdept.Name zxksmc
,zljy.CreateTime, userstaff.Name CreatorName
,isnull(sfxm.dj,0) dj, sfxm.dw, sfxm.dwjls, sfxm.jjcl
,zljy.bw,zljy.bz,zljy.zhbz
from zls_zljy(nolock) zljy
left join [NewtouchHIS_Base]..V_S_xt_sfxm sfxm
on sfxm.sfxmCode = zljy.itemCode and sfxm.OrganizeId = zljy.OrganizeId and sfxm.zt = '1'
left join [NewtouchHIS_Base]..V_S_xt_yzpc yzpc
on yzpc.yzpcCode = zljy.pc and yzpc.OrganizeId = zljy.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department sysdept
on sysdept.Code = zljy.zxksdm and sysdept.OrganizeId = zljy.OrganizeId
left join [NewtouchHIS_Base]..V_C_Sys_UserStaff userstaff
on userstaff.Account = zljy.CreatorCode and userstaff.OrganizeId = zljy.OrganizeId and userstaff.zt = '1'
where 1 = 1 and zljy.mczll is not null and zljy.sl is not null and zljy.zt = '1'
and zljy.OrganizeId = @orgId
and zljy.brlx = @brlx and mzzyh = @mzzyh	--1门诊 2住院
and sfxm.sfxmCode is not null
and userstaff.Account is not null
and zljy.CreateTime >= @startTime";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@brlx", brlx));
            pars.Add(new SqlParameter("@mzzyh", mzzyh));
            pars.Add(new SqlParameter("@startTime", startTime));
            return this.FindList<TherapistSuggestionVO>(sql, pars.ToArray());
        }

        /// <summary>
        /// 更新建议的转换状态
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cvList"></param>
        public void UpdateSuggestionCvStatus(string orgId, IList<SuggestionCvStatuUpdateDTO> cvList)
        {
            if (string.IsNullOrWhiteSpace(orgId) || cvList == null || cvList.Count == 0)
            {
                return;
            }
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var needCommit = false;
                foreach (var cv in cvList)
                {
                    var entity = db.IQueryable<TherapistSuggestionEntity>().Where(p => p.jyId == cv.jyId && p.OrganizeId == orgId).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.zhbz = cv.zhbz;
                        entity.Modify();
                        db.Update(entity);
                        needCommit = true;
                    }
                }
                if (needCommit)
                {
                    db.Commit();
                }
            }
        }

    }
}

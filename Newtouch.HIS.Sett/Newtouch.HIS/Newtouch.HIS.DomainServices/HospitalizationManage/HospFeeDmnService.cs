using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 住院费用
    /// </summary>
    public class HospFeeDmnService : DmnServiceBase, IHospFeeDmnService
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        public HospFeeDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据住院获取住院的项目计费 的 简要信息 列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<HospItemFeeDetailVO> GetAllItemFeeDetailVOList(string zyh, string orgId)
        {
            var sql = @"select a.jfbbh, a.dj, isnull(a.fwfdj,0) fwfdj, sl, c.sfxmmc, b.dlCode dl, b.dlmc
,a.CreateTime, isnull(a.jmje ,0 ) jmje, isnull(a.jmbl, 0) jmbl, a.zfbl
,a.zfxz, a.clzhxmbh, c.xnhybdm ybbm
from zy_xmjfb a
left join [NewtouchHIS_Base]..V_S_xt_sfdl b
on a.dl = b.dlCode and b.OrganizeId = @orgId
left join [NewtouchHIS_Base]..V_S_xt_sfxm c
on a.sfxm = c.sfxmCode and c.OrganizeId = @orgId
where a.zyh = @zyh
and a.OrganizeId = @orgId";
            return this.FindList<HospItemFeeDetailVO>(sql, new[] { new SqlParameter("@zyh", zyh),
            new SqlParameter("@orgId", orgId)});
        }

        /// <summary>
        /// 根据住院获取住院的项目计费 的 详细信息 列表（含退费）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<HospItemFeeDetailVO> GetItemFeeDetailVOList(string zyh, string orgId) {
            var sql = @"SELECT    xmjf.jfbbh,zyh ,
                            sfxm.sfxmmc sfxmmc ,
                            sfxm.sfxmCode sfxm ,
                            '' gg ,
                            sfxm.dw ,
                            xmjf.sl ,
                            xmjf.dj ,
                            CONVERT(DECIMAL(18, 2), xmjf.dj * xmjf.sl) je ,
                            CONVERT(DECIMAL(18, 2), xmjf.dj * xmjf.sl) zifei ,
                            tdrq sfrq
--取 报表大类
                            ,
                            CASE WHEN ISNULL(reportdlcode, '') <> ''
                                 THEN reportdlcode
                                 ELSE dl
                            END dl ,
                            sfdl.dlmc,
                            xmjf.OrganizeId OrganizeId,
							sfxm.ybdm ydbm,
                            xmjf.CreateTime
                  FROM      (
--
                              SELECT    jfbbh ,
                                        sl ,
                                        MIN(dj) dj ,
                                        MIN(tdrq) tdrq ,
                                        MIN(sfxm) sfxm ,
                                        MIN(OrganizeId) OrganizeId ,
                                        MIN(zyh) zyh ,
                                        MIN(dl) dl,
                                        CreateTime
                              FROM      ( SELECT    CASE WHEN ISNULL(cxzyjfbbh,
                                                              0) = 0
                                                         THEN jfbbh
                                                         ELSE cxzyjfbbh
                                                    END jfbbh ,
                                                    sl ,
                                                    dj ,
                                                    tdrq ,
                                                    sfxm ,
                                                    OrganizeId ,
                                                    zyh ,
                                                    dl,
                                                    CreateTime
                                          FROM      zy_xmjfb WITH ( NOLOCK )
                                          WHERE     zyh = @zyh
                                                    AND OrganizeId = @orgId
                                                    AND zt = '1'
                                        ) AS t
                              GROUP BY  t.jfbbh ,
                                        sl,
										CreateTime
                              --HAVING    SUM(sl) <> 0
--
                            ) AS xmjf
                            LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfxm sfxm
                            WITH ( NOLOCK ) ON xmjf.sfxm = sfxm.sfxmCode
                                               AND xmjf.OrganizeId = sfxm.OrganizeId
                                               AND sfxm.zt = 1
                            LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfdl sfdl
                            WITH ( NOLOCK ) ON sfxm.sfdlCode = sfdl.dlCode
                                               AND xmjf.OrganizeId = sfdl.OrganizeId
                                               AND sfdl.zt = 1";
            return this.FindList<HospItemFeeDetailVO>(sql, new[] { new SqlParameter("@zyh", zyh),
            new SqlParameter("@orgId", orgId)});
        }

        /// <summary>
        /// 根据住院获取住院的药品计费 的 简要信息 列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<HospMedicinFeeDetailVO> GetAllMedicinFeeDetailVOList(string zyh, string orgId)
        {
            var sql = @"select a.jfbbh, a.dj, a.sl, c.ypmc, b.dlCode dl, b.dlmc 
,a.CreateTime, isnull(a.jmje ,0 ) jmje, isnull(a.jmbl, 0) jmbl, a.zfbl
,a.zfxz, c.ybbm
from zy_ypjfb a
left join [NewtouchHIS_Base]..V_S_xt_sfdl b
on a.dl = b.dlCode and b.OrganizeId = @orgId 
left join [NewtouchHIS_Base]..V_S_xt_yp c
on a.yp = c.ypCode and c.OrganizeId = @orgId
where a.zyh = @zyh
and a.OrganizeId = @orgId";
            return this.FindList<HospMedicinFeeDetailVO>(sql, new[] { new SqlParameter("@zyh", zyh)
            ,new SqlParameter("@orgId", orgId)});
        }

        /// <summary>
        /// 根据住院获取住院的药品计费 的 详细信息 列表（含退费）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<HospItemFeeDetailVO> GetMedicinFeeDetailVOList(string zyh, string orgId) {
            var sql = @"                  SELECT    ypjf.jfbbh ,zyh ,
                            yp.ypmc sfxmmc ,
                            yp.ypCode sfxm ,
                            ypsx.ypgg gg ,
                            ypjf.jfdw dw,
                            ypjf.sl ,
                            ypjf.dj ,
                            CONVERT(DECIMAL(18, 2), ypjf.dj * ypjf.sl) je ,
                            CONVERT(DECIMAL(18, 2), ypjf.dj * ypjf.sl) zifei ,
                            tdrq sfrq
--取 报表大类
                            ,
                            CASE WHEN ISNULL(reportdlcode, '') <> ''
                                 THEN reportdlcode
                                 ELSE dl
                            END dl ,
							sfdl.dlmc,
                            ypjf.OrganizeId OrganizeId,
							ypsx.ybdm ybdm,
							ypjf.CreateTime
                  FROM      (
--
                              SELECT    jfbbh ,
                                        sl ,
                                        MIN(dj) dj ,
                                        MIN(tdrq) tdrq ,
                                        MIN(yp) yp ,
                                        OrganizeId ,
                                        MIN(zyh) zyh ,
                                        MIN(dl) dl ,
                                        MIN(jfdw) jfdw,
										CreateTime
                              FROM      ( SELECT    CASE WHEN ISNULL(cxzyjfbbh,
                                                              0) = 0
                                                         THEN jfbbh
                                                         ELSE cxzyjfbbh
                                                    END jfbbh ,
                                                    sl ,
                                                    dj ,
                                                    tdrq ,
                                                    yp ,
                                                    OrganizeId ,
                                                    zyh ,
                                                    dl ,
                                                    jfdw,
													CreateTime
                                          FROM      zy_ypjfb WITH ( NOLOCK )
                                          WHERE     zyh = @zyh
                                                    AND 
													OrganizeId = @orgId
                                                    AND zt = '1'
                                        ) AS t
                              GROUP BY  t.jfbbh ,
                                        OrganizeId ,
                                        sl,
										t.CreateTime
                             -- HAVING    SUM(sl) <> 0
--
                            ) ypjf
                            LEFT JOIN [NewtouchHIS_Base]..V_S_xt_yp yp WITH ( NOLOCK ) ON ypjf.yp = yp.ypCode
                                                              AND ypjf.OrganizeId = yp.OrganizeId
                                                              AND yp.zt = 1
                            LEFT JOIN [NewtouchHIS_Base]..V_S_xt_ypsx ypsx
                            WITH ( NOLOCK ) ON ypjf.yp = ypsx.ypCode
                                               AND ypjf.OrganizeId = ypsx.OrganizeId
                                               AND ypsx.zt = 1
                            LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfdl sfdl
                            WITH ( NOLOCK ) ON yp.dlCode = sfdl.dlCode
                                               AND ypjf.OrganizeId = sfdl.OrganizeId
                                               AND sfdl.zt = 1";
            return this.FindList<HospItemFeeDetailVO>(sql, new[] { new SqlParameter("@zyh", zyh),
            new SqlParameter("@orgId", orgId)});

        }


        /// <summary>
        /// 同步最新CPOE项目费用
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        public void SyncPatFee(string orgId, string zyh, int zxtype)
        {
            //是否启用费用同步最新机制
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var enable = _sysConfigRepo.GetBoolValueByCode("HOSP_INTERFACE_WITH_CPOE", orgId) ?? false;
                if (enable)
                {
                    string sql = "";
                    if (zxtype == 0) //同步项目费用
                    {
                        sql = @" exec [skd_syncxmfeefromcpoe] @orgId=@orgId,@lqrq=@lqrq,@zyh=@zyh,@newcount=@newcount output  ";
                    }
                    else //同步药品费用
                    {
                        sql = @" exec [skd_syncypfeefrompds] @orgId=@orgId,@lqrq=@lqrq,@zyh=@zyh,@newcount=@newcount output ";
                    }


                    SqlParameter[] para = new SqlParameter[] {
                        new SqlParameter("@orgId",orgId),
                        new SqlParameter("@lqrq",DateTime.Now),
                        new SqlParameter("@zyh",zyh),
                        new SqlParameter("@newcount",System.Data.SqlDbType.Int),
                    };
                    para[3].Direction = System.Data.ParameterDirection.Output;
                    db.ExecuteSqlCommand(sql, para);
                    var count = para[3].Value;

                    db.Commit();
                }
            }
        }

        /// <summary>
        /// check是否 产生非医保相关费用
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool CheckHasNonYbFee(string zyh, string orgId)
        {
            var sql = @"select 1 from V_C_Sys_HbtfZyXmjfb xmjf
left join [NewtouchHIS_Base]..V_S_xt_sfxm sfxm
on sfxm.sfxmCode = xmjf.sfxm and sfxm.OrganizeId = xmjf.OrganizeId
where xmjf.zyh = @zyh and xmjf.OrganizeId = @orgId
and isnull(sfxm.ybdm, '') <> ''
union all
select 1 from V_C_Sys_HbtfZyYpjfb ypjf
left join [NewtouchHIS_Base]..V_S_xt_ypsx ypsx
on ypsx.ypCode = ypjf.yp and ypsx.OrganizeId = ypjf.OrganizeId
where ypjf.zyh = @zyh and ypjf.OrganizeId = @orgId
and isnull(ypsx.ybdm, '') <> ''";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@zyh", zyh));
            pars.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<int>(sql, pars.ToArray()).Count > 0;
        }

    }

}

using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.TherapeutistCompleteManage
{
    public class TherapeutistCompleteDmnService : DmnServiceBase, ITherapeutistCompleteDmnService
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IDoctorWorkingDaysPlanRepo _doctorWorkingDaysPlanRepo;
        private readonly IAdjustWorkingHoursRepo _adjustWorkingHoursRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysStaffRepo _sysStaffRepo;

        public TherapeutistCompleteDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #region 治疗师工作列表
        public IList<TherapeutistListGridVO> GetTherapeutistListWorkedList(Pagination pagination, string BeginDate, string EndDate, string hzxm, string zls, string dl, string orgId)
        {
            var sb = new StringBuilder();
            sb.Append(@"SELECT DISTINCT
        d.xmnm jfbbh ,
        type = '门诊' ,
        CONVERT(DECIMAL(18, 2), ( CASE d.ttbz
                                    WHEN 1 THEN d.sl
                                    ELSE d.sl / dbo.search_dh(d.ys)
                                  END )) sl ,
        CONVERT(DECIMAL(18, 2), ( CASE d.ttbz
                                    WHEN 1 THEN d.zlsc
                                    ELSE ROUND(CONVERT(FLOAT, d.zlsc * d.sl)
                                               / CONVERT(FLOAT, dbo.search_dh(d.ys)),
                                               2)
                                  END )) zlsc ,
        d.jzsj ,
        d.ysgh ,
        staff.Name ysxm ,
        xx.xm hzxm ,
        sfdl.dlmc ,
        sfdl.dlcode ,
        sfxm.sfxmmc
FROM    ( SELECT  DISTINCT
                    xm.xmnm ,
                    xm.duration zlsc ,
                    xm.ssrq jzsj ,
                    jsmx.sl ,
                    xm.ys ,
                    c.ysgh ,
                    xm.ttbz ,
                    xm.patid ,
                    xm.sfxm
          FROM      mz_xm xm
                    LEFT JOIN mz_js js ON js.jszt <> '2'
                    INNER JOIN dbo.mz_jsmx jsmx ON jsmx.jsnm = js.jsnm
                                                   AND xm.xmnm = jsmx.mxnm
                    LEFT JOIN ( SELECT   DISTINCT
                                        a.xmnm ,
                                        ysgh = SUBSTRING(a.ys, b.number,
                                                         CHARINDEX(',',
                                                              a.ys + ',',
                                                              b.number)
                                                         - b.number)
                                FROM    ( SELECT    xm.xmnm ,
                                                    ys
                                          FROM      dbo.mz_xm xm
                                          WHERE     OrganizeId = @orgId
                                        ) a
                                        JOIN master..spt_values b ON b.type = 'P'
                                WHERE   CHARINDEX(',', ',' + a.ys, b.number) = b.number
                                        AND CHARINDEX(',', a.ys + ',',
                                                      b.number) - b.number > 0
                                        AND b.number > 0
                                        AND LEN(a.ys) > b.number
                              ) c ON c.xmnm = xm.xmnm
          WHERE     NOT EXISTS ( SELECT 1
                                 FROM   mz_js b
                                 WHERE  jszt = '2'
                                        AND b.cxjsnm = js.jsnm
                                        AND b.OrganizeId = js.OrganizeId
                                        AND OrganizeId = @orgId )
        ) d
        LEFT JOIN dbo.xt_brjbxx xx ON xx.patid = d.patid
        right JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = d.ysgh
                                                           AND staff.OrganizeId = @orgId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmcode = d.sfxm
                                                        AND sfxm.OrganizeId = @orgId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON sfdl.dlCode = sfxm.sfdlcode
                                                        AND sfdl.OrganizeId = @orgId
WHERE   ( xx.xm = @xm
              OR @xm = ''
            )
        AND ( d.ysgh = @ys
              OR @ys = ''
            )
        AND ( sfdl.dlCode = @dl
              OR @dl = ''
            )
        AND d.jzsj BETWEEN @begindate AND @enddate
GROUP BY d.zlsc,d.sl,d.xmnm,d.ttbz,d.ys,d.ysgh,d.jzsj,staff.Name,xx.xm,sfdl.dlmc,sfdl.dlCode,sfxm.sfxmmc
		HAVING d.zlsc>0 AND d.sl>0
                        UNION ALL
                        SELECT DISTINCT
        a.jfbbh ,
        type = '住院' ,
        CONVERT(DECIMAL(18, 2), ( CASE a.ttbz
                                    WHEN 1 THEN SUM(sl)
                                    ELSE SUM(sl) / dbo.search_dh(a.ys)
                                  END )) sl ,
        CONVERT(DECIMAL(18, 2), ( CASE a.ttbz
                                    WHEN 1 THEN a.duration
                                    ELSE ROUND(CONVERT(FLOAT, a.duration
                                               * SUM(sl))
                                               / CONVERT(FLOAT, dbo.search_dh(a.ys)),
                                               2)
                                  END )) zlsc ,
        tdrq jzsj,
        b.ysgh ,
        staff.Name ysxm ,
        xx.xm hzxm ,
        sfdl.dlmc ,
        sfdl.dlcode ,
        sfxm.sfxmmc
 FROM   ( SELECT    CASE WHEN ( cxzyjfbbh IS NOT NULL
                                AND cxzyjfbbh <> 0
                              ) THEN cxzyjfbbh
                         WHEN ( cxzyjfbbh IS NULL
                                OR cxzyjfbbh = 0
                              ) THEN jfbbh
                    END jfbbh ,
                    sl ,
                    tdrq ,
                    zyh ,
                    sfxm ,
                    ys ,
                    ttbz ,
                    duration
          FROM      zy_xmjfb
          WHERE     ys IS NOT NULL
        ) AS a
        INNER JOIN ( SELECT   DISTINCT
                            a.jfbbh ,
                            ysgh = SUBSTRING(a.ys, b.number,
                                             CHARINDEX(',', a.ys + ',',
                                                       b.number) - b.number)
                     FROM   ( SELECT    mx.jfbbh ,
                                        ys ,
                                        sl
                              FROM      dbo.zy_xmjfb mx WHERE organizeId=@orgId
                            ) a
                            JOIN master..spt_values b ON b.type = 'P'
                     WHERE  CHARINDEX(',', ',' + a.ys, b.number) = b.number
                            AND CHARINDEX(',', a.ys + ',', b.number)
                            - b.number > 0
                            AND b.number > 0
                            AND LEN(a.ys) > b.number
                   ) b ON a.jfbbh = b.jfbbh
                          AND b.ysgh IS NOT NULL
        LEFT JOIN dbo.zy_brjbxx xx ON xx.zyh = a.zyh AND xx.OrganizeId=@orgId
        right JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = b.ysgh
                                                           AND staff.organizeId = @orgId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmcode = a.sfxm
                                                        AND sfxm.organizeId = @orgId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON sfdl.dlCode = sfxm.sfdlcode
                                                        AND sfdl.organizeId = @orgId
 WHERE  ( xx.xm = @xm
          OR @xm = ''
        )
        AND ( b.ysgh = @ys
              OR @ys = ''
            )
        AND ( sfdl.dlCode = @dl
              OR @dl = ''
            )
        AND tdrq BETWEEN @begindate AND @enddate
 GROUP BY a.jfbbh ,
        a.tdrq ,
        b.ysgh ,
        xx.xm ,
        sfdl.dlmc ,
        sfdl.dlCode ,
        sfxm.sfxmmc ,
        a.ttbz ,
        a.ys ,
        a.duration ,
        staff.Name HAVING a.duration > 0 AND SUM(sl) > 0");
            DbParameter[] par =
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@xm",hzxm ?? ""),
                new SqlParameter("@ys", zls ?? ""),
                new SqlParameter("@begindate", BeginDate),
                new SqlParameter("@enddate", EndDate),
                new SqlParameter("@dl",dl ?? "")
            };
            return QueryWithPage<TherapeutistListGridVO>(sb.ToString(), pagination, par);
        }
        #endregion

        #region 治疗师工作时间
        /// <summary>
        /// 治疗师工作时间安排列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="ysgh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<TherapeutistWorkPlanGridVO> GetTherapeutistWorkPlanList(Pagination pagination, int? year, int? month, string ysgh, string orgId)
        {
            var sb = new StringBuilder();
            sb.Append(@"SELECT  ys.* ,
                        staff.Name
                FROM    dbo.jz_yspb ys
                        right JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = ys.ysgh
                       AND staff.OrganizeId = @orgId
                    WHERE   ( ys.ysgh = @gh
                              OR @gh = ''
                            )
                            AND ( ys.[year] = @year
                                  OR @year = '-1'
                                )
                            AND ( ys.[month] = @month
                                  OR @month = '-1'
                                )");
            DbParameter[] par =
         {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@gh",ysgh==null?"":ysgh),
                new SqlParameter("@year", year==null?0:year),
                new SqlParameter("@month", month==null?0:month)
            };
            return QueryWithPage<TherapeutistWorkPlanGridVO>(sb.ToString(), pagination, par);
        }

        /// <summary>
        /// 获取所有治疗师和默认天数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<AddStaffPlanVO> GetAllRehabDoctor(string orgId)
        {
            var confEntity = _sysConfigRepo.GetValueByCode("zls_pbts",
             orgId) ?? "22";
            var sb = new StringBuilder();
            sb.Append(@"SELECT  staff.Id ,
                            staff.Name ,
                            staff.gh ,
                            ( CASE WHEN ( SELECT TOP 1
                                                    ts
                                          FROM      dbo.jz_yspb pb
                                          WHERE     ysgh = staff.gh
                                          ORDER BY  CreateTime DESC
                                        ) != '' THEN ( SELECT TOP 1
                                                                ts
                                                       FROM     dbo.jz_yspb pb
                                                       WHERE    ysgh = staff.gh
                                                       ORDER BY CreateTime DESC
                                                     )
                                   ELSE @config
                              END ) ts
                    FROM    NewtouchHIS_Base..Sys_Staff staff
                            LEFT JOIN NewtouchHIS_Base..Sys_StaffDuty sd ON staff.Id = sd.StaffId
                            LEFT JOIN NewtouchHIS_Base..V_S_Sys_Duty duty ON sd.DutyId = duty.Id
                    WHERE   duty.Code = 'RehabDoctor'
                            AND staff.OrganizeId = @orgId");
            DbParameter[] par =
     {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@config", confEntity)
            };
            return FindList<AddStaffPlanVO>(sb.ToString(), par);
        }

        /// <summary>
        /// 新增排班时，提交到数据库
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        public void SubmitPlan(Dictionary<string, List<AddStaffPlanVO>> vo, string orgId)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                if (vo.Keys == null)
                {
                    throw new FailedCodeException("Account_THE_SYSTEM_Search_DOES_NOT_Null");//年月为空，请先选择年月
                }
                var vh = vo.Keys.First().Split(',');
                var year = int.Parse(vh[0]);
                var month = int.Parse(vh[1]);
                foreach (var item in vo.Values)
                {
                    foreach (var entity in item)
                    {
                        var orgStaffData = _sysStaffRepo.FindEntity(p => p.gh == entity.gh && p.OrganizeId == orgId);
                        if (orgStaffData == null)
                        {
                            throw new FailedException("不存在当前人员，请确认工号！");
                        }
                        var oldentity = _doctorWorkingDaysPlanRepo.IQueryable().FirstOrDefault(p => p.month == month && p.year == year && p.ysgh == entity.gh);
                        if (oldentity == null)
                        {
                            DoctorWorkingDaysPlanEntity dwdp = new DoctorWorkingDaysPlanEntity();
                            dwdp.Create(true, null);
                            dwdp.year = year;
                            dwdp.month = month;
                            dwdp.ts = entity.ts;
                            dwdp.ysgh = entity.gh;
                            db.Insert(dwdp);
                        }
                        else
                        {
                            oldentity.ts = entity.ts;
                            oldentity.Modify();
                            db.Update(oldentity);
                        }
                    }
                }
                db.Commit();
            }
        }

        /// <summary>
        /// 查看单个治疗师单月排班信息
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public EditPlanVO GetFormJson(string keyvalue, string orgId)
        {

            var sb = new StringBuilder();
            sb.Append(@"SELECT  pb.Id ,
                        pb.ysgh,
                        CAST(pb.[year] AS VARCHAR(20)) [year] ,
                        CAST(pb.[month] AS VARCHAR(20)) [month] ,
                        staff.Name ,
                        pb.ts
                FROM    dbo.jz_yspb pb
                        LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = pb.ysgh
                WHERE   pb.Id = @Id");
            DbParameter[] par =
           {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@Id", keyvalue)
            };
            return FirstOrDefault<EditPlanVO>(sb.ToString(), par);
        }

        /// <summary>
        /// 编辑单个治疗师单个时间情况
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyvalue"></param>
        public void EditRehabDoctorRange(DoctorWorkingDaysPlanEntity entity, string keyvalue, string orgId)
        {
            var orgStaffData = _sysStaffRepo.FindEntity(p => p.gh == entity.ysgh && p.OrganizeId == orgId);
            if (orgStaffData == null)
            {
                throw new FailedException("不存在当前人员，请确认工号！");
            }
            var oldentity = _doctorWorkingDaysPlanRepo.IQueryable().FirstOrDefault(p => p.month == entity.month && p.year == entity.year && p.ysgh == entity.ysgh && p.Id != entity.Id);
            if (oldentity != null)
            {
                throw new FailedException("该治疗师当前时间已排过班，请勿重复添加");

            }
            if (!string.IsNullOrWhiteSpace(keyvalue))
            {
                entity.Modify(keyvalue);
                _doctorWorkingDaysPlanRepo.Update(entity);
            }
            else
            {
                entity.Create(true, null);
                _doctorWorkingDaysPlanRepo.Insert(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyvalue"></param>
        public void DelArrange(string keyvalue)
        {
            _doctorWorkingDaysPlanRepo.Delete(p => p.Id == keyvalue);
        }
        #endregion

        #region 时长调整
        /// <summary>
        /// 时长调整时，统计治疗师时长
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<AdjustStaffHourVO> GetStaffWorkedHour(string orgId, string gh, string year, string month)
        {
            var sb = new StringBuilder();
            sb.Append(@"SELECT CONVERT(VARCHAR(200),c.xmnm) Id ,[type],
        c.ysgh ,
         CONVERT(CHAR(10), c.jzsj, 120) jzsj ,
        YEAR(c.jzsj) syear ,
        MONTH(c.jzsj) smonth ,
        staff.NAME ,
        SUM(c.zlsc) sc ,
        ( CASE WHEN ( c.tzsc != '' ) THEN c.tzsc
               ELSE SUM(c.zlsc)
          END ) tzsc ,
        c.tzly
FROM    ( SELECT  DISTINCT '门诊' [type],
                    xm.xmnm ,
                    CONVERT(DECIMAL(18, 2), ( CASE xm.ttbz
                                                WHEN 1 THEN jsmx.sl
                                                ELSE jsmx.sl
                                                     / dbo.search_dh(xm.ys)
                                              END )) sl ,
                    CONVERT(DECIMAL(18, 2), ( CASE xm.ttbz
                                                WHEN 1 THEN xm.duration
                                                ELSE ROUND(CONVERT(FLOAT, xm.duration
                                                           * jsmx.sl)
                                                           / CONVERT(FLOAT, dbo.search_dh(xm.ys)),
                                                           2)
                                              END )) zlsc ,
                    xm.ssrq jzsj ,
                    xm.ys ,
                    c.ysgh ,
                    xm.ttbz ,
                    xm.patid ,
                    xm.sfxm,
					tz.tzsc,
					tz.tzly
          FROM      mz_xm xm
                    LEFT JOIN mz_js js ON js.jszt <> '2'
                    INNER JOIN dbo.mz_jsmx jsmx ON jsmx.jsnm = js.jsnm
                                                   AND xm.xmnm = jsmx.mxnm
                    LEFT JOIN ( SELECT   DISTINCT
                                        a.xmnm ,
                                        ysgh = SUBSTRING(a.ys, b.number,
                                                         CHARINDEX(',',
                                                              a.ys + ',',
                                                              b.number)
                                                         - b.number)
                                FROM    ( SELECT    xm.xmnm ,
                                                    ys
                                          FROM      dbo.mz_xm xm
                                          WHERE     OrganizeId = @orgId
                                        ) a
                                        JOIN master..spt_values b ON b.type = 'P'
                                WHERE   CHARINDEX(',', ',' + a.ys, b.number) = b.number
                                        AND CHARINDEX(',', a.ys + ',',
                                                      b.number) - b.number > 0
                                        AND b.number > 0
                                        AND LEN(a.ys) > b.number
                              ) c ON c.xmnm = xm.xmnm   LEFT JOIN jz_sctz tz ON tz.mzxmjfbh = c.xmnm
                                AND tz.OrganizeId = @orgId
                                AND tz.ysgh = c.ysgh
          WHERE     NOT EXISTS ( SELECT 1
                                 FROM   mz_js b
                                 WHERE  jszt = '2'
                                        AND b.cxjsnm = js.jsnm
                                        AND b.OrganizeId = js.OrganizeId
                                        AND OrganizeId = @orgId )
          GROUP BY  xm.duration ,
                    jsmx.sl ,
                    xm.xmnm ,
                    xm.ttbz ,
                    xm.ys ,
                    xm.ssrq ,
                    c.ysgh ,
                    xm.patid ,
                    xm.sfxm,
					tz.tzsc,
					tz.tzly
          HAVING    xm.duration > 0
                    AND jsmx.sl > 0
          UNION ALL
          SELECT DISTINCT  '住院' [type],
                    a.jfbbh xmnm ,
                    CONVERT(DECIMAL(18, 2), ( CASE a.ttbz
                                                WHEN 1 THEN SUM(sl)
                                                ELSE SUM(sl)
                                                     / dbo.search_dh(a.ys)
                                              END )) sl ,
                    CONVERT(DECIMAL(18, 2), ( CASE a.ttbz
                                                WHEN 1 THEN a.duration
                                                ELSE ROUND(CONVERT(FLOAT, a.duration
                                                           * SUM(sl))
                                                           / CONVERT(FLOAT, dbo.search_dh(a.ys)),
                                                           2)
                                              END )) zlsc ,
                    tdrq jzsj ,
                    a.ys ,
                    b.ysgh ,
                    a.ttbz ,
                    xx.patid ,
                    a.sfxm,
					tz.tzsc,
					tz.tzly
          FROM      ( SELECT    CASE WHEN ( cxzyjfbbh IS NOT NULL
                                            AND cxzyjfbbh <> 0
                                          ) THEN cxzyjfbbh
                                     WHEN ( cxzyjfbbh IS NULL
                                            OR cxzyjfbbh = 0
                                          ) THEN jfbbh
                                END jfbbh ,
                                sl ,
                                tdrq ,
                                zyh ,
                                sfxm ,
                                ys ,
                                ttbz ,
                                duration
                      FROM      zy_xmjfb
                      WHERE     ys IS NOT NULL
                    ) AS a
                    INNER JOIN ( SELECT   DISTINCT
                                        a.jfbbh ,
                                        ysgh = SUBSTRING(a.ys, b.number,
                                                         CHARINDEX(',',
                                                              a.ys + ',',
                                                              b.number)
                                                         - b.number)
                                 FROM   ( SELECT    mx.jfbbh ,
                                                    ys ,
                                                    sl
                                          FROM      dbo.zy_xmjfb mx
                                          WHERE     organizeId = @orgId
                                        ) a
                                        JOIN master..spt_values b ON b.type = 'P'
                                 WHERE  CHARINDEX(',', ',' + a.ys, b.number) = b.number
                                        AND CHARINDEX(',', a.ys + ',',
                                                      b.number) - b.number > 0
                                        AND b.number > 0
                                        AND LEN(a.ys) > b.number
                               ) b ON a.jfbbh = b.jfbbh
                                      AND b.ysgh IS NOT NULL
                    LEFT JOIN dbo.zy_brjbxx xx ON xx.zyh = a.zyh
                                                  AND xx.OrganizeId = @orgId
                    LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = b.ysgh
                                                              AND staff.organizeId = @orgId
                    LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmcode = a.sfxm
                                                              AND sfxm.organizeId = @orgId
                    LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON sfdl.dlCode = sfxm.sfdlcode
                                                              AND sfdl.organizeId = @orgId
															   LEFT JOIN jz_sctz tz ON tz.zyxmjfbh =  b.jfbbh
                                AND tz.OrganizeId = @orgId
                                AND tz.ysgh = b.ysgh
          GROUP BY  xx.patid ,
                    a.duration ,
                    a.jfbbh ,
                    a.ttbz ,
                    a.ys ,
                    a.tdrq ,
                    b.ysgh ,
                    staff.Name ,
                    a.sfxm,
					tz.tzsc,
					tz.tzly
          HAVING    a.duration > 0
                    AND SUM(sl) > 0
        ) c
        right JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = c.ysgh
                                                           AND staff.OrganizeId = @orgId    
  WHERE   ( YEAR(c.jzsj) = @year
          OR @year = '-1'
        )
        AND ( MONTH(c.jzsj) = @month
              OR @month = '-1'
            )
        AND ( c.ysgh =@gh
              OR @gh = ''
            )
GROUP BY c.jzsj ,c.[type],
        c.xmnm ,
        c.tzsc ,
        c.tzly ,
        c.ysgh ,
        staff.NAME
ORDER BY c.jzsj");
            DbParameter[] par =
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@gh", gh??""),
                new SqlParameter("@year", year??""),
                new SqlParameter("@month", month??"-1"),
            };
            return FindList<AdjustStaffHourVO>(sb.ToString(), par);
        }

        /// <summary>
        /// 得到治疗师每月时长总和
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IList<AdjustStaffHourVO> GetStaffEachMonthHour(Pagination pagination, string orgId, string gh, string year, string month)
        {
            var data = GetStaffWorkedHour(orgId, gh, year, month);
            var elist = new List<AdjustStaffHourVO>();
            foreach (var item in data)
            {
                var ssmonth = elist.FirstOrDefault(
                    p => p.ysgh == item.ysgh.ToString() && p.syear == item.syear && p.smonth == item.smonth);//判断当前这个人是否存在某年的某月数据
                if (ssmonth == null)
                {
                    ssmonth = new AdjustStaffHourVO
                    {
                        ysgh = item.ysgh,
                        NAME = item.NAME,
                        smonth = item.smonth,
                        syear = item.syear
                    };
                    elist.Add(ssmonth);
                }
                var sc = elist.Where(p => p.ysgh == item.ysgh.ToString() && p.syear == item.syear && p.smonth == item.smonth).Sum(p => Convert.ToDecimal(p.sc));
                ssmonth.sc = sc + item.tzsc;
            };
            return elist;
        }

        public void UpdateTime(Dictionary<string, List<submitTimeVO>> vo)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                if (vo == null)
                {
                    throw new FailedCodeException("Account_THE_SYSTEM_Search_DOES_NOT_Null");//年月为空，请先选择年月
                }
                foreach (var item in vo.Values)
                {
                    foreach (var entity in item)
                    {
                        var ejzsj = Convert.ToDateTime(entity.jzsj);
                        var eysgh = vo.Keys.FirstOrDefault().ToString();
                        var oldentitylist = _adjustWorkingHoursRepo.IQueryable().Where(p => p.zlrq == ejzsj && p.ysgh == eysgh);
                        var oldentity = new AdjustWorkingHoursEntity();
                        if (entity.type == "门诊")
                        {
                            oldentity = oldentitylist.FirstOrDefault(p => p.mzxmjfbh == entity.Id);
                        }
                        else if (entity.type == "住院")
                        {
                            oldentity = oldentitylist.FirstOrDefault(p => p.zyxmjfbh == entity.Id);
                        }
                        if (oldentity == null)
                        {
                            AdjustWorkingHoursEntity dwdp = new AdjustWorkingHoursEntity();
                            dwdp.OrganizeId = Newtouch.Common.Operator.OperatorProvider.GetCurrent().OrganizeId;
                            dwdp.Create(true, null);
                            if (entity.type == "门诊")
                            {
                                dwdp.mzxmjfbh = entity.Id;
                            }
                            else if (entity.type == "住院")
                            {
                                dwdp.zyxmjfbh = entity.Id;
                            }

                            dwdp.zlrq = Convert.ToDateTime(entity.jzsj);
                            dwdp.sc = entity.sc.ToString();
                            dwdp.tzsc = entity.tzsc.ToString();
                            dwdp.tzly = entity.tzly;
                            dwdp.ysgh = eysgh;
                            db.Insert(dwdp);
                        }
                        else
                        {
                            oldentity.zlrq = Convert.ToDateTime(entity.jzsj);
                            if (entity.type == "门诊")
                            {
                                oldentity.mzxmjfbh = entity.Id;
                            }
                            else if (entity.type == "住院")
                            {
                                oldentity.zyxmjfbh = entity.Id;
                            }
                            oldentity.sc = entity.sc.ToString();
                            oldentity.tzsc = entity.tzsc.ToString();
                            oldentity.tzly = entity.tzly;
                            oldentity.ysgh = eysgh;
                            oldentity.Modify();
                            db.Update(oldentity);
                        }
                    }
                }
                db.Commit();
            }
        }

        #endregion

        #region 治疗师工作统计
        public IList<StaffReportVO> GetStaffReport(Pagination pagination, string orgId, string ysgh, string year, string month)
        {
            var data = GetStaffWorkedHour(orgId, ysgh, year, month);
            var elist = new List<StaffReportVO>();

            var confEntity = _sysConfigRepo.GetValueByCode("zls_hour", orgId) ?? "8";
            foreach (var item in data)
            {
                if (!string.IsNullOrWhiteSpace(year) && (string.IsNullOrWhiteSpace(month) || month == "-1"))
                {
                    var ssmonth = elist.FirstOrDefault(
                 p => p.ysgh == item.ysgh.ToString() && p.syear == item.syear);//判断当前这个人是否存在某年数据
                    if (ssmonth == null)
                    {
                        var zgsEntity = _doctorWorkingDaysPlanRepo.IQueryable().FirstOrDefault(p => p.year == item.syear && p.ysgh == item.ysgh);
                        var zgs = 0;
                        if (zgsEntity != null)
                        {
                            zgs = zgsEntity.ts * int.Parse(confEntity) * 60;
                        }
                        ssmonth = new StaffReportVO
                        {
                            ysgh = item.ysgh,
                            NAME = item.NAME,
                            smonth = item.smonth,
                            syear = item.syear,
                            zgs = zgs,
                        };
                        elist.Add(ssmonth);
                    }
                    var sc = elist.Where(p => p.ysgh == item.ysgh.ToString() && p.syear == item.syear).Sum(p => Convert.ToDecimal(p.zlgs));
                    ssmonth.zlgs = sc + Convert.ToDecimal(item.tzsc);
                }
                else if (!string.IsNullOrWhiteSpace(year) && !string.IsNullOrWhiteSpace(month) && month != "-1")
                {
                    var ssmonth = elist.FirstOrDefault(
             p => p.ysgh == item.ysgh.ToString() && p.syear == item.syear && (p.smonth == item.smonth));//判断当前这个人是否存在某年的某月数据
                    if (ssmonth == null)
                    {
                        var zgsEntity = _doctorWorkingDaysPlanRepo.IQueryable().FirstOrDefault(p => p.month == item.smonth && p.year == item.syear && p.ysgh == item.ysgh);
                        var zgs = 0;
                        if (zgsEntity != null)
                        {
                            zgs = zgsEntity.ts * int.Parse(confEntity) * 60;
                        }
                        ssmonth = new StaffReportVO
                        {
                            ysgh = item.ysgh,
                            NAME = item.NAME,
                            smonth = item.smonth,
                            syear = item.syear,
                            zgs = zgs,
                        };
                        elist.Add(ssmonth);
                    }
                    var sc = elist.Where(p => p.ysgh == item.ysgh.ToString() && p.syear == item.syear && p.smonth == item.smonth).Sum(p => Convert.ToDecimal(p.zlgs));
                    ssmonth.zlgs = sc + Convert.ToDecimal(item.tzsc);
                }
            };
            var returnlist = (from item in elist
                              select new StaffReportVO
                              {
                                  ysgh = item.ysgh,
                                  NAME = item.NAME,
                                  syear = item.syear,
                                  zgs = item.zgs,
                                  zlgs = item.zlgs,
                                  fzlgs = item.zgs - item.zlgs,
                                  zlzb1 = item.zlgs == 0 ? 0 : ((item.zgs - item.zlgs) / item.zlgs),
                                  zlzb2 = item.zgs == 0 ? 0 : (item.zlgs / item.zgs)
                              }).Where(p => p.syear == int.Parse(year)).ToList();
            return returnlist;
        }
        #endregion


        #region 治疗师工作效率柱状图
        public SCNumBO GetVisitSC(string orgId)
        {
            /*************************************门诊部门*****************************************************/
            List<OutpatientSCNumVO> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
                        --当前年份
 DECLARE @currYear VARCHAR(30)= ( CONVERT(VARCHAR, YEAR(GETDATE()), 10) )
                        --当前年月
 DECLARE @currYearMoth VARCHAR(30)
 SET @currYearMoth = @currYear + '01';
--门诊
 SELECT col ,
        ISNULL(num1, 0) num
 FROM   [dbo].[f_split]('' + @currYear + '01,' + @currYear + '02,' + @currYear
                        + '03,' + @currYear + '04,' + @currYear + '05,'
                        + @currYear + '06,' + @currYear + '07,' + @currYear
                        + '08,' + @currYear + '09,' + @currYear + '10,'
                        + @currYear + '11,' + @currYear + '12', ',')
        LEFT JOIN ( SELECT  bbb.groupDate groupDate ,
                            CONVERT(DECIMAL(18, 2), SUM(sc) / 60) num1
                    FROM    ( SELECT    CONVERT(VARCHAR(6), jzsj, 112) groupDate ,
                                        sc
                              FROM      ( SELECT  DISTINCT xm.xmnm,
                                                    ( CASE WHEN tz.tzsc IS not NULL
                                                           THEN tz.tzsc
                                                           WHEN tz.tzsc IS NULL
                                                           THEN jsmx.sl
                                                              * xm.duration
                                                      END ) sc ,
                                                    CONVERT(VARCHAR(6), MIN(xm.ssrq), 112) jzsj
                                          FROM      mz_js js
                                                    LEFT JOIN mz_jsmx jsmx ON js.jsnm = jsmx.jsnm
                                                    LEFT JOIN mz_xm xm ON xm.jsnm = js.jsnm
                                                    LEFT JOIN dbo.jz_sctz tz ON tz.mzxmjfbh = xm.xmnm
                                          WHERE     js.OrganizeId = @orgId
                                                    AND js.jszt <> '2'
                                                    AND NOT EXISTS ( SELECT
                                                              1
                                                              FROM
                                                              mz_js b
                                                              WHERE
                                                              jszt = '2'
                                                              AND b.cxjsnm = js.jsnm
                                                              AND b.OrganizeId = js.OrganizeId )
                                                    AND CONVERT(VARCHAR(6), xm.ssrq, 112) >= @currYearMoth
                                          GROUP BY  jsmx.sl ,
                                                    xm.duration,tz.tzsc,xm.xmnm
                                        ) aaa
                            ) bbb
                    GROUP BY bbb.groupDate
                  ) AS a ON a.groupDate = col");
            SqlParameter[] param =
            {
                    new SqlParameter("@orgId",orgId)
                };
            outpatientlist = this.FindList<OutpatientSCNumVO>(sqlStr.ToString(), param).ToList();


            /**********************************************住院部门,已去掉全退的人*****************************************************/

            List<InpatientSCNumVO> inpatientList = null;
            StringBuilder sqlStr2 = new StringBuilder();
            sqlStr2.Append(@"--当前年份
DECLARE @currYear VARCHAR(30)= ( CONVERT(VARCHAR, YEAR(GETDATE()), 10) )
                            --当前年月
DECLARE @currYearMoth VARCHAR(30)
SET @currYearMoth = @currYear + '01';
--住院
SELECT  col ,
        ISNULL(num1, 0) num
FROM    [dbo].[f_split]('' + @currYear + '01,' + @currYear + '02,' + @currYear
                        + '03,' + @currYear + '04,' + @currYear + '05,'
                        + @currYear + '06,' + @currYear + '07,' + @currYear
                        + '08,' + @currYear + '09,' + @currYear + '10,'
                        + @currYear + '11,' + @currYear + '12', ',')
        LEFT JOIN ( SELECT ddd.jzsj groupDate, CONVERT(DECIMAL(18, 2), SUM(sc) / 60) num1
FROM    ( SELECT DISTINCT
                    aaa.jfbbh ,
                    CONVERT(VARCHAR(6), MIN(tdrq), 112) jzsj ,
                    MIN(zyh) zyh ,
                    ( CASE WHEN tzsc IS NOT NULL THEN tzsc
                           WHEN tzsc IS NULL THEN sl * duration
                      END ) sc
          FROM      ( SELECT    jfb.jfbbh ,
                                tdrq ,
                                jfb.zyh ,
                                jfb.sl ,
                                jfb.duration ,
                                tz.tzsc
                      FROM      zy_xmjfb jfb
                                LEFT JOIN dbo.jz_sctz tz ON tz.zyxmjfbh = jfb.jfbbh
                      WHERE     jfb.OrganizeId = @orgId
                                AND jfb.zt = '1'
                                AND CONVERT(VARCHAR(6), tdrq, 112) >= @currYearMoth
                      GROUP BY  tdrq ,
                                jfb.sl ,
                                jfb.zyh ,
                                jfb.duration ,
                                tz.tzsc ,
                                jfb.jfbbh
                    ) aaa
          GROUP BY  aaa.sl ,
                    aaa.duration ,
                    aaa.tzsc ,
                    aaa.jfbbh
        ) ddd GROUP BY jzsj
                  ) AS a ON a.groupDate = col");
            SqlParameter[] param2 =
            {
                    new SqlParameter("@orgId",orgId)
                };
            inpatientList = FindList<InpatientSCNumVO>(sqlStr2.ToString(), param2).ToList();

            //放在一个对象，返回到页面
            var visitNumBO = new SCNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList
            };
            return visitNumBO;
        }

        /// <summary>
        /// 获取治疗师工作效率详细
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public IList<AdjustStaffHourVO> GetVisitDetailSC(Pagination pagination, string orgId, string type, string time)
        {
            IList<AdjustStaffHourVO> returndata = null;
            var syear = DateTime.Now.Year.ToString();
            switch (type)
            {
                case "门诊":
                    returndata = GetOutpatientMonthSC(pagination, orgId, syear, time);
                    break;
                case "住院":
                    returndata = GetInpatientMonthSC(pagination, orgId, syear, time);
                    break;
                case "All":
                    break;
            }
            return returndata;
        }

        /// <summary>
        /// 获取门诊单月详细时长
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        private IList<AdjustStaffHourVO> GetOutpatientMonthSC(Pagination pagination, string orgId, string year, string month)
        {
            var sb = new StringBuilder();
            sb.Append(@"SELECT  DISTINCT
        CONVERT(DECIMAL(18, 2), ( CASE xm.ttbz
                                    WHEN 1
                                    THEN ( CASE WHEN tz.tzsc IS NOT NULL
                                                THEN tz.tzsc
                                                WHEN tz.tzsc IS NULL
                                                THEN jsmx.sl * xm.duration
                                           END )
                                       ELSE ROUND(CONVERT(FLOAT, ( CASE
                                                              WHEN tz.tzsc IS NOT NULL
                                                              THEN tz.tzsc
                                                              WHEN tz.tzsc IS NULL
                                                              THEN jsmx.sl
                                                              * xm.duration / CONVERT(FLOAT, dbo.search_dh(b.ysgh))
                                                              END )),2)
                                  END )) sc ,
         CONVERT(VARCHAR(100),xm.xmnm)  Id ,
        CONVERT(CHAR(10), xm.ssrq, 120) jzsj ,
        b.ysgh ,xm.ys,
        staff.Name
FROM    ( SELECT    a.xmnm ,
                    ysgh = SUBSTRING(a.ys, b.number,
                                     CHARINDEX(',', a.ys + ',', b.number)
                                     - b.number)
          FROM      ( SELECT    xm.xmnm ,
                                ys
                      FROM      dbo.mz_xm xm
                    ) a
                    JOIN master..spt_values b ON b.type = 'P'
          WHERE     CHARINDEX(',', ',' + a.ys, b.number) = b.number
                    AND CHARINDEX(',', a.ys + ',', b.number) - b.number > 0
                    AND b.number > 0
                    AND LEN(a.ys) > b.number
        ) b
        INNER JOIN mz_xm xm ON xm.xmnm = b.xmnm
                               AND xm.organizeId = @orgId
        LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = b.ysgh
                                                           AND staff.OrganizeId = @orgId
        INNER JOIN mz_js js ON js.jszt <> '2'
                               AND js.jsnm = xm.jsnm
        LEFT JOIN dbo.mz_jsmx jsmx ON jsmx.jsnm = jsmx.jsnm
                                      AND js.OrganizeId = @orgId
                                      AND xm.xmnm = jsmx.mxnm
        LEFT JOIN jz_sctz tz ON tz.mzxmjfbh = b.xmnm
                                AND tz.OrganizeId = @orgId
                                AND tz.ysgh =b.ysgh
WHERE   ( YEAR(xm.ssrq) = @year
          OR @year = ''
        )
        AND ( MONTH(xm.ssrq) = @month
              OR @month = ''
            )
        AND NOT EXISTS ( SELECT 1
                         FROM   mz_js b
                         WHERE  jszt = '2'
                                AND b.cxjsnm = js.jsnm
                                AND b.OrganizeId = js.OrganizeId )
GROUP BY xm.duration ,
        jsmx.sl ,
        xm.ys ,
        xm.ttbz ,
        xm.ssrq ,
        xm.xmnm ,
        staff.Name ,
        b.ysgh ,
        tz.tzsc ,
        jsmx.jsnm
HAVING  xm.duration > 0 AND jsmx.sl > 0");
            DbParameter[] par =
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@year", year??""),
                new SqlParameter("@month", month??"-1"),
            };
            return QueryWithPage<AdjustStaffHourVO>(sb.ToString(), pagination, par);
        }


        /// <summary>
        /// 获取住院单月详细时长
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        private IList<AdjustStaffHourVO> GetInpatientMonthSC(Pagination pagination, string orgId, string year, string month)
        {
            var sb = new StringBuilder();
            sb.Append(@" SELECT CONVERT(DECIMAL(18, 2), ( CASE jhmx.ttbz
                                    WHEN 1 THEN jhmx.duration * xm.sl
                                    ELSE ROUND(CONVERT(FLOAT, jhmx.duration
                                               * xm.sl)
                                               / CONVERT(FLOAT, dbo.search_dh(jhmx.ys)),
                                               2)
                                  END )) sc ,
        b.jzjhmxId Id,
        CONVERT(CHAR(10), xm.tdrq, 120) jzsj ,
        b.ysgh ,
        staff.Name
 FROM   ( SELECT    a.jzjhmxId ,
                    ysgh = SUBSTRING(a.ys, b.number,
                                     CHARINDEX(',', a.ys + ',', b.number)
                                     - b.number)
          FROM      ( SELECT    mx.jzjhmxId ,
                                ys
                      FROM      dbo.zy_jzjhmx mx
                    ) a
                    JOIN master..spt_values b ON b.type = 'P'
          WHERE     CHARINDEX(',', ',' + a.ys, b.number) = b.number
                    AND CHARINDEX(',', a.ys + ',', b.number) - b.number > 0
                    AND b.number > 0
                    AND LEN(a.ys) > b.number
        ) b
        LEFT JOIN dbo.zy_jzjhmx jhmx ON b.jzjhmxId = jhmx.jzjhmxId
                                        AND jhmx.organizeId = @orgId
        LEFT JOIN dbo.zy_jzjh jh ON jh.jzjhId = jhmx.jzjhId
                                    AND jh.organizeId = @orgId
        RIGHT JOIN zy_xmjfb xm ON xm.jzjhmxId = jhmx.jzjhmxId
                                  AND xm.organizeId = @orgId
        LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = b.ysgh
                                                           AND staff.OrganizeId = @orgId
        LEFT JOIN jz_sctz tz ON tz.jzjhmxId = b.jzjhmxId
                                AND tz.OrganizeId = @orgId
                                AND tz.ysgh = b.ysgh
 WHERE  jhmx.jzjhmxId != ''
        AND xm.sl > 0
        AND ( YEAR(jhmx.CreateTime) = @year
              OR @year = ''
            )
        AND ( MONTH(jhmx.CreateTime) = @month
              OR @month = ''
            )
 GROUP BY jhmx.duration ,
        xm.sl ,
        xm.tdrq ,
        b.jzjhmxId ,
        jhmx.bz ,
        b.ysgh ,
        jhmx.ys ,
        jhmx.ttbz ,
        staff.Name
 HAVING jhmx.duration > 0");
            DbParameter[] par =
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@year", year??""),
                new SqlParameter("@month", month??"-1"),
            };
            return QueryWithPage<AdjustStaffHourVO>(sb.ToString(), pagination, par);
        }
        #endregion
    }
}

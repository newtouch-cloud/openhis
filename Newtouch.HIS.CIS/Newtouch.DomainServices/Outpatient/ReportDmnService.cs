using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtonsoft.Json;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO.OutputDto.Outpatient.API;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ViewModels;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportDmnService : DmnServiceBase, IReportDmnService
    {
        public ReportDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        /// <summary>
        /// 门诊处方单
        /// </summary>
        /// <param name="cfId"></param>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        public string GetCFDjson(string cfId, string mzh, string orgId)
        {
            OutpatientCFDDto dto = new OutpatientCFDDto();
            if (string.IsNullOrWhiteSpace(cfId))
            {
                throw new FailedException("处方主键为空");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号为空");
            }
            dto.PatientInfo = GetOutpatientInfo(orgId, mzh);
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@cfId", cfId));
            dto.cfd_xmInfo = this.FindList<cfd_xmInfo>("EXEC dbo.rpt_CSMzCFDInfo @orgId=@orgId,@cfId=@cfId ", par.ToArray());
            orgInfo i = new orgInfo();
            i.Name = GetorgNameById(orgId);
            dto.orgInfo = i;
            return dto.ToJson();
        }

        protected PatientInfo GetOutpatientInfo(string orgId,string mzh) {
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@mzh", mzh));
           return this.FirstOrDefault<PatientInfo>("EXEC dbo.rpt_MzPatientInfo @orgId=@orgId,@mzh=@mzh ", par.ToArray());
        }

        public string GetJYDjson(string cfId, string mzh,string orgId) {
            if (string.IsNullOrWhiteSpace(cfId))
            {
                throw new FailedException("处方主键为空");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号为空");
            }
            OutpatientJYDDto dto = new OutpatientJYDDto();
            dto.PatientInfo = GetOutpatientInfo(orgId, mzh);
            string sql = @" select * from ( select row_number()over(partition by ztmc order by a.createtime) num,(case a.cflx  when 4 then '检验申请单' when 5 then '检查申请单' when 6 then '项目处方单' end)cflxmc,
a.cflx,a.cfh,(b.dj*b.sl)zje,a.createtime, ztmc xmmc,b.dj,b.sl,c.name zxks,d.name sqys,e.name sqks
from xt_cf a with(nolock)
left join xt_cfmx b with(nolock) on a.cfid=b.cfid and a.organizeid=b.organizeid  
left join NewtouchHIS_Base.dbo.V_S_Sys_Department c on b.zxks=c.Code and b.organizeid=c.organizeid
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] d on a.ys=d.gh and a.organizeid =d.organizeid
left join NewtouchHIS_Base.dbo.V_S_Sys_Department e on a.ks=e.Code and a.organizeid=e.organizeid
where a.zt=1 and b.zt=1 and a.cfid=@cfId
and a.organizeid=@orgId
and exists(select 1 from xt_jz  d with(nolock) where a.jzid=d.jzid and a.organizeid=d.organizeid and d.mzh=@mzh) ) as d where num=1 ";
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@cfId", cfId));
            par.Add(new SqlParameter("@mzh", mzh));
            dto.jyd_xmInfo = this.FindList<jyd_xmInfo>(sql, par.ToArray());
            orgInfo i = new orgInfo();
            i.Name = GetorgNameById(orgId);
            dto.orgInfo = i;
            return dto.ToJson();
        }

        public string GetJCDjson(string cfId, string mzh,string orgId) {
            if (string.IsNullOrWhiteSpace(cfId))
            {
                throw new FailedException("处方主键为空");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号为空");
            }
            OutpatientJCDDto dto = new OutpatientJCDDto();
            dto.PatientInfo = GetOutpatientInfo(orgId, mzh);
            string sql = @" select * from( select row_number()over(partition by ztmc order by a.createtime) num,
            a.cflx,a.cfh,(b.dj*b.sl)zje,a.createtime, ztmc xmmc,b.dj,b.sl,c.name zxks,d.name sqys,e.name sqks,b.bw
            from xt_cf a with(nolock)
            left join xt_cfmx b with(nolock) on a.cfid=b.cfid and a.organizeid=b.organizeid  
            left join NewtouchHIS_Base.dbo.V_S_Sys_Department c on b.zxks=c.Code and b.organizeid=c.organizeid
            left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] d on a.ys=d.gh and a.organizeid =d.organizeid
            left join NewtouchHIS_Base.dbo.V_S_Sys_Department e on a.ks=e.Code and a.organizeid=e.organizeid
            where a.zt=1 and b.zt=1 
            and a.cfid=@cfId
            and a.organizeid=@orgId
            --and exists(select 1 from xt_jz  d with(nolock) where a.jzid=d.jzid and a.organizeid=d.organizeid and d.mzh=@mzh)
            and a.cflx = 5  ) as d where num=1 ";
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@cfId", cfId));
            dto.jcd_xmInfo = this.FindList<jcd_xmInfo>(sql, par.ToArray());
            orgInfo i = new orgInfo();
            i.Name = GetorgNameById(orgId);
            dto.orgInfo = i;
            string sql2 = @"select top 1 lcyx,sqbz,bl.zs from xt_cf cf
                    left join xt_bl bl
                    on bl.jzId = cf.jzId
                    where cf.cfId = @cfId and cf.OrganizeId = @orgId";
            var par2 = new List<SqlParameter>();
            par2.Add(new SqlParameter("@orgId", orgId));
            par2.Add(new SqlParameter("@cfId", cfId));
            dto.DataInfo = this.FirstOrDefault<DataInfo>(sql2, par2.ToArray());
            return dto.ToJson();
        }

        public string GetWHDjson(string cfId, string mzh, string orgId)
        {
            if (string.IsNullOrWhiteSpace(cfId))
            {
                throw new FailedException("处方主键为空");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号为空");
            }
            OutpatientWHDDto dto = new OutpatientWHDDto();
            dto.PatientInfo = GetOutpatientInfo(orgId, mzh);
            string sql = @"DECLARE @str VARCHAR(2000);
SELECT @str = Value
FROM dbo.Sys_Config
WHERE Code = 'whyfpz'
      AND OrganizeId = @orgId;

SELECT  zh,MAX(zhnum) zhmax
	INTO #zhfz
	FROM    ( SELECT    zh ,
						ROW_NUMBER() OVER ( PARTITION BY zh ORDER BY cfmx.zh ) zhnum,cfh
			  FROM      xt_cfmx cfmx
						INNER JOIN xt_cf cf ON cf.cfId = cfmx.cfId
											   AND cf.OrganizeId = cfmx.OrganizeId
			  WHERE      LEN(ISNULL(zh,'')) >0
						  and  cf.cfId = @cfId
					      AND cfmx.OrganizeId = @orgId
			) tabNum
	GROUP BY zh,cfh


SELECT  
       CASE WHEN LEN(ISNULL(mx.zh, '')) > 0
             THEN ( CASE WHEN zhNum = 1 THEN '┍'
                         WHEN zhNum = zhfz.zhmax THEN '┕'
                         ELSE '│'
                    END )
             ELSE ''
        END num , 
		mx.zh, mx.zhNum,
        --mx.zh ,
        mx.yzpcmcsm ,
        mx.yfmc ,
        mx.CreateTime ,
        mx.sl ,
        mx.dw ,
        mx.ypmc 
FROM    (SELECT ROW_NUMBER() OVER (PARTITION BY b.zh ORDER BY b.zh,b.px) zhNum,
       pc.yzpcmcsm,
       b.zh,
       yf.yfmc,
       a.CreateTime,
       b.mcjl sl,
       b.mcjldw  dw,
       b.ypmc
FROM xt_cf a WITH (NOLOCK)
    LEFT JOIN xt_cfmx b WITH (NOLOCK)
        ON a.cfId = b.cfId
           AND a.OrganizeId = b.OrganizeId
    LEFT JOIN NewtouchHIS_Base..xt_ypyf yf
        ON yf.yfCode = b.yfCode
    LEFT JOIN NewtouchHIS_Base..xt_yzpc pc
        ON pc.yzpcCode = b.pcCode
           AND pc.OrganizeId = @orgId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Department c
        ON b.zxks = c.Code
           AND b.OrganizeId = c.OrganizeId
    LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] d
        ON a.ys = d.gh
           AND a.OrganizeId = d.OrganizeId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Department e
        ON a.ks = e.Code
           AND a.OrganizeId = e.OrganizeId
WHERE a.zt = 1
      AND b.zt = 1
      AND b.yfCode IN (
                          SELECT * FROM dbo.f_split(@str, ',')
                      )
      AND a.cfId = @cfId
      AND a.OrganizeId = @orgId
      AND EXISTS
(
    SELECT 1
    FROM xt_jz d WITH (NOLOCK)
    WHERE a.jzId = d.jzId
          AND a.OrganizeId = d.OrganizeId
          AND d.mzh = @mzh
) ) mx
        LEFT JOIN #zhfz zhfz ON mx.zh = zhfz.zh 
      ORDER BY mx.zh, mx.zhNum
DROP TABLE #zhfz";
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@cfId", cfId));
            par.Add(new SqlParameter("@mzh", mzh));
            dto.whd_xmInfo = this.FindList<whd_xmInfo>(sql, par.ToArray());
            orgInfo i = new orgInfo();
            i.Name = GetorgNameById(orgId);
            dto.orgInfo = i;
            return dto.ToJson();
        }

        public string GetZSDjson(string cfId, string mzh, string orgId)
        {
            if (string.IsNullOrWhiteSpace(cfId))
            {
                throw new FailedException("处方主键为空");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号为空");
            }
            OutpatientZSDDto dto = new OutpatientZSDDto();
            dto.PatientInfo = GetOutpatientInfo(orgId, mzh);
            string sql = @"
DECLARE @str VARCHAR(2000);
SELECT @str = Value
FROM dbo.Sys_Config
WHERE Code = 'zsyfpz'
      AND OrganizeId = @orgId;


	SELECT  zh,MAX(zhnum) zhmax
	INTO #zhfz
	FROM    ( SELECT    zh ,
						ROW_NUMBER() OVER ( PARTITION BY zh ORDER BY cfmx.zh ) zhnum,cfh
			  FROM      xt_cfmx cfmx
						INNER JOIN xt_cf cf ON cf.cfId = cfmx.cfId
											   AND cf.OrganizeId = cfmx.OrganizeId
			  WHERE      LEN(ISNULL(zh,'')) >0
						  and  cf.cfId = @cfId
					      AND cfmx.OrganizeId = @orgId
			) tabNum
	GROUP BY zh,cfh


SELECT  
       CASE WHEN LEN(ISNULL(mx.zh, '')) > 0
             THEN ( CASE WHEN zhNum = 1 THEN '┍'
                         WHEN zhNum = zhfz.zhmax THEN '┕'
                         ELSE '│'
                    END )
             ELSE ''
        END num , 
		mx.zh, mx.zhNum,
        --mx.zh ,
        mx.yzpcmcsm ,
        mx.yfmc ,
        mx.CreateTime ,
        mx.sl ,
        mx.dw ,
        mx.ypmc,
        ISNULL(cast(mx.ds as varchar(50)),'') ds
FROM    (SELECT b.zh,
ROW_NUMBER() OVER ( PARTITION BY b.zh ORDER BY b.zh,b.px ) zhNum ,
       pc.yzpcmcsm,
       yf.yfmc,
       a.CreateTime,
       b.mcjl sl,
       b.mcjldw dw,
       b.ypmc,
       b.ds
FROM xt_cf a WITH (NOLOCK)
    LEFT JOIN xt_cfmx b WITH (NOLOCK)
        ON a.cfId = b.cfId
           AND a.OrganizeId = b.OrganizeId
    LEFT JOIN NewtouchHIS_Base..xt_ypyf yf
        ON yf.yfCode = b.yfCode
    LEFT JOIN NewtouchHIS_Base..xt_yzpc pc
        ON pc.yzpcCode = b.pcCode
           AND pc.OrganizeId = @orgId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Department c
        ON b.zxks = c.Code
           AND b.OrganizeId = c.OrganizeId
    LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] d
        ON a.ys = d.gh
           AND a.OrganizeId = d.OrganizeId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Department e
        ON a.ks = e.Code
           AND a.OrganizeId = e.OrganizeId
WHERE a.zt = 1
      AND b.zt = 1
      AND b.yfCode IN (
                          SELECT * FROM dbo.f_split(@str, ',')
                      )
      AND a.cfId = @cfId
      AND a.OrganizeId = @orgId
      AND EXISTS
(
    SELECT 1
    FROM xt_jz d WITH (NOLOCK)
    WHERE a.jzId = d.jzId
          AND a.OrganizeId = d.OrganizeId
          AND d.mzh = @mzh
)) mx
        LEFT JOIN #zhfz zhfz ON mx.zh = zhfz.zh 
      ORDER BY mx.zh, mx.zhNum
DROP TABLE #zhfz";
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@cfId", cfId));
            par.Add(new SqlParameter("@mzh", mzh));
            dto.zsd_xmInfo = this.FindList<zsd_xmInfo>(sql, par.ToArray());
            orgInfo i = new orgInfo();
            i.Name = GetorgNameById(orgId);
            dto.orgInfo = i;
            return dto.ToJson();
        }

        protected string GetorgNameById(string orgId) {
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            string sql = @"SELECT  Name
                    FROM NewtouchHIS_Base..Sys_Organize
                    WHERE   Id = @orgId";
            return this.FirstOrDefault<string>(sql, par.ToArray());
        }

        /// <summary>
        /// 门诊处方单
        /// </summary>
        /// <param name="cfId"></param>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        public string GetZCYCFjson(string cfId, string mzh, string orgId)
        {
            OutpatientZCYCFDto dto = new OutpatientZCYCFDto();
            if (string.IsNullOrWhiteSpace(cfId))
            {
                throw new FailedException("处方主键为空");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号为空");
            }
            dto.PatientInfo = GetOutpatientInfo(orgId, mzh);
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@cfId", cfId));
            dto.zycf_xmInfo = this.FindList<zycf_xmInfo>(@"declare @list table(
            uuid int,
            mc varchar(50)
        )
insert @list
select ROW_NUMBER() over(order by cfid) as uuid, ypmc + cast(mcjl as varchar(50)) + mcjldw mc from xt_cfmx where cfid = @cfId and OrganizeId = @orgId order by createtime desc

declare @list1 table(
            mc1 varchar(50),
            mc2 varchar(50),
            mc3 varchar(50)
        )


        declare @i int
        declare @max int

        set @i = 1

        select @max = max(uuid) from @list


            while @i <= @max

        begin
        declare @mc1 varchar(50);
            declare @mc2 varchar(50);
            declare @mc3 varchar(50);

            select @mc1 = mc from @list where uuid = @i;
            if @i + 1 <= @max
    
        select @mc2 = mc from @list where uuid = @i + 1;

            if @i + 2 <= @max
    
        select @mc3 = mc from @list where uuid = @i + 2;

            insert into @list1 values(@mc1, @mc2, @mc3);
            set @i+= 3;
            set @mc1 = '';
            set @mc2 = '';
            set @mc3 = '';
            end
        select* from @list1", par.ToArray());
            orgInfo i = new orgInfo();
            i.Name = GetorgNameById(orgId);
            dto.orgInfo = i;
            var par2 = new List<SqlParameter>();
            par2.Add(new SqlParameter("@orgId", orgId));
            par2.Add(new SqlParameter("@cfId", cfId));
            dto.cfd_Info = this.FirstOrDefault<cfd_Info>(@"select zje je,CreateTime,tieshu,a.cfh,b.yfmc from xt_cf a
            left join NewtouchHIS_Base..V_S_xt_ypyf b on a.cfyf=b.yfCode 
            where cfid=@cfId and OrganizeId=@orgId", par2.ToArray());

            return dto.ToJson();
        }


        #region 报表工具
        public IList<ReportConfigVO> ReportList(Pagination pagination, string orgId,string rptname,string keyword)
        {
            string sql = @"select [Id],[OrganizeId],[AppId],[RptName],[RptHost],[RptUrl],[RptSource]
,[Paras],[Tools],[CreateTime],[CreatorCode],[LastModifyTime],[LastModifierCode],[px],[zt],RptCode
from [NewtouchHIS_Base].dbo.ReportSvsConfig with(nolock)
where OrganizeId = @orgId and zt = '1'";
            if (!string.IsNullOrWhiteSpace(rptname))
            {
                sql += " and RptName=@rptname ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and RptName like @keyword ";
            }

            return FindList<ReportConfigVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@rptname",rptname??""),
                new SqlParameter("@keyword",string.IsNullOrWhiteSpace(keyword)==true?"":"%"+keyword+"%"),
            });
        }

        public ReportConfigVO ReportConfInfo(string orgId, string keyValue)
        {
            string sql = @"select [Id],[OrganizeId],[AppId],[RptName],[RptHost],[RptUrl],[RptSource]
,[Paras],[Tools],[CreateTime],[CreatorCode],[LastModifyTime],[LastModifierCode],[px],[zt],RptCode
from [NewtouchHIS_Base].dbo.ReportSvsConfig with(nolock)
where OrganizeId = @orgId and zt = '1'";
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                sql += " and (Id=@keyValue or  RptCode=@keyValue) ";
            }

            ReportConfigVO ety = FirstOrDefault<ReportConfigVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@keyValue",keyValue??"")
            });
            if (ety != null)
            {
                string[] plist = ety.Paras.Split(',');
                if (plist != null && plist.Length > 0)
                {
                    ety.Paralist = new List<ReportPara>();
                    foreach (string item in plist)
                    {
                        ReportPara rdata = new ReportPara();
                        rdata.p = item;
                        ety.Paralist.Add(rdata);
                    }
                }
            }

            return ety;
        }

        public void ReportAdd(ReportConfigVO ety,string keyValue,string rygh)
        {
            string sql = "select 1 from [NewtouchHIS_Base].dbo.ReportSvsConfig where OrganizeId=@orgId and zt='1' and RptCode='" + ety.RptCode+"'";
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                sql += " and Id<>@rptId ";
            }                
            int ck = ExecuteSqlCommand(sql, new SqlParameter[] {
                    new SqlParameter("orgId",ety.OrganizeId),
                    new SqlParameter("rptId",keyValue??""),
                });
            if (ck > 0)
            {
                throw new FailedException("报表代码已存在！");
            }

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                sql = @"update a
		set a.AppId=@appId,
		a.RptName=@rptName,
		a.RptHost=@rptHost,
		a.RptUrl=@rptUrl,
		a.RptSource=@rptSource,
		a.Paras=@paras,
		a.Tools=@tools,
        a.RptCode=@rptcode,
		a.[LastModifyTime]=getdate(),
		a.[LastModifierCode]=@rygh
		from [NewtouchHIS_Base].dbo.ReportSvsConfig a
		where a.id=@rptId and a.OrganizeId=@orgId and a.zt='1'
";
            }
            else
            {
                sql = @"insert into [NewtouchHIS_Base].dbo.ReportSvsConfig([Id],[OrganizeId],[AppId],[RptName],[RptHost],[RptUrl],
		[RptSource],[Paras],[Tools],[CreateTime],[CreatorCode],[LastModifyTime],
		[LastModifierCode],[px],[zt],RptCode)
		select newid(),
		@orgId,
		@appId,
		@rptName,
		@rptHost,
		@rptUrl,
		@rptSource,
		@paras,
		@tools,getdate(),@rygh,null,null,null,'1',@rptcode
";
            }

            try
            {
                ExecuteSqlCommand(sql, new SqlParameter[] {
                    new SqlParameter("orgId",ety.OrganizeId),
                    new SqlParameter("appId",ety.AppId),
                    new SqlParameter("rptName",ety.RptName),
                    new SqlParameter("rptHost",ety.RptHost),
                    new SqlParameter("rptUrl",ety.RptUrl),
                    new SqlParameter("rptSource",ety.RptSource),
                    new SqlParameter("paras",ety.Paras??""),
                    new SqlParameter("tools",ety.Tools??""),
                    new SqlParameter("rygh",rygh),
                    new SqlParameter("rptId",keyValue??""),
                    new SqlParameter("rptcode",ety.RptCode)
                });
            }
            catch (Exception ex)
            {
                throw new FailedException("保存失败！" + ex.Message);
            }
            
        }


        public void ReportDelete(string keyValue, string rygh,string orgId)
        {
            string sql = @"update a set a.zt='0',		
a.[LastModifyTime]=getdate(),
a.[LastModifierCode]=@rygh
from [NewtouchHIS_Base].dbo.ReportSvsConfig a 
where a.id=@rptId and a.OrganizeId=@orgId and a.zt='1'
";
            ExecuteSqlCommand(sql, new SqlParameter[] {
                    new SqlParameter("orgId",orgId),
                    new SqlParameter("rptId",keyValue),
                    new SqlParameter("rygh",rygh),
                });

        }
        #endregion


    }
}

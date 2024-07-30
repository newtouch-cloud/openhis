using System;
using System.Collections.Generic;
using Newtouch.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Data.SqlClient;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common.Operator;
using System.Linq;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class SysReportDmnService : RepositoryBase<SysReportTemplateEntity>, ISysReportDmnService
    {
        public SysReportDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<ReortListVO> GetReportTree(string keyword, string ReportType)
        {

            var sql = @"
select * from (
select convert(varchar(50),a.reportid) id,
convert(varchar(50),case parentId when 0 then null else parentId end) parentId,
case parentId when 0 then a.SystemName else a.ReportName end ReportName,
'0' ismx,
'1' isty,
convert(varchar(50),a.ReportCode)  ReportCode
,convert(varchar(50),case a.DirectoryFlag when 1 then 0 else a.reporttype end) reporttype
from dbo.Sys_Report a
where a.zt='1'
union all
select convert(varchar(50),'mx'+convert(varchar(50),b.templateid)) id,
convert(varchar(50),c.ReportID) parentId,
b.ReportNameDes ReportName,
convert(varchar(50),templateid) ismx,
convert(varchar(50),b.ReportStatus) isty,
convert(varchar(50),b.ReportCode) ReportCode
,convert(varchar(50),case c.DirectoryFlag when 1 then 0 else c.reporttype end) reporttype
 from Sys_Report c
 inner join  dbo.Sys_ReportTemplate b on b.ReportCode=c.ReportCode and b.HospitalCode=c.HospitalCode and c.zt=b.zt
 where b.zt='1'  and c.zt='1'
 ) b 
where 1=1
 ";
            if (ReportType != null && ReportType != "")
            {
                sql += @" and (b.reporttype=" + ReportType + " or b.reporttype=0) ";
            }
            var par = new List<SqlParameter>();
            if (keyword != null && keyword != "")
            {
                sql += @" and b.reportname like '%" + keyword + @"%' union all
select convert(varchar(50),a.reportid) id,
convert(varchar(50),case parentId when 0 then null else parentId end) parentId,
case parentId when 0 then a.SystemName else a.ReportName end ReportName,
'0' ismx,
'1' isty,
convert(varchar(50), a.ReportCode)  ReportCode
,convert(varchar(50),case a.DirectoryFlag when 1 then 0 else a.reporttype end) reporttype
  from dbo.Sys_Report a
 where a.zt = '1' and a.ParentId = 0
 ";
            }
            return this.FindList<ReortListVO>(sql, par.ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<ReortListVO> GetReportConcreteTree(int reportID)
        {

            var sql = @"
select * from dbo.Sys_Report a where parentid=@reportID and zt=1 order by px";

            var pars = new List<SqlParameter>()
            {
                new SqlParameter("@reportID",reportID),
            };
            return this.FindList<ReortListVO>(sql, pars.ToArray());
        }
        /// <summary>
        /// 获取 报表明细内容
        /// </summary>
        /// <returns></returns>
        public ReortMXListVO GetReportMXTree(string ReportId)
        {

            var sql = @"select ReportCode,ReportName,SystemCode,SystemName,HospitalCode from Sys_Report where zt='1' and ReportID=@ReportId";
            var pars = new List<SqlParameter>()
            {
                new SqlParameter("@ReportId",ReportId),
            };
            return this.FirstOrDefault<ReortMXListVO>(sql, pars.ToArray());
        }
        /// <summary>
        /// 获取 设计查询
        /// </summary>
        /// <returns></returns>
        public ReortMXListVO GetReportMXData(string ReportId)
        {

            var sql = @"select '1' ly,a.TemplateID ReportID,a.ReportCode,a.SystemCode,a.HospitalCode,ReportNameDes ReportName,b.ReportName mc from Sys_ReportTemplate a
left join Sys_Report b on a.ReportCode=b.ReportCode
 where a.zt='1' and TemplateID=@ReportId";
            var pars = new List<SqlParameter>()
            {
                new SqlParameter("@ReportId",ReportId),
            };
            return this.FirstOrDefault<ReortMXListVO>(sql, pars.ToArray());
        }
        /// <summary>
        /// 新增明细报表
        /// </summary>
        /// <param name="ReortMXListVO"></param>
        /// <returns></returns>
        public string SubmitForm(ReortMXListVO ReortMXListVO, string Type)
        {
            var reportTemplateEntity = new SysReportTemplateEntity();
            try
            {
                using (var db = new Infrastructure.EFDbTransaction(this._databaseFactory).BeginTrans())
                {


                    if (Type != null && Type == "update")
                    {
                        var Report = db.IQueryable<SysReportTemplateEntity>().FirstOrDefault(p => p.TemplateID == ReortMXListVO.ReportID);
                        Report.HospitalCode = ReortMXListVO.HospitalCode;
                        Report.ReportNameDes = ReortMXListVO.ReportName;
                        Report.LastModifyTime = DateTime.Now;
                        Report.LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                        db.Update(Report);
                    }
                    else
                    {
                        reportTemplateEntity.ReportCode = ReortMXListVO.ReportCode;
                        reportTemplateEntity.ReportNameDes = ReortMXListVO.ReportName;
                        reportTemplateEntity.HospitalCode = ReortMXListVO.HospitalCode;
                        reportTemplateEntity.SystemCode = ReortMXListVO.SystemCode;
                        reportTemplateEntity.zt = 1;
                        reportTemplateEntity.ReportStatus = 0;
                        reportTemplateEntity.Content = @"<?xml version=""1.0"" encoding=""utf-8""?><Report xmlns=""http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition""><dd:Name xmlns:dd=""http://schemas.datadynamics.com/reporting/2005/02/reportdefinition""> 报表1</dd:Name><Body><Height>5cm</Height></Body><BottomMargin>2.5cm</BottomMargin><LeftMargin>2.5cm</LeftMargin><PageHeight>29.7cm</PageHeight><PageWidth>21cm</PageWidth><RightMargin>2.5cm</RightMargin><TopMargin>2.5cm</TopMargin><Width>16cm</Width></Report>";
                        reportTemplateEntity.CreateTime = DateTime.Now;
                        reportTemplateEntity.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                        db.Insert(reportTemplateEntity);
                    }
                    db.Commit();
                }
            }
            catch (Exception ex)
            {
                return "" + ex.Message
                    ;
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReortMXListVO"></param>
        /// <returns></returns>
        public string SubmitFormMain(ReortMXListVO ReortMXListVO)
        {
            var reportEntity = new SysReportEntity();
            try
            {

                using (var db = new Infrastructure.EFDbTransaction(this._databaseFactory).BeginTrans())
                {

                    var sql = @"(select top 1 convert(int,ReportCode)+1 ReportCode from sys_report)  order by ReportCode desc";
                    var pars = new List<SqlParameter>() { };
                    var ReportCode = this.FirstOrDefault<int>(sql, pars.ToArray());
                    var Report = db.IQueryable<SysReportEntity>().FirstOrDefault(p => p.ReportCode == ReportCode.ToString());
                    if (Report != null)
                    {
                        return "报表编码重复！";
                    }
                    reportEntity.ReportCode = ReportCode.ToString();
                    reportEntity.ReportName = ReortMXListVO.ReportName;
                    reportEntity.HospitalCode = ReortMXListVO.HospitalCode;
                    reportEntity.SystemCode = ReortMXListVO.SystemCode;
                    reportEntity.ReportDes = ReortMXListVO.ReportName;
                    reportEntity.SystemName = ReortMXListVO.SystemName;
                    reportEntity.PinYin = ReortMXListVO.ReportName;
                    reportEntity.ReportType = int.Parse(ReortMXListVO.ReportType);
                    if (ReortMXListVO.px != null && ReortMXListVO.px != "")
                    {
                        reportEntity.px = int.Parse(ReortMXListVO.px);
                    }
                    else
                    {
                        reportEntity.px = null;
                    }
                    reportEntity.StatusTemplateID = null;
                    reportEntity.ParentId = ReortMXListVO.ReportID;
                    reportEntity.DirectoryFlag = 0;
                    reportEntity.zt = 1;
                    reportEntity.CreateTime = DateTime.Now;
                    reportEntity.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                    db.Insert(reportEntity);
                    db.Commit();
                }
            }
            catch (Exception ex)
            {
                return "" + ex.Message
                    ;
            }
            return "";
        }
        public string ReportStop(string ReportId, string ReportStatus)
        {
            try
            {
                if (ReportStatus == "1")
                {
                    var sql = @"
select * from Sys_ReportTemplate where ReportCode in (
select ReportCode from dbo.Sys_ReportTemplate a where TemplateID=@ReportId and zt=1)
and ReportStatus=1 and zt=1
";

                    var pars = new List<SqlParameter>()
            {
                new SqlParameter("@ReportId",ReportId),
            };
                    var Reort = this.FindList<ReortListVO>(sql, pars.ToArray());
                    if (Reort.Count > 0)
                    {
                        return "只能启用一个报表！";
                    }


                }
                using (var db = new Infrastructure.EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    int reportid = int.Parse(ReportId);
                    var ReportTemplate = db.IQueryable<SysReportTemplateEntity>().FirstOrDefault(p => p.TemplateID == reportid);

                    ReportTemplate.ReportStatus = int.Parse(ReportStatus);
                    db.Update(ReportTemplate);
                    db.Commit();
                }
            }
            catch (Exception ex)
            {
                return "" + ex.Message
                    ;
            }
            return "";
        }
        /// <summary>
        /// 明细报表删除
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        public string ReportDel(string ReportId)
        {
            try
            {
                using (var db = new Infrastructure.EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    int reportid = int.Parse(ReportId);
                    var ReportTemplate = db.IQueryable<SysReportTemplateEntity>().FirstOrDefault(p => p.TemplateID == reportid);
                    ReportTemplate.zt = 0;
                    ReportTemplate.LastModifyTime = DateTime.Now;
                    ReportTemplate.LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                    db.Update(ReportTemplate);
                    db.Commit();
                }
            }
            catch (Exception ex)
            {
                return "" + ex.Message
                    ;
            }
            return "";
        }
        /// <summary>
        /// 主报表删除
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        public string ReportDelMain(string ReportId)
        {
            try
            {
                using (var db = new Infrastructure.EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    int reportid = int.Parse(ReportId);
                    var Report = db.IQueryable<SysReportEntity>().FirstOrDefault(p => p.ReportID == reportid);
                    Report.zt = 0;
                    Report.LastModifyTime = DateTime.Now;
                    Report.LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                    db.Update(Report);
                    db.Commit();
                }
            }
            catch (Exception ex)
            {
                return "" + ex.Message
                    ;
            }
            return "";
        }
    }
}

using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.DTO;
using Newtouch.MR.ManageSystem.Domain.DTO.OutputDto;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.MR.ManageSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.MR.ManageSystem.DomainServices
{
    public class CommonDmnService: DmnServiceBase, ICommonDmnService
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        public CommonDmnService(IDefaultDatabaseFactory databaseFactory, ISysConfigRepo SysConfigRepo)
            : base(databaseFactory)
        {
            _sysConfigRepo = SysConfigRepo;
        }
        
        public IList<SysZdListDto> ZdList(string orgId, string keyword)
        {
            string sql = @"	select zdCode,zdmc,icd10,py
	from [NewtouchHIS_Base].[dbo].[xt_zd] with(nolock)
	where zt='1' and organizeid in('*',@orgId) ";

            return this.FindList<SysZdListDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId ??""),
                new SqlParameter("@keyword",keyword ??"")
            });
        }
        /// <summary>
        /// 手术字典列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysOpListDto> OpList(string orgId, string keyword, bool type)
        {
            string sql = "";
            string dd = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss");
            var showItems = _sysConfigRepo.GetByCode("ShowOpItemsCount", orgId);

            if (type && string.IsNullOrWhiteSpace(keyword))
            {
                string count = showItems == null ? "100" : showItems.Value;
                sql = "select top " + count + " ssdm,ssmc,zjm,ssjb from [Newtouch_OR].[dbo].[OR_Operation] a with(nolock) " +
                    "where OrganizeId=@orgId and zt='1'" +
                    " and exists(select SSJCZBM,SSJCZMC from mr_basy_ss b with(nolock) where b.OrganizeId=@orgId and b.CreateTime > @date and a.ssdm=b.SSJCZBM and b.zt='1' group by SSJCZBM,SSJCZMC)";

                var list = this.FindList<SysOpListDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@date",dd) });

                if (list == null || list.Count < Convert.ToInt32(count))
                {
                    sql = @"select top " + count + " ssdm,ssmc,zjm,ssjb from [Newtouch_OR].[dbo].[OR_Operation] with(nolock) where OrganizeId=@orgId and zt='1' ";
                }
                else
                {
                    return list;
                }
            }
            else
            {
                sql = @"select ssdm,ssmc,zjm,ssjb from [Newtouch_OR].[dbo].[OR_Operation] with(nolock) where OrganizeId=@orgId and zt='1' ";
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (ssdm like @kwd or ssmc like @kwd or zjm like @kwd)";
            }
            return this.FindList<SysOpListDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@kwd",string.IsNullOrWhiteSpace(keyword)==true? "":keyword+"%"),
                new SqlParameter("@date",dd)
            });
        }

        /// <summary>
        /// 麻醉方式列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysAnesListDto> AnesList(string orgId, string keyword)
        {
            string sql = @"SELECT   OrganizeId, AnesCode, AnesName, Aneszjm
from [Newtouch_OR].dbo.V_SYS_Anesthesia with(nolock)
where OrganizeId=@orgId ";
            if(!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ((charindex(@kwd,AnesCode)>0) or (charindex(@kwd,AnesName)>0) or (charindex(@kwd,Aneszjm)>0)) ";
            }

            return this.FindList<SysAnesListDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@kwd",keyword??"")
            });
        }
        /// <summary>
        /// 切口等级
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysDicDto> NotchGradeList(string orgId, string keyword)
        {
            string sql = @"SELECT   Code,Name,OrganizeId
from V_OR_SYS_NotchGrade with(nolock)
where OrganizeId=@orgId ";
            if(!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ((charindex(@kwd,Code)>0) or (charindex(@kwd,Name)>0)) ";
            }

            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@kwd",keyword??"")
            });
        }
        /// <summary>
        /// 通用字典
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="Rlue"></param>
        /// <returns></returns>
        public IList<SysDicDto> DicCommonList(string orgId, string keyword,string rule,string code=null)
        {
            string sql = @"SELECT ItemCode  Code,ItemName Name,OrganizeId,[order]
from [mr_dic_common] with(nolock)
where OrganizeId=@orgId ";
            if(!string.IsNullOrWhiteSpace(rule))
            {
                sql += " and RlueCode=@rule "; 
            }
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += " and ItemCode=@code ";
            }
            else if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ((charindex(@kwd,ItemCode)>0) or (charindex(@kwd,ItemName)>0)) ";
            }

            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@rule",rule??""),
                new SqlParameter("@kwd",keyword??""),
                new SqlParameter("@code",code??"")
            });
        }

        public IList<SysBrxzVO>BrxzList(string orgId,string keyword)
        {
            string sql = @"select brxz,brxzmc,ybjylx,mzzybz 
from [NewtouchHIS_Sett].dbo.xt_brxz with(nolock)
where organizeid=@orgId and zt='1'";

            if(!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (charindex(@keyword,brxz)>0 or charindex(@keyword,brxzmc)>0 )";
            }

            return this.FindList<SysBrxzVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@keyword",keyword??"")
            });
        }

        public IList<SysDicDto> DicNationalityList(string orgId, string keyword)
        {
            string sql = @"select gjcode Code,gjmc Name,py 
from [NewtouchHIS_Base].[dbo].[xt_gj] with(nolock)
where zt='1' ";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ((charindex(@kwd,gjcode)>0) or (charindex(@kwd,gjmc)>0) or (charindex(@kwd,py)>0)) ";
            }

            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@kwd",keyword??"")
            });
        }
        /// <summary>
        /// 民族
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysDicDto> DicNationsList(string orgId, string keyword)
        {
            string sql = @"select mzcode Code,mzmc Name,py 
from [NewtouchHIS_Base].[dbo].[V_S_xt_mz] with(nolock)
where zt='1' ";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ((charindex(@kwd,mzcode)>0) or (charindex(@kwd,mzmc)>0) ) ";
            }

            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@kwd",keyword??"")
            });
        }


        /// <summary>
        /// 获取病历类型对应表
        /// </summary>
        /// <param name="bllxId"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public string GetBllxTB(string bllx)
        {
            string bllxtb = "";
            if (bllx == EnumBllx.zybl.GetHashCode().ToString())
            {
                bllxtb = " bl_rybl ";
            }
            else if (bllx == EnumBllx.bcjl.GetHashCode().ToString())
            {
                bllxtb = " bl_bcjl ";
            }
            else if (bllx == EnumBllx.hljl.GetHashCode().ToString())
            {
                bllxtb = " bl_hljl ";
            }
            else if (bllx == EnumBllx.basy.GetHashCode().ToString())
            {
                bllxtb = " bl_basy ";
            }
            else if (bllx == EnumBllx.kfpg.GetHashCode().ToString())
            {
                bllxtb = " bl_basy ";
            }
            else
            {
                bllxtb = " bl_zqws ";
            }
            return bllxtb;
        }

        /// <summary>
        /// 根据DutyCode（职位）查询员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT DISTINCT
                                StaffName ,
                                StaffPY ,
                                StaffGh ,
                                c.Code ks ,
                                c.py kspy ,
                                c.Name ksmc
                        FROM    [NewtouchHIS_Base]..V_C_Sys_StaffDuty(nolock) AS A
                                LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) AS b ON a.StaffGh = b.gh and b.zt = '1' and b.OrganizeId = @OrganizeId
                                INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Department(nolock) AS C ON b.DepartmentCode = C.Code
                                                              AND C.OrganizeId = @OrganizeId
                                                              WHERE A.zt = '1'  AND a.OrganizeId = @OrganizeId");
            var par1 = new List<SqlParameter>() {
                     new SqlParameter("@OrganizeId",orgId),
                     new SqlParameter("@DutyCode",dutyCode??"")};
            if(!string.IsNullOrWhiteSpace(dutyCode))
            {
                strSql.Append("  and DutyCode = @DutyCode ");
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSql.Append(" and (StaffName like @searchkeyword or StaffPY like @searchkeyword or StaffGh like @searchkeyword)");
                par1.Add(new SqlParameter("searchkeyword", "%" + keyword + "%"));
            }
            return this.FindList<SysDutyStaffVO>(strSql.ToString(), par1.ToArray());
        }

        public IList<SysDicDto>GetDeptList(string orgId,string code)
        {
            string sql = @"
select Code,[Name]
from [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] with(nolock)
where OrganizeId=@orgId and zt='1' ";

            if(!string.IsNullOrWhiteSpace(code))
            {
                sql += " and Code=@code ";
            }
            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@code",code)
            });
        }

        public IList<HisSfdlVO> GetHisSfdl(string orgId,string keyword)
        {
            string sql = @"select  dlcode, dlmc,convert(varchar(10),mzzybz)mzzybz,convert(varchar(10),dllb)dllb
from  [NewtouchHIS_Base].[dbo].xt_sfdl with(nolock)
where zt='1' and  OrganizeId=@orgId ";
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and dlmc like @keyword ";
            }
            return this.FindList<HisSfdlVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@keyword","%"+keyword+"%")
            });
        }
    }
}

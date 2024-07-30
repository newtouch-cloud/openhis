using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.DTO.InputDto.MRHomePage;
using Newtouch.EMR.Domain.DTO.OutputDto.MRHomePage;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;

namespace Newtouch.EMR.DomainServices
{
    public class CommonDmnService: DmnServiceBase, ICommonDmnService
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IMrbasyzdRepo _MrbasyzdRepo;
        private readonly Ibl_bllxRepo _BllxRepo;



        public CommonDmnService(IDefaultDatabaseFactory databaseFactory,  ISysConfigRepo SysConfigRepo)
            : base(databaseFactory)
        {
            _sysConfigRepo = SysConfigRepo;
        }

        public IList<MedRecordTypeVO> GetSysItemDic(string OrgId, string Code, string bllxId, string DetailCode = null)
        {
            string sql = @"select Id,Name,Code,px
                        from [NewtouchHIS_Base].[dbo].[Sys_ItemsDetail] a with(nolock)
                        where exists(select 1 from [NewtouchHIS_Base].[dbo].[Sys_Items] b with(nolock)
			                        where a.itemid=b.id and b.code=@code and b.zt=1)
                        and( OrganizeId='*' or OrganizeId=@OrgId)
                        and a.zt=1";

            if (!string.IsNullOrWhiteSpace(bllxId))
            {
                sql += " and Id=@Id ";
            }
            if (!string.IsNullOrWhiteSpace(DetailCode))
            {
                sql += " and Code=@detailCode ";
            }
            return this.FindList<MedRecordTypeVO>(sql, new SqlParameter[] {
                            new SqlParameter("@OrgId", OrgId),
                            new SqlParameter("@code", Code),
                            new SqlParameter("@Id", bllxId==null?"":bllxId),
                            new SqlParameter("@detailCode", DetailCode==null?"":DetailCode)
                });
        }


        #region bllx string
        public string GetBllxTB(string bllx)
        {
            string sql = @"";
            return "";
        }

        /// <summary>
        /// 病历类型+岗位权限控制
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bllx"></param>
        /// <param name="dutycode"></param>
        /// <returns></returns>
        public IList<SysDicMarkDto> GetDutyListRelbllx(string orgId, string bllx, string dutycode)
        {
            string sql = @"
select a.Id,a.Code,a.[Name] ,(case when b.id>'' then 1 else 0 end)ischeck
from  [NewtouchHIS_Base].[dbo].[V_S_Sys_Duty] a(nolock) 
left join bl_bllx b on CHARINDEX(','+a.code+',',','+b.RelDutys)>0 and b.Id=@bllx and b.isroot='1'
where a.zt='1'
order by b.id desc ";

            if (!string.IsNullOrWhiteSpace(dutycode))
            {
                sql += " and ( Code=@code or [Name]=@code)  ";
            }
            return this.FindList<SysDicMarkDto>(sql, new SqlParameter[] {
                new SqlParameter("@code",dutycode ?? ""),
                new SqlParameter("@bllx",bllx ?? "")
            });
        }
        /// <summary>
        /// 根据关键字查询病历类型列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bllx"></param>
        /// <param name="keyword"></param>
        /// <param name="type">1 root 2 子目录</param>
        /// <returns></returns>
        public IList<BaseBllxVO> GetBllxListDetail(string orgId, string bllx = null, string keyword = null, string type = null)
        {
            string sql = @"SELECT a.[Id]
,a.[bllx]
,a.[bllxmc]
,a.[CreateTime]
,a.[CreatorCode]
,a.[LastModifyTime]
,a.[LastModifierCode]
,a.[zt]
,isnull(a.RelDutys,r.RelDutys)RelDutys
,(select b.[name]+'，' from [NewtouchHIS_Base].[dbo].[V_S_Sys_Duty] b where charindex(','+b.code+',',','+isnull(a.RelDutys,r.RelDutys))>0 for xml path('')) [RelDutysDesc]
,a.[OrganizeId]
,isnull(a.[bllxcode],r.[bllxcode])[bllxcode]
,a.[px],
isnull(a.[relTB],r.[relTB])[relTB],
isnull(a.MenuLev,r.MenuLev)MenuLev,a.ParentId,a.IsRoot,a.mzbz
FROM [dbo].[bl_bllx] a with(nolock)
left join [dbo].[bl_bllx] r with(nolock) on a.ParentId=r.Id 
where a.[OrganizeId]=@orgId and a.zt='1'
";
            if (type == "1")
            {
                sql += " and a.IsRoot=1 ";
                if (!string.IsNullOrWhiteSpace(bllx))
                {
                    sql += " and (a.Id=@bllx or a.bllx=@bllx) ";
                }
            }
            else if (type == "2")
            {
                sql += " and a.IsRoot=0 ";
                if (!string.IsNullOrWhiteSpace(bllx))
                {
                    sql += " and a.ParentId=@bllx ";
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(bllx))
                {
                    sql += " and (a.Id=@bllx or a.bllx=@bllx) ";
                }
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sql += " and a.bllxmc like @keyword ";
                }
            }

            sql += "  order by a.px ";
            return this.FindList<BaseBllxVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@bllx", bllx??""),
                new SqlParameter("@keyword", keyword??"")
            });
        }

        #endregion


        #region bllx old(int)
        /// <summary>
        /// 根据病历类型Id获取病历大类名称标识
        /// </summary>
        /// <param name="bllxId"></param>
        /// <param name="OrgId"></param>
        /// <returns>bllx --0 zybl,1 bcjl,2 ylws,3 hljl,4 basy</returns>
        public string GetBllxmcFlag(string bllxId, string OrgId)
        {
            string bllxmc = "";
            var bllxEty = GetSysItemDic(OrgId, "MedRecordDocuments", bllxId).FirstOrDefault();
            bllxmc = bllxEty.Code;
            return bllxmc;
        }

        /// <summary>
        /// 获取病历类型对应表
        /// </summary>
        /// <param name="bllxId"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public string GetBllxTB(int bllx)
        {
            string bllxtb = "";
            if (bllx == EnumBllx.zybl.GetHashCode())
            {
                bllxtb = " bl_rybl ";
            }
            else if (bllx == EnumBllx.bcjl.GetHashCode())
            {
                bllxtb = " bl_bcjl ";
            }
            else if (bllx == EnumBllx.hljl.GetHashCode())
            {
                bllxtb = " bl_hljl ";
            }
            else if (bllx == EnumBllx.basy.GetHashCode())
            {
                bllxtb = " mr_basy ";
            }
            else if (bllx == EnumBllx.kfpg.GetHashCode())
            {
                bllxtb = " bl_patrecords ";
            }
            else
            {
                bllxtb = " bl_zqws ";
            }
            return bllxtb;
        }

        /// <summary>
        /// 通过bllx获取病历大类相关信息
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="Code"></param>
        /// <param name="bllxId"></param>
        /// <returns></returns>
        public IList<MedRecordTypeVO> GetSysItemDicBybllx(string Code, int bllx)
        {
            string bllxCode = "";
            if (bllx == EnumBllx.zybl.GetHashCode())
            {
                bllxCode = EnumBllx.zybl.ToString();
            }
            else if (bllx == EnumBllx.bcjl.GetHashCode())
            {
                bllxCode = EnumBllx.bcjl.ToString();
            }
            if (bllx == EnumBllx.ylws.GetHashCode())
            {
                bllxCode = EnumBllx.ylws.ToString();
            }
            if (bllx == EnumBllx.hljl.GetHashCode())
            {
                bllxCode = EnumBllx.hljl.ToString();
            }
            if (bllx == EnumBllx.basy.GetHashCode())
            {
                bllxCode = EnumBllx.basy.ToString();
            }
            if (bllx == EnumBllx.kfpg.GetHashCode())
            {
                bllxCode = EnumBllx.kfpg.ToString();
            }

            string sql = @"select Id,Name,Code,px
                        from [NewtouchHIS_Base].[dbo].[Sys_ItemsDetail] a with(nolock)
                        where exists(select 1 from [NewtouchHIS_Base].[dbo].[Sys_Items] b with(nolock)
			                        where a.itemid=b.id and b.code=@code and b.zt=1)
                        and a.zt=1";

            if (!string.IsNullOrWhiteSpace(bllxCode))
            {
                sql += " and Code='" + bllxCode + "'";
            }

            return this.FindList<MedRecordTypeVO>(sql, new SqlParameter[] {
                            new SqlParameter("@code", Code)
                });
        }

        #endregion


        /// <summary>
        /// 获取模板对应岗位信息
        /// </summary>
        /// <returns></returns>
        public IList<BlmbqxkzEntity> GetblmbDuty(string blmbId, OperatorModel user)
        {
            string sql = @"select isnull(b.Id,newid())Id,mbId,a.Code dutyCode,a.Name dutyName,case when ''='' and (a.Code='Doctor' or a.Code='Nurse')   then 2 else isnull( ctrlLevel,0) end ctrlLevel,case when ''='' and (a.Code='Doctor' or a.Code='Nurse')   then '读写权限' else  ctrlLevelDesc end ctrlLevelDesc,
                            getdate() CreateTime ,@User CreatorCode,null LastModifyTime,null LastModifierCode,isnull(b.zt,1) zt,@OrgId OrganizeId
                            from [NewtouchHIS_Base].[dbo].[V_S_Sys_Duty] a with(nolock) 
                            left join [bl_mbqxkz] b with(nolock) on a.Code=b.dutyCode and b.mbid=@mbId and b.OrganizeId=@OrgId 
                            where a.zt=1";
            return this.FindList<BlmbqxkzEntity>(sql, new SqlParameter[] { new SqlParameter("@mbId", blmbId) ,
                                                                           new SqlParameter("@User", user.rygh),
                                                                           new SqlParameter("@OrgId", user.OrganizeId) });

        }
        /// <summary>
        /// 判断角色是否为病历模板管理员
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool GetIsblManager(OperatorModel user)
        {
            bool isbladmin = false;
            var para = new List<SqlParameter>();
            string sql = @"select a.userId,b.name RoleName,b.Code RoleCode 
                            from Sys_UserRole a with(nolock) ,Sys_Role b with(nolock)
                            where a.roleId = b.Id
                            and a.UserId = @userId and a.zt = 1 and b.Code='blmanager'";
            para.Add(new SqlParameter("@userId", user.UserId));

            DataTable dt = SqlQueryForDataTatable(sql, para.ToArray());
            if (dt != null && dt.Rows.Count > 0)
            {
                isbladmin = true;
            }

            return isbladmin;
        }


        public IList<DiagnoseDTO> GetList(string orgId)
        {
            string sql = @"select organizeid orgId,zdcode zdCode,zdmc zdName,icd10,zdlx 
                from [NewtouchHIS_Base].dbo.xt_zd with(nolock)
                where zt='1'
                and (organizeid=@orgId or organizeid='*' ) ";
            return this.FindList<DiagnoseDTO>(sql, new SqlParameter[] { new SqlParameter("@orgId", orgId) });

        }
        public IList<DiagnoseDTO> GetList(Pagination pagination, string orgId, string keyword,int? zdlx,string ybnhlx="")
        {
            string zdlxmc = "";
            string sql = @"select top 200 zdCode,zdmc zdName,icd10,zdlx from [NewtouchHIS_Base]..V_S_xt_zd(nolock) 
where (OrganizeId = '*' or OrganizeId = @orgId) 
and zt = '1'
";
            //string sql = @"select organizeid orgId,zdcode zdCode,zdmc zdName,icd10,zdlx 
            //    from [NewtouchHIS_Base].dbo.xt_zd with(nolock)
            //    where zt='1'
            //    and (organizeid=@orgId or organizeid='*' ) ";
            if(zdlx!=null)
            {
                sql += " and (isnull(@zdlx, '') = '' or zdlx = @zdlx) ";
                if (zdlx==(int)EnumZdlx.WM)
                {
                    zdlxmc = EnumZdlx.WM.ToString();
                }
                else if(zdlx== (int)EnumZdlx.TCM)
                {
                    zdlxmc = EnumZdlx.TCM.ToString();
                }
            }
            if(!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (zdCode like @searchKeyword or zdmc like @searchKeyword or py like @searchKeyword or icd10 like @searchKeyword or icd10fjm like @searchKeyword) ";
            }
            sql += " AND ( ybnhlx ='*' OR  ISNULL(@ybnhlx, '') = '' OR ybnhlx = @ybnhlx  )" +
                "order by isnull(icd10,'ZZZZ')";
            return this.QueryWithPage<DiagnoseDTO>(sql,pagination, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    ,new SqlParameter("@orgId", orgId)
                    ,new SqlParameter("@zdlx", zdlxmc ?? "")
                    ,new SqlParameter("@ybnhlx", ybnhlx ?? "")
            });

        }
        /// <summary>
        /// 获取根目录病历大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<SysDicMarkDto> GetBllxList(string orgId, string code,string mzbz=null)
        {
            string sql = @"
select convert(varchar(10),bllx) Code,bllxmc [Name],Id from bl_bllx with(nolock)
where OrganizeId=@orgId and zt='1' and isroot='1'
";

            if (!string.IsNullOrWhiteSpace(code) && code!="null")
            {
                sql += " and ( Id=@code or bllx=@code or bllxmc=@code)  ";
            }
            if (!string.IsNullOrWhiteSpace(mzbz) && mzbz != "null" && mzbz!=((int)EnumMzbz.all).ToString())
            {
                sql += " and mzbz in(@mzbz,"+ ((int)EnumMzbz.all).ToString() + ")  ";
            }
            sql += " order by bllx ";
            return this.FindList<SysDicMarkDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@code",code ?? ""),
                new SqlParameter("@mzbz",mzbz ?? "")
            });
        }
        /// <summary>
        /// 岗位列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<SysDicDto> GetDutyList(string orgId, string code)
        {
            string sql = @"
select Id,Code,[Name] 
from  [NewtouchHIS_Base].[dbo].[V_S_Sys_Duty](nolock) 
where zt='1'  ";

            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += " and ( Code=@code or [Name]=@code)  ";
            }
            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@code",code ?? "")
            });
        }


        public IList<StaffVO> StaffInfo(string user,string orgId)
        {
            string sql = @"select a.gh rygh,a.[name],b.ImageUrl imgurl,b.ImageData  imgbase64
from [NewtouchHIS_Base].dbo.Sys_Staff a,[NewtouchHIS_Base]..Sys_StaffSignature b
where a.OrganizeId=b.OrganizeId and  a.id=b.StaffId 
and a.zt='1' and  b.zt='1'
and gh=@user and a.OrganizeId=@orgId
";
            return this.FindList<StaffVO>(sql, new SqlParameter[] {
                new SqlParameter("@user",user),
                new SqlParameter("@orgId",orgId)
            });

        }

        public IList<xt_bq> GetInpatientArea(string orgId, string keyword)
        {
            string sql = @"select bqCode,bqmc,py,px from NewtouchHIS_Base..xt_bq where OrganizeId=@orgId and zt=1 order by px ";
            return this.FindList<xt_bq>(sql, new SqlParameter[] { new SqlParameter("@orgId",orgId)});
        }

        #region MR
        public IList<SysZdListDto> ZdList(string orgId, string keyword,string zdlx="",string ybnhlx="")
        {
            var sql = @"
select top 200 * from [NewtouchHIS_Base]..V_S_xt_zd(nolock) 
where  zt = '1'
  and zdlx = @zdlx
  and (zdCode like @searchKeyword or zdmc like @searchKeyword or py like @searchKeyword )
order by zdCode";
            return this.FindList<SysZdListDto>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    ,new SqlParameter("@orgId", orgId)
                    ,new SqlParameter("@zdlx", zdlx ?? "")
                    ,new SqlParameter("@ybnhlx", ybnhlx ?? "")
            });
            //           string sql = @"	select zdCode,zdmc,icd10,py
            //from [NewtouchHIS_Base].[dbo].[xt_zd] with(nolock)
            //where zt='1' and organizeid in('*',@orgId) ";

            //           return this.FindList<SysZdListDto>(sql, new SqlParameter[] {
            //               new SqlParameter("@orgId",orgId ??""),
            //               new SqlParameter("@keyword",keyword ??"")
            //           });
        }
        /// <summary>
        /// 手术字典列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysOpListDto> OpList(string orgId, string keyword,bool type)
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

                if(list==null || list.Count<Convert.ToInt32(count))
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
                sql += " and (ssdm like @kwd or ssmc like @kwd or zjm like @kwd )";
            }
            return this.FindList<SysOpListDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@kwd",string.IsNullOrWhiteSpace(keyword)==true? "":keyword+"%"),
                new SqlParameter("@date",dd)
            });
        }

        public IList<SysOpListDto> OpListPage(Pagination pagination, string orgId, string keyword)
        {
            string sql = @"select ssdm,ssmc,zjm,ssjb 
from [Newtouch_OR].[dbo].[OR_Operation] with(nolock) 
where OrganizeId=@orgId and zt='1' 
";
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (ssdm like @kwd or ssmc like @kwd or zjm like @kwd)";
            }
            return this.QueryWithPage<SysOpListDto>(sql,pagination, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@kwd",keyword ?? "")
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
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ((charindex(@kwd,AnesCode)>0) or (charindex(@kwd,AnesName)>0) or (charindex(@kwd,Aneszjm)>0)) ";
            }

            sql += "order by AnesCode";

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
from [Newtouch_OR].dbo.V_SYS_NotchGrade with(nolock)
where OrganizeId=@orgId ";
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ((charindex(@kwd,Code)>0) or (charindex(@kwd,Name)>0)) ";
            }

            sql += "order by [Name]";

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
        public IList<SysDicDto> DicCommonList(string orgId, string keyword, string rule, string code = null)
        {
            string sql = @"SELECT ItemCode  Code,ItemName Name,OrganizeId,[order]
from [Newtouch_MRMS].dbo.[mr_dic_common] with(nolock)
where OrganizeId=@orgId 
  and zt='1'";
            if (!string.IsNullOrWhiteSpace(rule))
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
            sql += " order by [order] ";
            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@rule",rule??""),
                new SqlParameter("@kwd",keyword??""),
                new SqlParameter("@code",code??"")
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
            if (!string.IsNullOrWhiteSpace(dutyCode))
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
        public IList<SysDicDto> GetDeptList(string orgId, string code)
        {
            string sql = @"
select Code,[Name]
from [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] with(nolock)
where OrganizeId=@orgId and zt='1' ";

            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += " and ( Code=@code or Name=@code) ";
            }
            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@code",code)
            });
        }

        public IList<SysDicDto> GetWardList(string orgId, string code)
        {
            string sql = @"
select bqCode Code,bqmc [Name] from [NewtouchHIS_Base].[dbo].[V_S_xt_bq] with(nolock)
where OrganizeId=@orgId and zt='1' ";

            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += " and ( bqCode=@code or bqmc=@code)  ";
            }
            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@code",code)
            });
        }

        public IList<SysDicDto> DicItemsDetailList(string orgId, string keyword, string types)
        {
            string sql = @"
select b.Name ,b.Code from [NewtouchHIS_Base].[dbo].[Sys_Items] a
left join [NewtouchHIS_Base].[dbo].[Sys_ItemsDetail] b on a.id=b.itemid and b.zt='1'
and (organizeid=@orgId or organizeid='*')
where a.zt='1' and a.Code=@types ";
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ((charindex(@keyword,b.Code)>0) or (charindex(@keyword,b.Name)>0) )  ";
            }
            return this.FindList<SysDicDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@types",types),
                new SqlParameter("@keyword",keyword??"")
            });
        }
        #endregion

        #region 医嘱续打
        public IList<CqyzPrintVo> GetPrintCqyzData(string zyh, string orgId, string isSign)
        {
            string sql = @"exec Newtouch_CIS..usp_Inp_Report_Prt_Cqyz @zyh,@orgId,@isSign

";
            return this.FindList<CqyzPrintVo>(sql, new SqlParameter[] {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@isSign",isSign)
            });

        }
        public IList<LsyzPrintVo> GetPrintLsyzData(string zyh, string orgId, string isSign)
        {
            string sql = @"exec Newtouch_CIS..usp_Inp_Report_Prt_Lsyz @zyh,@orgId,@isSign

";
            return this.FindList<LsyzPrintVo>(sql, new SqlParameter[] {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@isSign",isSign)
            });
        }

        public PatidInfoVo GetInpatInfo(string zyh, string orgId)
        {
            string sql = @"SELECT   a.zyh, a.blh, a.xm,b.cwmc BedName,Department.name DeptName,zyxx.nlshow age,case zyxx.sex when 1 then  '男' else '女' end sex
		 ,a.zdmc zd,org.Name yymc
FROM      Newtouch_CIS..zy_brxxk AS a WITH (nolock) LEFT OUTER JOIN
            NewtouchHIS_Base.dbo.xt_cw AS b WITH (nolock) ON a.BedCode = b.cwCode AND 
            a.OrganizeId = b.OrganizeId LEFT OUTER JOIN
            NewtouchHIS_Base.dbo.xt_bf AS c WITH (nolock) ON b.bfCode = c.bfCode AND b.OrganizeId = c.OrganizeId
			LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department Department
			ON Department.Code=a.DeptCode AND Department.OrganizeId= a.OrganizeId
			LEFT JOIN Newtouch_EMR..zy_brjbxx zyxx ON zyxx.zyh=a.zyh and zyxx.OrganizeId=a.OrganizeId and zyxx.zt=1
            LEFT JOIN NewtouchHIS_Base..Sys_Organize org on org.Id=a.OrganizeId and org.zt=1
WHERE   (a.zt = 1) AND (a.OrganizeId = @orgId) AND (a.zyh = @zyh)";
            return this.FirstOrDefault<PatidInfoVo>(sql, new SqlParameter[] {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId",orgId)
            });
        }
        #endregion


        #region 护理记录单打印
        public IList<HljlDataPrintVo> GetPrintHljlData(string zyh, string orgId, string hljb)
        {
            string sql = @"exec [usp_hljld] @zyh,@orgId,@hljb

";
            return this.FindList<HljlDataPrintVo>(sql, new SqlParameter[] {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@hljb",hljb)
            });

        }
        #endregion
    }
}

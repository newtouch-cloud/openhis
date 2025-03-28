using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.EMR;
using NewtouchHIS.Domain.IDomainService.EMR;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model.DRG;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;
using static NewtouchHIS.Domain.Enum.HisEnum;

namespace NewtouchHIS.DomainService.EMR
{
    /// <summary>
    /// 质控病历结构模板接口查询
    /// </summary>
    public class MedicalBlDmnService : BaseServices<MedicalBllxItemVo>, IMedicalBlDmnService
    {
        /// <summary>
        /// 病历类型
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<MedicalBllxItemVo> MedicalbllxRecord(string orgId, DBEnum db = DBEnum.EmrDb)
        {
            var bllxData = await baseDal.GetListBySqlQuery<MedicalBllxRecord>(db.ToString(), @"select Id,OrganizeId,Bllx,Bllxmc,ParentId,IsRoot from bl_bllx with(nolock) where organizeid=@orgId and zt='1' ", new List<DbParameter> {
                new SqlParameter("@orgId",orgId),
            });
            MedicalBllxItemVo bllxList =  new MedicalBllxItemVo();
            if (bllxData != null && bllxData.Count > 0)
            {
                bllxList.bllxRecord = bllxData;
            }
            return bllxList;
        }

        /// <summary>
        /// 病历模板
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<MedicalBlMbItemVo> MedicalblmbRecord(string orgId, DBEnum db = DBEnum.EmrDb)
        {
            var blmbData = await baseDal.GetListBySqlQuery<MedicalBlmbRecord>(db.ToString(), @"select  * from [bl_mblb] with(nolock) where organizeid=@orgId and zt='1' ", new List<DbParameter> {
                new SqlParameter("@orgId",orgId),
            });
            MedicalBlMbItemVo blmblist = new MedicalBlMbItemVo();
            if (blmbData != null && blmbData.Count > 0)
            {
                blmblist.blmbRecord = blmbData;
            }
            return blmblist;
        }

        /// <summary>
        /// 病历类型
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<MedicalBllxMbTreeVo> MedicalbllxTreeRecord(string orgId, DBEnum db = DBEnum.EmrDb)
        {
            var bllxtreeData = await baseDal.GetListBySqlQuery<MedicalBllxmbTreeRecord>(db.ToString(), @"select Id,parentId,bllx,bllxmc,isroot,'1' ly,OrganizeId 
from [bl_bllx] with(nolock) where zt=1 and OrganizeId=@orgId
--union all
--select  blmb.Id,blmb.bllxId,blmb.mbbm,blmb.mbmc,bllx.isroot,'2' ly ,bllx.OrganizeId
--from [bl_bllx]  bllx with(nolock)
--left join [bl_mblb] blmb with(nolock) on bllx.Id=blmb.bllxId and bllx.organizeId=blmb.organizeId
--where blmb.zt=1 and bllx.isroot<>'1' and bllx.OrganizeId=@orgId
", new List<DbParameter> {
                new SqlParameter("@orgId",orgId),
            });
            MedicalBllxMbTreeVo bllxmbList = new MedicalBllxMbTreeVo();
            if (bllxtreeData != null && bllxtreeData.Count > 0)
            {
                bllxmbList.bllxmbRecord = bllxtreeData;
            }
            return bllxmbList;
        }
        /// <summary>
        /// 住院病人列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<List<MedicalPatientVo>> MedicalCenterPatInfo(string brbz, DateTime ksrq, DateTime jsrq, string srz, string orgId, DBEnum db = DBEnum.EmrDb)
        {
           
            string sqlstr = @"SELECT  brxx.DeptCode,dept.Name DeptName,BedCode,cw.cwmc BedName,brxx.Xm,Sex,brxx.nlshow Age,brxx.ryrq Ryrq,
	    cqrq Cyrq, brxx.sfzh HealthCardNo,brxx.Zyh ,brxx.brxzmc Fylb,basy.ZFY TotalFee,ysgh RyysGh,Zyys,Zzys1,Zrys,brxx.Sfzh,
	    basy.Csrq,(CSD_SN+CSD_SI+CSD_QX) Csd, Lxrxm,basy.Lxrdh,basy.Lxrdz,Gzdwjdz ,basy.Bazt,'' Ylzh,
	    case when brxz.brxzlb='1' then  zyxx.kh else '' end Sbkh
    FROM Newtouch_EMR.[dbo].[zy_brjbxx] brxx  with(nolock)
	left join NewtouchHIS_Sett..zy_brjbxx zyxx with(nolock) on zyxx.zyh=brxx.zyh and zyxx.OrganizeId=brxx.OrganizeId
	left join NewtouchHIS_Sett..xt_brxz brxz with(nolock) on brxz.brxz=zyxx.brxz and brxz.OrganizeId=zyxx.OrganizeId
	left join Newtouch_EMR.[dbo].[mr_basy] basy with(nolock) on basy.zyh=brxx.zyh and basy.OrganizeId=brxx.OrganizeId and basy.OrganizeId=brxx.OrganizeId
	left join NewtouchHIS_Base..Sys_Department dept with(nolock) on dept.Code=brxx.DeptCode and dept.OrganizeId=brxx.OrganizeId
    left join NewtouchHIS_Base.dbo.V_S_xt_cw cw with(nolock) on brxx.organizeid=cw.organizeid and brxx.bedcode=cw.cwcode and cw.zt=1  
    left join NewtouchHIS_Base..V_S_xt_bq  d with(nolock) on brxx.organizeid=d.organizeid and brxx.wardcode=d.bqcode and d.zt=1
    where brxx.zt=1 and brxx.[OrganizeId]=@orgId   
	   ";
            if (brbz== ((int)PatTypeEnum.zy).ToString())
            {
                sqlstr += " and brxx.zybz in(" + Convert.ToInt32(EnumZYBZ.Bqz) + "," + Convert.ToInt32(EnumZYBZ.Zq) + ") and brxx.ryrq >=@ksrq and brxx.ryrq<=@jsrq ";
            }
            else if (brbz == ((int)PatTypeEnum.cy).ToString())
            {
                sqlstr += " and brxx.zybz in(" + Convert.ToInt32(EnumZYBZ.Ycy) + "," + Convert.ToInt32(EnumZYBZ.Djz) + ") and brxx.cqrq >=@ksrq and brxx.cqrq<=@jsrq ";
            }
            if (!string.IsNullOrWhiteSpace(srz))
            {
                sqlstr += " and (brxx.zyh like  @srz or brxx.xm like  @srz)";
            }

            var patlist = await baseDal.GetListBySqlQuery<MedicalPatientVo>(db.ToString(), sqlstr, new List<DbParameter> {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@ksrq",ksrq),
                new SqlParameter("@jsrq",jsrq),
                new SqlParameter("@srz","%" + srz.Trim() + "%"),
            });
            return patlist;
        }
        /// <summary>
        /// 出入院诊断列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<List<MedDiagListVo>> MedicalCenterDiaglist(string zyh, string orgId, DBEnum db = DBEnum.EmrDb)
        {
            string sqlstr = @" select Zyh, Zddm, Zdmc,1 ZDOrder,'1' ZdType,ryrq Zdrq,staff.Name Zdys from [zy_brjbxx] zd 
	left join NewtouchHIS_Base..Sys_Staff staff on staff.gh=zd.ysgh and staff.OrganizeId=zd.OrganizeId
    where zd.zt='1' and zd.zyh=@zyh and zd.organizeid=@orgId   
	union all
	select Zyh, Jbdm Zddm,jbmc Zdmc,ZDOrder,'2' ZdType,zd.CreateTime Zdrq,staff.Name Zdys
	from [mr_basy_zd] zd
	left join NewtouchHIS_Base..Sys_Staff staff on staff.gh=zd.CreatorCode and staff.OrganizeId=zd.OrganizeId  
	where zd.zt='1' and zd.zyh=@zyh and zd.organizeid=@orgId ";
            var DiagList = await baseDal.GetListBySqlQuery<MedDiagListVo>(db.ToString(), sqlstr, new List<DbParameter> {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh),
            });
            return DiagList;
        }
        /// <summary>
        /// 病历文书tree
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<List<MedRecordTree>> MedicalCenterBlwsTree(string zyh, string orgId, DBEnum db = DBEnum.EmrDb)
        {
            string sqlstr = @" 
	select  a.Id,a.bllxmc Blmc,ParentId,a.Bllx,convert(varchar(50),'') Zyh,convert(varchar(50),'') BlId ,convert(varchar(50),'') MbId,
		convert(varchar(20),'')Doccode,
		convert(varchar(50),'')Docname,  
		convert(datetime,null) Blrq, 
		convert(varchar(150),'') Mblj,
		convert(INT,NULL) Blzt,
		IsRoot into #bltab1
	from bl_bllx a with(nolock)  
	where a.zt='1'  and a.OrganizeId=@orgId 

	select a.Id,blmc,c.bllxId ParentId,a.bllx,zyh,blId,mbId,a.ysgh Doccode,ysxm Docname,blrq,c.mblj,a.blzt,a.OrganizeId,'0' IsRoot
	into #bltab2
	from  [dbo].[zy_meddocs_relation] a with(nolock)
	left join bl_mblb c with(nolock) on a.mbid=c.id  
	where a.organizeid=@orgId 
	and a.zyh=@zyh 
	and a.zt='1'  
	if(exists(select 1 from #bltab2 where '1'=left(bllx,1)))--入院病历
	begin
	insert into #bltab1
	select t1.Id,Blmc,ParentId,Bllx,t1.Zyh,BlId,MbId,Doccode,Docname,t1.Blrq,blxtml+blxtmc_yj+'.xml' Mblj,Blzt,IsRoot from #bltab2 t1
	left join bl_rybl t2 with(nolock) on t1.BlId=t2.Id and t1.OrganizeId=t2.OrganizeId 
	where t2.zt='1' and  '1'=left(bllx,1)
	end
	if(exists(select 1 from #bltab2 where '2'=left(bllx,1))) --病程记录
	begin
	insert into #bltab1
	select t1.Id,Blmc,ParentId,Bllx,t1.Zyh,BlId,MbId,Doccode,Docname,t1.Blrq,blxtml+blxtmc_yj+'.xml' Mblj,Blzt,IsRoot from #bltab2 t1
	left join bl_bcjl t2 with(nolock) on t1.BlId=t2.Id and t1.OrganizeId=t2.OrganizeId 
	where t2.zt='1' and  '2'=left(bllx,1)
	end
	if(exists(select 1 from #bltab2 where '3'=left(bllx,1)))--医疗文书
	begin
	insert into #bltab1
	select t1.Id,Blmc,ParentId,Bllx,t1.Zyh,BlId,MbId,Doccode,Docname,t1.Blrq,blxtml+blxtmc_yj+'.xml' Mblj,Blzt,IsRoot from #bltab2 t1
	left join bl_zqws t2 with(nolock) on t1.BlId=t2.Id and t1.OrganizeId=t2.OrganizeId 
	where t2.zt='1' and  '3'=left(bllx,1)
	end
	if(exists(select 1 from #bltab2 where '4'=left(bllx,1)))--护理记录
	begin
	insert into #bltab1
	select t1.Id,Blmc,ParentId,Bllx,t1.Zyh,BlId,MbId,Doccode,Docname,t1.Blrq,blxtml+blxtmc_yj+'.xml' Mblj,Blzt,IsRoot from #bltab2 t1
	left join bl_hljl t2 with(nolock) on t1.BlId=t2.Id and t1.OrganizeId=t2.OrganizeId 
	where t2.zt='1' and  '4'=left(bllx,1)
	end
	if(exists(select 1 from #bltab2 where '5'=left(bllx,1))) --病案首页
	begin
	insert into #bltab1
	select t1.Id,Blmc,ParentId,Bllx,t1.Zyh,BlId,MbId,Doccode,Docname,t1.Blrq,blxtml+blxtmc_yj+'.xml' Mblj,Blzt,IsRoot from #bltab2 t1
	left join bl_basy t2 with(nolock) on  t1.BlId=t2.Id and t1.OrganizeId=t2.OrganizeId 
	where t2.zt='1' and  '5'=left(bllx,1)
	end
	if(exists(select 1 from #bltab2 where '6'=left(bllx,1))) --康复评估
	begin
	insert into #bltab1
	select t1.Id,Blmc,ParentId,t1.Bllx,t1.Zyh,BlId,MbId,Doccode,Docname,t1.Blrq,blxtml+blxtmc_yj+'.xml' Mblj,Blzt,IsRoot from #bltab2 t1
	left join bl_patrecords t2 with(nolock) on t1.BlId=t2.Id and t1.OrganizeId=t2.OrganizeId 
	where t2.zt='1' and  '6'=left(t1.bllx,1)
	end
	insert into #bltab1
	select Id,Blmc,ParentId,Bllx,Zyh,BlId,MbId,Doccode,Docname,Blrq,mblj Mblj,Blzt,IsRoot from #bltab2
	where bllx='5'

	select * from #bltab1 order by bllx
	drop table #bltab1
	drop table #bltab2
  ";
            var bltree = await baseDal.GetListBySqlQuery<MedRecordTree>(db.ToString(), sqlstr, new List<DbParameter> {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh),
            });
            return bltree;
        }
    }
}

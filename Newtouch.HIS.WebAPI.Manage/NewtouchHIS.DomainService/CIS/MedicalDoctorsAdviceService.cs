using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Lib.Base.EnumExtend;
using System.Data.Common;
using static NewtouchHIS.Domain.Enum.HisEnum;

namespace NewtouchHIS.DomainService.CIS
{
    public class MedicalDoctorsAdviceService: BaseServices<MedicalBllxItemVo>, IMedicalDoctorsAdviceDmnService
    {
        /// <summary>
        /// 住院医嘱
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="cxlx"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<List<MedicalDoctorsAdviceVo>> MedicalDoctorsAdviceRecord(string zyh,string orgId,string yzlx, DBEnum db = DBEnum.CisDb)
        {
            string sqlstr = @"IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#yzdata') and type='U')
BEGIN
	DROP TABLE #yzdata;
END
select  '长期' Yzlb ,Yzlx , Kssj ,s.Name Ysmc ,
        xmmc as Yzmc, CONCAT(CONVERT(float,ypjl),dw) as Yzjl, ypyf.Yfmc, yzpc.Yzpcmc,
		yz.Zh , tzsj Tzsj , NULL Tzr ,zxr.Name Zxr , yz.Yzzt ,yz.CreateTime,yz.Shsj,
		yz.Zxsj,dept.Name DeptName,Yztag,
        case yztag when 'JI' then '精I' when 'JII' then '精II' when 'MZ' then '麻醉' else yztag end YztagName,
         Xmdm,Sl,
		case when yzlx in (2,4,10) then (cast(sl as varchar)+ypxx.zycldw) end Zycldw
		into #yzdata
from zy_cqyz yz
LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Staff] s 
	ON  s.gh = yz.ysgh AND s.zt = '1' AND s.OrganizeId =yz.OrganizeId 
LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Staff] zxr
	ON  zxr.gh = yz.zxr AND zxr.zt = '1'  AND zxr.OrganizeId =yz.OrganizeId 
LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Department] dept
	ON yz.zxksdm=dept.Code  AND dept.zt = '1'AND dept.OrganizeId =yz.OrganizeId  
LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_yp] ypxx
	ON yz.xmdm = ypxx.ypCode AND ypxx.OrganizeId = yz.OrganizeId 
LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_ypyf] ypyf on yz.ypyfdm = ypyf.yfCode 
LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_yzpc] yzpc on yz.pcCode = yzpc.yzpcCode and yzpc.OrganizeId =yz.OrganizeId  
where zyh=@zyh and
yz.OrganizeId =@orgId
insert into #yzdata
select  '临时' yzlb  ,yzlx , kssj ,s.Name ysmc ,
        xmmc as yzmc, CONCAT(CONVERT(float,ypjl),dw) as yzjl, ypyf.yfmc, yzpc.yzpcmc,
		yz.zh , NULL tzsj , NULL tzr ,zxr.Name zxr , yz.yzzt ,yz.CreateTime,yz.shsj,
		yz.zxsj,dept.Name deptName,yztag,
        case yztag when 'JI' then '精I' when 'JII' then '精II' when 'MZ' then '麻醉' else yztag end yztagName,
        xmdm,sl,
		case when yzlx in (2,4,10) then (cast(sl as varchar)+ypxx.zycldw) end zycldw 
from zy_lsyz yz
LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Staff] s 
	ON  s.gh = yz.ysgh AND s.zt = '1' AND s.OrganizeId =yz.OrganizeId 
LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Staff] zxr
	ON  zxr.gh = yz.zxr AND zxr.zt = '1'  AND zxr.OrganizeId =yz.OrganizeId 
LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Department] dept
	ON yz.zxksdm=dept.Code  AND dept.zt = '1'AND dept.OrganizeId =yz.OrganizeId  
LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_yp] ypxx
	ON yz.xmdm = ypxx.ypCode AND ypxx.OrganizeId = yz.OrganizeId 
LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_ypyf] ypyf on yz.ypyfdm = ypyf.yfCode 
LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_yzpc] yzpc on yz.pcCode = yzpc.yzpcCode and yzpc.OrganizeId =yz.OrganizeId  
where zyh=@zyh and
yz.OrganizeId =@orgId 
delete from #yzdata  where yzlx='6' or yzlx='7'
select * from #yzdata  where '1'='1' ";
            if (yzlx == "cq")
            {
                sqlstr += " and yzlb='长期' ";
            }
            else if (yzlx == "ls")
            {
                sqlstr += " and yzlb='临时' ";
            }
            else if (yzlx == "dr")
            {
                sqlstr += " and CONVERT(VARCHAR(10), CreateTime, 111) = CONVERT(VARCHAR(10), GETDATE(), 111) ";
            }
            else if (yzlx == "zx")
            {
                sqlstr += " and  yzzt=" + Convert.ToInt32(EnumYzzt.Zx);
            }
            var yzData = await baseDal.GetListBySqlQuery<MedicalDoctorsAdviceVo>(db.ToString(), sqlstr, new List<DbParameter> {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh),
            });
            
            return yzData;
        }

        /// <summary>
        /// 门诊住院检验检查申请单
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="bglx"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<List<MedicalInspectionExaminationVo>> MedicalJyjcRecord(string jzh, string orgId,string reportType,string mzzybz, DateTime ksrq, DateTime jsrq, DBEnum db = DBEnum.CisDb)
        {
            string sqlstr = string.Empty;
            if (mzzybz == "mz")
            {
                sqlstr = @"Select OrganizeId,Xm,Lissqdh,Sqdh,Sqys,CONVERT(datetime,Null) Bgrq,
stuff((select distinct ','+u.ztmc from xt_jz t 
join xt_cf y on t.jzId=y.jzId and t.OrganizeId=y.OrganizeId
join xt_cfmx u on y.cfId=u.cfId and y.OrganizeId=u.OrganizeId
where  t.OrganizeId=q.OrganizeId and q.lissqdh=y.cfh and q.sqdh=y.sqdh
for xml path('')),1,1,'') as Sqxm,SyncStatus,max(sqsj) Sqsj 
from (
	select distinct a.OrganizeId,a.xm,b.cfh lissqdh,b.sqdh,c.ztId,c.ztmc,a.jzysmc Sqys,
	 case d.sqdzt  when '0' then '已申请' when '1' then '已接收' when '2' then '已完成' else '待申请' end syncStatus,
	CONVERT(varchar(100), b.CreateTime, 20) sqsj,row_number() over(partition by c.ztmc order by b.CreateTime desc) rn
	from xt_jz a
	join xt_cf b on a.jzId=b.jzId and a.OrganizeId=b.OrganizeId
	join xt_cfmx c on b.cfId=c.cfId and b.OrganizeId=c.OrganizeId
	join [NewtouchHIS_Sett].dbo.mz_cf d on d.cfh=b.cfh and d.OrganizeId=b.OrganizeId   and d.zt='1'
	where  a.mzh=@jzh  and a.OrganizeId=@orgId and a.zt=1 
            and a.CreateTime>=@ksrq and a.CreateTime<=@jsrq ";
                if (reportType == "jy")
                {
                    sqlstr += " and b.cflx=4";
                }
                else {
                    sqlstr += " and b.cflx=5";
                }
            }
            else {
                sqlstr = @" Select q.OrganizeId,Xm,Lissqdh,Sqdh,Sqys,CONVERT(datetime,Null) Bgrq,
stuff((select distinct ','+b.ztmc from zy_lsyz b with(nolock)
where b.yzh=q.lissqdh and b.OrganizeId=q.OrganizeId
for xml path('')),1,1,'') as Sqxm,
Syncstatus,max(sqsj) Sqsj  
FROM (
	select a.OrganizeId, hzxm xm,yzh lissqdh,sqdh,ztId,ztmc,staff.Name Sqys,
	case syncStatus when '0' then '已申请' when '1' then '已接收' when '2' then '已完成' else '待申请' end syncStatus
	,CONVERT(varchar(100), a.CreateTime, 20) sqsj,row_number() over(partition by a.ztmc order by a.CreateTime desc) rn  
	from zy_lsyz a with(nolock)
	left join NewtouchHIS_Base..Sys_Staff staff with(nolock) on staff.gh=a.ysgh and staff.OrganizeId=a.OrganizeId 
	where   a.zyh=@jzh  and a.OrganizeId=@orgId  and a.zt=1 and a.CreateTime>=@ksrq and a.CreateTime<=@jsrq";
                if (reportType == "jy")
                {
                    sqlstr += " and yzlx=6";
                }
                else
                {
                    sqlstr += " and yzlx=7";
                }
            }
            sqlstr += @" ) q 
WHERE rn = 1 group by OrganizeId ,xm,lissqdh,sqdh,syncstatus,Sqys order by sqsj desc";
            var Data = await baseDal.GetListBySqlQuery<MedicalInspectionExaminationVo>(db.ToString(), sqlstr, new List<DbParameter> {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@jzh",jzh),
                new SqlParameter("@ksrq",ksrq),
                new SqlParameter("@jsrq",jsrq),
            });

            return Data;
        }
    }
}

USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_zy_SelfCost4202_data]    Script Date: 2025/2/18 17:40:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**
【4202】自费病人住院就诊和诊断信息上传
exec  [usp_zy_SelfCost4202_data] '6d5752a7-234a-403e-aa1c-df8b45d3469f','4277'
**/
ALTER PROCEDURE [dbo].[usp_zy_SelfCost4202_data]
@orgId varchar(50),
@jsnm  int
as
select 
  zyjs.zyh fixmedins_mdtrt_id,
  org.gjjgdm fixmedins_code,
  org.Name fixmedins_name,
  zybrxx.zjlx psn_cert_type,
  zybrxx.zjh certno,
  zybrxx.xm psn_name,
  case when xb='1' then '男' else '女' end gend,
  mz.mzmc naty,
  convert(varchar(10),csny,121) brdy,
  convert(decimal(18,1),nl) age,
  zybrxx.lxr coner_name,
  zybrxx.lxrdh tel,
  lxrdz addr,
  convert(varchar(19),zybrxx.ryrq,121) begntime,
  convert(varchar(19),cyrq,121) endtime,
  '21' med_type,
  zyjs.zyh ipt_otp_no,
  zybrxx.blh medrcdno,
  isnull(staff.gjybdm,doctor) chfpdr_code,
  '' adm_diag_dscr,
  ybksbm adm_dept_codg,
  dept.Name adm_dept_name,
  cw adm_bed,
  cw wardarea_bed,
  '' traf_dept_flag,
  zyxxk.cyzddm dscg_maindiag_code,
  ybksbm dscg_dept_codg,
  dept.Name dscg_dept_name,
  '1'  dscg_way,
  DATEDIFF(DAY,zyxxk.rqrq,zyxxk.cqrq) ipt_days,
  '1' vali_flag,
  zyjs.CreatorCode opter_id,
  staff1.Name opter_name,
  convert(varchar(19),zyjs.CreateTime,121) opt_time,
  staff.Name chfpdr_name,
  zyxxk.zdmc dscg_maindiag_name,
  zyjs.zje medfee_sumamt,
  '0' inhosp_stas
from  
zy_js zyjs with(nolock)
join zy_brjbxx zybrxx with(nolock) on zybrxx.zyh=zyjs.zyh and zybrxx.OrganizeId=zyjs.OrganizeId and zybrxx.zt='1'
left join Newtouch_CIS..zy_brxxk zyxxk with(nolock) on zyxxk.zyh=zyjs.zyh and zyxxk.OrganizeId=zyjs.OrganizeId and zyxxk.zt='1'
join NewtouchHIS_Base..Sys_Organize org on org.Id=zyjs.OrganizeId
left join NewtouchHIS_Base..xt_mz mz on mz.mzCode=zybrxx.mz
left join NewtouchHIS_Base..Sys_Department dept on dept.Code=zybrxx.ks and dept.OrganizeId=zybrxx.OrganizeId and dept.zt='1'
left join NewtouchHIS_Base..Sys_Staff staff on staff.gh=zybrxx.doctor and staff.OrganizeId=zybrxx.OrganizeId and staff.zt='1'
left join NewtouchHIS_Base..Sys_Staff staff1 on staff1.gh=zyjs.CreatorCode and staff1.OrganizeId=zyjs.OrganizeId and staff1.zt='1'
left join Drjk_jxcsc_output jxcxc on jxcxc.mlbm_id=cast(zyjs.jsnm as varchar) and jxcxc.organizeid=zyjs.organizeid and jxcxc.type='4202'
where zyjs.jsnm=@jsnm and zyjs.OrganizeId=@orgId
and (jxcxc.mlbm_id is null or jxcxc.issuccess='False') 

GO



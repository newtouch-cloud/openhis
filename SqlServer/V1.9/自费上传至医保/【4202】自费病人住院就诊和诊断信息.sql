USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_zy_SelfCost4202_Diagdata]    Script Date: 2025/2/18 17:42:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**
【4202】自费病人住院就诊和诊断信息上传
exec  [usp_zy_SelfCost4202_Diagdata] '6d5752a7-234a-403e-aa1c-df8b45d3469f','03317'
select * from Newtouch_CIS..zy_PatDxInfo where zyh='03317' and zt='1' order by createtime desc
**/
ALTER PROCEDURE [dbo].[usp_zy_SelfCost4202_Diagdata]
@orgId varchar(50),
@zyh  varchar(20)
as
select 
 zdlb inout_diag_type,
 case when isnull(zddl,'WM')='WM' THEN  1 WHEN isnull(zddl,'TCM')='TCM' AND isnull(patdx.zdlx,'2')='1' THEN 2
		 WHEN isnull(zddl,'TCM')='TCM' AND isnull(patdx.zdlx,'2')='2' THEN 3 ELSE 1 END diag_type,
 case when zdlb='1' then '1' else (case when patdx.zdlx='1' and zddl='WM' then 1 else 0 end) end maindiag_flag,
-- patdx.zdlx  diag_srt_no,
 ROW_NUMBER() over(order by zdlb,zddl DESC,patdx.zdlx) diag_srt_no,
 isnull(zd.icd10,patdx.zddm) diag_code,
 patdx.zdmc diag_name,
 ybksbm diag_dept,
 isnull(staff.gjybdm,brxx.ysgh) diag_dr_code,
 staff.Name diag_dr_name,
 CONVERT(varchar(19),patdx.CreateTime,121) diag_time,
 '1' vali_flag
from Newtouch_CIS..zy_PatDxInfo patdx
left join NewtouchHIS_Base..xt_zd zd on zd.zdCode=patdx.zddm and zd.zt='1'
left join Newtouch_CIS..zy_brxxk brxx on brxx.zyh=patdx.zyh and brxx.OrganizeId=patdx.OrganizeId and brxx.zt='1'
left join NewtouchHIS_Base..Sys_Department dept on dept.Code=brxx.DeptCode and dept.OrganizeId=brxx.OrganizeId and dept.zt='1'
left join NewtouchHIS_Base..Sys_Staff staff on staff.gh=brxx.ysgh and staff.OrganizeId=brxx.OrganizeId and staff.zt='1'
where patdx.zyh=@zyh and patdx.OrganizeId=@orgId and patdx.zt='1'
ORDER BY inout_diag_type,diag_type
GO



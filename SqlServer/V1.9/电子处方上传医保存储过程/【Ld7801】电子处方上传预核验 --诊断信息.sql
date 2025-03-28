USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D001_Prescription_diseData]    Script Date: 2024/11/11 10:28:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/**
 【Ld7801】电子处方上传预核验 --诊断信息
exec  [usp_ElectronicPrescription_D001_Prescription_diseData] '6d5752a7-234a-403e-aa1c-df8b45d3469f','2410309600005','R20241030N000010'
select * from NewtouchHIS_Base..G_yb_diag_list_b where diag_name like '%流行性感冒%'

select * from NewtouchHIS_Base..xt_zd where zdmc='流行性感冒'
*/


CREATE proc [dbo].[usp_ElectronicPrescription_D001_Prescription_diseData]
	@orgId varchar(50),
	@hisId varchar(50),
	@cfh varchar(500)
as
begin
select  
'1' diagType   --	诊断类别 	字符型 	3 	Y 	Y 	参考诊断类别（diag_type） 
,case zyzd.zdlx when '1' then '1' else '0' end maindiagFlag --	主诊断标志 	字符型 	3 	Y 	Y 	0-否、1-是 
,ROW_NUMBER() OVER(ORDER BY zyzd.zdlx, zyzd.zdlx) diagSrtNo 	--诊断排序号 	数值型 	2 	 	Y 	 
,isnull(zd.zdCode_yb,zd.icd10) diagCode 	--诊断代码 	字符型 	30 	Y 	Y 	医保疾病诊断代码 
,zyzd.zdmc diagName 	--诊断名称 	字符型 	100 	 	Y 	 
,ks.Name diagDept 	--诊断科室名称 	字符型 	50 	 	Y 	 
,ks.ybksbm diagDeptCode 	--诊断科室代码 	字符型 	20 	 	 	与医药机构服务的科室管理：【3401科室信息上传、3401A 批量科室信息上传】中上传的
,gh.ks hosp_dept_codg --医院科室编码字段保持一致 
,ys.gjybdm diagDrNo 	--诊断医生编码 	字符型 	30 	 	Y 	国家医保医师代码 
,ys.Name diagDrName --	诊断医生姓名 	字符型 	50 	 	Y 	 
,convert (varchar(50),zyzd.CreateTime,120) diagTime 	--诊断时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
,zyzd.zdCode tcmDiseCode 	--中医病名代码 	字符型 	30 	 	 	diagType为中医诊断（值为2，3）时上传 
,zyzd.zdmc tcmDiseName 	--中医病名 	字符型 	300 	 	 	diagType为中医诊断（值为2，3）时上传 
,isnull(zyzd.zhCode,'') tcmsympCode 	--中医证候代码 	字符型 	30 	 	 	diagType为中医诊断（值为2，3）时上传 
,isnull(zyzd.zhmc,'') tcmsymp 	--中医证候 	字符型 	300 	 	 	diagType为中医诊断（值为2，3）时上传 
from  [Newtouch_CIS]..xt_zyzd zyzd
inner join [Newtouch_CIS]..xt_jz jz on jz.jzId=zyzd.jzId and jz.OrganizeId=zyzd.OrganizeId and zyzd.zt='1'
inner join mz_gh gh  on jz.mzh=gh.mzh and jz.OrganizeId=gh.OrganizeId and jz.zt='1'
left join [NewtouchHIS_Base]..xt_zd zd on zd.zdCode=zyzd.zdcode and zd.OrganizeId=zyzd.OrganizeId and zd.zt='1'
left join [NewtouchHIS_Base].[dbo].[Sys_Department] ks on ks.code=gh.ks and ks.OrganizeId=gh.OrganizeId
left join [NewtouchHIS_Base]..[Sys_Staff] ys on ys.gh=zyzd.CreatorCode and ys.OrganizeId=zyzd.OrganizeId 
where zyzd.zt='1'
and gh.mzh=@hisId
and gh.OrganizeId=@orgId
union all
select  
'1' diagType   --	诊断类别 	字符型 	3 	Y 	Y 	参考诊断类别（diag_type） 
,case xyzd.zdlx when '1' then '1' else '0' end maindiagFlag --	主诊断标志 	字符型 	3 	Y 	Y 	0-否、1-是 
,ROW_NUMBER() OVER(ORDER BY xyzd.zdlx, xyzd.zdlx) diagSrtNo 	--诊断排序号 	数值型 	2 	 	Y 	 
,isnull(zd.zdCode_yb,zd.icd10) diagCode 	--诊断代码 	字符型 	30 	Y 	Y 	医保疾病诊断代码 
,xyzd.zdmc diagName 	--诊断名称 	字符型 	100 	 	Y 	 
,ks.Name diagDept 	--诊断科室名称 	字符型 	50 	 	Y 	 
,ks.ybksbm diagDeptCode 	--诊断科室代码 	字符型 	20 	 	 	与医药机构服务的科室管理：【3401科室信息上传、3401A 批量科室信息上传】中上传的
,gh.ks hosp_dept_codg --医院科室编码字段保持一致 
,ys.gjybdm diagDrNo 	--诊断医生编码 	字符型 	30 	 	Y 	国家医保医师代码 
,ys.Name diagDrName --	诊断医生姓名 	字符型 	50 	 	Y 	 
,convert (varchar(50),xyzd.CreateTime,120) diagTime 	--诊断时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
,'' tcmDiseCode 	--中医病名代码 	字符型 	30 	 	 	diagType为中医诊断（值为2，3）时上传 
,'' tcmDiseName 	--中医病名 	字符型 	300 	 	 	diagType为中医诊断（值为2，3）时上传 
,'' tcmsympCode 	--中医证候代码 	字符型 	30 	 	 	diagType为中医诊断（值为2，3）时上传 
,'' tcmsymp 	--中医证候 	字符型 	300 	 	 	diagType为中医诊断（值为2，3）时上传 
from  [Newtouch_CIS]..xt_xyzd xyzd
inner join [Newtouch_CIS]..xt_jz jz on jz.jzId=xyzd.jzId and jz.OrganizeId=xyzd.OrganizeId and xyzd.zt='1'
inner join mz_gh gh  on jz.mzh=gh.mzh and jz.OrganizeId=gh.OrganizeId and jz.zt='1'
left join [NewtouchHIS_Base]..xt_zd zd on zd.zdCode=xyzd.zdcode and zd.OrganizeId=xyzd.OrganizeId and zd.zt='1'
left join [NewtouchHIS_Base].[dbo].[Sys_Department] ks on ks.code=gh.ks and ks.OrganizeId=gh.OrganizeId
left join [NewtouchHIS_Base]..[Sys_Staff] ys on ys.gh=xyzd.CreatorCode and ys.OrganizeId=xyzd.OrganizeId 
where xyzd.zt='1'
and gh.mzh=@hisId
and gh.OrganizeId=@orgId
end


GO



USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D003_Prescription_top20]    Script Date: 2024/11/4 15:10:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/**
 【Ld7802】电子处方医保电子签名
exec usp_ElectronicPrescription_D003_Prescription_top20 '6d5752a7-234a-403e-aa1c-df8b45d3469f',
'2410309600005','R20241030N000010','23','23','','',''
select * from Dzcf_D001_output
*/

CREATE proc [dbo].[usp_ElectronicPrescription_D003_Prescription_top20]
	@orgId varchar(500),
	@hisId varchar(500),
	@cfh varchar(500),
	@fixmedinsName varchar(500),
	@fixmedinsCode varchar(500),
	@shks varchar(100),
	@shys varchar(100),
	@shrq varchar(100)
as
begin
select 
D001.rxTraceCode rxTraceCode --	处方追溯码 	字符型 	20 	 	Y 	有效时间和处方有效时间保持一致，上传时每张处方只能使用一次 
,D001.hiRxno hiRxno 	--医保处方编号 	字符型 	30 	 	Y 	 
,gh.jzid mdtrtId 	--医保就诊ID 	字符型 	30 	 	Y 	参保病人信息字段（注：医保门诊挂号时返回） 
,gh.xm patnName 	--患者姓名 	字符型 	40 	 	Y 	 
,'01' psnCertType 	--人员证件类型 	字符型 	6 	Y 	Y 	 
,gh.zjh certno 	--证件号码 	字符型 	50 	 	Y 	 
,@fixmedinsName fixmedinsName --	定点医疗机构名称 	字符型 	200 	 	Y 	 
,@fixmedinsCode fixmedinsCode --	定点医疗机构编号 	字符型 	20 	 	Y 	 
,ys.gjybdm drCode --	开方医保医师代码 	字符型 	20 	 	Y 	国家医保医师代码 
,ys.Name prscDrName --	开方医师姓名 	字符型 	50 	 	Y 	 
,sfks.Name pharDeptName --	审方药师科室名称 	字符型 	50 	 	Y 	 
,sfks.Code pharDeptCode --	审方药师科室编号 	字符型 	30 	 	Y 	与医药机构服务的科室管理：【3401科室信息上传、3401A批量科室信息上传】中上传的 hosp_dept_codg 医院科室编码字段保持一致 
,'' pharProfttlCodg --	审方药师职称编码 	字符型 	20 	Y 	 	参照审方药师职称编码（phar_pro_tech_duty） 
,'' pharProfttlName --	审方药师职称名称 	字符型 	20 	 	 	 
,sfys.gjybdm pharCode --	审方医保药师代码 	字符型 	20 	 	Y 	国家医保定点医疗机构药学、技术人员代码（如 HY110000000001） 
,'' pharCertType 	--审方药师证件类型 	字符型 	6 	Y 	 	参照人员证件类型 (psn_cert_type) 
,'' pharCertno 	--审方药师证件号码 	字符型 	50 	 	 	 
,sfys.Name pharName 	--审方药师姓名 	字符型 	50 	 	Y 	 
,'' pharPracCertNo --	审方药师执业资格证号 	字符型 	50 	 	 	 
,@shrq pharChkTime --	医疗机构药师审方时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
,'' extras 	--扩展字段 	JSON 	4000 	 	 	（预留字段，当前未使用），JSON序列化成字符串后长度不能超过4000 
from Dzcf_D001_output D001
inner join mz_gh gh on d001.mzh=gh.mzh and gh.zt=1
inner join [Newtouch_CIS]..xt_jz jz on jz.mzh=gh.mzh and jz.OrganizeId=gh.OrganizeId and jz.zt='1'
left join [Newtouch_CIS]..xt_cf cf on cf.jzId=jz.jzid and cf.OrganizeId=jz.OrganizeId and cf.zt='1'
left join [NewtouchHIS_Base].[dbo].[Sys_Department] ks on ks.code=cf.ks and ks.OrganizeId=gh.OrganizeId
left join [NewtouchHIS_Base]..[Sys_Staff] ys on ys.gh=cf.ys and ys.OrganizeId=cf.OrganizeId and ys.zt='1'
left join (select * from [NewtouchHIS_Base]..[Sys_Staff] where gh=@shys and OrganizeId=@orgId and zt='1') sfys  on 1=1
left join (select * from [NewtouchHIS_Base]..[Sys_Department] where Code=@shks  and OrganizeId=@orgId and zt='1') sfks  on 1=1
where D001.cfh=@cfh
and D001.mzh=@hisId
and D001.zt=1
and cf.OrganizeId=@orgId
end

--select top 100 * from Drjk_mzjzxxsc_input left join  (select * from [NewtouchHIS_Base]..[Sys_Staff] where gh='000000' and zt='1') sfys on 1=1
GO



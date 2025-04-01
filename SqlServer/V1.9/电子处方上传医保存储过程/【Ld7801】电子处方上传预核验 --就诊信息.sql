USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D001_Prescription_mdtrtData]    Script Date: 2024/11/11 10:28:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/**
 【Ld7801】电子处方上传预核验 --就诊信息
 select * from mz_gh where mzh='2410309600005'
 select * from Newtouch_CIS..xt_jz   where mzh='2410309600005'
exec [usp_ElectronicPrescription_D001_Prescription_mdtrtData] '6d5752a7-234a-403e-aa1c-df8b45d3469f','2410087040001','R20241104N000007','',''
*/


CREATE proc [dbo].[usp_ElectronicPrescription_D001_Prescription_mdtrtData]
	@orgId varchar(500),
	@hisId varchar(500),
	@cfh varchar(500),
	@fixmedinsName varchar(500),
	@fixmedinsCode varchar(500)
as
begin
select 
@fixmedinsName fixmedinsName --	定点医疗机构名称 	字符型 	200 	 	Y 	 
,@fixmedinsCode fixmedinsCode --	定点医疗机构编号 	字符型 	20 	 	Y 	 
,gh.jzid  mdtrtId 	--医保就诊ID 	字符型 	30 	 	Y 	参保病人信息字段（注：医保门诊挂号时返回） 
,'11' medType 	--医疗类别 	字符型 	6 	Y 	Y 	参考医疗类别（med_type） 
,gh.mzh iptOtpNo 	--门诊/住院号 	字符型 	30 	 	Y 	 
,'1' otpIptFlag 	--门诊住院标识 	字符型 	3 	Y 	 	1-门诊、2-住院，值为空时默认为1门诊。 
,kh.grbh psnNo 	--医保人员编号 	字符型 	30 	 	Y 	 
,gh.xm patnName 	--患者姓名 	字符型 	40 	 	Y 	 
,'01' psnCertType 	--人员证件类型 	字符型 	6 	Y 	Y 	参照人员证件类型(psn_cert_type) 
,gh.zjh certno 	--证件号码 	字符型 	50 	 	Y 	 
,datediff(YEAR,gh.csny,getdate()) patnAge 	--年龄 	数值型 	4,1 	 	Y 	 
,'' patnHgt 	--患者身高 	数值型 	6,2 	 	 	 
,'' patnWt 	--患者体重 	数值型 	6,2 	 	 	 
,gh.xb gend 	--性别 	字符型 	6 	Y 	Y 	参考性别（gend）
,'' birctrlType 	--计划生育手术类别 	字符型 	6 	Y 	 	生育门诊按需录入
,'' birctrlMatnDate 	--计划生育手术或生育日期 	日期型 	 	 	 	生育门诊按需录入，yyyy-MM-dd 
,'' matnType 	--生育类别 	字符型 	6 	 	 	 
,'' gesoVal --	妊娠(孕周) 	数值型 	2 	 	 	 
,'' nwbFlag --	新生儿标志 	字符型 	3 	Y 	 	0-否、1-是 
,'' nwbAge 	--新生儿日、月龄 	字符型 	20 	 	 	 
,'' suckPrdFlag 	--哺乳期标志 	数值型 	3 	Y 	 	0-否、1-是 
,'' algsHis 	--过敏史 	字符型 	1000 	 	 	 
,ks.Name prscDeptName 	--开方科室名称 	字符型 	50 	 	Y 	 
,cf.ks prscDeptCode --	开方科室编号 	字符型 	30 	Y 	Y 	与医药机构服务的科室管理：【3401 科室信息上传、3401A批量科室信息上传】中上传的 hosp_dept_codg医院科室编码字段保持一致 
,ys.gjybdm drCode 	--开方医保医师代码 	字符型 	20 	 	Y 	国家医保医师代码
,ys.name prscDrName 	--开方医师姓名 	字符型 	50 	 	Y 	 
,'' prscDrCertType --	开方医师证件类型 	字符型 	6 	Y 	 	参照人员证件类型(psn_cert_type) 
,'' prscDrCertno 	--开方医师证件号码 	字符型 	50 	 	 	 
,'233' drProfttlCodg --	医生职称编码 	字符型 	20 	 	Y 	参照开单医生职称(drord_dr_profttl) 
,'主诊医师' drProfttlName 	--医生职称名称 	字符型 	20 	 	Y 	 
,ks.ybksbm drDeptCode --	医生科室编码 	字符型 	30 	 	Y 	与医药机构服务的科室管理：【3401科室信息上传、3401A 批量科室信息上传 】 中 上 传 的 hosp_dept_codg医院科室编码字段保持一致 
,ks.Name drDeptName 	--医生科室名称 	字符型 	50 	 	Y 	 
,'' caty --	科别 	字符型 	10 	Y 	 	参照附录A:科室代码（dept） 
,convert (varchar(50),jz.CreateTime,120) mdtrtTime 	--就诊时间 	日期时间型	 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
,'' diseCodg 	--病种编码 	字符型 	30 	 	 	按照标准编码填写： 按病种结算病种目录代码(bydise_setl_list_code)、 门诊慢特病病种目录代码(opsp_dise_cod)；医疗类别（medType）为门诊慢特病时必传 
,'' diseName --	病种名称 	字符型 	500 	 	 	 
,'0' spDiseFlag 	--特殊病种标志 	字符型 	3 	Y 	Y 	0-否、1-是 
--,'J10.000' maindiagCode 
,isnull(zd.zdCode_yb,gh.zdicd10) maindiagCode --	主诊断代码 	字符型 	30 	 	Y 	医保疾病诊断代码
,gh.zdmc maindiagName --	主诊断名称 	字符型 	100 	 	Y 	 
,'' diseCondDscr 	--疾病病情描述 	字符型 	2000 	 	 	 
,'' hiFeesetlType 	--医保费用结算类型 	字符型 	6 	Y 	 	参考医保费用结算类型（hi_feesetl_type） 
,'' hiFeesetlName 	--医保费用类别名称 	字符型 	20 	 	 	 
,'' rgstFee 	--挂号费 	数值型 	16,2 	 	 	 
,'' medfeeSumamt 	--医疗费总额 	数值型 	16,2 	 	 	注：本次需要结算的医疗费用总额，院外购药时不包括处方药品费用 
,'' fstdiagFlag --	是否初诊 	字符型 	3 	 	 	0-否、1-是 
,'' extras 	--扩展数据 	对象型 	 	 	 	地方业务扩展信息，处方结算核验时可传递予地方核心业务系统 
,cf.ys
from mz_gh gh
inner join [NewtouchHIS_Sett]..xt_card kh on kh.CardNo=gh.kh and kh.CardType=gh.CardType and kh.OrganizeId=gh.OrganizeId and kh.zt='1'
left join [NewtouchHIS_Base]..xt_zd zd on zd.icd10=gh.zdicd10 and zd.OrganizeId=gh.OrganizeId and zd.zt='1'
--left join [NewtouchHIS_Sett]..Drjk_mzjs_input ybdj on ybdj.mzh=gh.mzh and ybdj.zt='1'
inner join [Newtouch_CIS]..xt_jz jz on jz.mzh=gh.mzh and jz.OrganizeId=gh.OrganizeId and jz.zt='1'
left join [Newtouch_CIS]..xt_cf cf on cf.jzId=jz.jzid and cf.OrganizeId=jz.OrganizeId and cf.zt='1'
left join [NewtouchHIS_Base].[dbo].[Sys_Department] ks on ks.code=cf.ks and ks.OrganizeId=gh.OrganizeId
left join [NewtouchHIS_Base]..[Sys_Staff] ys on ys.gh=cf.ys and ys.OrganizeId=cf.OrganizeId and ys.zt='1'
where 
gh.zt='1'
and gh.mzh=@hisId
and gh.OrganizeId=@orgId
and gh.brxz!='0'
and cf.cfh=@cfh
end
GO



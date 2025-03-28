USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D004_Prescription]    Script Date: 2024/11/11 10:30:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/**
 【Ld7104】电子处方撤销
exec [usp_ElectronicPrescription_D004_Prescription] '6d5752a7-234a-403e-aa1c-df8b45d3469f',
'2410309600005','R20241030N000010','23','23'

*/
CREATE proc [dbo].[usp_ElectronicPrescription_D004_Prescription]
	@orgId varchar(500),
	@hisId varchar(500),
	@fixmedinsCode varchar(500),
	@cfh varchar(500),
	@cxys  varchar(500)
as
begin
select 
D003.hiRxno hiRxno --	医保处方编号 	字符型 	30 	 	Y 	 
,@fixmedinsCode fixmedinsCode 	--定点医疗机构编号 	字符型 	20 	 	Y 	 
,ys.gjybdm drCode 	--撤销医师的医保医师代码 	字符型 	20 	 	Y 	 
,ys.Name undoDrName 	--撤销医师姓名 	字符型 	50 	 	Y 	 
,'01' undoDrCertType 	--撤销医师证件类型 	字符型 	6 	Y 	Y 	参照人员证件(psn_cert_type) 
,ys.zjh undoDrCertno 	--撤销医师证件号码 	字符型 	50 	 	Y 	 
,'' undoRea 	--撤销原因描述 	字符型 	200 	 	Y 	 
,convert(varchar(50),getdate(),120) undoTime 	--撤销时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
from [NewtouchHIS_Sett]..Dzcf_D003_output D003
left join (select * from [NewtouchHIS_Base]..[Sys_Staff] where gh=@cxys) ys on 1=1 
where D003.cfh=@cfh
and D003.zt=1
end
--select convert(varchar(50),getdate(),120)
--select * from Dzcf_D003_output
--select * from [NewtouchHIS_Base]..[Sys_Staff] 
--select top 100 * from Drjk_mzjzxxsc_input left join  (select * from [NewtouchHIS_Base]..[Sys_Staff] where gh='000000' and zt='1') sfys on 1=1
GO



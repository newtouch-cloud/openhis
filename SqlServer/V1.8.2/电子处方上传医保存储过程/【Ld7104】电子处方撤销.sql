USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D004_Prescription]    Script Date: 2024/11/11 10:30:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/**
 ��Ld7104�����Ӵ�������
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
D003.hiRxno hiRxno --	ҽ��������� 	�ַ��� 	30 	 	Y 	 
,@fixmedinsCode fixmedinsCode 	--����ҽ�ƻ������ 	�ַ��� 	20 	 	Y 	 
,ys.gjybdm drCode 	--����ҽʦ��ҽ��ҽʦ���� 	�ַ��� 	20 	 	Y 	 
,ys.Name undoDrName 	--����ҽʦ���� 	�ַ��� 	50 	 	Y 	 
,'01' undoDrCertType 	--����ҽʦ֤������ 	�ַ��� 	6 	Y 	Y 	������Ա֤��(psn_cert_type) 
,ys.zjh undoDrCertno 	--����ҽʦ֤������ 	�ַ��� 	50 	 	Y 	 
,'' undoRea 	--����ԭ������ 	�ַ��� 	200 	 	Y 	 
,convert(varchar(50),getdate(),120) undoTime 	--����ʱ�� 	����ʱ���� 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
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



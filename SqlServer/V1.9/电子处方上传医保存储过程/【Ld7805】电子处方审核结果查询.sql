USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D006_Prescription]    Script Date: 2024/11/4 15:11:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**
��Ld7805�����Ӵ�����˽����ѯ
*/

CREATE proc [dbo].[usp_ElectronicPrescription_D006_Prescription]
	@orgId varchar(500),
	@hisId varchar(500),
	@cfh varchar(500),
	@fixmedinsCode varchar(500)
as
begin
select 
D003.hiRxno hiRxno 	--ҽ��������� 	�ַ��� 	30 	 	Y 	 
,@fixmedinsCode fixmedinsCode 	--����ҽ�ƻ������ 	�ַ��� 	20 	 	Y 	 
,ybdj.mdtrt_id mdtrtId 	--ҽ������ID 	�ַ��� 	30 	 	Y 	ҽ������Һ�ʱ���� 
,gh.xm psnName 	--��Ա���� 	�ַ��� 	50 	 	Y 	 
,'01' psnCertType 	--��Ա֤������ 	�ַ��� 	6 	Y 	Y 	 
,gh.zjh certno 	--֤������ 	�ַ��� 	50 	 	Y 	 
 from Dzcf_D003_output D003
  inner join Drjk_mzjzxxsc_input ybdj on D003.mzh=ybdj.mzh and ybdj.zt=1
  inner join mz_gh gh on D003.mzh=gh.mzh and gh.zt=1 and gh.OrganizeId=d003.OrganizeId
  where D003.organizeid=@orgId
  and D003.mzh=@hisId
  and D003.cfh=@cfh
  and D003.zt=1
end

GO



USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D003_Prescription_top20]    Script Date: 2024/11/4 15:10:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/**
 ��Ld7802�����Ӵ���ҽ������ǩ��
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
D001.rxTraceCode rxTraceCode --	����׷���� 	�ַ��� 	20 	 	Y 	��Чʱ��ʹ�����Чʱ�䱣��һ�£��ϴ�ʱÿ�Ŵ���ֻ��ʹ��һ�� 
,D001.hiRxno hiRxno 	--ҽ��������� 	�ַ��� 	30 	 	Y 	 
,gh.jzid mdtrtId 	--ҽ������ID 	�ַ��� 	30 	 	Y 	�α�������Ϣ�ֶΣ�ע��ҽ������Һ�ʱ���أ� 
,gh.xm patnName 	--�������� 	�ַ��� 	40 	 	Y 	 
,'01' psnCertType 	--��Ա֤������ 	�ַ��� 	6 	Y 	Y 	 
,gh.zjh certno 	--֤������ 	�ַ��� 	50 	 	Y 	 
,@fixmedinsName fixmedinsName --	����ҽ�ƻ������� 	�ַ��� 	200 	 	Y 	 
,@fixmedinsCode fixmedinsCode --	����ҽ�ƻ������ 	�ַ��� 	20 	 	Y 	 
,ys.gjybdm drCode --	����ҽ��ҽʦ���� 	�ַ��� 	20 	 	Y 	����ҽ��ҽʦ���� 
,ys.Name prscDrName --	����ҽʦ���� 	�ַ��� 	50 	 	Y 	 
,sfks.Name pharDeptName --	��ҩʦ�������� 	�ַ��� 	50 	 	Y 	 
,sfks.Code pharDeptCode --	��ҩʦ���ұ�� 	�ַ��� 	30 	 	Y 	��ҽҩ��������Ŀ��ҹ�����3401������Ϣ�ϴ���3401A����������Ϣ�ϴ������ϴ��� hosp_dept_codg ҽԺ���ұ����ֶα���һ�� 
,'' pharProfttlCodg --	��ҩʦְ�Ʊ��� 	�ַ��� 	20 	Y 	 	������ҩʦְ�Ʊ��루phar_pro_tech_duty�� 
,'' pharProfttlName --	��ҩʦְ������ 	�ַ��� 	20 	 	 	 
,sfys.gjybdm pharCode --	��ҽ��ҩʦ���� 	�ַ��� 	20 	 	Y 	����ҽ������ҽ�ƻ���ҩѧ��������Ա���루�� HY110000000001�� 
,'' pharCertType 	--��ҩʦ֤������ 	�ַ��� 	6 	Y 	 	������Ա֤������ (psn_cert_type) 
,'' pharCertno 	--��ҩʦ֤������ 	�ַ��� 	50 	 	 	 
,sfys.Name pharName 	--��ҩʦ���� 	�ַ��� 	50 	 	Y 	 
,'' pharPracCertNo --	��ҩʦִҵ�ʸ�֤�� 	�ַ��� 	50 	 	 	 
,@shrq pharChkTime --	ҽ�ƻ���ҩʦ��ʱ�� 	����ʱ���� 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
,'' extras 	--��չ�ֶ� 	JSON 	4000 	 	 	��Ԥ���ֶΣ���ǰδʹ�ã���JSON���л����ַ����󳤶Ȳ��ܳ���4000 
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



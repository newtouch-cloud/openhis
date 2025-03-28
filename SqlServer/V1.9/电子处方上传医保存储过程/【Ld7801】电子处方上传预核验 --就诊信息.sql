USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D001_Prescription_mdtrtData]    Script Date: 2024/11/11 10:28:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/**
 ��Ld7801�����Ӵ����ϴ�Ԥ���� --������Ϣ
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
@fixmedinsName fixmedinsName --	����ҽ�ƻ������� 	�ַ��� 	200 	 	Y 	 
,@fixmedinsCode fixmedinsCode --	����ҽ�ƻ������ 	�ַ��� 	20 	 	Y 	 
,gh.jzid  mdtrtId 	--ҽ������ID 	�ַ��� 	30 	 	Y 	�α�������Ϣ�ֶΣ�ע��ҽ������Һ�ʱ���أ� 
,'11' medType 	--ҽ����� 	�ַ��� 	6 	Y 	Y 	�ο�ҽ�����med_type�� 
,gh.mzh iptOtpNo 	--����/סԺ�� 	�ַ��� 	30 	 	Y 	 
,'1' otpIptFlag 	--����סԺ��ʶ 	�ַ��� 	3 	Y 	 	1-���2-סԺ��ֵΪ��ʱĬ��Ϊ1��� 
,kh.grbh psnNo 	--ҽ����Ա��� 	�ַ��� 	30 	 	Y 	 
,gh.xm patnName 	--�������� 	�ַ��� 	40 	 	Y 	 
,'01' psnCertType 	--��Ա֤������ 	�ַ��� 	6 	Y 	Y 	������Ա֤������(psn_cert_type) 
,gh.zjh certno 	--֤������ 	�ַ��� 	50 	 	Y 	 
,datediff(YEAR,gh.csny,getdate()) patnAge 	--���� 	��ֵ�� 	4,1 	 	Y 	 
,'' patnHgt 	--������� 	��ֵ�� 	6,2 	 	 	 
,'' patnWt 	--�������� 	��ֵ�� 	6,2 	 	 	 
,gh.xb gend 	--�Ա� 	�ַ��� 	6 	Y 	Y 	�ο��Ա�gend��
,'' birctrlType 	--�ƻ������������ 	�ַ��� 	6 	Y 	 	�������ﰴ��¼��
,'' birctrlMatnDate 	--�ƻ������������������� 	������ 	 	 	 	�������ﰴ��¼�룬yyyy-MM-dd 
,'' matnType 	--������� 	�ַ��� 	6 	 	 	 
,'' gesoVal --	����(����) 	��ֵ�� 	2 	 	 	 
,'' nwbFlag --	��������־ 	�ַ��� 	3 	Y 	 	0-��1-�� 
,'' nwbAge 	--�������ա����� 	�ַ��� 	20 	 	 	 
,'' suckPrdFlag 	--�����ڱ�־ 	��ֵ�� 	3 	Y 	 	0-��1-�� 
,'' algsHis 	--����ʷ 	�ַ��� 	1000 	 	 	 
,ks.Name prscDeptName 	--������������ 	�ַ��� 	50 	 	Y 	 
,cf.ks prscDeptCode --	�������ұ�� 	�ַ��� 	30 	Y 	Y 	��ҽҩ��������Ŀ��ҹ�����3401 ������Ϣ�ϴ���3401A����������Ϣ�ϴ������ϴ��� hosp_dept_codgҽԺ���ұ����ֶα���һ�� 
,ys.gjybdm drCode 	--����ҽ��ҽʦ���� 	�ַ��� 	20 	 	Y 	����ҽ��ҽʦ����
,ys.name prscDrName 	--����ҽʦ���� 	�ַ��� 	50 	 	Y 	 
,'' prscDrCertType --	����ҽʦ֤������ 	�ַ��� 	6 	Y 	 	������Ա֤������(psn_cert_type) 
,'' prscDrCertno 	--����ҽʦ֤������ 	�ַ��� 	50 	 	 	 
,'233' drProfttlCodg --	ҽ��ְ�Ʊ��� 	�ַ��� 	20 	 	Y 	���տ���ҽ��ְ��(drord_dr_profttl) 
,'����ҽʦ' drProfttlName 	--ҽ��ְ������ 	�ַ��� 	20 	 	Y 	 
,ks.ybksbm drDeptCode --	ҽ�����ұ��� 	�ַ��� 	30 	 	Y 	��ҽҩ��������Ŀ��ҹ�����3401������Ϣ�ϴ���3401A ����������Ϣ�ϴ� �� �� �� �� �� hosp_dept_codgҽԺ���ұ����ֶα���һ�� 
,ks.Name drDeptName 	--ҽ���������� 	�ַ��� 	50 	 	Y 	 
,'' caty --	�Ʊ� 	�ַ��� 	10 	Y 	 	���ո�¼A:���Ҵ��루dept�� 
,convert (varchar(50),jz.CreateTime,120) mdtrtTime 	--����ʱ�� 	����ʱ����	 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
,'' diseCodg 	--���ֱ��� 	�ַ��� 	30 	 	 	���ձ�׼������д�� �����ֽ��㲡��Ŀ¼����(bydise_setl_list_code)�� �������ز�����Ŀ¼����(opsp_dise_cod)��ҽ�����medType��Ϊ�������ز�ʱ�ش� 
,'' diseName --	�������� 	�ַ��� 	500 	 	 	 
,'0' spDiseFlag 	--���ⲡ�ֱ�־ 	�ַ��� 	3 	Y 	Y 	0-��1-�� 
--,'J10.000' maindiagCode 
,isnull(zd.zdCode_yb,gh.zdicd10) maindiagCode --	����ϴ��� 	�ַ��� 	30 	 	Y 	ҽ��������ϴ���
,gh.zdmc maindiagName --	��������� 	�ַ��� 	100 	 	Y 	 
,'' diseCondDscr 	--������������ 	�ַ��� 	2000 	 	 	 
,'' hiFeesetlType 	--ҽ�����ý������� 	�ַ��� 	6 	Y 	 	�ο�ҽ�����ý������ͣ�hi_feesetl_type�� 
,'' hiFeesetlName 	--ҽ������������� 	�ַ��� 	20 	 	 	 
,'' rgstFee 	--�Һŷ� 	��ֵ�� 	16,2 	 	 	 
,'' medfeeSumamt 	--ҽ�Ʒ��ܶ� 	��ֵ�� 	16,2 	 	 	ע��������Ҫ�����ҽ�Ʒ����ܶԺ�⹺ҩʱ����������ҩƷ���� 
,'' fstdiagFlag --	�Ƿ���� 	�ַ��� 	3 	 	 	0-��1-�� 
,'' extras 	--��չ���� 	������ 	 	 	 	�ط�ҵ����չ��Ϣ�������������ʱ�ɴ�����ط�����ҵ��ϵͳ 
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



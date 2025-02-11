USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_ElectronicPrescription_D001_Prescription_diseData]    Script Date: 2024/11/11 10:28:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/**
 ��Ld7801�����Ӵ����ϴ�Ԥ���� --�����Ϣ
exec  [usp_ElectronicPrescription_D001_Prescription_diseData] '6d5752a7-234a-403e-aa1c-df8b45d3469f','2410309600005','R20241030N000010'
select * from NewtouchHIS_Base..G_yb_diag_list_b where diag_name like '%�����Ը�ð%'

select * from NewtouchHIS_Base..xt_zd where zdmc='�����Ը�ð'
*/


CREATE proc [dbo].[usp_ElectronicPrescription_D001_Prescription_diseData]
	@orgId varchar(50),
	@hisId varchar(50),
	@cfh varchar(500)
as
begin
select  
'1' diagType   --	������ 	�ַ��� 	3 	Y 	Y 	�ο�������diag_type�� 
,case zyzd.zdlx when '1' then '1' else '0' end maindiagFlag --	����ϱ�־ 	�ַ��� 	3 	Y 	Y 	0-��1-�� 
,ROW_NUMBER() OVER(ORDER BY zyzd.zdlx, zyzd.zdlx) diagSrtNo 	--�������� 	��ֵ�� 	2 	 	Y 	 
,isnull(zd.zdCode_yb,zd.icd10) diagCode 	--��ϴ��� 	�ַ��� 	30 	Y 	Y 	ҽ��������ϴ��� 
,zyzd.zdmc diagName 	--������� 	�ַ��� 	100 	 	Y 	 
,ks.Name diagDept 	--��Ͽ������� 	�ַ��� 	50 	 	Y 	 
,ks.ybksbm diagDeptCode 	--��Ͽ��Ҵ��� 	�ַ��� 	20 	 	 	��ҽҩ��������Ŀ��ҹ�����3401������Ϣ�ϴ���3401A ����������Ϣ�ϴ������ϴ���
,gh.ks hosp_dept_codg --ҽԺ���ұ����ֶα���һ�� 
,ys.gjybdm diagDrNo 	--���ҽ������ 	�ַ��� 	30 	 	Y 	����ҽ��ҽʦ���� 
,ys.Name diagDrName --	���ҽ������ 	�ַ��� 	50 	 	Y 	 
,convert (varchar(50),zyzd.CreateTime,120) diagTime 	--���ʱ�� 	����ʱ���� 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
,zyzd.zdCode tcmDiseCode 	--��ҽ�������� 	�ַ��� 	30 	 	 	diagTypeΪ��ҽ��ϣ�ֵΪ2��3��ʱ�ϴ� 
,zyzd.zdmc tcmDiseName 	--��ҽ���� 	�ַ��� 	300 	 	 	diagTypeΪ��ҽ��ϣ�ֵΪ2��3��ʱ�ϴ� 
,isnull(zyzd.zhCode,'') tcmsympCode 	--��ҽ֤����� 	�ַ��� 	30 	 	 	diagTypeΪ��ҽ��ϣ�ֵΪ2��3��ʱ�ϴ� 
,isnull(zyzd.zhmc,'') tcmsymp 	--��ҽ֤�� 	�ַ��� 	300 	 	 	diagTypeΪ��ҽ��ϣ�ֵΪ2��3��ʱ�ϴ� 
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
'1' diagType   --	������ 	�ַ��� 	3 	Y 	Y 	�ο�������diag_type�� 
,case xyzd.zdlx when '1' then '1' else '0' end maindiagFlag --	����ϱ�־ 	�ַ��� 	3 	Y 	Y 	0-��1-�� 
,ROW_NUMBER() OVER(ORDER BY xyzd.zdlx, xyzd.zdlx) diagSrtNo 	--�������� 	��ֵ�� 	2 	 	Y 	 
,isnull(zd.zdCode_yb,zd.icd10) diagCode 	--��ϴ��� 	�ַ��� 	30 	Y 	Y 	ҽ��������ϴ��� 
,xyzd.zdmc diagName 	--������� 	�ַ��� 	100 	 	Y 	 
,ks.Name diagDept 	--��Ͽ������� 	�ַ��� 	50 	 	Y 	 
,ks.ybksbm diagDeptCode 	--��Ͽ��Ҵ��� 	�ַ��� 	20 	 	 	��ҽҩ��������Ŀ��ҹ�����3401������Ϣ�ϴ���3401A ����������Ϣ�ϴ������ϴ���
,gh.ks hosp_dept_codg --ҽԺ���ұ����ֶα���һ�� 
,ys.gjybdm diagDrNo 	--���ҽ������ 	�ַ��� 	30 	 	Y 	����ҽ��ҽʦ���� 
,ys.Name diagDrName --	���ҽ������ 	�ַ��� 	50 	 	Y 	 
,convert (varchar(50),xyzd.CreateTime,120) diagTime 	--���ʱ�� 	����ʱ���� 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
,'' tcmDiseCode 	--��ҽ�������� 	�ַ��� 	30 	 	 	diagTypeΪ��ҽ��ϣ�ֵΪ2��3��ʱ�ϴ� 
,'' tcmDiseName 	--��ҽ���� 	�ַ��� 	300 	 	 	diagTypeΪ��ҽ��ϣ�ֵΪ2��3��ʱ�ϴ� 
,'' tcmsympCode 	--��ҽ֤����� 	�ַ��� 	30 	 	 	diagTypeΪ��ҽ��ϣ�ֵΪ2��3��ʱ�ϴ� 
,'' tcmsymp 	--��ҽ֤�� 	�ַ��� 	300 	 	 	diagTypeΪ��ҽ��ϣ�ֵΪ2��3��ʱ�ϴ� 
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



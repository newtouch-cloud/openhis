USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_Inp_InventoryUpload_invinfo]    Script Date: 2025/1/16 16:00:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO











-- ���ƽ̨��JG10010��������Ʒ��������ϴ�
--exec  [usp_Inp_RegulatoryDataJg10010] '6d5752a7-234a-403e-aa1c-df8b45d3469f',''
--select top 1244 * from [NewtouchHIS_PDS]..xt_yp_pdxxmx order by createtime desc
--select * from [NewtouchHIS_PDS].dbo.xt_yp_pdxx order by createtime desc
CREATE proc [dbo].[usp_Inp_RegulatoryDataJg10010]
	@orgId varchar(50), --��֯����
	@pdId varchar(50)  --�̵���ϸid
as
begin
select  top 20
    org.gjjgdm+pdmx.pdmxid goods_id,
	yp.ypCode xm_id,
	yp.ypmc goods_name,
	yp.py goods_brevity_code, --��Ʒ����ƴ����
	yp.ypgg spec,  --���
	yp.djdw  prcunt,-- ���۵�λ 
	yp.jldw dosform,-- ��������
	case when sfdl.dlmc like '��ҩ%' then '09' when sfdl.dlmc like '��ҩ%' then '10' 
	when sfdl.dlmc like '�г�%' then '11' else '14' end charge_category_coed,  --�շ�������
	sfdl.dlmc charge_category_name,   --�շ��������
	yp.ycmc prodentp_name, --������������
	pdmx.pc aprvno,   --��׼�ĺ�
	case when sfdl.dlmc like '��ҩ%' then '101' when sfdl.dlmc like '��ҩ%' then '103' 
	when sfdl.dlmc like '�г�%' then '102' else '999' end drug_class_code,  --ҩƷ�������
	sfdl.dlmc drug_class_name,  --ҩƷ��������
	yp.gjybdm med_list_codg,--ҽ��Ŀ¼����  
	yp.ypmc med_list_name,  --ҽ��Ŀ¼����
	'' drug_prod_barc, --������
	'' selfcode,--�Ա���
	case when sfdl.dlmc like '��ҩ%' then '101' when sfdl.dlmc like '��ҩ%' then '103' 
	when sfdl.dlmc like '�г�%' then '102' else '999' end commodity_class_code,--��Ʒ�������
	sfdl.dlmc commodity_class_name,  --��Ʒ��������
	yp.pfj purchase_pric ,--�ɹ��� ��ֵ�� 18,4 Y
	yp.lsj pric, --���ۼ� ��ֵ�� 18,4 Y
	'1' is_rcp,-- �Ƿ񴦷�ҩ 0 ��1 ��
	case when yp.gjybdm is null then '0' else '1' end is_medicare_drugs ,--�Ƿ�ҽ��ҩƷ �ַ��� 2 Y 0 ��1 ��
	pdmx.sjsl  inout_cnt ,--��������� ��ֵ�� 18,4 Y ����ʱ������

	-- 1ҩƷ���  2�ⲿ���� 3 ֱ�ӳ���  4������� 5 �ڲ���ҩ�˻�  6���ҷ�ҩ  13 ��ҩ����  14 ��������
	--01 �ɹ���� 02 ���۳��� 03 ������� 05 �ɹ��˻����� 06 ������� 07 �̵���� 08 �̵���� 09 ������� 10 �������� 11 �嶷�����⣩
	--12 װ������⣩ 13 �����˻���� 99 ���ڿ�棨��⣩
	'99' inout_type_code ,					--��������ͱ��� �ַ��� 10 YY �����ֵ� 1.8.4
	'���ڿ��' inout_type_name,   -- ������������� �ַ��� 100 Y 
	'01' bills_type_code,  -- �������ͱ��� �ַ��� 10 Y
	'��ⵥ��' bills_type_name,  --   ������������ �ַ��� 100 Y 
	convert(varchar(50),substring(pdmx.pc,0,5)+'-'+substring(pdmx.pc,5,2)+'-'+substring(pdmx.pc,7,2))  manu_date,  -- �������� ������ Y
    convert(varchar(50),isnull(pdmx.Yxq,'2033-07-01'),23) expy_end,   -- ��Ч�� ������ Y yyyy-MM-dd
	pdmx.pc manu_lotnum ,  --�������� �ַ��� 100
    '/' spler_name ,  --��Ӧ������ �ַ��� 500 Y
	convert(varchar(19),pdmx.CreateTime,121) inout_inventory_date --�����ʱ�� ������ Y yyyy-MM-dd
    into #ypkc
from [NewtouchHIS_PDS].dbo.xt_yp_pdxx (NOLOCK) pdxx
inner join [NewtouchHIS_PDS].dbo.xt_yp_pdxxmx (NOLOCK) pdmx on  pdxx.pdid=pdmx.pdid
--inner join NewtouchHIS_Base..[xt_ypgys] gys on pdmx.ckbm=gys.gyscode
inner join NewtouchHIS_Base..V_C_xt_yp yp on pdmx.ypdm=yp.ypcode and pdxx.organizeid=yp.organizeid and yp.zt='1'
inner join NewtouchHIS_Base..xt_sfdl sfdl on sfdl.dlCode=yp.dlCode and sfdl.organizeid=yp.organizeid and sfdl.zt='1'
left join NewtouchHIS_Base..Sys_Organize org on org.Id=pdxx.organizeId and org.zt='1'
left join Drjk_RegulatoryDataUpload_Output jxcxc on jxcxc.mlbm_id=pdmx.pdmxid and pdxx.organizeid=jxcxc.organizeid and jxcxc.zt='1' and jxcxc.type='JG10010'
where  pdxx.organizeid=@orgId  and pdxx.Jssj is not null 
   --and yp.gjybdm is not null
  -- and pdmx.Llsl!=pdmx.Sjsl
   and pdxx.zt='1'
   and (jxcxc.mlbm_id is null or jxcxc.issuccess='False')
   and pdxx.CreateTime>=convert(datetime,(convert(varchar(50),GETDATE()-31,23)+' 00:00:00'))
  
   if(@pdId!=NULL OR @pdId!='')
   BEGIN
	 select * from #ypkc WHERE goods_id=@pdId  
   END
   ELSE
   BEGIN
    select * from #ypkc 
   END
   drop table #ypkc
end

GO



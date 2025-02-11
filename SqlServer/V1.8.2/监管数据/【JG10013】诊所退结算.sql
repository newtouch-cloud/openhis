USE [NewtouchHIS_Sett]
GO

/****** Object:  StoredProcedure [dbo].[usp_Inp_RegulatoryDataJg10012_feedetail]    Script Date: 2025/1/17 14:11:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--【JG10013】诊所退结算
--exec  [usp_Inp_RegulatoryDataJg10013] '6d5752a7-234a-403e-aa1c-df8b45d3469f',''
alter proc [dbo].[usp_Inp_RegulatoryDataJg10013]
	@orgId varchar(50),
	@setl_id varchar(50)
as
begin
select  jxcxc.mlbm_id,
    mzjs.cxjsnm medins_setl_id --医药机构结算 ID 字符型 30 Y ERP 系统结算唯一标识
	into #temp
from [NewtouchHIS_Sett]..mz_js mzjs with(nolock)
left join Drjk_RegulatoryDataUpload_Output jxcxc on jxcxc.mlbm_id=cast(mzjs.cxjsnm as varchar) and jxcxc.organizeid=@orgId and jxcxc.zt='1' 
	and jxcxc.type='JG10012'
where   mzjs.OrganizeId=@orgId   and mzjs.zje>0 and mzjs.jszt='2'
	and mzjs.CreateTime>=convert(datetime,(convert(varchar(50),GETDATE()-30,23)+' 00:00:00'))
	and jxcxc.mlbm_id is not null
if(@setl_id!=NULL OR @setl_id!='')
   BEGIN
	select * from #temp where medins_setl_id=@setl_id 
   END
   ELSE
   BEGIN
    select * from #temp 
   END
end

--select * from mz_js order by createtime desc

GO



USE [Newtouch_CIS]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/27/2024 18:12:38
ALTER proc [dbo].[usp_zy_brxxexpand_create]
	@orgId varchar(50),
	@date varchar(10)
as
begin
	declare @bdate datetime,@edate datetime
	
	if(ISDATE(@date)=0)
	begin
		set @edate=convert(datetime,convert(datetime,getdate()))
		set @bdate=dateadd(mm,-1,@edate)
	end
	else
	begin
		set @edate=convert(datetime,convert(datetime,@date))
		set @bdate=dateadd(mm,-1,@edate)
	end

	select @bdate,@edate

	select @date tdrq,a.OrganizeId,a.zyh,a.blh,c.patid,a.[cqrq],a.[ryrq],
	 a.[zybz],c.zhcode,c.zhxz,c.zhye 
	into #tmp
	from zy_brxxk a with(nolock)
	left join [NewtouchHIS_Sett].dbo.zy_zh c with(nolock) on a.OrganizeId=c.OrganizeId and c.zt='1' and a.zyh=c.zyh
	where a.OrganizeId=@orgId and a.zt='1'
	--and a.zybz in(1,7)
	and( a.CreateTime between @bdate and @edate or a.LastModifyTime between @bdate and @edate)


	select OrganizeId,zyh,sum(sl*dj) [xmzfy]
	into #xmfy
	from [NewtouchHIS_Sett].[dbo].[V_C_Sys_HbtfZyXmjfb] a with(nolock)
	where a.OrganizeId=@orgId and a.zt='1'
	and exists(select 1 from [NewtouchHIS_Sett].[dbo].[V_C_Sys_HbtfZyXmjfb] b with(nolock)
				where a.organizeid=b.organizeid and a.zyh=b.zyh and b.zt='1'
					and( b.CreateTime between @bdate and @edate or b.LastModifyTime between @bdate and @edate))
	group by OrganizeId,zyh

	select OrganizeId,zyh,sum(sl*dj) [ypzfy]
	into #ypfy
	from [NewtouchHIS_Sett].[dbo].[V_C_Sys_HbtfZyYpjfb] a with(nolock)
	where a.OrganizeId=@orgId and a.zt='1'
	and exists(select 1 from [NewtouchHIS_Sett].[dbo].[V_C_Sys_HbtfZyYpjfb] b with(nolock)
			where a.organizeid=b.organizeid and a.zyh=b.zyh and b.zt='1'
				and( b.CreateTime between @bdate and @edate or b.LastModifyTime between @bdate and @edate))
	group by OrganizeId,zyh


	select  OrganizeId,zyh,sum(zje) yjfy
	into #tmpyj
	from [NewtouchHIS_Sett].dbo.zy_js  a with(nolock)
	where a.OrganizeId=@orgId and a.zt='1' and jsxz=2 and jszt=1
	and not exists(select 1 from  [NewtouchHIS_Sett].dbo.zy_js b with(nolock) where a.zyh=b.zyh and a.organizeid =b.OrganizeId and a.jsnm=b.cxjsnm) 
	group by  OrganizeId,zyh



	update a
	set a.cqrq=b.cqrq,a.ryrq=b.ryrq,
	a.zhcode=b.zhcode,a.zhxz=b.zhxz,a.zhye=b.zhye,a.zybz=b.zybz,
	a.LastModifierCode='ETL',a.LastModifyTime=GETDATE()
	from [zy_brxxk_expand] a,#tmp b
	where a.zyh=b.zyh and a.OrganizeId=b.OrganizeId and a.zt='1'

	update a
	set a.xmzfy=c.[xmzfy],a.LastModifierCode='ETL',a.LastModifyTime=GETDATE()
	from [zy_brxxk_expand] a,#xmfy c
	where a.OrganizeId=c.OrganizeId and a.zyh=c.zyh  and a.zt='1'
	update a
	set a.ypzfy=c.[ypzfy],a.LastModifierCode='ETL',a.LastModifyTime=GETDATE()
	from [zy_brxxk_expand] a,#ypfy c
	where a.OrganizeId=c.OrganizeId and a.zyh=c.zyh  and a.zt='1'

	update a
	set a.yjfy=b.yjfy 
	from [zy_brxxk_expand] a,#tmpyj b
	where a.zyh=b.zyh and a.OrganizeId=b.OrganizeId and a.zt='1'

	INSERT INTO [dbo].[zy_brxxk_expand]
	([id],[OrganizeId],[tdrq],[zyh],[blh],[patid],[ryrq],[cqrq],[zybz],[zhcode],[zhye],[zhxz]
	,[xmzfy],[ypzfy],[CreateTime],[CreatorCode],[zt],yjfy)
	select newid(),a.[OrganizeId],[tdrq],a.[zyh],[blh],[patid],[ryrq],[cqrq],[zybz],[zhcode],[zhye],[zhxz]
	,[xmzfy],[ypzfy],getdate(),'ETL','1',c.yjfy
	from #tmp a 
	left join #xmfy b on a.OrganizeId=b.OrganizeId and a.zyh=b.zyh
	left join #ypfy d on a.OrganizeId=d.OrganizeId and a.zyh=d.zyh
	left join #tmpyj c on a.OrganizeId=c.OrganizeId and a.zyh=c.zyh
	where not exists(select 1 from [zy_brxxk_expand] c where a.zyh=c.zyh and a.OrganizeId=c.OrganizeId and c.zt='1' )
	order by patid

	select @@ROWCOUNT

	drop table #tmp
	drop table #xmfy
	drop table #ypfy
	drop table #tmpyj

end







USE [NewtouchHIS_Sett]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/20/2024 11:50:34

















/*
author:tyz
modify:2021-10-13
desc:实时同步药品信息 
declare @newcount int
exec skd_syncypfeefrompds  '6d5752a7-234a-403e-aa1c-df8b45d3469f','','00001',@newcount output
select @newcount
*/
ALTER proc [dbo].[skd_syncypfeefrompds]
	@orgId varchar(50),
	@lqrq datetime,
	@zyh varchar(20),
	@newcount int output
as
begin
	select 0 tyczid,convert(int,id) id, organizeid,zyh,zxrq,ypcode,dl,zlff,yl,yldw,ysgh,cw,dj,sl,je,zfbl,
	zfxz,fyyf,yzxz,ksCode ks,bqCode bq,creatorcode,a.yzId
	into #ykinfo
	from [NewtouchHIS_PDS].[dbo].[zy_ypyzxx] a with(nolock)
	where 1=2

	if(@zyh<>'')
	begin
		--print 'zyh'
		select @lqrq=ryrq from zy_brjbxx with(nolock) where zyh=@zyh and organizeid=@orgId and zt=1

		insert into #ykinfo
		select 0 tyczid,convert(int,id) id, organizeid,zyh,zxrq,ypcode,dl,zlff,yl,yldw,ysgh,cw,dj,sl,je,zfbl,
		zfxz,fyyf,yzxz,ksCode ks,bqCode bq,creatorcode,yzId	 
		FROM [NewtouchHIS_PDS].[dbo].[zy_ypyzxx] a with(nolock)
		where a.OrganizeId=@orgId and  a.zyh = @zyh and
		fybz in(2,3)
		and createtime >= @lqrq 
		and isnull(sl,0)>0
		--and not exists(select 1 from zy_ypjfb b with(nolock) where a.id=b.bdzxid and a.organizeid=b.organizeid)
	end
	else
	begin
		insert into #ykinfo
		select 0 tyczid,convert(int,id) id, organizeid,zyh,zxrq,ypcode,dl,zlff,yl,yldw,ysgh,cw,dj,sl,je,zfbl,
		zfxz,fyyf,yzxz,ksCode ks,bqCode bq,creatorcode,yzId	 
		FROM [NewtouchHIS_PDS].[dbo].[zy_ypyzxx] a with(nolock)
		where OrganizeId=@orgId and  --(isnull(@zyh, '') = '' or a.zyh = @zyh) and
		fybz in(2,3)   --2 发药 3 退药
		and createtime between DateAdd(day, -100, @lqrq) and @lqrq
		and isnull(sl,0)>0
		--and not exists(select 1 from zy_ypjfb b with(nolock) where a.id=b.bdzxid and a.organizeid=b.organizeid)
	end

	insert into #ykinfo
	select a.id tyczid,b.id, a.organizeid,zyh,zxrq,a.ypcode,dl,zlff,yl,yldw,ysgh,cw,dj,-a.sl,-a.sl*dj je,zfbl,
	zfxz,fyyf,yzxz,b.ksCode ks,b.bqCode bq,a.LastModifierCode,a.yzId
	--select *
	from  [NewtouchHIS_PDS].[dbo].[zy_ypyzczjl] a with(nolock),[NewtouchHIS_PDS].[dbo].[zy_ypyzxx] b with(nolock)
	where a.ypyzxxid=b.id and a.OrganizeId=b.OrganizeId and
	a.operatetype=2 --1 发药 2 退药
	and exists(select 1 from #ykinfo c where a.ypyzxxid=c.id)
	
	if(exists(select 1 from #ykinfo))
	begin
		--生成流水
		declare @startjfbId int = 0,	--插入记录开始Id
				@jfCount int

		select @jfCount=count(1) 
		from #ykinfo a 
		where not exists(select 1 from zy_ypjfb b with(nolock) 
						where CONVERT(varchar(50), a.id)=b.bdzxid and a.organizeid=b.organizeid and a.tyczid=isnull(b.yftyczId,0))

		SELECT @startjfbId = CurrentSerialNo FROM [NewtouchHIS_Sett].[dbo].[EntitySerialNo] where EntityName = 'zy_ypjfb'
		--占用Id
		if(@startjfbId > 0)
			update EntitySerialNo set CurrentSerialNo = CurrentSerialNo + @jfCount + 1 where EntityName = 'zy_ypjfb'
		else
			insert into EntitySerialNo(EntityName,SerialNoMin,SerialNoMax,CurrentSerialNo)
			values('zy_ypjfb', 1, 999999999, @jfCount + 1)

		select tyczid,@startjfbId+row_number()over(order by a.id) jfbbh,a.id,a.organizeid,zyh,zxrq,ypcode,dl,'' zlff,yl,yldw,ysgh,b.name,
		ks,c.name ksmc,cw,dj,sl,yldw jfdw,zfbl,zfxz,fyyf,1 yzxz,1 yzzt,CreatorCode,getdate() CreateTime,bq,1 zt,
		(case when sl<0 then a.id else 0 end) cxzyjfbbh,a.yzId
		into #ykinfoend
		from #ykinfo a
		left join [NewtouchHIS_Base].[dbo].[V_C_Sys_UserStaff]  b with(nolock) on a.ysgh=b.gh and a.organizeid=b.organizeid and b.zt=1
		left join [NewtouchHIS_Base].[dbo].V_S_Sys_Department c with(nolock) on a.ks=c.code and a.organizeid=c.organizeid and c.zt=1
		where not exists(select 1 from zy_ypjfb d with(nolock) 
				where CONVERT(varchar(50), a.id)=d.bdzxid and a.organizeid=d.organizeid and a.tyczid=isnull(d.yftyczId,0))

				print 'test1'
		--新增记录
		insert into zy_ypjfb(yftyczId,jfbbh, OrganizeId, zyh, tdrq, yp, dl, zlff,yl,yldw,ys,ysmc,ks,ksmc,cw,dj,sl,jfdw,zfbl,zfxz,
		lyyf,yzxz,yzzt,CreatorCode,CreateTime,bq,zt,cxzyjfbbh,bdzxid,yzwym)
		select a.tyczid,--case when a.sl<0 then b.tyczid else 0 end tyczid, 
		a.jfbbh,a.organizeid,a.zyh,a.zxrq,a.ypcode,c.dlCode,a.zlff,a.yl,a.yldw,a.ysgh,a.name,
		a.ks,a.ksmc,a.cw,a.dj,a.sl,c.zycldw,c.zfbl,c.zfxz,a.fyyf,a.yzxz,a.yzzt,a.CreatorCode,a.CreateTime,a.bq,a.zt,--,cxzyjfbbh
		(case when a.sl<0 then b.jfbbh else 0 end) cxzyjfbbh ,a.id,a.yzId
		from #ykinfoend a
		left join #ykinfoend b on a.id=b.id and b.sl>0
		left join [NewtouchHIS_Base].[dbo].[xt_yp] c  on a.organizeid=c.organizeid and a.ypcode=c.ypcode
		--left join [NewtouchHIS_Base].[dbo].[V_S_xt_yp] c on a.organizeid=c.organizeid and a.ypcode=c.ypcode and c.zt=1
		where not exists(select 1 from zy_ypjfb d with(nolock) where CONVERT(varchar(50), a.id)=d.bdzxid and a.organizeid=d.organizeid )
		
		select @newcount=@@rowcount
		print 'test'
		--补新增退费
		insert into zy_ypjfb(yftyczId,jfbbh, OrganizeId, zyh, tdrq, yp, dl, zlff,yl,yldw,ys,ysmc,ks,ksmc,cw,dj,sl,jfdw,zfbl,zfxz,
		lyyf,yzxz,yzzt,CreatorCode,CreateTime,bq,zt,cxzyjfbbh,bdzxid,yzwym)
		select tyczid,a.jfbbh,a.organizeid,a.zyh,a.zxrq,a.ypcode,c.dlCode,a.zlff,a.yl,a.yldw,a.ysgh,a.name,
		a.ks,a.ksmc,a.cw,a.dj,a.sl,c.zycldw,c.zfbl,c.zfxz,a.fyyf,a.yzxz,a.yzzt,a.CreatorCode,a.CreateTime,a.bq,a.zt,--,cxzyjfbbh
		(case when a.sl<0 then d.jfbbh else 0 end) cxzyjfbbh ,a.id,a.yzId
		from zy_ypjfb d with(nolock),#ykinfoend a
		left join [NewtouchHIS_Base].[dbo].[xt_yp] c  on a.organizeid=c.organizeid and a.ypcode=c.ypcode
		--left join [NewtouchHIS_Base].[dbo].[V_S_xt_yp] c on a.organizeid=c.organizeid and a.ypcode=c.ypcode and c.zt=1
		where not exists(select 1 from zy_ypjfb b with(nolock) where CONVERT(varchar(50), a.id)=b.bdzxid and a.organizeid=b.organizeid and a.tyczid=b.yftyczid )
		and CONVERT(varchar(50), a.id)=d.bdzxid and a.organizeid=d.organizeid and isnull(d.cxzyjfbbh,0)=0 --去除关联重复

		select @newcount=@newcount + @@rowcount
		
		drop table #ykinfoend

	end
	else
	begin
		set @newcount=0
	end


	drop table #ykinfo

end



















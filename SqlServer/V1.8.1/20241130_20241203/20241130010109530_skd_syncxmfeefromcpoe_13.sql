USE [NewtouchHIS_Sett]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/30/2024 01:01:09







/*
declare @newcount int
exec skd_syncxmfeefromcpoe '6d5752a7-234a-403e-aa1c-df8b45d3469f','2024-8-27 23:59:59', '03319', @newcount output
select @newcount
*/
ALTER PROCEDURE [dbo].[skd_syncxmfeefromcpoe]  
@orgId VARCHAR(50),
@lqrq datetime,	--拉取日期（带时分秒）	会拉取 @lqrq - day1 至 @lqrq 24小时之内 的 记录
@zyh varchar(20),	--若仅同步个人 指定值。
@newcount int output
AS
begin
	set @newcount = 0
	declare @jfCount int = 0	--插入总记录数
	declare @zzfbz int --转自费标志
	select zyh,zxrq,xmdm,ysgh,DeptCode,cwdm,dj,sl,CreateTime,zxksdm,Id,CreatorCode,WardCode,OrganizeId,yzxh,@zzfbz zzfbz,yzxz
	into #zy_xm
	from [Newtouch_CIS]..zy_fymxk xmfymx with(nolock)
	where 1=2

	select zyh,id,zzfbz,OrganizeId,zt into #yztab from Newtouch_CIS..zy_cqyz  where 1=2

	if(@zyh<>'')
	begin
		select @lqrq=ryrq from zy_brjbxx with(nolock) where zyh=@zyh and organizeid=@orgId and zt=1

		insert into #yztab
		select zyh,Id,zzfbz,OrganizeId,zt from (
		select zyh,id,zzfbz,OrganizeId,zt from Newtouch_CIS..zy_cqyz
		where zt='1' and zyh=@zyh and OrganizeId=@orgId and createtime >=@lqrq
		union all

		select zyh,id,zzfbz,OrganizeId,zt from Newtouch_CIS..zy_lsyz
		where zt='1' and zyh=@zyh and OrganizeId=@orgId and createtime >=@lqrq
		) yztable

		insert into #zy_xm
		select a.zyh,zxrq,xmdm,ysgh,DeptCode,cwdm,dj,isnull(sl,0)sl,CreateTime,zxksdm,
		a.Id,CreatorCode,WardCode,a.OrganizeId,yzxh,isnull(zzfbz,0) zzfbz,a.yzxz
		from [Newtouch_CIS]..zy_fymxk a with(nolock)
		left join #yztab yzxx on yzxx.id=a.yzxh and yzxx.zyh=a.zyh and yzxx.OrganizeId=a.OrganizeId and yzxx.zt='1'
		where a.OrganizeId=@orgId and a.zyh=@zyh
		and CreateTime>=@lqrq
		and a.yzlb in('-1','1')
		--and isnull(a.sl,0) > 0
		and a.zt=1 and isnull(a.isjf,1)<>'0'
		and not exists(select 1 from zy_xmjfb b with(nolock) 
					where a.OrganizeId=b.OrganizeId and a.zyh=b.zyh and a.id=b.bdzxid)
		and exists(select 1 from zy_brjbxx c with(nolock) where a.OrganizeId=c.OrganizeId and a.zyh=c.zyh and 
					c.zybz in (1,2,7) and c.zt=1)	--病区中、出病区、转区状态
	end
	else
	begin
		insert into #yztab
		select zyh,Id,zzfbz,OrganizeId,zt from (
		select zyh,id,zzfbz,OrganizeId,zt from Newtouch_CIS..zy_cqyz
		where zt='1'  and OrganizeId=@orgId and createtime  between DateAdd(day, -10, @lqrq) and @lqrq
		union all

		select zyh,id,zzfbz,OrganizeId,zt from Newtouch_CIS..zy_lsyz
		where zt='1'  and OrganizeId=@orgId and createtime between DateAdd(day, -10, @lqrq) and @lqrq
		) yztable

		insert into #zy_xm
		select a.zyh,zxrq,xmdm,ysgh,DeptCode,cwdm,dj,isnull(sl,0)sl,CreateTime,zxksdm,
		a.Id,CreatorCode,WardCode,a.OrganizeId,yzxh,isnull(zzfbz,0) zzfbz,a.yzxz
		from [Newtouch_CIS]..zy_fymxk a with(nolock)
		left join #yztab yzxx on yzxx.id=a.yzxh and yzxx.zyh=a.zyh and yzxx.OrganizeId=a.OrganizeId and yzxx.zt='1'
		where a.OrganizeId=@orgId --and a.zyh=@zyh
		and a.createtime between DateAdd(day, -1, @lqrq) and @lqrq
		and a.yzlb in('-1','1')
		--and isnull(a.sl,0) > 0
		and a.zt=1 and isnull(a.isjf,1)<>'0'
		and not exists(select 1 from zy_xmjfb b with(nolock) 
					where a.OrganizeId=b.OrganizeId and a.zyh=b.zyh and a.id=b.bdzxid)
		and exists(select 1 from zy_brjbxx c with(nolock) where a.OrganizeId=c.OrganizeId and a.zyh=c.zyh and 
					c.zybz in (1,2,7) and c.zt=1)	--病区中、出病区、转区状态
		
		--检验计费准备
		select b.id
		into #jyid
		from Newtouch_Interface.dbo.lis_confirm_zy c with(nolock),[Newtouch_CIS].dbo.zy_lsyz b with(nolock) 
		where c.createtime>convert(date,getdate()) and c.qrzt=1 and c.sqdh>'' 
		and c.zyh=b.zyh and c.sqdh=b.yzh and c.organizeid=b.OrganizeId and b.zt='1' and b.yzlx=6
		and  exists(select 1 from zy_brjbxx d with(nolock) where b.OrganizeId=d.OrganizeId and b.zyh=d.zyh and d.zt=1) 
		and not exists(select 1 from zy_xmjfb f with(nolock) 
			where b.OrganizeId=f.OrganizeId and b.zyh=f.zyh and b.id=f.yzwym)

		if(exists(select 1 from #jyid))
		begin
			insert into #zy_xm
			select a.zyh,zxrq,xmdm,ysgh,DeptCode,cwdm,dj,isnull(sl,0)sl,CreateTime,zxksdm,
			a.Id,CreatorCode,WardCode,a.OrganizeId,yzxh,isnull(zzfbz,0) zzfbz,a.yzxz
			from [Newtouch_CIS]..zy_fymxk a with(nolock)
			left join #yztab yzxx on yzxx.id=a.yzxh and yzxx.zyh=a.zyh and yzxx.OrganizeId=a.OrganizeId and yzxx.zt='1'
			where a.OrganizeId=@orgId --and a.zyh=@zyh
			and yzxh in(select id from #jyid)
			and a.yzlb in('-1','1')
			--and isnull(a.sl,0) > 0
			and a.zt=1 and isnull(a.isjf,1)<>'0'
			and not exists(select 1 from #zy_xm e where a.Id=e.Id )
			and not exists(select 1 from zy_xmjfb b with(nolock) 
					where a.OrganizeId=b.OrganizeId and a.zyh=b.zyh and a.id=b.bdzxid)
		end

		drop table #jyid
	end

	--2021-9-16 chl 检验单独计费
	select * into #sync_xm from #zy_xm where isnull(yzxz,0)<>1
	if(exists(select 1 from #zy_xm where yzxz=1))
	begin
		insert into #sync_xm
		select *
		from #zy_xm a 
		where a.yzxz=1 
		and exists(select 1 from Newtouch_CIS.dbo.zy_lsyz b with(nolock) 
			where a.yzxh=b.id --and b.yzlx<>6
			)
		
		--print('lis已接收同步')
		--insert into #sync_xm
		--select *
		--from #zy_xm a 
		--where a.yzxz=1 
		--and exists(select 1 from [Newtouch_CIS].dbo.zy_lsyz b with(nolock) 
		--	where a.yzxh=b.id and b.yzlx=6
		--	and exists(select 1
		--		from Newtouch_Interface.dbo.lis_confirm_zy c with(nolock)
		--		where b.OrganizeId=c.OrganizeId and b.zyh=c.zyh and b.yzh=c.sqdh
		--		 and c.createtime> b.createtime
		--		and c.qrzt=1 and c.zt<>9 and c.sqdh>'' )
		--		)  --已接收 计费
	end


	if(exists(select 1 from #sync_xm))
	begin
		select @jfCount = count(1) from #sync_xm

		declare @startjfbId int = 0	--插入记录开始Id
		SELECT @startjfbId = CurrentSerialNo FROM [NewtouchHIS_Sett].[dbo].[EntitySerialNo] where EntityName = 'zy_xmjfb'
		--占用Id	得再加1
		if(@startjfbId > 0)
			update EntitySerialNo set CurrentSerialNo = CurrentSerialNo + @jfCount + 1 where EntityName = 'zy_xmjfb'
		else
			insert into EntitySerialNo(EntityName,SerialNoMin,SerialNoMax,CurrentSerialNo)
			values('zy_xmjfb', 1, 999999999, @jfCount + 1)

		--执行插入
		insert into zy_xmjfb(jfbbh, OrganizeId, zyh, tdrq, sfxm, dl, ys, ks, cw, dj, sl, jfdw
		,zfbl,zfxz,ssbz,ssry,ssrq,zxks,yzxz,yzzt,cxzyjfbbh,bdzxId,CreatorCode,CreateTime,bq,zt,ysmc,ksmc,yzwym,zzfbz)
		select @startjfbId + ROW_NUMBER() over(order by xmfymx.CreateTime) as jfbbh, @orgId OrganizeId
		, xmfymx.zyh, CONVERT(varchar(100), xmfymx.zxrq, 121) tdrq, xmdm sfxm, sfdl.dlCode dl
		, isnull(xmfymx.ysgh, '') ys, isnull(xmfymx.DeptCode, '') ks, xmfymx.cwdm cw
		, xmfymx.dj, xmfymx.sl, sfxm.dw jfdw
		, sfxm.zfbl, sfxm.zfxz, 1 ssbz, '' ssry, xmfymx.CreateTime ssrq,xmfymx.zxksdm
		, '2' yzxz, '1' yzzt, 0 cxzyjfbbh
		, xmfymx.Id bdzxId, xmfymx.CreatorCode, getdate() CreateTime
		, xmfymx.WardCode bq, '1' zt, '' ysmc, '' ksmc,yzxh,zzfbz
		--同
		from #sync_xm xmfymx,[NewtouchHIS_Base]..V_S_xt_sfxm sfxm,[NewtouchHIS_Base]..V_S_xt_sfdl sfdl
		where sfxm.sfxmCode = xmfymx.xmdm and sfxm.Organizeid = xmfymx.OrganizeId and sfxm.zt = '1'
		and sfdl.dlCode = sfxm.sfdlCode and sfdl.Organizeid = xmfymx.OrganizeId and sfdl.zt = '1'
	end

	select @newcount=@jfCount
	drop table #zy_xm
	drop table #sync_xm
end








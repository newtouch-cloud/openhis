USE [Newtouch_EMR]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/21/2024 11:09:46

/*
author:chl
createtime:3021-7-29
desc:病案首页费用 从Sett结算同步 病案系统-病案费用维护  版本
exec usp_sync_basy_feefromSett
*/
ALTER proc [dbo].[usp_sync_basy_feefromSett]
as
begin
	declare  @date datetime
	select @date=convert(datetime,convert(date,getdate()-2))

	print(@date)

	select jsnm,[OrganizeId],zyh,brxz,zyts,
	(case when a.jszt=1 then a.zje else -a.zje end)zje,
	zlfy,zffy,jzfy,
	(case when a.jszt=1 then a.xjzf else -a.xjzf end)xjzf,xjzffs,
	fph,jszt,cxjsnm,jsksrq,jsjsrq,ysk,zl,a.CreateTime
	into #js
	from [NewtouchHIS_Sett].dbo.zy_js a with(nolock)
	where zt='1' and --a.jszt=1 and
	a.CreateTime >= @date --'2021-7-1' --and a.CreateTime<'2021-7-28' 
	--and not exists(select 1 from [NewtouchHIS_Sett].dbo.zy_js b with(nolock) where a.jsnm=b.cxjsnm and jszt=2 and zt='1' )

	insert into #js
	select jsnm,[OrganizeId],zyh,brxz,zyts,
	(case when a.jszt=1 then a.zje else -a.zje end)zje,
	zlfy,zffy,jzfy,
	(case when a.jszt=1 then a.xjzf else -a.xjzf end)xjzf,xjzffs,
	fph,jszt,cxjsnm,jsksrq,jsjsrq,ysk,zl,a.CreateTime
	from [NewtouchHIS_Sett].dbo.zy_js a with(nolock)
	where --exists(select 1 from #js b where a.jsnm=b.cxjsnm) and 
	a.zt='1' and exists(select 1 from #js where a.zyh=#js.zyh and a.OrganizeId=#js.OrganizeId)
	and not exists(select 1 from #js c where a.jsnm=c.jsnm)


	select m.*,n.zyts,n.jsksrq,n.jsjsrq
	into #zyh
	from (
		select a.zyh,a.brxz,organizeid,
		sum(a.zje)zje,
		sum(a.xjzf)xjzf,max(jsnm) jsnm
		from #js a 
		where --zyh='00069' and 
		jszt=1 and not exists(select 1 from #js b where a.jsnm=b.cxjsnm)
		group by a.zyh,a.brxz,organizeid
	)m
	left join #js n on m.zyh=n.zyh and m.jsnm=n.jsnm
	order by m.zyh

	select b.Id,a.zyh,b.BAH,a.brxz,a.zyts,a.zje,a.xjzf
	into #updzfy
	from mr_basy b with(nolock),#zyh a
	where  a.zyh=b.zyh and a.organizeid=b.organizeid and b.zt='1'
	and isnull(b.bazt,'0')<>3
	and exists(select 1 from zy_brjbxx c  with(nolock) where b.OrganizeId=c.OrganizeId and b.zyh=c.zyh and c.zt='1' and c.RecordStu>0)
	and isnull(b.ZFY,0.0000)<>a.zje
	 
	--select * from #updzfy where zyh='00032'
	--select * from #zyh where zyh='00032'
	--select * from #js where zyh='00032'

	if(exists(select 1 from #updzfy))
	begin
		select a.[OrganizeId],zyh,tdrq,
		isnull(b.ShortCode,'QTF')FeetypeCode,
		isnull(b.Name,'其他费') FeetypeName,
		a.dj*a.sl Amount
		into #fee
		from [NewtouchHIS_Sett].[dbo].[V_C_Sys_HbtfZyYpjfb] a with(nolock)
		left join [NewtouchHIS_Base].[dbo].[V_S_xt_sfdl]  c with(nolock) on a.dl=c.dlcode and a.organizeid=c.organizeid and c.zt='1'
		left join [Newtouch_MRMS].[dbo].mr_dic_sfdlrelbafee d with(nolock) on c.dlCode=d.[hissfdl] and c.OrganizeId=d.OrganizeId and d.zt='1'
		left join [Newtouch_MRMS].[dbo].[mr_dic_bafeetype] b with(nolock) on d.[feetypecode]=b.Code and d.organizeid=b.organizeid and b.zt='1'
		where a.zyh in(select zyh from #updzfy)

		insert into #fee
		select a.[OrganizeId],zyh,tdrq,isnull(b.ShortCode,'QTF')FeetypeCode,isnull(b.Name,'其他费') FeetypeName,a.dj*a.sl Amount
		from [NewtouchHIS_Sett].[dbo].[V_C_Sys_HbtfZyXmjfb] a with(nolock)
		left join [Newtouch_MRMS].[dbo].[mr_dic_sfxmrel] c with(nolock) on a.sfxm=c.sfxm and a.organizeid=c.organizeid and c.zt='1'
		left join [Newtouch_MRMS].[dbo].[mr_dic_bafeetype] b with(nolock) on c.feetypecode=b.code and c.organizeid=b.organizeid and b.zt='1'
		where a.zyh in(select zyh from #updzfy)

		--select * from #fee where zyh='00032'

		begin try
			--begin tran
				--更新总金额
				print '更新总金额'
				update a
				set a.ZFY=b.zje , a.ZFJE=b.xjzf ,
				a.SJZYTS=(case when isnull(a.SJZYTS,'')='' 
						then convert(varchar(10),b.zyts) else a.SJZYTS end)--,a.LastModifierCode='admin',a.LastModifyTime=getdate()
				from mr_basy a,#updzfy b 
				where a.Id=b.Id and a.zt='1'  

				--select zfy,ZFJE, * from mr_basy a,#updzfy b 
				--where a.Id=b.Id and a.zt='1'  

				--更新明细费
				print '更新明细费'
				select  a.id,a.bah,a.zyh,
					XYF=max(case when FeetypeCode='XYF' then Amount else 0.00 end),
					ZCYF=max(case when FeetypeCode='ZCYF' then Amount else 0.00 end),
					ZCYF1=max(case when FeetypeCode='ZCYF1' then Amount else 0.00 end),
					QTF=max(case when FeetypeCode='QTF' then Amount else 0.00 end)
				into #feedetail
				from mr_basy a,(	
					select OrganizeId, zyh, FeetypeCode,FeetypeName,convert(decimal(18,2),(isnull(sum(Amount),0)))Amount
					from #fee
					group by OrganizeId,zyh, FeetypeCode,FeetypeName
						)c
				where a.ZYH=c.zyh and a.OrganizeId=c.OrganizeId and a.zt='1'
				group by a.id,a.bah,a.zyh
	
				update a
				set	a.XYF=b.XYF,
					a.ZCYF=b.ZCYF,
					a.ZCYF1=b.ZCYF1,
					a.QTF=b.QTF
					--,a.LastModifierCode='admin',a.LastModifyTime=getdate()
				from mr_basy a,#feedetail b
				where a.Id=b.Id and a.zt='1'

				--select a.XYF,a.ZCYF,a.ZCYF1,a.QTF,a.ZYH from mr_basy a where zyh in(select zyh from #updzfy) and zt='1' and a.OrganizeId='6d5752a7-234a-403e-aa1c-df8b45d3469f'

				drop table #feedetail
				drop table #fee
			--rollback
		end try
		begin catch
			print(@@error)
		end catch

	end
	else
	begin
		print '无可更新费用'
	end

	drop table #js
	drop table #zyh
	drop table #updzfy

end


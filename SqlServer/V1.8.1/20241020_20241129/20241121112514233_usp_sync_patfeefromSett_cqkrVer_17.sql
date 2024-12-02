USE [Newtouch_EMR]
GO

-- 事件类型: CREATE_PROCEDURE
-- 变更时间: 11/21/2024 11:25:14






/*
author:chl
createtime:2021-7-29
desc:病案首页费用 从Sett结算同步 重庆宽仁 病案系统-病案费用维护  版本
exec [usp_sync_patfeefromSett] '6d5752a7-234a-403e-aa1c-df8b45d3469f' ,'00137'
*/
CREATE proc [dbo].[usp_sync_patfeefromSett_cqkrVer]
	@orgId varchar(50),
	@zyh varchar(20)
as
begin
	select a.jsnm, a.zyh, a.zje,a.zyts,a.jsksrq,a.jsjsrq ,a.xjzf
	into #js
	from [NewtouchHIS_Sett].dbo.zy_js a with(nolock) 
	where a.OrganizeId=@orgId
	and a.jszt=1 and a.zyh=@zyh and a.zt=1
	and not exists(select 1 from [NewtouchHIS_Sett].dbo.zy_js b with(nolock) where a.jsnm=b.cxjsnm and jszt=2 and zt='1' )
	--and exists(select 1 from Newtouch_EMR.dbo.mr_basy c with(nolock) where a.zyh=c.zyh and c.zt='1')

	select jsnm,yzlx,d.code,sum(jyje) je,--d.shortcode,b.sfxm,c.sfxmmc	
	isnull(d.ShortCode,'QTF')FeetypeCode,
	isnull(d.Name,'其他费') FeetypeName
	into #jsmx
	--select *
	from [NewtouchHIS_Sett].dbo.zy_jsmx a with(nolock)
	left join [NewtouchHIS_Sett].dbo.zy_xmjfb b with(nolock) on a.xmjfbbh=b.jfbbh 
	left join [NewtouchHIS_Base]..xt_sfxm g on b.sfxm=g.sfxmcode and b.OrganizeId=g.OrganizeId and g.zt='1'
	left join Newtouch_MRMS.dbo.mr_dic_sfxmrel c with(nolock) on b.sfxm=c.sfxm and g.sfxmmc=c.sfxmmc and b.OrganizeId=c.OrganizeId and c.zt='1'
	left join Newtouch_MRMS.dbo.mr_dic_bafeetype d with(nolock) on c.feetypecode=d.code and c.OrganizeId=d.OrganizeId and c.zt='1'
	where exists(select 1 from #js b where a.jsnm=b.jsnm )
	and a.zt='1' and b.zt='1'
	and xmjfbbh>0 
	group by jsnm,yzlx,d.code,d.ShortCode,d.Name--,c.sfxm,c.sfxmmc

	insert into #jsmx
	select  jsnm,yzlx,d.code,sum(jyje) je,--d.shortcode,b.sfxm,c.sfxmmc	
	isnull(d.ShortCode,'QTF')FeetypeCode,
	isnull(d.Name,'其他费') FeetypeName
	--select b.*
	from [NewtouchHIS_Sett].dbo.zy_jsmx a with(nolock)
	left join [NewtouchHIS_Sett].dbo.zy_ypjfb b with(nolock) on a.ypjfbbh=b.jfbbh and b.zt='1'
	left join [NewtouchHIS_Base]..xt_yp g on b.yp=g.ypcode and g.zt='1'
	left join Newtouch_MRMS.dbo.mr_dic_sfxmrel c with(nolock) on b.yp=c.sfxm and g.ypmc=c.sfxmmc and b.OrganizeId=c.OrganizeId and c.zt='1'
	left join Newtouch_MRMS.dbo.mr_dic_bafeetype d with(nolock) on c.feetypecode=d.code and c.OrganizeId=d.OrganizeId and d.zt='1'
	where exists(select 1 from #js b where a.jsnm=b.jsnm )
	and a.zt='1' and b.zt='1'
	and ypjfbbh>0 
	group by jsnm,yzlx,d.code,d.ShortCode,d.Name

	select a.zyh,FeetypeCode,FeetypeName,sum(b.je) Amount
	from #js a,#jsmx b 
	where a.jsnm=b.jsnm
	group by a.zyh,FeetypeCode,FeetypeName
	union all
	select zyh,'ZFY','总费用',sum(zje) zje
	from #js group by zyh
	union all
	select zyh,'ZFJE','自付金额',sum(xjzf) xjzf
	from #js group by zyh

	select * into #newjsmx from  (
	select a.zyh,FeetypeCode,FeetypeName,sum(b.je) Amount
	from #js a,#jsmx b 
	where a.jsnm=b.jsnm
	group by a.zyh,FeetypeCode,FeetypeName
	union all
	select zyh,'ZFY','总费用',sum(zje) zje
	from #js group by zyh
	union all
	select zyh,'ZFJE','自付金额',sum(xjzf) xjzf
	from #js group by zyh)a

	if(not exists(select 1 from Newtouch_EMR.dbo.mr_basy with(nolock) where zyh=@zyh and OrganizeId=@orgId and zt='1'))
	begin
		return;
	end
	declare @maxjsnm int;
    declare @zyhs varchar(20);
    select @maxjsnm=max(jsnm),@zyhs=max(zyh) from #js

	select  @zyh zyh,@maxjsnm maxjsnm,
    XJZF=sum(case when b.FeetypeCode='ZFJE' then Amount else 0.00 end),
    ZJE=sum(case when b.FeetypeCode='ZFY' then Amount else 0.00 end),
	YLFUF=sum(case when b.FeetypeCode='YLFUF' then Amount else 0.00 end), --一般医疗服务费
	ZLCZF=sum(case when b.FeetypeCode='ZLCZF' then Amount else 0.00 end), --一般治疗操作费
	HLF=sum(case when b.FeetypeCode='HLF' then Amount else 0.00 end), --护理费
	QTFY=sum(case when b.FeetypeCode='QTFY' then Amount else 0.00 end), --1.(4)其他费用
	BLZDF=sum(case when b.FeetypeCode='BLZDF' then Amount else 0.00 end),--病理诊断费
	SYSZDF=sum(case when b.FeetypeCode='SYSZDF' then Amount else 0.00 end),--实验室诊断费
	YXXZDF=sum(case when b.FeetypeCode='YXXZDF' then Amount else 0.00 end), --影像学诊断费
	LCZDXMF=sum(case when b.FeetypeCode='LCZDXMF' then Amount else 0.00 end),--临床诊断项目费
	FSSZLXMF=sum(case when b.FeetypeCode='FSSZLXMF' then Amount else 0.00 end), --非手术治疗项目费
	WLZLF=sum(case when b.FeetypeCode='WLZLF' then Amount else 0.00 end),--3.治疗类 -(9)非手术治疗项目费-（其中：临床物理治疗费
	SSZLF=sum(case when b.FeetypeCode='SSZLF' then Amount else 0.00 end),--手术治疗费
	MAF=sum(case when b.FeetypeCode='MAF' then Amount else 0.00 end),--(10)手术治疗费-麻醉费
	SSF=sum(case when b.FeetypeCode='SSF' then Amount else 0.00 end),--手术费
	KFF=sum(case when b.FeetypeCode='KFF' then Amount else 0.00 end),--(11)康复费
	ZYZLF=sum(case when b.FeetypeCode='ZYZLF' then Amount else 0.00 end),--中医治疗费
	XYF=sum(case when b.FeetypeCode='XYF' then Amount else 0.00 end),--西药费
	KJYWF=sum(case when b.FeetypeCode='KJYWF' then Amount else 0.00 end),--（其中：抗菌药物费用
	ZCYF=sum(case when b.FeetypeCode='ZCYF' then Amount else 0.00 end),--(14)中成药费
	ZCYF1=sum(case when b.FeetypeCode='ZCYF1' then Amount else 0.00 end),--中草药费
	XF=sum(case when b.FeetypeCode='XF' then Amount else 0.00 end),--血费
	BDBLZPF=sum(case when b.FeetypeCode='BDBLZPF' then Amount else 0.00 end),--白蛋白类制品费
	QDBLZPF=sum(case when b.FeetypeCode='QDBLZPF' then Amount else 0.00 end),--球蛋白类制品费
	NXYZLZPF=sum(case when b.FeetypeCode='NXYZLZPF' then Amount else 0.00 end),--凝血因子类制品费
	XBYZLZPF=sum(case when b.FeetypeCode='XBYZLZPF' then Amount else 0.00 end),--细胞因子类制品费
	HCYYCLF=sum(case when b.FeetypeCode='HCYYCLF' then Amount else 0.00 end),--检查用一次性医用材料费
	YYCLF=sum(case when b.FeetypeCode='YYCLF' then Amount else 0.00 end),--治疗用一次性医用材料费
	YCXYYCLF=sum(case when b.FeetypeCode='YCXYYCLF' then Amount else 0.00 end),--手术用一次性医用材料费
	QTF=sum(case when b.FeetypeCode='QTF' then Amount else 0.00 end)
	into #fee
	--select *
	from  #newjsmx b
	group by zyh
	--select  a.zyh,max(a.jsnm) maxjsnm,max(a.xjzf)xjzf,max(a.zje)zje,
	--YLFUF=sum(case when b.FeetypeCode='YLFUF' then je else 0.00 end), --一般医疗服务费
	--ZLCZF=sum(case when b.FeetypeCode='ZLCZF' then je else 0.00 end), --一般治疗操作费
	--HLF=sum(case when b.FeetypeCode='HLF' then je else 0.00 end), --护理费
	--QTFY=sum(case when b.FeetypeCode='QTFY' then je else 0.00 end), --1.(4)其他费用
	--BLZDF=sum(case when b.FeetypeCode='BLZDF' then je else 0.00 end),--病理诊断费
	--SYSZDF=sum(case when b.FeetypeCode='SYSZDF' then je else 0.00 end),--实验室诊断费
	--YXXZDF=sum(case when b.FeetypeCode='YXXZDF' then je else 0.00 end), --影像学诊断费
	--LCZDXMF=sum(case when b.FeetypeCode='LCZDXMF' then je else 0.00 end),--临床诊断项目费
	--FSSZLXMF=sum(case when b.FeetypeCode='FSSZLXMF' then je else 0.00 end), --非手术治疗项目费
	--WLZLF=sum(case when b.FeetypeCode='WLZLF' then je else 0.00 end),--3.治疗类 -(9)非手术治疗项目费-（其中：临床物理治疗费
	--SSZLF=sum(case when b.FeetypeCode='SSZLF' then je else 0.00 end),--手术治疗费
	--MAF=sum(case when b.FeetypeCode='MAF' then je else 0.00 end),--(10)手术治疗费-麻醉费
	--SSF=sum(case when b.FeetypeCode='SSF' then je else 0.00 end),--手术费
	--KFF=sum(case when b.FeetypeCode='KFF' then je else 0.00 end),--(11)康复费
	--ZYZLF=sum(case when b.FeetypeCode='ZYZLF' then je else 0.00 end),--中医治疗费
	--XYF=sum(case when b.FeetypeCode='XYF' then je else 0.00 end),--西药费
	--KJYWF=sum(case when b.FeetypeCode='KJYWF' then je else 0.00 end),--（其中：抗菌药物费用
	--ZCYF=sum(case when b.FeetypeCode='ZCYF' then je else 0.00 end),--(14)中成药费
	--ZCYF1=sum(case when b.FeetypeCode='ZCYF1' then je else 0.00 end),--中草药费
	--XF=sum(case when b.FeetypeCode='XF' then je else 0.00 end),--血费
	--BDBLZPF=sum(case when b.FeetypeCode='BDBLZPF' then je else 0.00 end),--白蛋白类制品费
	--QDBLZPF=sum(case when b.FeetypeCode='QDBLZPF' then je else 0.00 end),--球蛋白类制品费
	--NXYZLZPF=sum(case when b.FeetypeCode='NXYZLZPF' then je else 0.00 end),--凝血因子类制品费
	--XBYZLZPF=sum(case when b.FeetypeCode='XBYZLZPF' then je else 0.00 end),--细胞因子类制品费
	--HCYYCLF=sum(case when b.FeetypeCode='HCYYCLF' then je else 0.00 end),--检查用一次性医用材料费
	--YYCLF=sum(case when b.FeetypeCode='YYCLF' then je else 0.00 end),--治疗用一次性医用材料费
	--YCXYYCLF=sum(case when b.FeetypeCode='YCXYYCLF' then je else 0.00 end),--手术用一次性医用材料费
	--QTF=sum(case when b.FeetypeCode='QTF' then je else 0.00 end)
	--into #fee
	--from #js a
	--left join #jsmx b on a.jsnm=b.jsnm
	--group by a.zyh--,FeetypeCode,FeetypeName

	if(exists(select 1 from #fee))
	begin
		--select * from #fee

		begin try
			--begin tran
		print '更新明细费'
		update a
		set	a.ZFY=b.zje ,  a.ZFJE=b.xjzf ,
		a.SJZYTS=(case when isnull(a.SJZYTS,'')='' 
				then convert(varchar(10),c.zyts) else a.SJZYTS end),
		a.YLFUF=b.YLFUF, --一般医疗服务费
		a.ZLCZF=b.ZLCZF, --一般治疗操作费
		a.HLF=b.HLF, --护理费
		a.QTFY=b.QTFY, --1.(4)其他费用
		a.BLZDF=b.BLZDF,--病理诊断费
		a.SYSZDF=b.SYSZDF,--实验室诊断费
		a.YXXZDF=b.YXXZDF, --影像学诊断费
		a.LCZDXMF=b.LCZDXMF,--临床诊断项目费
		a.FSSZLXMF=b.FSSZLXMF, --非手术治疗项目费
		a.WLZLF=b.WLZLF,--3.治疗类 -(9)非手术治疗项目费-（其中：临床物理治疗费
		a.SSZLF=b.SSZLF,--手术治疗费
		a.MAF=b.MAF,--(10)手术治疗费-麻醉费
		a.SSF=b.SSF,--手术费
		a.KFF=b.KFF,--(11)康复费
		a.ZYZLF=b.ZYZLF,--中医治疗费
		a.XYF=b.XYF,--西药费
		a.KJYWF=b.KJYWF,--（其中：抗菌药物费用
		a.ZCYF=b.ZCYF,--(14)中成药费
		a.ZCYF1=b.ZCYF1,--中草药费
		a.XF=b.XF,--血费
		a.BDBLZPF=b.BDBLZPF,--白蛋白类制品费
		a.QDBLZPF=b.QDBLZPF,--球蛋白类制品费
		a.NXYZLZPF=b.NXYZLZPF,--凝血因子类制品费
		a.XBYZLZPF=b.XBYZLZPF,--细胞因子类制品费
		a.HCYYCLF=b.HCYYCLF,--检查用一次性医用材料费
		a.YYCLF=b.YYCLF,--治疗用一次性医用材料费
		a.YCXYYCLF=b.YCXYYCLF,--手术用一次性医用材料费
		a.QTF=b.QTF
			--,a.LastModifierCode='admin',a.LastModifyTime=getdate()
		from mr_basy a,#fee b
		left join #js c on b.maxjsnm=c.jsnm 
		where a.zyh=b.zyh and a.zt='1'

		end try
		begin catch
			print(@@error)
		end catch

	end
	else
	begin
		print '无可更新费用'
	end

	drop table #newjsmx
	drop table #js
	drop table #jsmx
	drop table #fee

end





USE [Newtouch_EMR]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/21/2024 11:12:18


/*======================================
过程说明：病案首页费用反算
修改人：邓烽
修改时间：2023年2月27日16:3:17
修改内容：优化过程查询费用与更新病案首页费用

修改人:邓烽
修改时间：2023年3月10日17:47:12
修改内容：总费用为0时显示0

后台系统  收费项目单独维护病案大类版本  xt_basfdl病案code需按竖转横的 FeetypeCode值命名
exec [usp_sync_patfeefromSett] '6d5752a7-234a-403e-aa1c-df8b45d3469f','03335'
======================================*/

alter proc [dbo].[usp_sync_patfeefromSett] 
(
@orgId varchar(50),      
@zyh varchar(20)
)
as 

--declare
--@orgId varchar(50),      
--@zyh varchar(20)
--select @orgId=N'9bb029d0-5da0-4118-9d19-06b829eede46',@zyh=N'00040'
CREATE TABLE #temptable 
( 
[zyh] varchar(20), 
[FeetypeCode] varchar(50), 
[FeetypeName] varchar(50), 
[Amount] decimal(38,2) 
)

insert into #temptable (zyh,FeetypeCode,FeetypeName,Amount)
select @zyh,dlCode,dlmc ,0
  from NewtouchHIS_Base..xt_basfdl where OrganizeId=@orgId and zt=1 order by px


select @zyh zyh,FeetypeCode,FeetypeName,sum(Amount) Amount
  into #brfy
  from V_HIS_InpPatFeeForBA 
 where zyh=@zyh and OrganizeId=@orgId 
 group by FeetypeCode,FeetypeName

update zb set zb.Amount=fy.Amount
  from #temptable zb,#brfy fy
where zb.FeetypeCode=fy.FeetypeCode

/*非手术治疗费更新*/
update #temptable set Amount=(select sum(Amount) from #temptable where FeetypeCode  in('LCZDXMF','fsszlxmf')) where FeetypeCode='fsszlxmf'
/*手术治疗费更新*/
update #temptable set Amount=(select sum(Amount) from #temptable where FeetypeCode  in('sszlf','MAF','SSF')) where FeetypeCode='sszlf'
/*抗菌药物费用更新*/

/*结算信息*/
 select a.jsnm, a.zyh, a.zje,a.zyts,a.jsksrq,a.jsjsrq ,a.xjzf      
 into #js      
 from [NewtouchHIS_Sett].dbo.zy_js a with(nolock)       
 where a.OrganizeId=@orgId      
 and a.jszt=1 and a.zyh=@zyh      
 and not exists(select 1 from [NewtouchHIS_Sett].dbo.zy_js b with(nolock) where a.jsnm=b.cxjsnm and jszt=2 and zt='1' ) 

declare @zyf money=(select sum(Amount) from #brfy where FeetypeCode not in('LCZDXMF','MAF','SSF','KJYWF'))
declare @ZFJE money=(select xjzf from #js)
select @ZFJE=isnull(@ZFJE,0)
select @zyf=isnull(@zyf,0)
insert into #temptable (zyh,FeetypeCode,FeetypeName,Amount)
select @zyh,'ZFY','总费用',@zyf
insert into #temptable (zyh,FeetypeCode,FeetypeName,Amount)
select @zyh,'ZFJE','自付金额',@ZFJE

/*竖转横*/
 select  @zyh zyh,   
  ZFY=sum(case when b.FeetypeCode='ZFY' then Amount else 0.00 end), --一般医疗服务费
   ZFJE=sum(case when b.FeetypeCode='ZFJE' then Amount else 0.00 end), --一般医疗服务费
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
into #mrfy
 from #temptable b    


/*更新费用*/
update a      
  set a.ZFY=b.ZFY ,      
  a.ZFJE=b.ZFJE ,    
  a.QTZF = b.ZFY - b.ZFJE,        
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
  from mr_basy a,#mrfy b          
  where a.zyh=b.zyh and a.zt='1'
    and a.OrganizeId=@orgId

select * from #temptable  
DROP TABLE #temptable,#brfy,#js,#mrfy
return


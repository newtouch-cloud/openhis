USE [Newtouch_EMR]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/18/2024 10:43:10

  
/*=========================================  
使用程序：emr  
过程（视图）名称：usp_getbrxx  
过程说明：病人基本信息查询  
新 增 人：王侯文   
新增时间：2022-12-16  
  
修改人：  
修改时间：  
修改内容：  
=========================================*/ 
/*
----------------------------------------------------  
修改原因：添加联系人isnull的判断  
修改方式：新增查询视图
修 改 人：朱骏   
修改日期：2023-01-09  
修改标志：20230109A  
---------------------------------------------------- 
*/
ALTER PROCEDURE [dbo].[usp_getbrxx]  
(  
@orgId varchar(200),  
@mainId varchar(200)  
)  
AS  
  
 SELECT a.[Id],a.[YLFKFS],a.[JKKH],a.[ZYCS],a.[BAH],a.[PATID],a.[ZYH],a.[XM],a.[XB],convert(varchar(10),a.CSRQ,120)[CSRQ],a.[NL],a.[GJ],a.[BZYZSNL],a.[XSECSTZ]
,a.[XSERYTZ],a.[CSD],a.[GG],a.[MZ],a.[SFZH],a.[ZY],a.[HY],a.[XZZ],a.[DH],a.[XZZYB],a.[HKDZ],a.[HKDYB],a.[GZDWJDZ],a.[DWDH],a.[DWYB],isnull(a.[LXRXM],cxxx.LXRXM) as [LXRXM]
,isnull(a.[GX],cxxx.GX) as [GX],isnull(a.[LXRDZ],cxxx.LXRDZ) as [LXRDZ]  
,isnull(a.[LXRDH],cxxx.LXRDH) as [LXRDH],isnull(a.[RYTJ],cxxx.RYTJ) [RYTJ],convert(varchar(20),a.RYSJ,120)[RYSJ],a.[RYSJS],a.[RYKB],a.[RYBF],a.[ZKKB]
,convert(varchar(20),a.CYSJ,120)[CYSJ]
,a.[CYSJS],a.[CYKB],a.[CYBF],a.[SJZYTS],a.[MZZD],a.[MZZDDM],a.[BWHBZ],a.[RYZD],a.[RYZDDM],a.[SSLCLJ]  
,a.[QJCS],a.[QJCGCS],a.[QZRQ],a.[ZQSS],a.[BRLY],a.[WBYY],a.[H23],a.[BLZD],a.[BLZDDM],a.[BLH],a.[YWGM],a.[GMYW],a.[SWHZSJ],a.[XX],a.[RH],isnull(a.[KZR],staff.Name) KZR  
,isnull(a.[ZRYS],zrys.ysmc)ZRYS,isnull(a.[ZZYS],zz.ysmc)ZZYS,isnull(a.[ZYYS],zyys.ysmc)ZYYS,isnull(a.[ZRHS],zrhs.ysmc)ZRHS,isnull(a.ZZYS1,zzys.ysmc)ZZYS1 
,a.[JXYS],a.[SXYS],a.[BMY],a.[BAZL],a.[ZKYS],a.[ZKHS],convert(varchar(10),ZKRQ,120)[ZKRQ],a.[LYFS],a.[YZZY_YLJG],a.[WSY_YLJG],a.[SFZZYJH]  
,a.[MD],a.[RYQ_T],a.[RYQ_XS],a.[RYQ_F],a.[RYH_T],a.[RYH_XS],a.[RYH_F],a.[zt],a.[CreateTime],a.[CreatorCode],a.[LastModifyTime],a.[LastModifierCode]  
,a.[LCLJGL],a.[LCBZGL],a.[BZGLFL],a.[BQFX],a.[OrganizeId],a.[CSD_SN],a.[CSD_SI],a.[CSD_QX],a.[CSD_JD],a.[XZZ_SN],a.[XZZ_SI],a.[XZZ_QX],a.[XZZ_JD]  
,a.[HKDZ_SN],a.[HKDZ_SI],a.[HKDZ_QX],a.[HKDZ_JD],a.[LXRDZ_SN],a.[LXRDZ_SI],a.[LXRDZ_QX],a.[LXRDZ_JD],a.[RYCH],a.[CYCH],a.[bazt]  
,a.[ZFY],a.[ZFJE],a.[QTZF],a.[YLFUF],a.[BZLZF],a.[ZYBLZHZF],a.[ZLCZF],a.[HLF],a.[QTFY],a.[BLZDF],a.[SYSZDF],a.[YXXZDF],a.[LCZDXMF],a.[FSSZLXMF],a.[WLZLF]  
,a.[SSZLF],a.[MAF],a.[SSF],a.[KFF],a.[ZYZLF],a.[ZYZL],a.[ZYWZ],a.[ZYGS],a.[ZCYJF],a.[ZYTNZL],a.[ZYGCZL],a.[ZYTSZL],a.[ZYQT],a.[ZYTSTPJG],a.[BZSS]  
,a.[XYF],a.[KJYWF],a.[ZCYF],a.[ZYZJF],a.[ZCYF1],a.[XF],a.[BDBLZPF],a.[QDBLZPF],a.[NXYZLZPF],a.[XBYZLZPF],a.[HCYYCLF],a.[YYCLF],a.[YCXYYCLF],a.[QTF],  
b.[YLFKFS] R_YLFKFS,b.[XB] R_XB,b.[GJ] R_GJ,b.[MZ] R_MZ,b.[ZY] R_ZY,b.[HY] R_HY,b.[GX] R_GX,b.[RYTJ] R_RYTJ,b.[RYKB] R_RYKB,b.[RYBF] R_RYBF  
,b.[ZKKB] R_ZKKB,b.[CYBF] R_CYBF,b.[CYKB] R_CYKB,b.[BLZDDM] R_BLZDDM,b.[BRLY] R_BRLY,b.[YWGM] R_YWGM,b.[XX] R_XX,b.[RH] R_RH
,isnull(b.[KZR],bq.kzr_gh) R_KZR,isnull(b.[ZRYS],zrys.ysmc) R_ZRYS,isnull(b.[ZZYS] ,zz.ysmc)R_ZZYS  
,isnull(b.[ZYYS],zyys.ysgh) R_ZYYS,isnull(b.[ZRHS],zrhs.ysgh) R_ZRHS,b.[JXYS] R_JXYS,b.[SXYS] R_SXYS   
,b.[BMY] R_BMY,b.[BAZL] R_BAZL,b.[ZKYS] R_ZKYS,b.[ZKHS] R_ZKHS,b.[LYFS] R_LYFS,b.[SFZZYJH] R_SFZZYJH,b.[BQFX] R_BQFX  
,a.[HXB],a.[XXB],a.[XJ],a.[QX],a.[ZTXHS],a.[BDB],a.[LCD],a.[QT],a.[SXFY],a.[SZ],a.[SZQXZ],a.[SZQXY],a.[SZQXN],a.[WCLCLJ],a.[TCYY],a.[SFBY],a.[BYYY]  
,a.[CT],a.[PETCT],a.[SYCT],a.[BC],a.[XP],a.[CSXDT],a.[MRI],a.[TWSJC],a.[SYCXSJ],a.[LHYY]  
,a.[YYGRQK],a.[YYGRSSXG],a.[YYGRSFQRXG],a.[KJYWSYQK],a.[KJYWMC1],a.[KJYWMC2],a.[KJYWMC3],a.[KJYWMC4],a.[KJYWMC5],a.[KJYWMC6]  
,a.[SFFSYC],a.[SFZYQJFS],a.[YCFQ],a.[SYFY],a.[YFFYDYW],a.[SYLCBX],a.[ZYSFDDHZC],a.[ZYDDHZCDCD],a.[DDHZCDYY],a.[ZYQJSTYY],a.[LYTXNSDZ]
,a.[DWFZR],a.[TJFZR],a.[TBR],a.[LXDH],a.[SJ]  ,convert(varchar(10),a.[BCRQ],25) BCRQ,a.BZYYSNL,a.QTYLJGZR,isnull(b.ZZYS1,zzys.ysgh)R_ZZYS1
,ZYSSLCLJ,SYYLJGZYZJ,SYZYZLSB,SYZYZLJS,BZSH
FROM [dbo].[mr_basy] a with(nolock)    
left join [dbo].[mr_basy_rel_code] b with(nolock) on a.organizeid=b.organizeid and a.id=b.syid and a.bah=b.bah   
left join NewtouchHIS_Base..xt_bq bq on bq.bqmc=a.RYBF and bq.zt=1 and bq.OrganizeId=@orgId  
left join NewtouchHIS_Base..Sys_Staff staff on bq.kzr_gh=staff.gh and staff.OrganizeId=@orgId and staff.zt=1
left join [Newtouch_EMR].[dbo].[V_HIS_InpPatInfoForBA] cxxx on cxxx.ZYH = a.ZYH and cxxx.OrganizeId = @orgId					/*20230109A*/
left join Newtouch_CIS..zy_PatDocInfo zrys on a.ZYH=zrys.zyh and zrys.OrganizeId=@orgId and zrys.zt=1 and zrys.TypeName='主任医生'  
left join Newtouch_CIS..zy_PatDocInfo zz on a.ZYH=zz.zyh and zz.OrganizeId=@orgId and zrys.zt=1 and zz.TypeName='主诊医生'  
left join Newtouch_CIS..zy_PatDocInfo zzys on a.ZYH=zzys.zyh and zzys.OrganizeId=@orgId and zzys.zt=1 and zzys.TypeName='主治医生'  
left join Newtouch_CIS..zy_PatDocInfo zrhs on a.ZYH=zrhs.zyh and zrhs.OrganizeId=@orgId and zrhs.zt=1 and zrhs.TypeName='责任护士'  
left join Newtouch_CIS..zy_PatDocInfo zyys on a.ZYH=zyys.zyh and zyys.OrganizeId=@orgId and zyys.zt=1 and zyys.TypeName='住院医生'
where a.zt='1' and a.OrganizeId=@orgId and a.id=@mainId   
return


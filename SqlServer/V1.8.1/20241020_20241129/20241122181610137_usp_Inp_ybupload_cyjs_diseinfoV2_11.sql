USE [NewtouchHIS_Sett]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/22/2024 18:16:10


/**
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
--
exec [usp_Inp_ybupload_cyjs_diseinfoV2] @OrgId='6d5752a7-234a-403e-aa1c-df8b45d3469f',@zyh='03333' 
-- =============================================  
*/
ALTER proc [dbo].[usp_Inp_ybupload_cyjs_diseinfoV2]    
 @orgId varchar(50),    
 @zyh varchar(20)    
as    


begin       
      --诊断类型 1:西医诊断 2中医主病诊断  3 中医主证诊断 4 手术操作 5 按病种付费病种
	  -- 主诊断标准 1：是 0 否
	  -- 入院病情  1有 2 临床未确定  3 情况不明 4 无
--declare       
-- @orgId varchar(50),          
-- @zyh varchar(20)       
-- select @orgId='6d5752a7-234a-403e-aa1c-df8b45d3469f',@zyh='03333'        
       
 select --row_number() over( order by zdlb desc,a.zdlx) xh,
 case  WHEN ZDLB='TCM' AND a.ZDLX='1' THEN '2' when zdlb='TCM' AND a.zdlx!='1' then '3' else '1' end diag_type
	,isnull(b.icd10,jbdm) diag_code,jbmc diag_name, case when RYBQ='1' then '1' else '4' end adm_cond_type  ,      
        case a.ZDLX when '1' then '1' else '0' end maindiag_flag,row_number() over( order by zdlb desc,a.zdlx) ZDOrder        
 --into #brdz      
 from Newtouch_EMR.dbo.mr_basy_zd a with(nolock)   
 left join  NewtouchHIS_Base..xt_zd b on b.zdCode=a.JBDM and b.OrganizeId=a.OrganizeId and b.zt='1'
 where a.OrganizeId=@orgId       
 and a.zyh=@zyh          
 and a.zt='1' and jbmc<>''          
 and JBDM<>'999999999'      
 and   jbmc not like '%待查%'  
 order by ZDLB desc,a.zdlx        
        
--update #brdz set maindiag_flag=1 where xh=1      
--update #brdz set maindiag_flag=0 where xh<>1      
      
/*更新医保诊断名称*/    
--update zd set zd.diag_code=dmmx.TTCode,zd.diag_name=dmmx.TTName    
--  from #brdz as zd,NewtouchHIS_Base.dbo.TTCataloguesComparisonDetail as dmmx with(nolock)    
-- where zd.diag_code=dmmx.Code    
--   and dmmx.OrganizeId=@orgId    
--   and dmmx.MainId='4A03F7E7-5E45-428F-B75F-6F5ADD709591'    
--   and isnull(dmmx.TTCode,'')<>''    
--   and dmmx.zt=1  
--select diag_type,diag_code,diag_name,adm_cond_type,maindiag_flag      
-- from #brdz  order by convert(int, #brdz.xh)      
--drop table #brdz      
      
      
 --select 1 diag_type,zddm diag_code,zdmc diag_name, 1 adm_cond_type  ,case zdlx when '0' then '1' else '0' end maindiag_flag          
 --from newtouch_cis.. zy_PatDxInfo where zyh=@zyh   and zdlb = 2 and zt = 1 and zdmc <> '999999999'        
end        
return      
        


USE [NewtouchHIS_Base]
GO

-- 事件类型: CREATE_PROCEDURE
-- 变更时间: 11/18/2024 14:53:40



/***
exec [RPT_ERM_病案首页诊断_中西医] '6d5752a7-234a-403e-aa1c-df8b45d3469f','03245'

***/
CREATE proc [dbo].[RPT_ERM_病案首页诊断_中西医]
 @orgId varchar(50),    
 @zyh varchar(15)
as
begin  
  
   SELECT 
 CASE
          WHEN [ZDLB] = 'TCM' AND [ZDLX] = 1 THEN '主病   ' + [JBMC]
          WHEN [ZDLB] = 'TCM' AND [ZDLX] = 2 THEN '主证   ' + [JBMC]
          ELSE NULL
      END AS [出院中医诊断],
      
      -- New column for 出院西医诊断
      CASE
          WHEN [ZDLB] = 'WM' AND [ZDLX] = 1 THEN '主要诊断  ' + [JBMC]
          WHEN [ZDLB] = 'WM' AND [ZDLX] = 2 THEN '次要诊断  ' + [JBMC]
          ELSE NULL
      END AS [出院西医诊断],
	  [BAH] [病案号],
      [ZYH] [住院号],
      [ZDLB] [诊断类别],
      [ZDLX] [诊断类型],
      [JBDM] [疾病编码],
      [JBMC] [疾病名臣],
      [RYBQ] [入院病情],
      [RYBQMS] [入院病情描述],
      [CYQK] [出院病情],
      [CYQKMS] [出院病情描述]

 FROM      Newtouch_EMR..mr_basy_zd with(nolock)  
 WHERE zyh =@zyh AND organizeid = @orgId
 and JBDM<>'999999999' and zt='1'  order by ZDLB, ZDLX,ZDOrder
 
end  
  


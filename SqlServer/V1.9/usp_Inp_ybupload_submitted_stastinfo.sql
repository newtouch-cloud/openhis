USE [NewtouchHIS_Sett]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[usp_Inp_ybupload_submitted_stastinfo]  
 @orgId varchar(50),  
 @zyh varchar(20)  
as  
begin 

select a.psn_no,a.setl_id ,0 stas_type from drjk_zyjs_output a with(nolock) 
 where a.zt='1' 
 and a.zyh =@zyh
 and jsqd_tjzt is not null 
 and jsqd_tjzt = '1'

end  
USE [Newtouch_CIS]
GO

/****** Object:  StoredProcedure [dbo].[usp_SelectSfxmYp]    Script Date: 2024/10/22 18:13:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO













    
/*  
�޸��ˣ��˷�  
�޸�ʱ�䣺2022��3��18��11:28:20  
�޸����ݣ����cxjje�ֶ�  
*/    
  
/*  
�޸��ˣ�������  
�޸�ʱ�䣺2022��3��31��14:54:20  
�޸����ݣ����tsypbz�ֶ�  
*/   
  
/*  
�޸��ˣ�������  
�޸�ʱ�䣺2022��4��12��11:00:00  
�޸����ݣ����kssqxjb�ֶ�  
*/  
/*
�� �� �ˣ��쿥
�޸�ʱ�䣺2022��8��5��15:13
�޸����ݣ�ҽԺҪ����Ʋ���ʾ���Ϊ0��ҩ�����ڴ˿��ơ��������޸�ֱ��ע�ʹ˴���
�޸ı�־��20220805A
exec [usp_SelectSfxmYp] '521','6d5752a7-234a-403e-aa1c-df8b45d3469f','9','1','WM','','',null,null,null,null,null,null
exec [usp_SelectSfxmYp] '5','6d5752a7-234a-403e-aa1c-df8b45d3469f','2','1','','','',null,null,null,null,null,null
exec [Newtouch_CIS].[dbo].[usp_open_SelectCfYp] @topCount, @orgId, @mzzybz,@ypyfbmCode,  @sfdllx, @keyword, @isQyKssKZ, @qxjb
*/  
    
ALTER PROCEDURE [dbo].[usp_SelectSfxmYp]  
(  
 @topCount int, --ǰ������    
 @orgId varchar(50),    
 @mzzybz varchar(50) = '', --1���� �� 2סԺ ����ָ���ǲ����� ����סԺ     3���ұ�ҩ���   9���Ӵ���
    
 @dllb varchar(50) = '', --1ҩƷ 2��Ŀ 3��������Ŀ ��� ���ŷָ�    
 @sfdllx varchar(20) = '', --�����ֵ�ChargeCateType����WM ��ҩ    
    
 @keyword varchar(50) = '',    
    
 @dlCode varchar(20) = '', --������շѴ���Code ��� ���ŷָ�    
 @isContansChildDl bit = 1, --���@dlCodeʹ��    
    
 @useypckflag bit = 0, --�Ƿ�ʹ��ҩƷ����߼�    
 @ypyfbmCode varchar(128) = '', --ҩƷ��ҩ��ɸѡ ��� ���ŷָ����ʲô���鲻��    
 @containyp0ck bit = 0, --�Ƿ����ҩƷ0��棬0������ 1����    
 @onlyybflag bit = 0, --�Ƿ��ҽ����Ŀ/ҩƷ    
 @isQyKssKZ bit = 0, --�Ƿ����ÿ����ؿ���    
 @qxjb VARCHAR(2) = '' --�����ص�Ȩ�޼���    
)  
AS
DECLARE @isdzcfyp VARCHAR(50) = '0' --ʹ�õ��Ӵ���Ŀ¼
if @mzzybz='9'
begin
	select @mzzybz='1';
	select @isdzcfyp ='1';
end
if(@mzzybz <> '0'and @mzzybz <> '1' and @mzzybz <> '2' and @mzzybz <> '9' and @mzzybz <> '' and   charindex('3|',@mzzybz)<=0)    
 return; -- '0' ��ͨ�õ� Empty�������ﻹ��סԺ��ͨ��    
    
if(@useypckflag = 0)    
begin    

 exec [NewtouchHIS_Base].dbo.usp_SelectSfxmYp_WithNoKc @topCount, @orgId, @mzzybz, @dllb, @sfdllx, @keyword, @dlCode    
 , @isContansChildDl, @onlyybflag,@isQyKssKZ,@qxjb    
 return;    
end
;   

if(charindex('3|',@mzzybz)>0) --ȡ���ұ�ҩ����
begin
set @mzzybz=substring(@mzzybz,3,len(@mzzybz))
 exec [Newtouch_CIS].dbo.[usp_SelectKsbyYp] @topCount, @orgId, '2', @dllb, @sfdllx, @keyword, @dlCode    
 , @isContansChildDl,@ypyfbmCode, @onlyybflag,@isQyKssKZ,@qxjb , @mzzybz
 return; 
end
;

if (@isdzcfyp='1') --ҽ�����Ӵ���
begin
	exec [usp_SelectDzcf] @topCount,@keyword
	return;	
end

--Add by zzm
if (@mzzybz = '2' and @dllb = '1' and @sfdllx='') set @sfdllx = 'WM'
if (@sfdllx = 'WM' OR @sfdllx='TCM')
begin
	exec [Newtouch_CIS].[dbo].[usp_open_SelectCfYp] @topCount, @orgId, @mzzybz,@ypyfbmCode,  @sfdllx, @keyword, @isQyKssKZ, @qxjb
	return;
end
else if ((@mzzybz = '1' or @mzzybz = '2') and @dllb = '2' and @sfdllx <> '')
begin
	exec [Newtouch_CIS].[dbo].[usp_open_SelectCfXm] @topCount, @orgId, @mzzybz, @sfdllx, @keyword
	return;
end
;  

if exists(select 1 from Sys_Config where OrganizeId = @orgId and Code = 'HISSyncMethod' and Value = 'IDB')    
begin    
 --������м��   

 exec usp_SelectSfxmYp_WithIDB @topCount, @orgId, @mzzybz, @dllb, @sfdllx, @keyword, @dlCode    
 , @isContansChildDl, @useypckflag, @ypyfbmCode, @containyp0ck, @onlyybflag    
 return;    
end    
;    
 
--dlCode dlmc���� --������ʱ��#tbdlCodeTemp    
WITH cteTree AS    
    (SELECT *    
    FROM [NewtouchHIS_Base]..V_S_xt_sfdl    
 where dlId in (    
  select dlId from [NewtouchHIS_Base]..V_S_xt_sfdl    
  where Organizeid  = @orgId and zt = '1' and (isnull(@dlCode, '') = '' or dlCode in (select * from dbo.f_split(isnull(@dlCode, ''), ',')))    
 ) and zt = '1'    
    UNION ALL    
 SELECT a.*    
    FROM [NewtouchHIS_Base]..V_S_xt_sfdl a    
    INNER JOIN cteTree ON cteTree.dlId=a.ParentId and a.zt = '1' and @isContansChildDl = 1)    
    
select distinct dlCode, dlmc, dllb    
into #tbdlCodeTemp    
from cteTree where (isnull(@dllb, '') = '' or dllb in (select Convert(int, col) from dbo.f_split(isnull(@dllb, ''), ',')))    
and (    
isnull(@sfdllx, '') = ''    
or    
dlCode in     
 (    
  select dlCode from [NewtouchHIS_Base]..V_S_xt_sfdl_lx where Organizeid  = @orgId and zt = '1'     
  and ((left(@sfdllx,1) <> '-' and Type in (select col from [dbo].[f_split](@sfdllx,','))) or (left(@sfdllx,1) = '-' and Type not in   
  (select col from [dbo].[f_split]((case when isnull(@sfdllx, '') <> '' then SUBSTRING(@sfdllx,2,len(@sfdllx) - 1) else '' end),','))))    
 )    
)    
and (    
 mzzybz = @mzzybz or mzzybz = '0' or @mzzybz = ''    
)    
    
 select     
 sfxmCode,    
 sfxmmc,    
 a.sfdlCode sfdlCode,    
 b.dlmc sfdlmc,    
 dw,     
 CAST(ROUND(dj,2) AS NUMERIC(12, 2)) dj,    
 a.mzzybz,    
 a.py,    
    a.px,    
 a.duration    
 ,isnull(a.bz,'') bz    
 ,isnull(a.dwjls,0) dwjls    
 ,isnull(a.jjcl,2) jjcl --Ĭ�ϰ�����    
 ,a.zfxz    
 ,a.zfbl    
 ,a.zxks    
 ,sysdept.Name zxksmc    
 ,a.gg    
 ,a.ybdm  
 ,a.cxjje
 INTO #XmQuery    
 from[NewtouchHIS_Base]..V_S_xt_sfxm(nolock) a    
 INNER join #tbdlCodeTemp(nolock) b    
 on a.sfdlCode = b.dlCode    
 left join [NewtouchHIS_Base]..V_S_Sys_Department(nolock) sysdept    
 on sysdept.Code = a.zxks and sysdept.OrganizeId = a.OrganizeId and sysdept.zt = '1'    
 where a.OrganizeId = @orgId and a.zt = '1'    
 and (@mzzybz = '' or @mzzybz = '1' or @mzzybz = '2' or (@mzzybz = '0' and dllb = 3)) --��ѯ��Ŀһ��Ҫָ�������סԺ����ͨ�ý���������Ŀ���򲻹����ﻹ��סԺ��ͨ��    
 and (    
  a.mzzybz = @mzzybz or a.mzzybz = '0' or @mzzybz = ''    
 )    
    
 CREATE TABLE #YpQuery(    
 ypCode varchar(20),    
 ypmc varchar(256),    
 sfdlCode varchar(20),    
 sfdlmc varchar(50),    
 dw  varchar(20),    
 cls  numeric(9,4),    
 dj  numeric(9,4),    
 py  VARCHAR(70),    
 px  INT ,    
 jldw varchar(10) ,    
 jldwzhxs numeric(9,4),    
 zfxz char(1),    
 zfbl numeric(9,4),    
 yfbmCode varchar(100),    
 kcsl  numeric(11,2),    
 yfbmmc varchar(50),    
 ypgg varchar(100),    
 ypjxCode varchar(20),    
 kzbz varchar(1),    
 zyqzlx char(1),    
 ybdm varchar(20),    
 xzyy BIT,    
 xzyysm varchar(256),    
 mrjl decimal,    
 mrpc varchar(20),    
 mrpcmc varchar(50),    
 isKss char(1),    
 jlfwBegin NUMERIC(9,4),    
 jlfwEnd  NUMERIC(9,4),    
 pcfwBegin NUMERIC(9,4),    
 pcfwEnd  NUMERIC(9,4),    
 kssKy CHAR(1),  
 cxjje numeric(9,4),  
 tsypbz varchar(20),  
 kssqxjb varchar(20),
 bz varchar(200)
 )    
    
IF @isQyKssKZ=0    
BEGIN    
INSERT INTO #YpQuery    
SELECT aaa.ypCode, aaa.ypmc, aaa.dlCode sfdlCode, bbb.dlmc sfdlmc    
    , case @mzzybz when '1' then aaa.mzcldw when '2' then aaa.zycldw end dw    
 , case @mzzybz when '1' then aaa.mzcls when '2' then aaa.zycls end cls    
    , CONVERT(DECIMAL(19,4), case @mzzybz when '1' then (case when aaa.mzcldw = aaa.bzdw then aaa.lsj else (aaa.lsj / aaa.bzs * aaa.mzcls) end) when '2' then (case when aaa.zycldw = aaa.bzdw then aaa.lsj else (aaa.lsj / aaa.bzs * aaa.zycls) end)end) dj   
 
    , aaa.py    
    , aaa.px    
    , aaa.jldw    
 , aaa.jl jldwzhxs    
 , aaa.zfxz    
 , aaa.zfbl    
 , ypkc.yfbmCode, convert(numeric(11,2),isnull(ypkc.zxdwsl,0)) kcsl, yfbm.yfbmmc    
 , ypsx.ypgg    
 , aaa.jx ypjxCode    
 , ypkc.kzbz    
 , ypsx.jzlx zyqzlx    
 , ypsx.ybdm    
 , ypsx.xzyy,ypsx.xzyysm,ypsx.mrjl,ypsx.mrpc,yzpc.yzpcmc mrpcmc,NULL isKss,NULL jlfwBegin,NULL jlfwEnd,NULL pcfwBegin,NULL pcfwEnd,NULL kssKy  
 , aaa.cxjje,aaa.tsypbz,ypkss.qxjb kssqxjb
,isnull(aaa.bz,'') bz 
    from [NewtouchHIS_Base]..V_S_xt_yp(nolock) aaa    
    INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=aaa.ypId AND ypsx.zt='1' AND ypsx.OrganizeId=aaa.OrganizeId    
    INNER join #tbdlCodeTemp(nolock) bbb on aaa.dlCode = bbb.dlCode    
 INNER join [NewtouchHIS_PDS].dbo.V_S_P_Kc ypkc    
 on ypkc.OrganizeId = aaa.OrganizeId and ypkc.ypCode = aaa.ypCode    
 left join NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm    
 on yfbm.OrganizeId = aaa.OrganizeId and yfbm.yfbmCode = ypkc.yfbmCode    
 LEFT JOIN NewtouchHIS_Base..V_S_xt_yzpc yzpc    
 ON ypsx.mrpc=yzpc.yzpcCode AND yzpc.zt = '1' AND yzpc.OrganizeId = ypsx.OrganizeId    
 LEFT JOIN NewtouchHIS_Base..xt_ypKss ypkss on aaa.kssId = ypkss.Id and ypkss.OrganizeId=aaa.OrganizeId  
    where aaa.OrganizeId = @orgId    
 and (isnull(@ypyfbmCode,'') = '' or ypkc.yfbmCode in (select * from dbo.f_split(isnull(@ypyfbmCode, ''), ',')))    
    and aaa.zt = '1'    
 and (@mzzybz = '1' or @mzzybz = '2') --��ѯҩƷһ��Ҫָ����ѯ�����סԺ    
 and (yfbm.mzzybz = @mzzybz or yfbm.mzzybz = '3') --����/סԺҩ�� �� ͨ��ҩ��    
 and (@containyp0ck = 1 or ypkc.zxdwsl > 0)    
END    
ELSE    
BEGIN    
INSERT INTO #YpQuery    
SELECT aaa.ypCode, aaa.ypmc, aaa.dlCode sfdlCode, bbb.dlmc sfdlmc    
    , case @mzzybz when '1' then aaa.mzcldw when '2' then aaa.zycldw end dw    
 , case @mzzybz when '1' then aaa.mzcls when '2' then aaa.zycls end cls    
    , CONVERT(DECIMAL(19,4), case @mzzybz when '1' then (case when aaa.mzcldw = aaa.bzdw then aaa.lsj else (aaa.lsj / aaa.bzs * aaa.mzcls) end) when '2' then (case when aaa.zycldw = aaa.bzdw then aaa.lsj else (aaa.lsj / aaa.bzs * aaa.zycls) end)end) dj   
 
    , aaa.py    
    , aaa.px    
    , aaa.jldw    
 , aaa.jl jldwzhxs    
 , aaa.zfxz    
 , aaa.zfbl    
 , ypkc.yfbmCode, convert(numeric(11,2),isnull(ypkc.zxdwsl,0)) kcsl, yfbm.yfbmmc    
 , ypsx.ypgg    
 , aaa.jx ypjxCode    
 , ypkc.kzbz    
 , ypsx.jzlx zyqzlx    
 , ypsx.ybdm    
 , ypsx.xzyy,ypsx.xzyysm,ypsx.mrjl,ypsx.mrpc,yzpc.yzpcmc mrpcmc,aaa.isKss,kss.jlfwBegin,kss.jlfwEnd,    
 kss.pcfwBegin,kss.pcfwEnd,CASE WHEN ISNULL(kss.qxjb,'0') <=@qxjb THEN '1' ELSE '0' END kssKy --�����ؿ��� 1 ����,0 ������(qxjb:0 ������ʹ��ҩ��,1 ����ʹ��ҩ��,2 ����ʹ��ҩ��)   
 ,aaa.cxjje,aaa.tsypbz,ypkss.qxjb kssqxjb 
 ,isnull(aaa.bz,'') bz 
    from [NewtouchHIS_Base]..V_S_xt_yp(nolock) aaa    
    INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=aaa.ypId AND ypsx.zt='1' AND ypsx.OrganizeId=aaa.OrganizeId    
    INNER join #tbdlCodeTemp(nolock) bbb on aaa.dlCode = bbb.dlCode    
 INNER join [NewtouchHIS_PDS].dbo.V_S_P_Kc ypkc    
 on ypkc.OrganizeId = aaa.OrganizeId and ypkc.ypCode = aaa.ypCode    
 left join NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm    
 on yfbm.OrganizeId = aaa.OrganizeId and yfbm.yfbmCode = ypkc.yfbmCode    
 LEFT JOIN NewtouchHIS_Base..V_S_xt_yzpc yzpc    
 ON ypsx.mrpc=yzpc.yzpcCode AND yzpc.zt = '1' AND yzpc.OrganizeId = ypsx.OrganizeId    
 LEFT JOIN NewtouchHIS_Base..xt_ypKss(NOLOCK) kss ON aaa.isKss='1' AND aaa.kssId = kss.Id AND kss.zt='1' AND aaa.OrganizeId = kss.OrganizeId  
 LEFT JOIN NewtouchHIS_Base..xt_ypKss ypkss on aaa.kssId = ypkss.Id and ypkss.OrganizeId=aaa.OrganizeId  
    where aaa.OrganizeId = @orgId    
 and (isnull(@ypyfbmCode,'') = '' or ypkc.yfbmCode in (select * from dbo.f_split(isnull(@ypyfbmCode, ''), ',')))    
    and aaa.zt = '1'    
 and (@mzzybz = '1' or @mzzybz = '2') --��ѯҩƷһ��Ҫָ����ѯ�����סԺ    
 and (yfbm.mzzybz = @mzzybz or yfbm.mzzybz = '3') --����/סԺҩ�� �� ͨ��ҩ��    
 and (@containyp0ck = 1 or ypkc.zxdwsl > 0)    
END    
    
/*ҽԺҪ����Ʋ���ʾ���Ϊ0��ҩ�����ڴ˿��ơ��������޸�ֱ��ע�ʹ˴���20220805A*/
delete from #YpQuery where kcsl <= 0    
        
select top (@topCount) * from    
(    
select sfxmCode sfxmCode , sfxmmc sfxmmc, py py    
, sfdlCode sfdlCode, sfdlmc sfdlmc    
, dw dw, dj dj    
, '2' yzlx    
, bz bz    
, duration duration    
, dwjls dwjls    
, jjcl jjcl    
, '' jldw    
, null jldwzhxs    
, zfxz zfxz    
, zfbl zfbl    
, 0 cls    
, '' yfbmCode    
, '' yfbmmc    
, null kcsl    
, gg gg    
, '' ypjxCode    
, '' kzbz    
, '' zyqzlx    
, zxks zxks    
, zxksmc zxksmc    
, ybdm ybdm    
, null xzyy,null xzyysm,NULL mrjl, NULL mrpc,NULL mrpcmc    
, NULL isKss    
, NULL jlfwBegin    
, NULL jlfwEnd    
, NULL pcfwBegin    
, NULL pcfwEnd    
, NULL kssKy  
, cxjje,''tsypbz,''kssqxjb  
,'2' jybz --��ҩ��־ �����Ƿ���ұ�ҩ��ʶ   1�����ұ�ҩ��� 2��ҩ�����
from #XmQuery    
    
union all    
    
select ypCode sfxmCode , ypmc sfxmmc, py py    
, sfdlCode sfdlCode, sfdlmc sfdlmc    
, dw dw, dj dj    
, '1' yzlx    
, bz bz    
, null duration    
, null dwjls    
, null jjcl    
, jldw jldw    
, jldwzhxs jldwzhxs    
, zfxz zfxz    
, zfbl zfbl    
, cls cls    
, yfbmCode yfbmCode    
, yfbmmc yfbmmc    
, kcsl kcsl    
, ypgg gg    
, ypjxCode ypjxCode    
, kzbz kzbz    
, zyqzlx zyqzlx    
, '' zxks    
, '' zxksmc    
, ybdm ybdm    
, xzyy xzyy    
, xzyysm xzyysm    
,mrjl mrjl    
,mrpc mrpc    
,mrpcmc mrpcmc    
,isKss    
,jlfwBegin    
,jlfwEnd    
,pcfwBegin    
,pcfwEnd    
,kssKy  
,cxjje 
, tsypbz 
, kssqxjb
,'2' jybz --��ҩ��־ �����Ƿ���ұ�ҩ��ʶ    1�����ұ�ҩ��� 2��ҩ�����
from #YpQuery    
) as reslt    
where (isnull(@keyword, '') = '' or sfxmCode like '%' + @keyword + '%' or sfxmmc like '%' + @keyword + '%' or py like '%' + @keyword + '%')    
and (@onlyybflag = 0 or isnull(ybdm,'')<>'')    
    
order by sfxmCode    
    
--ɾ����ʱ��    
drop table #tbdlCodeTemp    
  
return  
    
    
    
    
    
    



GO



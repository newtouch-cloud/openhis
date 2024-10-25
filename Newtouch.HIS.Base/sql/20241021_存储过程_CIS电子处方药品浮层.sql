/**
exec [usp_SelectDzcf] '2',''

*/

USE [Newtouch_CIS]
GO


create PROCEDURE [dbo].[usp_SelectDzcf]
 @topCount int, --前多少条          
 @keyword varchar(50) = ''  

as
 CREATE TABLE #YpQuery(      
    ypCode varchar(20),      
    ypmc varchar(256),      
    sfdlCode varchar(20),      
    sfdlmc varchar(50),      
    dw  varchar(50),      
    cls  numeric(9,4),      
    dj  numeric(9,4),      
    py  VARCHAR(500),      
    px  INT ,      
    jldw varchar(50) ,      
    jldwzhxs numeric(9,4),      
    zfxz char(1),      
    zfbl numeric(9,4),      
    yfbmCode varchar(20),      
    kcsl  numeric(11,2),      
    yfbmmc varchar(50),      
    ypgg varchar(500),      
    ypjxCode varchar(20),      
    kzbz varchar(1),      
    zyqzlx char(1),      
    ybdm varchar(500),      
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
    bz varchar(500),
	sccj varchar(500),
	gjybdm varchar(50)
    )   
INSERT INTO #YpQuery  
	select 
	cfypcode ypCode,
	regname ypmc, 
	case listType when '101' then '01' when '102' then '03' end sfdlCode,--收费大类 默认
	case listType when '101' then '西药费' when '102' then '中药费' end sfdlmc  --默认西药费
	,minPacunt dw  
	,case when minPacCnt='' or minPacCnt=NULL THEN 0 else minPacCnt end cls  
	,convert(NUMERIC(9,4),'0.01') dj  
	,py py  
	,0 px  
	,minPrepunt jldw  
	,case when minPacCnt='' or minPacCnt=NULL THEN 0 else minPacCnt end jldwzhxs  
	,'4' zfxz  
	,convert(NUMERIC(9,4),'0') zfbl  
	,'' yfbmCode
	,'1000' kcsl
	,'' yfbmmc
	,specName ypgg  
	, '17' ypjxCode  
	,'' kzbz
	, '' zyqzlx  
	, medListCodg ybdm  
	, null xzyy
	,null xzyysm
	,NULL mrjl
	,NULL mrpc
	,NULL mrpcmc  
	, NULL isKss  
	, NULL jlfwBegin  
	, NULL jlfwEnd  
	, NULL pcfwBegin  
	, NULL pcfwEnd  
	, NULL kssKy
	,convert(NUMERIC(9,4),'0') cxjje,
	''tsypbz,
	''kssqxjb  
	,'' bz
	,isnull(prdrName,'') sccj
	,medListCodg gjybdm
from [NewtouchHIS_Sett]..Dzcf_CFYP_output 
where  regname!=''


select top (@topCount) * from      
(            
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
,cxjje,    
 tsypbz,    
 kssqxjb
 ,sccj  
 ,gjybdm
from #YpQuery      
) as reslt      
where (isnull(@keyword, '') = '' or sfxmCode like '%' + @keyword + '%' or sfxmmc like '%' + @keyword + '%' or py like '%' + @keyword + '%')      
and isnull(ybdm,'')<>''      
      
order by sfxmCode   
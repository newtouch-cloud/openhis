USE [NewtouchHIS_Base]
GO

-- 事件类型: ALTER_PROCEDURE
-- 变更时间: 11/19/2024 16:21:08





/*
author:tyz
time:2023年3月10日
des: 门诊结算单
exec [RPT_SETT_住院医保结算单_报销明细]   '6d5752a7-234a-403e-aa1c-df8b45d3469f', '4294'
*/   
ALTER PROCEDURE [dbo].[RPT_SETT_住院医保结算单_报销明细]    
(
@hospitalCode varchar(50),    
@jsnm int
)    
AS

declare @kssj datetime
declare @jssj datetime
declare @patid varchar(20)
declare @xjzf numeric(10,2)
declare @yjj numeric(10,2)--预交金
declare @szje numeric(10,2)--出院收支
declare @xzzffs varchar(20)
select @kssj=jsksrq,@jssj=a.CreateTime,@patid=patid,@xjzf=a.xjzf,@xzzffs=xjzffs from NewtouchHIS_Sett..zy_js a
left join NewtouchHIS_Sett..zy_brjbxx b on a.zyh=b.zyh and a.OrganizeId=b.OrganizeId and a.zt=1
where jsnm=@jsnm

if(@xzzffs='3')
begin
select top 1 @yjj=b.zhye ,@szje=case when b.zhye>@xjzf then -(b.zhye-@xjzf) else (b.zhye-@xjzf) end   from NewtouchHIS_Sett..xt_zh a
left join NewtouchHIS_Sett..xt_zhszjl b on a.zhCode=b.zhCode and a.OrganizeId=b.OrganizeId and b.zt='1'
where (szxz=1 or szxz=2 or szxz=6) and a.patid=@patid and b.createtime >=@kssj and b.createtime <=@jssj and a.zt='1' order by b.CreateTime desc
end
 else
 begin
 select top 1 @yjj=0.00 ,@szje=@xjzf  from NewtouchHIS_Sett..xt_zh a
left join NewtouchHIS_Sett..xt_zhszjl b on a.zhCode=b.zhCode and a.OrganizeId=b.OrganizeId and b.zt='1'
where (szxz=1 or szxz=2 or szxz=6) and a.patid=@patid and b.createtime >=@kssj and b.createtime <=@jssj and a.zt='1' order by b.CreateTime desc
 end
--select top 1 @yjj=zhye ,@szje=case when zhye>@xjzf then -(zhye-@xjzf) else (zhye-@xjzf) end   from xt_zhszjl 
--where szxz=1 and patid=@patid and createtime >=@kssj and createtime <=@jssj and zt='1' order by CreateTime desc
 
select zybrxx.xm,zyjs.zyts,zyjs.zje,dbo.fn_getformatmoney(zyjs.zje) zjedx,zyjs.zlfy,zyjs.zffy,zyjs.flzffy,zyjs.jzfy,zyjs.xjzf,zyjs.fph,zyjs.jmje,zyjs.ysk,
zyjs.createTime sfrq,brxz.brxzmc ,xjzffs.xjzffsmc ,s.Name,
isnull(zyjs.xjwc,0.00) srje,
isnull(jsjl.cvlserv_pay,0.00) gwybz,
isnull(jsjl.hifp_pay,0.00) cqtczf,
isnull(jsjl.psn_cash_pay,0.00) cqxjzf,
isnull(jsjl.hifob_pay,0.00) delpje,
isnull(jsjl.maf_pay,0.00) mzjzje,
isnull(jsjl.hifes_pay,0.00) qyzc,
isnull(jsjl.hifmi_pay,0.00) jmdb,
isnull(jsjl.oth_pay,0.00) qtzc,
isnull(jsjl.acct_mulaid_pay,0.00) zhdy,
isnull(jsjl.acct_pay,0.00) zhzf,jsjl.hosp_part_amt,case when isnull(@yjj,0.00)=0.00 then xjzf else @szje end szje,isnull(@yjj,0.00) yjj 
from NewtouchHIS_Sett..zy_js zyjs
INNER JOIN NewtouchHIS_Sett..zy_brjbxx zybrxx ON zybrxx.zyh = zyjs.zyh AND zybrxx.OrganizeId = zyjs.OrganizeId AND zybrxx.zt = 1
--LEFT JOIN NewtouchHIS_Sett..cqyb_OutPut05 cqyb on cqyb.jsnm=zyjs.jsnm and cqyb.OrganizeId=zyjs.OrganizeId and cqyb.zt=1 and cqyb.jslb=3
LEFT JOIN NewtouchHIS_Sett..drjk_zyjs_output jsjl on jsjl.setl_id=zyjs.ybjslsh  --cqyb.jylsh 
LEFT JOIN NewtouchHIS_Sett..xt_xjzffs xjzffs ON xjzffs.xjzffs = zyjs.xjzffs AND xjzffs.zt = 1
LEFT JOIN NewtouchHIS_Sett..xt_brxz brxz ON brxz.brxz = zyjs.brxz AND brxz.OrganizeId = zyjs.OrganizeId AND brxz.zt = 1 
LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff s ON s.Account=zyjs.CreatorCode and s.organizeid=zyjs.organizeid
where zyjs.OrganizeId =@hospitalCode
                            AND zyjs.jsnm =  @jsnm   and zyjs.zt='1'



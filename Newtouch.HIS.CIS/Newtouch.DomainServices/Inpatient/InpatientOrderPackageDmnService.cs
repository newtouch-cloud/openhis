using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.DomainServices.Inpatient
{
    public class InpatientOrderPackageDmnService : DmnServiceBase, IInpatientOrderPackageDmnService
    {
        private readonly IInpatientOrderPackageRepo _inpatientOrderPackageRepo;
        private readonly IInpatientOrderPackageDetailRepo _inpatientOrderPackageDetailRepo;
        private readonly IInpatientDietTemplateDetailSplitRepo _inpatientDietTemplateDetailSplitRepo;
        private readonly IInpatientDietBaseRepo _inpatientDietBaseRepo;
        public InpatientOrderPackageDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        { }

        #region 医嘱模板
        /// <summary>
        /// 另存为模板
        /// </summary>
        /// <param name="mbObj"></param>
        /// <param name="mxList"></param>
        /// <param name="orgId"></param>
        public string saveAsTemplate(InpatientOrderPackageEntity mbObj, List<InpatientOrderPackageVO> mxList,string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                try
                {
                    var dbMbObj = db.IQueryable<InpatientOrderPackageEntity>(p => p.Id == mbObj.Id).FirstOrDefault();
                    if (dbMbObj!=null)  //修改
                    {
                        mbObj.zt = "1";
                        mbObj.OrganizeId = orgId;
                        mbObj.Modify();

                        db.DetacheEntity(dbMbObj);

                        db.Update(mbObj);

                        db.Delete<InpatientOrderPackageDetailEntity>(a => a.MainId == mbObj.Id && a.OrganizeId == mbObj.OrganizeId); //先全删，再新增
                    }
                    else
                    {
                       
                        mbObj.Id = Guid.NewGuid().ToString();
                        mbObj.OrganizeId = orgId;
                        mbObj.Create();
                        db.Insert(mbObj);
                    }

                    //组号生成集合 key操作界面带过来的组号，value后台生成的组号，最终保存到数据库
                    var diczh = new Dictionary<int, int>();
                    foreach (var item in mxList)
                    {
                        //不存在频次，没办法继续。
                        if (string.IsNullOrWhiteSpace(item.pcCode))
                        {
                            continue;
                        }
                        //组号重新赋值
                        if (!string.IsNullOrWhiteSpace(item.zh.ToString()))
                        {
                            if (diczh.ContainsKey(item.zh.ToInt()))
                            {
                                foreach (KeyValuePair<int, int> zhitem in diczh)
                                {
                                    if (item.zh == zhitem.Key)
                                    {
                                        item.zh = zhitem.Value;
                                    }
                                }
                            }
                            else
                            {
                                //生成组号
                                var No = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_cqlsyz.zh", orgId, "", false);
                                diczh.Add(item.zh.ToInt(), No.ToInt());
                                item.zh = No.ToInt();
                            }
                        }

                        //医嘱内容 格式：药品名称 药品规格  药品剂量  剂量单位 用法编码 频次名称 部位
                        item.yznr = item.xmmc + " "
                            + (item.ypgg ?? "")
                            + " " + (item.ypjl.ToString() ?? "")
                            + (item.dw ?? "")
                            + " " + (item.yfmc ?? "")
                            + " " + item.pcmc + " "
                            + " " + (item.bw ?? "");
                        if (item.yzlx == (int)EnumYzlx.ssyz)
                        {
                            if (item.xmmc == "禁食")
                            {
                                item.yznr = item.xmmc;
                            }
                            else if (item.yslb == "膳食自备")
                            {
                                item.yznr = item.yslb;
                            }
                            else
                            {
                                item.yznr = (item.yslb ?? item.xmmc) + "[" + item.yszs + "]" + "  " + (item.ypjl.ToString() ?? "") + " " + (item.nlmd ?? "") + "  " + item.yfmc + "  " + item.pcmc;
                            }
                        }
                        if (item.yzlx == (int)EnumYzlx.zcy)
                        {
                            //医嘱格式：药品名称 规格 每次剂量 每次剂量单位 用法 频次 贴数
                            item.yznr = item.xmmc + "  " + item.ypgg + " " + item.ypjl + item.dw + " " + item.yfmc + " " + item.pcmc + " " + item.sl + "剂";
                        }
                        //对象赋值  
                        var addentity = item.MapperTo<InpatientOrderPackageVO, InpatientOrderPackageDetailEntity>();//AddCQdoctorService(item, orgId, tzsj);
                        addentity.OrganizeId = orgId;
                        addentity.MainId = mbObj.Id;
                        addentity.Create(true);
                        db.Insert(addentity);
                        db.Insert(savessyzTemplate(item, orgId, addentity.Id));
                        continue;
                    }
                    db.Commit();
                    return mbObj.Id;
                }
                catch (Exception e)
                {
                    throw;
                }
               
            }
        }

        public List<InpatientDietTemplateDetailSplitEntity> savessyzTemplate(InpatientOrderPackageVO item, string orgId,string tcmxId)
        {
            var list = new List<InpatientDietTemplateDetailSplitEntity>();
            if (item != null && !string.IsNullOrWhiteSpace(item.yszsval))
            {
                var ssIds = item.yszsval.Split(',');
                if (ssIds != null)
                {
                    for (int i = 0; i < ssIds.Length; i++)
                    {
                        var v = ssIds[i];
                        if (string.IsNullOrWhiteSpace(v))
                        {
                            continue;
                        }
                        var nameentity = _inpatientDietBaseRepo.FindEntity(p => p.Id == v && p.zt == "1"&&p.OrganizeId==orgId);
                        if (nameentity == null)
                        {
                            throw new FailedException("缺少膳食基础");
                        }
                        var entity = new InpatientDietTemplateDetailSplitEntity();
                        entity.OrganizeId = orgId;
                        entity.tcmxId = tcmxId;
                        entity.BaseId = ssIds[i];
                        entity.Name = nameentity.Name;
                        entity.sslb = item.yslbdm;
                        entity.zt = "1";
                        entity.Create(true);
                        list.Add(entity);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 根据模板ID找到模板详情
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<DoctorServiceUIRequestDto> GetMBDetailByMainId(string Id, string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"SELECT   
    tcmx.Id,
    tc.tcmc ,
    tc.tcfw,
    zh ,
    pcCode ,
    pc.yzpcmc pcmc ,
    tcmx.zxcs ,
    tcmx.zxzq ,
    tcmx.zxzqdw ,
    zdm ,
    xmdm ,
    xmmc ,
    tcmx.dw dwwwwwww ,
    tcmx.dw ,
    tcmx.sl ,
    tcmx.yzlb,
    dwlb ,
    yzlx ,
    ypjl ,
    tcmx.ypgg ,
    ztnr ,
    ypyfdm ,
    yf.yfmc yfmcval,
    tc.WardCode ,
    tc.DeptCode ,
    tc.ysgh ,
    tcmx.zbbz zbbzzzzzzz,
    ( CASE tcmx.dwlb WHEN '1' THEN zycldw  WHEN 4 THEN jldw  END ) redundant_jldw,
    zycldw zydw,
	tcmx.bw,
    yp.jx jxCode ,
    ypkc.sl kcsl ,
    ypkc.kzbz,
	tcmx.nlmd nlmddm,
    ( SELECT DISTINCT
                STUFF(( SELECT  ',' + Name
                        FROM    zy_sstcmxcf WITH ( NOLOCK )
                        WHERE   tcmxId = tcmx.Id
                                AND OrganizeId = tcmx.OrganizeId
                                AND zt = '1'
                        FOR
                        XML PATH('')
                        ), 1, 1, '') AS jzhw
        FROM      zy_sstcmxcf t
    ) yszs ,
    ( SELECT DISTINCT
                STUFF(( SELECT  ',' + BaseId
                        FROM    zy_sstcmxcf WITH ( NOLOCK )
                        WHERE   tcmxId = tcmx.Id
                                AND OrganizeId = tcmx.OrganizeId
                                AND zt = '1'
                        FOR
                        XML PATH('')
                        ), 1, 1, '') AS jzhw
        FROM      zy_sstcmxcf t
    ) yszsval ,
    ( SELECT TOP 1
                sslb
        FROM      zy_sstcmxcf
        WHERE     tcmxId = tcmx.Id
                AND OrganizeId = tcmx.OrganizeId
                AND zt = '1'
    ) yslbdm ,
    ( SELECT    b.Name
        FROM      dbo.zy_DietBase b
                INNER JOIN ( SELECT TOP 1
                                    *
                                FROM   zy_sstcmxcf
                                WHERE  tcmxId = tcmx.Id
                                    AND OrganizeId = tcmx.OrganizeId
                                    AND zt = '1'
                            ) a ON b.Id = a.sslb
        WHERE     b.OrganizeId = tcmx.OrganizeId
                AND b.zt = '1'
    ) yslb,
	sfxm.dwjls,
	sfxm.dj,tcmx.zxksdm,ks.Name zxksmc,sfxm.zfxz
FROM    dbo.zy_yztcmx tcmx
JOIN dbo.zy_yztc tc ON tc.Id = tcmx.MainId AND tcmx.OrganizeId = tc.OrganizeId
LEFT JOIN NewtouchHIS_Base..xt_yzpc pc ON tcmx.pcCode = pc.yzpcCode AND pc.OrganizeId = tcmx.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_xt_ypyf yf ON yf.yfCode = tcmx.ypyfdm AND yf.zt = '1'
LEFT JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = tcmx.xmdm
                            AND yp.OrganizeId = tcmx.OrganizeId
LEFT JOIN NewtouchHIS_PDS..V_S_P_Kc ypkc ON ypkc.ypCode = tcmx.xmdm AND ypkc.yfbmCode = tcmx.zxksdm AND ypkc.OrganizeId = tcmx.OrganizeId
left join NewtouchHIS_Base..V_S_xt_sfxm sfxm on sfxm.sfxmCode=tcmx.xmdm and sfxm.OrganizeId=tcmx.OrganizeId
left join NewtouchHIS_Base..Sys_Department ks on ks.Code=tcmx.zxksdm and ks.OrganizeId=tcmx.OrganizeId
WHERE   tcmx.zt = '1' AND tc.zt = '1' AND pc.zt = '1' 
AND tc.Id = @Id
AND tcmx.OrganizeId = @orgId
ORDER BY tcmx.CreateTime");
            par.Add(new SqlParameter("@Id", Id));
            par.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<DoctorServiceUIRequestDto>(sqlstr.ToString(), par.ToArray());
        }
        
        public List<PatientDto> GetPatientQuery(string zyh,string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"select brjbxx.zyh,
xm,
case xb when '1' then '男' else '女' end xb,
convert(varchar(50),ryrq,23) ryrq,
convert(varchar(50),cyrq,23) cyrq,
zd.zdmc ryzd,
cyzd.zdmc cyzd
from 
(select 
patid
from [NewtouchHIS_Sett].dbo.zy_brjbxx brjbxx
 where  brjbxx.zt='1' 
 and  brjbxx.zyh=@zyh
 and brjbxx.OrganizeId=@orgId) brxx
left join [NewtouchHIS_Sett].dbo.zy_brjbxx brjbxx
on brxx.patid=brjbxx.patid
and  brjbxx.OrganizeId=@orgId
left join [zy_PatDxInfo] zd
on zd.zyh=brjbxx.zyh and zd.OrganizeId=brjbxx.OrganizeId and zd.zt='1'
and zdlb='1' and zdlx='0'
left join [zy_PatDxInfo] cyzd
on cyzd.zyh=brjbxx.zyh and cyzd.OrganizeId=brjbxx.OrganizeId and cyzd.zt='1'
and cyzd.zdlb='2' and cyzd.zdlx='0'
");
            par.Add(new SqlParameter("@zyh", zyh));
            par.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<PatientDto>(sqlstr.ToString(), par.ToArray());
        }
    
        /// <summary>
        /// 根据模板ID找到模板详情
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<DoctorServiceUIRequestDto> GetMBDetailByDetailId(string IdList, string orgId)
        {
            if (string.IsNullOrWhiteSpace(IdList))
            {
                throw new FailedException("缺少套餐");
            }

            IdList= IdList.Substring(0,IdList.Length-1);
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"SELECT   
                                    tc.Id,
                                    tc.tcmc , 
                                    zh ,
                                    pcCode ,
                                    pc.yzpcmc pcmc ,
                                    tcmx.zxcs ,
                                    tcmx.zxzq ,
                                    tcmx.zxzqdw ,
                                    zdm ,
                                    xmdm ,
                                    xmmc ,
                                    tcmx.dw dwwwwwww ,
                                    tcmx.dw ,
                                    tcmx.sl ,
                                    dwlb ,
                                    yzlx ,
                                    ypjl ,
                                     tcmx.ypgg ,
                                    ztnr ,
                                    ypyfdm ,
                                    yf.yfmc yfmcval,
                                    tc.WardCode ,
                                    tc.DeptCode ,
                                    tc.ysgh ,
                                    tcmx.zbbz zbbzzzzzzz,
                                    ( CASE tcmx.dwlb
                                        WHEN '1' THEN zycldw
                                        WHEN 4 THEN jldw
                                      END ) redundant_jldw,
                                    zycldw zydw,
		                            tcmx.bw,
                                    yp.jx jxCode,
		                            tcmx.zxksdm ,
                                    ypkc.sl kcsl ,
                                    ypkc.kzbz,
									yp.jl jlzhxs,
									yp.zycls zyzhxs,
                                    CASE ypsx.jzlx
                                      WHEN 1 THEN 'day'
                                      WHEN 0 THEN 'times'
                                    END qzfs,
                                    ks.name zxksmc,
		                            sfxm.dwjls,
                                    tcmx.nlmd nlmddm,
                                ( SELECT DISTINCT
                                            STUFF(( SELECT  ',' + Name
                                                    FROM    zy_sstcmxcf WITH ( NOLOCK )
                                                    WHERE   tcmxId = tcmx.Id
                                                            AND OrganizeId = tcmx.OrganizeId
                                                            AND zt = '1'
                                                  FOR
                                                    XML PATH('')
                                                  ), 1, 1, '') AS jzhw
                                  FROM      zy_sstcmxcf t
                                ) yszs ,
                                ( SELECT DISTINCT
                                            STUFF(( SELECT  ',' + Id
                                                    FROM    zy_sstcmxcf WITH ( NOLOCK )
                                                    WHERE   tcmxId = tcmx.Id
                                                            AND OrganizeId = tcmx.OrganizeId
                                                            AND zt = '1'
                                                  FOR
                                                    XML PATH('')
                                                  ), 1, 1, '') AS jzhw
                                  FROM      zy_sstcmxcf t
                                ) yszsval ,
                                ( SELECT TOP 1
                                            sslb
                                  FROM      zy_sstcmxcf
                                  WHERE     tcmxId = tcmx.Id
                                            AND OrganizeId = tcmx.OrganizeId
                                            AND zt = '1'
                                ) yslb ,
                                ( SELECT    b.Name
                                  FROM      dbo.zy_DietBase b
                                            INNER JOIN ( SELECT TOP 1
                                                                *
                                                         FROM   zy_sstcmxcf
                                                         WHERE  tcmxId = tcmx.Id
                                                                AND OrganizeId = tcmx.OrganizeId
                                                                AND zt = '1'
                                                       ) a ON b.Id = a.sslb
                                  WHERE     b.OrganizeId = tcmx.OrganizeId
                                            AND b.zt = '1'
                                ) yslbdm
                            FROM    dbo.zy_yztcmx tcmx
                                    JOIN dbo.zy_yztc tc ON tc.Id = tcmx.MainId
                                                           AND tcmx.OrganizeId = tc.OrganizeId
                                    LEFT JOIN NewtouchHIS_Base..xt_yzpc pc ON tcmx.pcCode = pc.yzpcCode
                                                                              AND pc.OrganizeId = tcmx.OrganizeId
                                    LEFT JOIN NewtouchHIS_Base..V_S_xt_ypyf yf ON yf.yfCode = tcmx.ypyfdm AND yf.zt = '1'
                                    LEFT JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = tcmx.xmdm
                                                    AND yp.OrganizeId = tcmx.OrganizeId
                        LEFT JOIN NewtouchHIS_PDS..V_S_P_Kc ypkc ON ypkc.ypCode = tcmx.xmdm
                                                    AND ypkc.yfbmCode = tcmx.zxksdm
                                                    AND ypkc.OrganizeId = tcmx.OrganizeId
                          LEFT JOIN NewtouchHIS_Base..xt_ypsx ypsx ON ypsx.ypCode = tcmx.xmdm AND ypsx.zt='1'
                                                    AND ypsx.OrganizeId = tcmx.OrganizeId
                            LEFT JOIN NewtouchHIS_Base..xt_sfxm sfxm ON sfxm.sfxmCode = tcmx.xmdm AND sfxm.zt='1'
                                                    AND sfxm.OrganizeId = tcmx.OrganizeId
                            LEFT JOIN NewtouchHIS_Base..[Sys_Department] ks on ks.code=tcmx.zxksdm and ks.OrganizeId=tcmx.OrganizeId and ks.zt='1'
                            WHERE   tcmx.zt = '1'
                                    AND tc.zt = '1'
                                    AND pc.zt = '1'
                                    AND tcmx.Id in (select * from dbo.f_split(@Id,','))
                                    AND tcmx.OrganizeId = @orgId
                                    ORDER BY tcmx.CreateTime");
            //var  inid = "'" + string.Join("','", IdList.Split(',').Distinct().ToList()) + "'";
            par.Add(new SqlParameter("@Id", IdList));
            par.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<DoctorServiceUIRequestDto>(sqlstr.ToString(), par.ToArray());
        }
       
        public void DeleteTemplate(string mbId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(mbId))
            {
                throw new FailedException("缺少套餐");
            }
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Delete<InpatientOrderPackageEntity>(p => p.Id == mbId && p.zt == "1" && p.OrganizeId == orgId);
                var temdetail = db.IQueryable<InpatientOrderPackageDetailEntity>().Where(p => p.MainId == mbId && p.zt == "1" && p.OrganizeId == orgId).ToList();
                if (temdetail != null && temdetail.Count() > 0)
                {
                    foreach (var item in temdetail)
                    {
                        db.Delete<InpatientDietTemplateDetailSplitEntity>(p => p.tcmxId == item.Id && p.OrganizeId == orgId && p.zt == "1");
                    }
                }
                db.Delete<InpatientOrderPackageDetailEntity>(p => p.Id == mbId && p.zt == "1" && p.OrganizeId == orgId);
                db.Commit();
            }
        }
        #endregion

        #region  医嘱引用
        /// <summary>
        /// 历史医嘱引用
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<HistoricalOrdersRequestDto> GetHistoricalOrders(string zyh, string type, string kssj, string jssj, string cqorls, string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"
select *from(
select 
cqyz.[Id]
      ,cqyz.[OrganizeId]
      ,[zyh]
      ,[zh]
      ,[WardCode]
      ,[DeptCode]
      ,[ysgh]
      ,[pcCode]
      ,cqyz.[zxcs]
      ,cqyz.[zxzq]
      ,cqyz.[zxzqdw]
      ,[zdm]
      ,[xmdm]
      ,[xmmc]
      ,[yzzt]
	  ,cqyz.[dw] dwwwwwww
      ,cqyz.[dw]
      ,cqyz.[zbbz]
      ,cqyz.[sl]
      ,[dwlb]
      ,[yzlx]
      ,convert(varchar(50),[shsj],120) shsj
      ,[shr]
      ,convert(varchar(50),[kssj],120) kssj
      ,convert(varchar(50),cqyz.[zxsj],120) zxsj
      ,[zxr]
      ,[ypjl]
      ,cqyz.[ypgg]
      ,[ztnr]
      ,[yznr]
      ,[ypyfdm]
      ,[zxksdm]
      ,[printyznr]
      ,convert(varchar(50),cqyz.[CreateTime],120) CreateTime
      ,cqyz.[CreatorCode]
      ,cqyz.[LastModifyTime]
      ,cqyz.[LastModifierCode]
      ,cqyz.[zt]
      ,[hzxm]
      ,[bw]
      ,[zxsjd]
      ,[nlmddm]
      ,[kssReason]
      ,[ds]
      ,[yzh]
      ,[zzfbz]
      ,[yztag]
      ,[isjf]
      ,[zxing]
      ,[ispscs]
      ,[ztId]
      ,[ztmc]
      ,[ztsl]
      ,[dcztbs]
      ,[yfztbs]
	  ,'长' yzlb
	  ,yf.yfmc yfmcval
	  ,pc.yzpcmc pcmc
	  ,ypkc.sl kcsl
	  ,ypkc.kzbz
	  ,yp.jldw redundant_jldw
	  ,yp.jx jxCode
	  ,CASE ypsx.jzlx
                                      WHEN 1 THEN 'day'
                                      WHEN 0 THEN 'times'
                                    END qzfs
	,ks.name zxksmc
	,sfxm.dwjls
	,sfxm.dj
	,sfxm.zfxz
 from zy_cqyz cqyz
 LEFT JOIN NewtouchHIS_Base..V_S_xt_ypyf yf ON yf.yfCode = cqyz.ypyfdm AND yf.zt = '1'
 LEFT JOIN NewtouchHIS_Base..xt_yzpc pc ON cqyz.pcCode = pc.yzpcCode AND pc.OrganizeId = cqyz.OrganizeId
 LEFT JOIN NewtouchHIS_PDS..V_S_P_Kc ypkc ON ypkc.ypCode = cqyz.xmdm
                                                    AND ypkc.yfbmCode = cqyz.zxksdm
                                                    AND ypkc.OrganizeId = cqyz.OrganizeId
 LEFT JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = cqyz.xmdm
                            AND yp.OrganizeId = cqyz.OrganizeId
LEFT JOIN NewtouchHIS_Base..xt_ypsx ypsx ON ypsx.ypCode = cqyz.xmdm AND ypsx.zt='1'
                                                    AND ypsx.OrganizeId = cqyz.OrganizeId
LEFT JOIN NewtouchHIS_Base..[Sys_Department] ks on ks.code=cqyz.zxksdm and ks.OrganizeId=cqyz.OrganizeId and ks.zt='1'
LEFT JOIN NewtouchHIS_Base..xt_sfxm sfxm ON sfxm.sfxmCode = cqyz.xmdm AND sfxm.zt='1'
                                                    AND sfxm.OrganizeId = cqyz.OrganizeId
union all
select lsyz.[Id]
      ,lsyz.[OrganizeId]
      ,[zyh]
      ,[zh]
      ,[WardCode]
      ,[DeptCode]
      ,[ysgh]
      ,[pcCode]
      ,lsyz.[zxcs]
      ,lsyz.[zxzq]
      ,lsyz.[zxzqdw]
      ,[zdm]
      ,[xmdm]
      ,[xmmc]
      ,[yzzt]
	  ,lsyz.[dw] dwwwwwww
      ,lsyz.[dw]
      ,lsyz.[zbbz]
      ,lsyz.[sl]
      ,[dwlb]
      ,[yzlx]
      ,convert(varchar(50),[shsj],120) shsj
      ,[shr]
      ,convert(varchar(50),[kssj],120) kssj
      ,convert(varchar(50),lsyz.[zxsj],120) zxsj
      ,[zxr]
      ,[ypjl]
      ,lsyz.[ypgg]
      ,[ztnr]
      ,[yznr]
      ,[ypyfdm]
      ,[zxksdm]
      ,[printyznr]
      , convert(varchar(50),lsyz.[CreateTime],120) CreateTime
      ,lsyz.[CreatorCode]
      ,lsyz.[LastModifyTime]
      ,lsyz.[LastModifierCode]
      ,lsyz.[zt]
      ,[hzxm]
      ,[bw]
      ,[zxsjd]
      ,[nlmddm]
      ,[kssReason]
      ,[ds]
      ,[yzh]
      ,[zzfbz]
      ,[yztag]
      ,[isjf]
      ,[zxing]
      ,[ispscs]
      ,[ztId]
      ,[ztmc]
      ,[ztsl]
      ,[dcztbs]
      ,[yfztbs]
	  ,'临' yzlb
	  ,yf.yfmc yfmcval
	  ,pc.yzpcmc pcmc
	  ,ypkc.sl kcsl
	  ,ypkc.kzbz
	  ,yp.jldw redundant_jldw
	  ,yp.jx jxCode
	  ,CASE ypsx.jzlx
                                      WHEN 1 THEN 'day'
                                      WHEN 0 THEN 'times'
                                    END qzfs
	    ,ks.name zxksmc
		,sfxm.dwjls
		,sfxm.dj
		,sfxm.zfxz
	   from zy_lsyz lsyz
	    LEFT JOIN NewtouchHIS_Base..V_S_xt_ypyf yf ON yf.yfCode = lsyz.ypyfdm AND yf.zt = '1'
		LEFT JOIN NewtouchHIS_Base..xt_yzpc pc ON lsyz.pcCode = pc.yzpcCode AND pc.OrganizeId = lsyz.OrganizeId
		LEFT JOIN NewtouchHIS_PDS..V_S_P_Kc ypkc ON ypkc.ypCode = lsyz.xmdm
                                                    AND ypkc.yfbmCode = lsyz.zxksdm
                                                    AND ypkc.OrganizeId = lsyz.OrganizeId
		LEFT JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = lsyz.xmdm
                            AND yp.OrganizeId = lsyz.OrganizeId
		LEFT JOIN NewtouchHIS_Base..xt_ypsx ypsx ON ypsx.ypCode = lsyz.xmdm AND ypsx.zt='1'
                                                    AND ypsx.OrganizeId = lsyz.OrganizeId
		LEFT JOIN NewtouchHIS_Base..[Sys_Department] ks on ks.code=lsyz.zxksdm and ks.OrganizeId=lsyz.OrganizeId and ks.zt='1'
LEFT JOIN NewtouchHIS_Base..xt_sfxm sfxm ON sfxm.sfxmCode = lsyz.xmdm AND sfxm.zt='1'
                                                    AND sfxm.OrganizeId = lsyz.OrganizeId
) a
where yzlx=@type
and zt='1'
and zyh=@zyh
and CreateTime>=@kssj
and CreateTime<=@jssj
and OrganizeId=@orgId
");

            if (cqorls != "")
            {
                sqlstr.Append(@"  and yzlb=@cqorls");
                par.Add(new SqlParameter("@cqorls", cqorls));
            }
            par.Add(new SqlParameter("@zyh", zyh));
            par.Add(new SqlParameter("@type", type));
            if (kssj != null && jssj != null & kssj != "" & jssj != "")
            {
                par.Add(new SqlParameter("@kssj", kssj));
                par.Add(new SqlParameter("@jssj", jssj));
            }
            par.Add(new SqlParameter("@orgId", orgId));
            sqlstr.Append(@" order by CreateTime");
            return this.FindList<HistoricalOrdersRequestDto>(sqlstr.ToString(), par.ToArray());
        }

        /// <summary>
        /// 引用历史医嘱
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<HistoricalOrdersRequestDto> GetHistoricalOrdersById(string yzlistId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(yzlistId))
            {
                throw new FailedException("缺少套餐");
            }

            yzlistId = yzlistId.Substring(0, yzlistId.Length - 1);
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"
select *from(
select 
cqyz.[Id]
      ,cqyz.[OrganizeId]
      ,[zyh]
      ,[zh]
      ,[WardCode]
      ,[DeptCode]
      ,[ysgh]
      ,[pcCode]
      ,cqyz.[zxcs]
      ,cqyz.[zxzq]
      ,cqyz.[zxzqdw]
      ,[zdm]
      ,[xmdm]
      ,[xmmc]
      ,[yzzt]
	  ,cqyz.[dw] dwwwwwww
      ,cqyz.[dw]
      ,cqyz.[zbbz]
      ,cqyz.[sl]
      ,[dwlb]
      ,[yzlx]
      ,convert(varchar(50),[shsj],120) shsj
      ,[shr]
      ,convert(varchar(50),[kssj],120) kssj
      ,convert(varchar(50),cqyz.[zxsj],120) zxsj
      ,[zxr]
      ,[ypjl]
      ,cqyz.[ypgg]
      ,[ztnr]
      ,[yznr]
      ,[ypyfdm]
      ,[zxksdm]
      ,[printyznr]
      ,convert(varchar(50),cqyz.[CreateTime],120) CreateTime
      ,cqyz.[CreatorCode]
      ,cqyz.[LastModifyTime]
      ,cqyz.[LastModifierCode]
      ,cqyz.[zt]
      ,[hzxm]
      ,[bw]
      ,[zxsjd]
      ,[nlmddm]
      ,[kssReason]
      ,[ds]
      ,[yzh]
      ,[zzfbz]
      ,[yztag]
      ,[isjf]
      ,[zxing]
      ,[ispscs]
      ,[ztId]
      ,[ztmc]
      ,[ztsl]
      ,[dcztbs]
      ,[yfztbs]
	  ,'长' yzlb
	  ,yf.yfmc yfmcval
	  ,pc.yzpcmc pcmc
	  ,ypkc.sl kcsl
	  ,ypkc.kzbz
	  ,yp.jldw redundant_jldw
	  ,yp.jx jxCode
	  ,CASE ypsx.jzlx
                                      WHEN 1 THEN 'day'
                                      WHEN 0 THEN 'times'
                                    END qzfs
	,ks.name zxksmc
	,sfxm.dwjls
	,sfxm.dj
	,sfxm.zfxz
 from zy_cqyz cqyz
 LEFT JOIN NewtouchHIS_Base..V_S_xt_ypyf yf ON yf.yfCode = cqyz.ypyfdm AND yf.zt = '1'
 LEFT JOIN NewtouchHIS_Base..xt_yzpc pc ON cqyz.pcCode = pc.yzpcCode AND pc.OrganizeId = cqyz.OrganizeId
 LEFT JOIN NewtouchHIS_PDS..V_S_P_Kc ypkc ON ypkc.ypCode = cqyz.xmdm
                                                    AND ypkc.yfbmCode = cqyz.zxksdm
                                                    AND ypkc.OrganizeId = cqyz.OrganizeId
 LEFT JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = cqyz.xmdm
                            AND yp.OrganizeId = cqyz.OrganizeId
LEFT JOIN NewtouchHIS_Base..xt_ypsx ypsx ON ypsx.ypCode = cqyz.xmdm AND ypsx.zt='1'
                                                    AND ypsx.OrganizeId = cqyz.OrganizeId
LEFT JOIN NewtouchHIS_Base..[Sys_Department] ks on ks.code=cqyz.zxksdm and ks.OrganizeId=cqyz.OrganizeId and ks.zt='1'
LEFT JOIN NewtouchHIS_Base..xt_sfxm sfxm ON sfxm.sfxmCode = cqyz.xmdm AND sfxm.zt='1'
                                                    AND sfxm.OrganizeId = cqyz.OrganizeId
union all
select lsyz.[Id]
      ,lsyz.[OrganizeId]
      ,[zyh]
      ,[zh]
      ,[WardCode]
      ,[DeptCode]
      ,[ysgh]
      ,[pcCode]
      ,lsyz.[zxcs]
      ,lsyz.[zxzq]
      ,lsyz.[zxzqdw]
      ,[zdm]
      ,[xmdm]
      ,[xmmc]
      ,[yzzt]
	  ,lsyz.[dw] dwwwwwww
      ,lsyz.[dw]
      ,lsyz.[zbbz]
      ,lsyz.[sl]
      ,[dwlb]
      ,[yzlx]
      ,convert(varchar(50),[shsj],120) shsj
      ,[shr]
      ,convert(varchar(50),[kssj],120) kssj
      ,convert(varchar(50),lsyz.[zxsj],120) zxsj
      ,[zxr]
      ,[ypjl]
      ,lsyz.[ypgg]
      ,[ztnr]
      ,[yznr]
      ,[ypyfdm]
      ,[zxksdm]
      ,[printyznr]
      , convert(varchar(50),lsyz.[CreateTime],120) CreateTime
      ,lsyz.[CreatorCode]
      ,lsyz.[LastModifyTime]
      ,lsyz.[LastModifierCode]
      ,lsyz.[zt]
      ,[hzxm]
      ,[bw]
      ,[zxsjd]
      ,[nlmddm]
      ,[kssReason]
      ,[ds]
      ,[yzh]
      ,[zzfbz]
      ,[yztag]
      ,[isjf]
      ,[zxing]
      ,[ispscs]
      ,[ztId]
      ,[ztmc]
      ,[ztsl]
      ,[dcztbs]
      ,[yfztbs]
	  ,'临' yzlb
	  ,yf.yfmc yfmcval
	  ,pc.yzpcmc pcmc
	  ,ypkc.sl kcsl
	  ,ypkc.kzbz
	  ,yp.jldw redundant_jldw
	  ,yp.jx jxCode
	  ,CASE ypsx.jzlx
                                      WHEN 1 THEN 'day'
                                      WHEN 0 THEN 'times'
                                    END qzfs
	    ,ks.name zxksmc
		,sfxm.dwjls
		,sfxm.dj
		,sfxm.zfxz
	   from zy_lsyz lsyz
	    LEFT JOIN NewtouchHIS_Base..V_S_xt_ypyf yf ON yf.yfCode = lsyz.ypyfdm AND yf.zt = '1'
		LEFT JOIN NewtouchHIS_Base..xt_yzpc pc ON lsyz.pcCode = pc.yzpcCode AND pc.OrganizeId = lsyz.OrganizeId
		LEFT JOIN NewtouchHIS_PDS..V_S_P_Kc ypkc ON ypkc.ypCode = lsyz.xmdm
                                                    AND ypkc.yfbmCode = lsyz.zxksdm
                                                    AND ypkc.OrganizeId = lsyz.OrganizeId
		LEFT JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = lsyz.xmdm
                            AND yp.OrganizeId = lsyz.OrganizeId
		LEFT JOIN NewtouchHIS_Base..xt_ypsx ypsx ON ypsx.ypCode = lsyz.xmdm AND ypsx.zt='1'
                                                    AND ypsx.OrganizeId = lsyz.OrganizeId
		LEFT JOIN NewtouchHIS_Base..[Sys_Department] ks on ks.code=lsyz.zxksdm and ks.OrganizeId=lsyz.OrganizeId and ks.zt='1'
LEFT JOIN NewtouchHIS_Base..xt_sfxm sfxm ON sfxm.sfxmCode = lsyz.xmdm AND sfxm.zt='1'
                                                    AND sfxm.OrganizeId = lsyz.OrganizeId
) a
where zt='1'
AND a.Id in (select * from dbo.f_split('" + yzlistId + @"',','))
and OrganizeId='" + orgId + @"'
order by CreateTime");
            return this.FindList<HistoricalOrdersRequestDto>(sqlstr.ToString(), par.ToArray());
        }
        /// <summary>
        /// 历史医嘱复制引用
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="yzlistId"></param>
        /// <param name="orgId"></param>
        /// <param name="creatorcode"></param>
        /// <returns></returns>
        public string GetHistoricalRegist(string zyh,string curzyh,string yzlistId, string orgId,string creatorcode)
        {
            if (string.IsNullOrWhiteSpace(yzlistId))
            {
                throw new FailedException("缺少需引用医嘱");
            }

            string sql = @"if(@yzlistId!='')
begin
insert into zy_lsyz ( Id, OrganizeId, zyh, WardCode, DeptCode, ysgh, pcCode, zxcs, zxzq, zxzqdw
, zdm, xmdm, xmmc, yzzt, dw, zbbz, sl, dwlb, yzlx
, kssj, ypjl, ypgg, ztnr, yznr, ypyfdm, zxksdm,  CreateTime, CreatorCode, zt
, hzxm, bw,  yzh, djbz, cydybz ,zzfbz, isjf,ispscs
)
select NEWID() Id, OrganizeId,@curzyh zyh, WardCode, DeptCode, ysgh, pcCode, zxcs, zxzq, zxzqdw
, zdm, xmdm, xmmc, '0' yzzt, dw, zbbz, sl, dwlb, yzlx
,GETDATE() kssj, ypjl, ypgg, ztnr, yznr, ypyfdm, zxksdm, GETDATE() CreateTime, @creatorcode CreatorCode, zt
, hzxm, bw, @yzh yzh, djbz, cydybz ,zzfbz, isjf,ispscs
from zy_lsyz with(nolock)
where zyh=@zyh and OrganizeId=@orgId and Id in (select * from dbo.f_split(@yzlistId,','))

insert into zy_cqyz ( Id, OrganizeId, zyh, WardCode, DeptCode, ysgh, pcCode, zxcs, zxzq, zxzqdw
, zdm, xmdm, xmmc, yzzt, dw, zbbz, sl, dwlb, yzlx
,kssj,ypjl,ypgg,ztnr,yznr,ypyfdm,zxksdm, createtime, CreatorCode, zt
, hzxm, bw, yzh,zzfbz, isjf,ispscs
)
select NEWID() Id, OrganizeId,@curzyh zyh, WardCode, DeptCode, ysgh, pcCode, zxcs, zxzq, zxzqdw
, zdm, xmdm, xmmc, '0' yzzt, dw, zbbz, sl, dwlb, yzlx
,GETDATE() kssj,ypjl,ypgg,ztnr,yznr,ypyfdm,zxksdm,GETDATE() createtime,@creatorcode CreatorCode, zt
, hzxm, bw, @yzh yzh,zzfbz, isjf,ispscs
from zy_cqyz with(nolock)
where zyh=@zyh and OrganizeId=@orgId and Id  in (select * from dbo.f_split(@yzlistId,','))

SELECT 'T'
return
end

SELECT 'F'
return


";
            var yzhPart1 = DateTime.Now.ToString("yyyyMMddHHmmss");
            var yzhPart2 = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_lsyz.yzh", orgId);
            string yzh = string.Format("{0}{1}", yzhPart1, yzhPart2);
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@zyh", zyh));
            par.Add(new SqlParameter("@curzyh", curzyh));
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@yzlistId", yzlistId));
            par.Add(new SqlParameter("@creatorcode", creatorcode));
            par.Add(new SqlParameter("@yzh", yzh));
            return this.FindList<string>(sql.ToString(), par.ToArray()).FirstOrDefault();

        }

        #endregion
    }
}


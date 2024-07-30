using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.DomainServices.Inpatient
{
    public class OperationDmnService : DmnServiceBase, IOperationDmnService
    {

        private readonly IInpatientFeeDetailRepo _IInpatientFeeDetailRepo;

        public OperationDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        { }

        /// <summary>
        /// 手术安排列表查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<ArrangeQueryGridVO> ArrangeQueryGridView(Pagination pagination, ArrangeQueryRequestVO req)
        {
            if (req == null) throw new FailedException("缺少查询条件");
            var sqlstr = new StringBuilder(@"
         SELECT    '临' yzlb ,
                    yz.Id ,
                    yzlx ,
				    yz.zyh,
				    yz.hzxm xm,
                    CONVERT(VARCHAR(20), kssj,120) kssj ,
                    s.Name ysmc ,
                    yz.yznr ,
                    yz.zh ,
                    NULL tzsj ,
                    NULL tzr ,
                    zxr.Name zxr ,
                    yz.yzzt ,
                    yz.CreateTime ,
                    yz.shsj ,
                    yz.zxsj ,
                    CASE WHEN op.id IS NULL THEN '未安排' ELSE '已安排' END ztmc,
		            op.surgeonName,
		            item.Name ssAddr,
		            op.aprq
          FROM      zy_lsyz yz WITH(NOLOCK)
                    LEFT JOIN zy_OperationArrangement op WITH(NOLOCK) ON yz.id = op.lsyzid AND yz.OrganizeId = op.OrganizeId AND op.zt = '1'
                    LEFT JOIN NewtouchHIS_Base.dbo.Sys_ItemsDetail item ON item.Code=op.ssAddr AND item.zt='1' AND item.OrganizeId=yz.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff s WITH(NOLOCK) ON ( ( s.gh = yz.ysgh
                                                              AND s.zt = '1'
                                                              AND s.OrganizeId = @orgId
                                                              )
                                                              OR yz.ysgh = ''
                                                              )
                    LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff zxr WITH(NOLOCK) ON ( ( zxr.gh = yz.zxr
                                                              AND zxr.zt = '1'
                                                              AND zxr.OrganizeId = @orgId
                                                              )
                                                              OR yz.zxr = ''
                                                              )
          WHERE     yz.zt = '1' AND yz.yzlx=9 AND yz.yzzt in (1,2)
                    AND yz.OrganizeId = @orgId ");
            var par = new List<SqlParameter>();
            if (req.kssj != DateTime.MaxValue && req.kssj != DateTime.MinValue &&
                req.jssj != DateTime.MaxValue && req.jssj != DateTime.MinValue)
            {
                sqlstr.Append(" AND yz.kssj BETWEEN @kssj AND DATEADD(dd, 1, @jssj)");
                par.Add(new SqlParameter("@kssj", req.kssj));
                par.Add(new SqlParameter("@jssj", req.jssj));
            }
            if (req.zyh != null && req.zyh.Trim().Length > 0)
            {
                sqlstr.Append(" AND yz.zyh = @zyh ");
                par.Add(new SqlParameter("@zyh", req.zyh));
            }
            par.Add(new SqlParameter("@orgId", req.orgId));
            return QueryWithPage<ArrangeQueryGridVO>(sqlstr.ToString(), pagination, par.ToArray(), false).ToList();
        }

        /// <summary>
        /// 手术安排内容获取
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public ArrangementVO ArrangementForID(Pagination pagination, ArrangementRequestVO req)
        {
            if (req == null)
                throw new FailedException("无法获取医嘱信息");
            const string sqlstr = @" SELECT a.zyh,a.hzxm,a.yznr,b.id,CONVERT(VARCHAR(20), b.aprq,120) aprq,ssAddr,urgent,surgeonId,surgeonName,
assistant ,assistantName ,anesthesiaType ,remark
FROM zy_lsyz a WITH(NOLOCK) 
LEFT JOIN zy_OperationArrangement b WITH(NOLOCK) ON a.Id = b.lsyzid
WHERE a.Id = @Id AND a.OrganizeId=@orgId ";
            return this.FirstOrDefault<ArrangementVO>(sqlstr,
                new DbParameter[] { new SqlParameter("@Id", req.lsyzid),
                new SqlParameter("@orgId", req.orgId)});
        }

        /// <summary>
        /// 获取手术医嘱病人
        /// </summary>
        /// <param name="psOrgId"></param>
        /// <returns></returns>
        public List<OperatPatVO> GetOperationPatSearchList(string psOrgId)
        {
            const string sql = @"
select zyxx.zyh,zyxx.xm,zyxx.nl, zyxx.xb,xtbrxx.py  from NewtouchHIS_Sett..zy_brjbxx zyxx
left join NewtouchHIS_Sett..xt_brjbxx xtbrxx
on xtbrxx.patid = zyxx.patid and xtbrxx.zt = '1' and xtbrxx.OrganizeId = zyxx.OrganizeId
where zyxx.OrganizeId = @orgId and zyxx.zt = '1'
AND EXISTS(SELECT 1 FROM zy_lsyz WHERE zyh=zyxx.zyh AND yzlx=9 AND OrganizeId = @orgId AND yzzt IN (1,2))
AND zyxx.zybz in ('1','2','3')
";
            return this.FindList<OperatPatVO>(sql, new DbParameter[] { new SqlParameter("@orgId", psOrgId) });
        }

        /// <summary>
        /// 提交补录费用
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="FeeDetails"></param>
        public void InpatientFeeDetailSubmit(string orgId, string zyh, List<InpatientFeeDetailEntity> FeeDetails)
        {
            try
            {
                //List<InpatientFeeDetailEntity> Exists = _IInpatientFeeDetailRepo.GetListByZyhYzxz(orgId, FeeDetails[0].zyh, "7");
                List<InpatientFeeDetailEntity> Exists = new List<InpatientFeeDetailEntity>();
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    InpatientFeeDetailEntity tmp = null;
                    foreach (InpatientFeeDetailEntity item in FeeDetails)//添加或更新的数据
                    {
                        if (string.IsNullOrWhiteSpace(item.Id))
                        {
                            item.Create(true);
                            db.Insert(item);
                        }
                        else
                        {
                            tmp = Exists.FirstOrDefault(p => p.Id == item.Id);
                            if (tmp != null)
                            {
                                if (tmp.xmdm != item.xmdm || tmp.sl != item.sl)//数据有修改,则更新(项目或数量变动)
                                {
                                    item.LastModifyTime = DateTime.Now;
                                    item.LastModifierCode = item.czyh;
                                    db.Update<InpatientFeeDetailEntity>(item);
                                }
                                Exists.Remove(tmp);//提取要删除的数据
                            }
                        }
                    }
                    foreach (InpatientFeeDetailEntity item in Exists)//删除的数据
                    {
                        item.zt = "0";
                        item.LastModifyTime = DateTime.Now;
                        item.LastModifierCode = item.czyh;
                        db.Update<InpatientFeeDetailEntity>(item);//逻辑删除
                    }
                    db.Commit();
                }
            }
            catch (Exception e)
            {
                throw new FailedException("收费项目保存失败，" + e.Message);
            }
        }


        /// <summary>
        /// 通过住院号,医嘱性质获取费用信息
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        public List<OperatFeeVO> GetOperatFee(string OrganizeId, string zyh, string yzxz)
        {
            const string lsSQL = @" 
SELECT * FROM 
(
SELECT a.Id ,a.OrganizeId ,a.zyh ,a.yzxh ,a.zxrq ,a.qqrq ,a.xmdm ,a.xmmc ,
a.dxmdm ,a.gg ,a.dw ,a.dwxs ,a.ykxs ,ISNULL(b.sl,a.sl) sl ,a.dj ,a.zfdj ,a.yhdj ,a.zje ,a.zfje ,a.yhje ,a.yzxz ,a.memo ,
a.flzfje ,a.ybshbz ,a.ybdm ,a.jzks ,a.gdzxbz , a.yzlb ,a.WardCode ,a.DeptCode ,a.fjdm ,a.cwdm ,a.czyh ,a.ysgh ,a.ysksdm ,
a.qrksdm ,a.zxksdm ,a.CreateTime , a.CreatorCode ,a.LastModifyTime ,a.LastModifierCode ,a.zt,
CASE WHEN b.jfbbh IS NOT NULL THEN '1' ELSE '0' END zxzt 
FROM zy_fymxk a WITH(NOLOCK)
LEFT JOIN NewtouchHIS_Sett..zy_ypjfb b WITH(NOLOCK) ON a.id = b.bdzxid AND a.OrganizeId = b.OrganizeId AND b.zt=1  
WHERE a.zyh=@zyh AND a.OrganizeId = @orgId AND a.zt ='1' AND a.yzlb = 0 AND (a.yzxz = @yzxz OR @yzxz='' ) 

UNION ALL
SELECT a.Id ,a.OrganizeId ,a.zyh ,a.yzxh ,a.zxrq ,a.qqrq ,a.xmdm ,a.xmmc ,
a.dxmdm ,a.gg ,a.dw ,a.dwxs ,a.ykxs ,ISNULL(b.sl,a.sl) sl ,a.dj ,a.zfdj ,a.yhdj ,a.zje ,a.zfje ,a.yhje ,a.yzxz ,a.memo ,
a.flzfje ,a.ybshbz ,a.ybdm ,a.jzks ,a.gdzxbz , a.yzlb ,a.WardCode ,a.DeptCode ,a.fjdm ,a.cwdm ,a.czyh ,a.ysgh ,a.ysksdm ,
a.qrksdm ,a.zxksdm ,a.CreateTime , a.CreatorCode ,a.LastModifyTime ,a.LastModifierCode ,a.zt,
CASE WHEN b.jfbbh IS NOT NULL THEN '1' ELSE '0' END zxzt 
FROM zy_fymxk a WITH(NOLOCK)
LEFT JOIN NewtouchHIS_Sett..zy_xmjfb b WITH(NOLOCK) ON a.id = b.bdzxid AND a.OrganizeId = b.OrganizeId AND b.zt=1  
WHERE a.zyh=@zyh AND a.OrganizeId = @orgId AND a.zt ='1' AND a.yzlb = -1 AND (a.yzxz = @yzxz OR @yzxz='' )
) T ORDER BY CreateTime 
";
            return this.FindList<OperatFeeVO>(lsSQL, new DbParameter[] {new SqlParameter("@zyh", zyh),
                new SqlParameter("@orgId", OrganizeId), new SqlParameter("@yzxz", yzxz), });
        }
    }
}


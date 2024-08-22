using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using Newtouch.Core.Common;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Data.Common;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;

namespace Newtouch.HIS.Repository
{
    public class SysPatientBasicInfoRepo : RepositoryBase<SysPatientBasicInfoEntity>, ISysPatientBasicInfoRepo
    {
        public SysPatientBasicInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void SubmitForm(SysPatientBasicInfoEntity sysPatBasicInfoEntity, string keyValue)
        {

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                //先变更标志和时间 暂定修改 其实是更改到病人基本信息变更表
                var entity = this.FindEntity(sysPatBasicInfoEntity.patid);
                entity.Modify(keyValue);
                this.Update(entity);
                //再insert
                sysPatBasicInfoEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_brjbxx"));
                this.Insert(sysPatBasicInfoEntity);
            }
            else
            {
                sysPatBasicInfoEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_brjbxx"));
                this.Insert(sysPatBasicInfoEntity);
            }
        }

        /// <summary>
        /// （病历号由系统自动生成）获取最新病历号
        /// </summary>
        /// <returns></returns>
        public string Getblh(string orgId)
        {
            return EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_blh", orgId, initFormat: "{0:D5}");
        }

        /// <summary>
        /// 根据patid获取病人基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public SysPatientBasicInfoEntity GetInfoByPatid(string patid, string orgId)
        {
            int id = Convert.ToInt32(patid);
            return this.IQueryable().FirstOrDefault(p => p.patid == id && p.zt == "1" && p.OrganizeId == orgId);
        }

        /// <summary>
        /// 根据证件号，姓名获取病人基本信息
        /// </summary>
        /// <param name="zjh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetBasicInfoByZjh(string zjh, string orgId)
        {
            var sce = IQueryable().Where(p => p.OrganizeId == orgId);
            var firentity = new SysPatientBasicInfoEntity();
            if (!string.IsNullOrWhiteSpace(zjh))
            {
                firentity = sce.FirstOrDefault(p => p.zjh == zjh);
            }
            if (firentity != null)
            {
                return firentity.patid.ToString();
            }
            return "0";
        }

        /// <summary>
        /// 根据姓名获取病人基本信息
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<SysPatientBasicInfoEntity> GetBasicInfoByxm(string xm, string orgId)
        {
            var sce = this.IQueryable().Where(p => p.OrganizeId == orgId);
            if (!string.IsNullOrWhiteSpace(xm))
            {
                sce = sce.Where(p => p.xm.Contains(xm));
            }
            return sce.ToList();
        }

        /// <summary>
        /// 根据病历号获取信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="blh"></param>
        /// <returns></returns>
        public SysPatientBasicInfoEntity GetInfoByblh(string orgId, string blh)
        {
            var sce = IQueryable().FirstOrDefault(p => p.OrganizeId == orgId && p.blh == blh);
            return sce;
        }

        /// <summary>
        /// 根据病历号查询
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        public IList<PatOnlyBlhSearchVO> GetPatOnlyBlhSearchList(Pagination pagination, string orgId, string blh, string xm)
        {
            if (orgId == null)
            {
                return null;
            }
            IList<SqlParameter> parlist = new List<SqlParameter>();
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
SELECT  patId,
        blh,
         xm,
         xb,
		 CAST( DATEDIFF(YEAR,csny,GETDATE()) AS INT) nl
FROM xt_brjbxx brjbxx with(nolock)
WHERE OrganizeId = @orgId
        AND zt='1'
                ");
            if (!string.IsNullOrEmpty(blh))
            {
                sqlStr.Append(" AND blh LIKE @blh");
                parlist.Add(new SqlParameter("@blh", "%" + blh + "%"));
            }
            if (!string.IsNullOrEmpty(xm))
            {
                sqlStr.Append(" AND xm LIKE @xm");
                parlist.Add(new SqlParameter("@xm", "%" + xm + "%"));
            }
            parlist.Add(new SqlParameter("@orgId", orgId));
            IList<PatOnlyBlhSearchVO> list = this.QueryWithPage<PatOnlyBlhSearchVO>(sqlStr.ToString(), pagination, parlist.ToArray());
            return list;
        }

        /// <summary>
        /// 更新紧急联系人相关信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <param name="lxr"></param>
        /// <param name="lxrgx"></param>
        /// <param name="lxrdh"></param>
        /// <param name="userCode"></param>
        public void UpdatelxrInfo(string patid, string orgId, string lxr, string lxrgx, string lxrdh, string userCode)
        {
            int t = int.Parse(patid);
            var xtbrxx = this.FindEntity(p => p.patid == t && p.OrganizeId == orgId && p.zt == "1");
            xtbrxx.jjllr = lxr;
            xtbrxx.jjlldh = lxrdh;
            xtbrxx.jjllrgx = lxrgx;


            //系统病人基本信息
            if (xtbrxx != null)
            {
                xtbrxx.LastModifyTime = DateTime.Now;
                xtbrxx.LastModifierCode = userCode;
                Update(xtbrxx);
            }
        }

        public void Updateybxx(string kh, string zjh, string cbdbm, string cblb, string grbh, string xzlx, string orgId, string userCode)
        {

//            var entity = this.FindEntity(p => p.OrganizeId == orgId && (p.sbbh == kh || p.zjh == zjh) && p.brxz == "1" && p.zt == "1");
//            if (entity != null)
//            {
//                entity.cbdbm = cbdbm;
//                entity.cblb = cblb;
//                entity.grbh = grbh;
//                entity.xzlx = xzlx;
//                entity.LastModifyTime = DateTime.Now;
//                entity.LastModifierCode = userCode;
//                Update(entity);

//                const string sql = @"
//UPDATE dbo.xt_brjbxx SET cbdbm=@cbdbm,cblb=@cblb,grbh=@grbh,xzlx=@xzlx, LastModifyTime=GETDATE(), LastModifierCode=@usercode 
//WHERE  zjh=@zjh AND OrganizeId=@orgId  and zt='1'
//";
//                var param = new DbParameter[]
//                {
//                new SqlParameter("@zjh", entity.zjh),
//                new SqlParameter("@orgId", orgId),
//                new SqlParameter("@userCode", userCode),
//                new SqlParameter("@cbdbm", cbdbm),
//                new SqlParameter("@cblb", cblb),
//                new SqlParameter("@grbh", grbh),
//                new SqlParameter("@xzlx", xzlx),
//                };
//                int i= ExecuteSqlCommand(sql, param);
//            }
        }
        public void UpdateZt(string patid, string orgId, string zt,string userCode)
        {
            int t = int.Parse(patid);
            var xtbrxx = this.FindEntity(p => p.patid == t && p.OrganizeId == orgId);
            //系统病人基本信息
            if (xtbrxx != null)
            {
                xtbrxx.zt = zt;
                xtbrxx.LastModifyTime = DateTime.Now;
                xtbrxx.LastModifierCode = userCode;
                Update(xtbrxx);
            }
        }



        #region 查询病人信息修改日志
        public IList<SysPatientBasiInfoLOGVO> GetModifyLog(Pagination pagination, string orgId, string xm)
        {
            var sql = string.Format(@"select * from [NewtouchHIS_Sett].[dbo].[xt_brjbxxLOG]  
where OrganizeId=@orgId and zt='1' ");
            IList<SqlParameter> parlist = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(xm))
            {
                sql += "  AND ( zjh like @xm or  blh like @xm or xm like @xm or py like @xm) ";
                parlist.Add(new SqlParameter("@xm", "%" + xm + "%"));
            }
            parlist.Add(new SqlParameter("@orgId", orgId));
            return this.QueryWithPage<SysPatientBasiInfoLOGVO>(sql.ToString(), pagination, parlist.ToArray());
        }

        public IList<SysPatientBasiInfoLOGVO> GetDetailsData(string Id, string orgId)
        {
            var sql = string.Format(@"select a.id,a.bz 
into #dataname 
 from ( 
select s.code,s.id,d.bz from ( 
 select SUBSTRING(a.data_name,number,CHARINDEX(',',a.data_name+',',number)-number) as code,a.id   
  from [NewtouchHIS_Sett].[dbo].[xt_brjbxxLOG]  a  with(nolock) ,master..spt_values  with(nolock)   
  where type='p'  
   and SUBSTRING(','+a.data_name,number,1)=','  and a.Id=@Id 
   and a.OrganizeId=@orgId 
   ) s 
left join  ( 
SELECT A.NAME,ISNULL(G.[VALUE], ' ') bz 
        FROM  SYSCOLUMNS   A INNER   JOIN SYSOBJECTS   D  ON  A.ID=D.ID     AND   D.XTYPE= 'U '   AND     D.NAME <> 'DTPROPERTIES' 
        LEFT   JOIN  sys.extended_properties   G  ON  A.ID=G.major_id   AND   A.COLID=G.minor_id 
		 where d.name='xt_brjbxxLOG' 
		 ) d 
on d.name=s.code 
) a 
select a.Id,b.bz as datanames,a.datavalue_old,a.datavalue_new from [NewtouchHIS_Sett].[dbo].[xt_brjbxxLOG] a 
left join (
select id,(select STUFF(( SELECT','+convert(VARCHAR, bz) FROM #dataname  FOR XML PATH('')), 1, 1, '') AS bz) bz from #dataname group by id) b 
on a.id=b.id 
left join [NewtouchHIS_Sett].[dbo].[xt_card] c 
on a.patid=c.patid where c.zt='1' and c.OrganizeId=@orgId 
and a.Id=@Id 
drop table #dataname ");
            IList<SqlParameter> parlist = new List<SqlParameter>();
            parlist.Add(new SqlParameter("@orgId", orgId));
            parlist.Add(new SqlParameter("@Id", Id));
            return this.FindList<SysPatientBasiInfoLOGVO>(sql.ToString(), parlist.ToArray());
        }

        #endregion


        #region 一卡通管理
        public IList<SysCardEntity> GetCardNoInfo(string patId, string orgId, string cartype)
        {
            var sql = "select * from [NewtouchHIS_Sett].[dbo].[xt_card] with(nolock) where patid=@patId and OrganizeId=@orgId and zt='1' ";
            IList<SqlParameter> parlist = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(cartype))
            {
                sql += "  and CardType=@cartype ";
                parlist.Add(new SqlParameter("@cartype", cartype));
            }
            //if (!string.IsNullOrEmpty(cardno))
            //{
            //    sql += "  and CardNo=@CardNo ";
            //    parlist.Add(new SqlParameter("@CardNo", cardno));
            //}
            sql += "  order by CreateTime desc";
            parlist.Add(new SqlParameter("@orgId", orgId));
            parlist.Add(new SqlParameter("@patId", patId));
            return this.FindList<SysCardEntity>(sql.ToString(), parlist.ToArray());
        }

        public void SubmitCard(SysHosBasicInfoVO vo, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                SysCardEntity newCardEntity = null;
                newCardEntity = new SysCardEntity();
                newCardEntity.CardNo = vo.kh;
                newCardEntity.CardType = vo.cardtype;
                newCardEntity.CardTypeName = ((EnumCardType)(Convert.ToInt32(vo.cardtype))).GetDescription();
                newCardEntity.grbh = vo.grbh;
                newCardEntity.xzlx = vo.xzlx;
                newCardEntity.cbdbm = vo.cbdbm;
                newCardEntity.cblb = vo.cblb;
                newCardEntity.brxz = vo.brxz;
                newCardEntity.zt = "1";
                newCardEntity.hzxm = vo.xm;
                newCardEntity.OrganizeId = orgId;
                newCardEntity.patid = int.Parse(vo.patid.ToString());

                if (newCardEntity != null)
                {
                    newCardEntity.Create(true);
                    db.Insert(newCardEntity);
                    db.Commit();
                }
            }


        }

        public void CardVoids(string CardId, string orgId,string upcode)
        {
            var uptime= DateTime.Now.ToString();
            var sql = "update [NewtouchHIS_Sett].[dbo].[xt_card] set zt='0' ,LastModifyTime=@uptime, LastModifierCode=@upcode where CardId=@CardId and OrganizeId=@orgId ";
            var param = new DbParameter[]
                {
                new SqlParameter("@CardId", CardId),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@upcode", upcode),
                new SqlParameter("@uptime", uptime),
                };
            int i = ExecuteSqlCommand(sql, param);
        }

        #endregion


    }
}

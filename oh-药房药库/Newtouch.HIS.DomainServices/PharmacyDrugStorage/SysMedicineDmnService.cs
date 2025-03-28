using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class SysMedicineDmnService : DmnServiceBase, ISysMedicineDmnService
    {
        

        public SysMedicineDmnService(IDefaultDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        /// <summary>
        /// 获取当前组织下的系统药品信息
        /// </summary>
        /// <returns></returns>
        public IList<SysMedicineVO> GetPaginationList(string organizeId, Pagination pagination, string zt, string ypflCode, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return null;
            }
            var sql = @"
            SELECT x.ypId, x.zxdw,x.ypCode,x.ypmc,x.spm,x.py,x.bzdw,x.mzcls,x.mzcldw,x.zycls,x.zycldw,
            x.djdw,x.lsj,x.pfj,x.zfbl,x.zfxz,x.dlCode,x.ycmc,x.zt,0 bgbz,x.ypbzdm,x.cxjje
			,x.nbdl,x.mzzybz,x.CreateTime,d.pzwh,
            d.yptssx,d.ypflCode,d.zlff,d.jzlx,d.mrbzq,d.sjap,d.zbbz,d.yl,d.yldw,d.zjtzsj
			,d.ghdw,d.ypcd,d.ypgg,d.ybdm,d.xnhybdm, d.gjybdm,c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc, case when d.ybdm IS NULL then '2' when d.ybdm IS NOT NULL AND d.LastYBUploadTime IS NOT NULL AND d.LastYBUploadTime >= d.LastModifyTime then '1' else '0' end isSynch,d.gjybmc
            FROM [NewtouchHIS_Base].dbo.xt_yp as x  
			left join [NewtouchHIS_Base].dbo.xt_ypsx as d on x.ypId=d.ypId
			left join [NewtouchHIS_Base].dbo.xt_sfdl c	on x.dlCode = c.dlCode and c.OrganizeId = @organizeId
			left join [NewtouchHIS_Base].dbo.xt_ypjx e	on x.jx = e.jxCode
			left join [NewtouchHIS_Base].dbo.xt_ypfl f	on d.ypflCode = f.ypflCode
			where x.OrganizeId = @organizeId and d.OrganizeId = @organizeId ";
            DbParameter[] par;
            if (!string.IsNullOrEmpty(zt))
            {
                sql += " and x.zt=@zt";
            }
            if (!string.IsNullOrEmpty(ypflCode))
            {
                sql += " and d.ypflCode=@ypflCode";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += @"
            and (x.ypCode like @keyword or x.ypmc like @keyword or d.gjybdm like @keyword or d.ybdm like @keyword or x.py like @keyword) ";
                par = new DbParameter[]
                {
                    new SqlParameter("@organizeId", organizeId),
                    new SqlParameter("@keyword",  "%" + (keyword??"") + "%"),
                    new SqlParameter("@zt", zt),
                    new SqlParameter("@ypflCode", ypflCode)
                };
            }
            else
            {
                par = new DbParameter[]
                {
                    new SqlParameter("@organizeId", organizeId),
                    new SqlParameter("@zt", zt),
                    new SqlParameter("@ypflCode", ypflCode)
                };
            }

            return this.QueryWithPage<SysMedicineVO>(sql, pagination, par);
        }



        /// <summary>
        /// 获取当前组织下的系统药品信息
        /// </summary>
        /// <returns></returns>
        public IList<SysMedicineVO> GetPaginationListdm(string organizeId, Pagination pagination, string type, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return null;
            }
            var sql = @"
            SELECT x.ypId, x.zxdw,x.ypCode,x.ypmc,x.spm,x.py,x.bzdw,x.mzcls,x.mzcldw,x.zycls,x.zycldw,
            x.djdw,x.lsj,x.pfj,x.zfbl,x.zfxz,x.dlCode,x.ycmc,x.zt,0 bgbz,x.ypbzdm,x.cxjje
			,x.nbdl,x.mzzybz,x.CreateTime,d.pzwh,
            d.yptssx,d.ypflCode,d.zlff,d.jzlx,d.mrbzq,d.sjap,d.zbbz,d.yl,d.yldw,d.zjtzsj
			,d.ghdw,d.ypcd,d.ypgg,d.ybdm,d.xnhybdm, d.gjybdm,c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc, case when d.ybdm IS NULL then '2' when d.ybdm IS NOT NULL AND d.LastYBUploadTime IS NOT NULL AND d.LastYBUploadTime >= d.LastModifyTime then '1' else '0' end isSynch,d.gjybmc
            FROM xt_yp as x  
			left join xt_ypsx as d on x.ypId=d.ypId
			left join xt_sfdl c	on x.dlCode = c.dlCode and c.OrganizeId = @organizeId
			left join xt_ypjx e	on x.jx = e.jxCode
			left join xt_ypfl f	on d.ypflCode = f.ypflCode
			where x.OrganizeId = @organizeId and d.OrganizeId = @organizeId ";
            DbParameter[] par;
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += @"
            and (x.ypCode like @keyword or x.ypmc like @keyword or d.gjybdm like @keyword or d.ybdm like @keyword or x.py like @keyword) ";
                if (type != "qb" && type == "yd")
                {
                    sql += "  and d.gjybdm<>'0'";
                }
                else if (type != "qb" && type == "wd")
                {
                    sql += "and d.gjybdm='0'";
                }
                par = new DbParameter[]
                {
                    new SqlParameter("@organizeId", organizeId),
                    new SqlParameter("@keyword",  "%" + (keyword??"") + "%")
                };
            }
            else
            {
                if (type != "qb" && type == "yd")
                {
                    sql += "  and d.gjybdm<>'0'";
                }
                else if (type != "qb" && type == "wd")
                {
                    sql += "and d.gjybdm='0'";
                }
                par = new DbParameter[]
                {
                    new SqlParameter("@organizeId", organizeId)
                };
            }

            return this.QueryWithPage<SysMedicineVO>(sql, pagination, par);
        }
        /// 目录对照查询
		/// </summary>
		/// <returns></returns>
		public IList<SysMedicineMldzxxVO> GetPaginationListMldzxx(string organizeId, Pagination pagination, string type, string keyword = null)
        {
            DbParameter[] par;
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return null;
            }
            var sql = @" ";
            if (type == "101")
            {
                sql = @"select 
mldz.id,
yp.ypcode code,
yp.ypmc name,
yp.ypgg gg,
yp.gjybdm gjybdm,
case isnull(mldz.isdz,'未对照') when '1' then '已对照' when '0' then '未对照' else '未对照' end isdz
 from V_S_xt_yp yp 
left join  xt_mldz mldz on yp.ypCode=mldz.code
 and yp.gjybdm=mldz.gjybdm and yp.OrganizeId =mldz.OrganizeId
 and  mldz.zt='1'
 where (yp.dlCode='01' or yp.dlCode='02')
 and yp.OrganizeId=@organizeId
 and yp.zt='1'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += @"
            and (yp.ypCode like @keyword or yp.ypmc like @keyword or yp.gjybdm like @keyword or yp.ybdm like @keyword or yp.py like @keyword) ";
                }

            }
            if (type == "102")
            {
                sql = @"select 
mldz.id,
yp.ypcode code,
yp.ypmc name,
yp.ypgg gg,
yp.gjybdm gjybdm,
case isnull(mldz.isdz,'未对照') when '1' then '已对照' when '0' then '未对照' else '未对照' end isdz
 from V_S_xt_yp yp 
left join  xt_mldz mldz on yp.ypCode=mldz.code
 and yp.gjybdm=mldz.gjybdm and yp.OrganizeId =mldz.OrganizeId
 and  mldz.zt='1'
 where  1=1
 and yp.dlCode='03'
 and yp.OrganizeId=@organizeId
 and yp.zt='1'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += @"
            and (yp.ypCode like @keyword or yp.ypmc like @keyword or yp.gjybdm like @keyword or yp.ybdm like @keyword or yp.py like @keyword) ";
                }

            }
            if (type == "301")
            {
                sql = @"  select 
mldz.id,
xm.sfxmCode code,
xm.sfxmmc name,
xm.gg gg,
xm.gjybdm gjybdm,
case isnull(mldz.isdz,'未对照') when '1' then '已对照' when '0' then '未对照' else '未对照' end isdz
 from V_S_xt_sfxm xm 
left join  xt_mldz mldz on xm.sfxmCode=mldz.code
 and xm.gjybdm=mldz.gjybdm and xm.OrganizeId =mldz.OrganizeId
 and  mldz.zt='1'
 where 1=1 
 and xm.sfdlCode='126'
 and xm.OrganizeId=@organizeId
 and xm.zt='1'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += @"
            and (xm.sfxmCode like @keyword or xm.sfxmmc like @keyword or xm.gjybdm like @keyword  or xm.py like @keyword) ";
                }

            }
            if (type == "201")
            {
                sql = @"  select 
mldz.id,
xm.sfxmCode code,
xm.sfxmmc name,
xm.gg gg,
xm.gjybdm gjybdm,
case isnull(mldz.isdz,'未对照') when '1' then '已对照' when '0' then '未对照' else '未对照' end isdz
 from V_S_xt_sfxm xm 
left join  xt_mldz mldz on xm.sfxmCode=mldz.code
 and xm.gjybdm=mldz.gjybdm and xm.OrganizeId =mldz.OrganizeId
 and  mldz.zt='1'
 where 1=1 
 and xm.sfdlCode!='126'
 and xm.OrganizeId=@organizeId
 and xm.zt='1'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += @"
            and (xm.sfxmCode like @keyword or xm.sfxmmc like @keyword or xm.gjybdm like @keyword  or xm.py like @keyword) ";
                }

            }
            par = new DbParameter[]
              {
                    new SqlParameter("@organizeId", organizeId),
                    new SqlParameter("@keyword",  "%" + (keyword??"") + "%")
              };
            return this.QueryWithPage<SysMedicineMldzxxVO>(sql, pagination, par);
        }
        /// <summary>
        /// 查询医保药品信息
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        public IList<G_yb_ypxxVO> GetYpypxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh)
        {
            string sql = @"select top 200 * from (
select 
药品代码 ypdm
,数据来源 sjly
,注册名称 zcmc
,商品名称 spmc
,注册剂型 zcjx
,实际剂型 sjjx
,注册规格 zcgg
,实际规格 sjgg
,包装材质 bzcz
,最小包装数量 zxbzsl
,最小制剂单位 zxzjdw
,最小包装单位 zxbzdw
,药品企业 ypqy
,上市药品持有人 shypcyr
,批准文号 pzwh
,原批准文号 ypzwh
,药品本位码 ypbwm
,分包装企业名称 fbzqymc
,生产企业 scqy
,市场状态 sczt
,医保药品名称 ybypmc
,[2021版甲乙类] ybjyl
,医保剂型 ybjx
,编号 bh
,备注 bz
,甲乙类 jyl
,备注1 bz1
,[备注（特殊属性标识）] bztssxbs
,医保支付标准 ybzfbz
,pym
from dbo.gb_ypxx 
)a where 1=1 
";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(gjybdm))
            {
                sql += "  and (ypdm like  '%'+@gjybdm+'%' or zcmc like '%'+@gjybdm+'%' or pzwh like '%'+@gjybdm+'%' or  ybypmc like '%'+@gjybdm+'%' or ypqy like '%'+@gjybdm+'%' or pym like '%'+@gjybdm+'%')";
                pars.Add(new SqlParameter("@gjybdm", gjybdm));
            }
            if (!string.IsNullOrEmpty(pzwh))
            {
                sql += "  and pzwh like '%'+@pzwh+'%'";
                pars.Add(new SqlParameter("@pzwh", pzwh));
            }
            if (!string.IsNullOrEmpty(ypmc))
            {
                sql += "  and scqy like '%'+@ypmc+'%'";
                pars.Add(new SqlParameter("@ypmc", ypmc));
            }
            //if (!string.IsNullOrEmpty(py))
            //{
            //	sql += "  and 注册名称 like @ypmc";
            //	pars.Add(new SqlParameter("@ypmc", "%" + ypmc + "%"));
            //}
            return this.FindList<G_yb_ypxxVO>(sql, pars.ToArray());
        }

        /// <summary>
        /// 保存药品医保对码
        /// </summary>
        /// <param name="ybxx"></param>
        /// <param name="ypid"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        //public int SaveYpYb(G_yb_ypxxVO ybxx, int? ypid, string organizeId)
        //{
        //    int updatecount = 0;
        //    if (ypid.HasValue && ypid.Value > 0)
        //    {
        //        using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
        //        {
        //            var ypEntity = db.IQueryable<SysMedicineEntity>().FirstOrDefault(p => p.ypId == ypid && p.OrganizeId == organizeId);
        //            var ypsxEntity = db.IQueryable<SysMedicinePropertyEntity>().FirstOrDefault(p => p.ypId == ypid && p.OrganizeId == organizeId);
        //            if (!string.IsNullOrEmpty(ybxx.ypdm) && !string.IsNullOrEmpty(ybxx.zcmc))
        //            {
        //                ypsxEntity.gjybdm = ybxx.ypdm;
        //                ypsxEntity.gjybmc = ybxx.zcmc;
        //            }
        //            if (!string.IsNullOrEmpty(ybxx.jyl))
        //            {
        //                ypsxEntity.ybbz = "1";//存在国家医保代码 不查看药品本身属性 直接默认医保
        //                ypEntity.zfxz = EnumZFXZv2.J.GetDescription().Contains(ybxx.jyl) ? "4" : EnumZFXZv2.Y.GetDescription().Contains(ybxx.jyl) ? "5" : EnumZFXZv2.B.GetDescription().Contains(ybxx.jyl) ? "6" : "1";
        //            }

        //            db.Update(ypEntity);
        //            db.Update(ypsxEntity);

        //            updatecount = db.Commit();
        //        }

        //    }

        //    return updatecount;
        //}

        /// <summary>
        /// 查询系统材料信息
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysChargeItemVO> GetclxxList(string organizeId, Pagination pagination, string type, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return null;
            }
            var sql = @" select * from dbo.xt_sfxm where sfdlcode='126' and zt='1'  and organizeid=@organizeId  ";
            DbParameter[] par;
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " and sfxmmc like @keyword ";
                if (type != "qb" && type == "yd")
                {
                    sql += "  and gjybdm is not null";
                }
                else if (type != "qb" && type == "wd")
                {
                    sql += " and gjybdm is  null";
                }
                par = new DbParameter[]
                {
                    new SqlParameter("@organizeId", organizeId),
                    new SqlParameter("@keyword",  "%" + (keyword??"") + "%")
                };
            }
            else
            {
                if (type != "qb" && type == "yd")
                {
                    sql += "  and gjybdm is not null";
                }
                else if (type != "qb" && type == "wd")
                {
                    sql += " and gjybdm is  null";
                }
                par = new DbParameter[]
                {
                    new SqlParameter("@organizeId", organizeId)
                };
            }

            return this.QueryWithPage<SysChargeItemVO>(sql, pagination, par);
        }

        /// <summary>
        /// 查询医保材料信息 根据系统材料信息
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        public IList<G_yb_clxxVO> GetYbclxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh)
        {
            string sql = @"select top 200 * from(
select 
耗材代码 hcdm
,一级分类 yjfl
,二级分类 ejfl
,三级分类 sjfl
,医保通用名 ybtym
,材质 cz
,特征 tz
,注册备案号 zcbah
,单件产品名称 djcpmc
,耗材企业 hcqy
,规格型号数 ggxhs
,注册备案人 zcbar
,ybxz
,pym
from gb_clxx )a where 1=1 ";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(gjybdm))
            {
                sql += "  and (hcdm like  '%'+@gjybdm+'%' or djcpmc like '%'+@gjybdm+'%' or zcbah like '%'+@gjybdm+'%' or  hcqy like '%'+@gjybdm+'%' or pym like '%'+@gjybdm+'%')";
                pars.Add(new SqlParameter("@gjybdm", gjybdm));
            }
            if (!string.IsNullOrEmpty(pzwh))
            {
                sql += "  and zcbah like '%'+@pzwh+'%'";
                pars.Add(new SqlParameter("@pzwh", pzwh));
            }
            if (!string.IsNullOrEmpty(ypmc))
            {
                sql += "  and hcqy like '%'+@ypmc+'%'";
                pars.Add(new SqlParameter("@ypmc", ypmc));
            }
            return this.FindList<G_yb_clxxVO>(sql, pars.ToArray());
        }
        /// <summary>
        /// 保存材料对码
        /// </summary>
        /// <param name="ybxx"></param>
        /// <param name="ypid"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        //public int SaveYpcl(G_yb_clxxVO ybxx, int? ypid, string organizeId)
        //{
        //    int updatecount = 0;
        //    if (ypid.HasValue && ypid.Value > 0)
        //    {
        //        using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
        //        {
        //            var sfxmEntity = db.IQueryable<SysChargeItemEntity>().FirstOrDefault(p => p.sfxmId == ypid && p.OrganizeId == organizeId);

        //            if (!string.IsNullOrEmpty(ybxx.hcdm) && !string.IsNullOrEmpty(ybxx.djcpmc))
        //            {
        //                sfxmEntity.gjybdm = ybxx.hcdm;
        //                sfxmEntity.gjybmc = ybxx.djcpmc;
        //            }
        //            if (!string.IsNullOrEmpty(ybxx.ybxz))
        //            {
        //                sfxmEntity.ybbz = "1";
        //                sfxmEntity.zfxz = EnumZFXZv2.J.GetDescription().Contains(ybxx.ybxz) ? "4" : EnumZFXZv2.Y.GetDescription().Contains(ybxx.ybxz) ? "5" : EnumZFXZv2.B.GetDescription().Contains(ybxx.ybxz) ? "6" : "1";
        //            }
        //            if (!string.IsNullOrEmpty(ybxx.zcbah))
        //            {
        //                sfxmEntity.pzwh = ybxx.zcbah;
        //            }


        //            db.Update(sfxmEntity);

        //            updatecount = db.Commit();
        //        }

        //    }

        //    return updatecount;
        //}


        /// <summary>
        /// 查询系统项目信息
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysChargeItemVO> GetxmxxList(string organizeId, Pagination pagination, string type, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return null;
            }
            var sql = @" select * from dbo.xt_sfxm where sfdlcode!='126' and zt='1'  and organizeid=@organizeId  ";
            DbParameter[] par;
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " and sfxmmc like @keyword ";
                if (type != "qb" && type == "yd")
                {
                    sql += "  and gjybdm is not null";
                }
                else if (type != "qb" && type == "wd")
                {
                    sql += " and gjybdm is  null";
                }
                par = new DbParameter[]
                {
                    new SqlParameter("@organizeId", organizeId),
                    new SqlParameter("@keyword",  "%" + (keyword??"") + "%")
                };
            }
            else
            {
                if (type != "qb" && type == "yd")
                {
                    sql += "  and gjybdm is not null";
                }
                else if (type != "qb" && type == "wd")
                {
                    sql += " and gjybdm is  null";
                }
                par = new DbParameter[]
                {
                    new SqlParameter("@organizeId", organizeId)
                };
            }

            return this.QueryWithPage<SysChargeItemVO>(sql, pagination, par);
        }
        /// <summary>
        /// 查询医保项目信息 根据系统项目信息
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        public IList<G_yb_xmxxVO> GetYbxmxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh)
        {
            string sql = @"select top 200 * from(
select
结算传输编码 jscsbm
,国家项目代码 gjxmdm
,国家项目名称 gjxmmc
,编码 bm
,项目名称 xmmc
,项目内涵 xmnh
,除外内容 cwnr
,计价单位 jjdw
,说明 sm
,ybxz
,pym
from gb_sfxm)a where 1=1 ";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(gjybdm))
            {
                sql += " and(jscsbm like '%'+@gjybdm+'%' or gjxmdm like '%'+@gjybdm+'%' or bm like '%'+@gjybdm+'%' or gjxmmc like '%'+@gjybdm+'%' or xmmc like '%'+@gjybdm+'%' or pym like '%'+@gjybdm+'%')";
                pars.Add(new SqlParameter("@gjybdm", gjybdm));
            }
            return this.FindList<G_yb_xmxxVO>(sql, pars.ToArray());
        }

        /// <summary>
        /// 保存项目对码
        /// </summary>
        /// <param name="ybxx"></param>
        /// <param name="ypid"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        //public int SaveYpxm(G_yb_xmxxVO ybxx, int? ypid, string organizeId)
        //{
        //    int updatecount = 0;
        //    if (ypid.HasValue && ypid.Value > 0)
        //    {
        //        using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
        //        {
        //            var sfxmEntity = db.IQueryable<SysChargeItemEntity>().FirstOrDefault(p => p.sfxmId == ypid && p.OrganizeId == organizeId);

        //            if (!string.IsNullOrEmpty(ybxx.jscsbm) && !string.IsNullOrEmpty(ybxx.gjxmmc))
        //            {
        //                sfxmEntity.gjybdm = ybxx.jscsbm;
        //                sfxmEntity.gjybmc = ybxx.gjxmmc;
        //            }
        //            if (!string.IsNullOrEmpty(ybxx.ybxz))
        //            {
        //                sfxmEntity.ybbz = "1";
        //                sfxmEntity.zfxz = EnumZFXZv2.J.GetDescription().Contains(ybxx.ybxz) ? "4" : EnumZFXZv2.Y.GetDescription().Contains(ybxx.ybxz) ? "5" : EnumZFXZv2.B.GetDescription().Contains(ybxx.ybxz) ? "6" : "1";
        //            }

        //            db.Update(sfxmEntity);

        //            updatecount = db.Commit();
        //        }

        //    }

        //    return updatecount;
        //}

        /// <summary>
        /// 获取系统人员信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysMedicineVO GetFormJson(int keyValue)
        {
            const string sql = @"
SELECT TOP 1000 x.ypId, x.zxdw,x.ypCode,x.ypmc,x.spm,x.py,x.bzdw,x.mzcls,x.mzcldw,x.zycls,x.zycldw,x.cxjje,
x.djdw,Convert(decimal(18,4),x.lsj) lsj,Convert(decimal(18,4),x.pfj) pfj,x.zfbl,x.zfxz,x.dlCode,x.ycmc,x.zt,0 bgbz,x.ypbzdm,x.nbdl,x.mzzybz,x.CreateTime,d.pzwh,
d.yptssx,d.ypflCode,d.zlff,d.jzlx,d.mrbzq,d.sjap,d.zbbz,d.yl,d.yldw,d.zjtzsj,d.ghdw,d.ypcd,d.ypgg,d.ybdm,d.xnhybdm,
x.jx,x.jl,x.jldw,x.OrganizeId,x.bzs,x.CreatorCode, c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc ,d.mrjl,d.mrpc,
x.isKss,x.kssId,x.jybz,d.ybbz,x.bz,d.gjybdm,x.tsypbz,d.gjybmc,d.dcxl,d.mbxl,d.mryf,g.yfmc mryfmc,d.ybgg ybgg
FROM [NewtouchHIS_Base].dbo.xt_yp(NOLOCK) x  
LEFT JOIN [NewtouchHIS_Base].dbo.xt_ypsx(NOLOCK) d on x.ypId=d.ypId 
LEFT JOIN [NewtouchHIS_Base].dbo.xt_sfdl(NOLOCK) c ON x.dlCode = c.dlCode and c.OrganizeId = x.OrganizeId
LEFT JOIN [NewtouchHIS_Base].dbo.xt_ypjx(NOLOCK) e ON x.jx = e.jxCode
LEFT JOIN [NewtouchHIS_Base].dbo.xt_ypfl(NOLOCK) f ON d.ypflCode = f.ypflCode
LEFT JOIN [NewtouchHIS_Base].dbo.xt_ypyf(NOLOCK) g ON g.yfCode=d.mryf
WHERE x.ypId =@ypId
";
            DbParameter[] par = {
                new SqlParameter("@ypId", keyValue)
            };
            return FirstOrDefault<SysMedicineVO>(sql, par);
        }

        /// <summary>
        /// 获取系统人员信息
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public SysMedicineVO SelectMedicineInfo(string ypCode, string organizeId)
        {
            const string sql = @"
SELECT TOP 1000 x.ypId, x.zxdw,x.ypCode,x.ypmc,x.spm,x.py,x.bzdw,x.mzcls,x.mzcldw,x.zycls,x.zycldw,
x.djdw,x.lsj,x.pfj,x.zfbl,x.zfxz,x.dlCode,x.ycmc,x.zt,0 bgbz,x.ypbzdm,x.nbdl,x.mzzybz,x.CreateTime,d.pzwh,
d.yptssx,d.ypflCode,d.zlff,d.jzlx,d.mrbzq,d.sjap,d.zbbz,d.yl,d.yldw,d.zjtzsj,d.ghdw,d.ypcd,d.ypgg,d.ybdm,d.xnhybdm,
x.jx,x.jl,x.jldw,x.OrganizeId,x.bzs,x.CreatorCode, c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc ,d.mrjl,d.mrpc,
x.isKss,x.kssId,x.jybz,d.ybbz 
FROM xt_yp(NOLOCK) x  
LEFT JOIN xt_ypsx(NOLOCK) d on x.ypId=d.ypId 
LEFT JOIN xt_sfdl(NOLOCK) c ON x.dlCode = c.dlCode and c.OrganizeId = x.OrganizeId AND c.zt='1'
LEFT JOIN xt_ypjx(NOLOCK) e ON x.jx = e.jxCode AND e.zt='1' 
LEFT JOIN xt_ypfl(NOLOCK) f ON d.ypflCode = f.ypflCode AND f.zt='1' 
WHERE x.ypCode =@ypCode
AND x.OrganizeId=@OrganizeId
";
            DbParameter[] par = {
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FirstOrDefault<SysMedicineVO>(sql, par);
        }

        /// <summary>
        /// 新增药品信息以及药属性信息表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ypId"></param>
        public void SubmitMedicine(SysMedicineVO model, int? ypId)
        {
            if (ypId.HasValue && ypId.Value > 0)
            {
                try
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {
                        var ypSql = "select * from [NewtouchHIS_Base].dbo.xt_yp where zt=1 and ypId = @ypId";
                        var parList = new List<SqlParameter>
                        {
                            new SqlParameter("@ypId", ypId),
                        };
                        var ypEntity = FirstOrDefault<SysMedicineBaseVO>(ypSql, parList.ToArray());

                        var ypsxSql = "select * from [NewtouchHIS_Base].dbo.xt_ypsx where zt=1 and ypId = @ypId";
                        var parList2 = new List<SqlParameter>
                        {
                            new SqlParameter("@ypId", ypId),
                        };
                        var ypsxEntity = FirstOrDefault<SysMedicinePropertyBaseVO>(ypsxSql, parList.ToArray());

                        model.ypId = ypId.Value;
                        ypEntity.pfj = model.pfj;
                        model.OrganizeId = ypEntity.OrganizeId;//组织 机构 不能变
                        UpdateEnityProperties(ypEntity, ypsxEntity, model);
                        var strSql = new StringBuilder();
                        strSql.Append(@"  UPDATE [NewtouchHIS_Base].dbo.xt_yp
SET ypCode=@ypCode,ypmc=@ypmc,OrganizeId=@OrganizeId,ypqz=@ypqz,yphz=@yphz,spm=@spm,py=@py,cfl=@cfl,cfdw=@cfdw,jl=@jl,jldw=@jldw,bzs=@bzs,bzdw=@bzdw,mzcls=@mzcls,mzcldw=@mzcldw,zycls=@zycls,zycldw=@zycldw,zxdw=@zxdw,djdw=@djdw,lsj=@lsj,pfj=@pfj,zfbl=@zfbl,zfxz=@zfxz,dlCode=@dlCode,jx=@jx,ycmc=@ycmc,medid=@medid,medextid=@medextid,ypbzdm=@ypbzdm,nbdl=@nbdl,mzzybz=@mzzybz,CreatorCode=@CreatorCode,CreateTime=@CreateTime,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode,zt=@zt,px=@px,lsbz=@lsbz,mjzbz=@mjzbz,yfCode=@yfCode,isKss=@isKss,kssId=@kssId,jybz=@jybz,bz=@bz,cxjje=@cxjje,tsypbz=@tsypbz
where ypId=@ypId");
                        var paraList = new DbParameter[]
                        {
                       new SqlParameter("@ypId", ypId),
                       new SqlParameter("@ypCode", ypEntity.ypCode),
                       new SqlParameter("@ypmc", ypEntity.ypmc),
                       new SqlParameter("@OrganizeId", ypEntity.OrganizeId),
                       new SqlParameter("@ypqz", ypEntity.ypqz),
                       new SqlParameter("@yphz", ypEntity.yphz),
                       new SqlParameter("@spm", ypEntity.spm),
                       new SqlParameter("@py", ypEntity.py),
                       new SqlParameter("@cfl", ypEntity.cfl),
                       new SqlParameter("@cfdw", ypEntity.cfdw),
                       new SqlParameter("@jl", ypEntity.jl),
                       new SqlParameter("@jldw", ypEntity.jldw),
                       new SqlParameter("@bzs", ypEntity.bzs),
                       new SqlParameter("@bzdw", ypEntity.bzdw),
                       new SqlParameter("@mzcls", ypEntity.mzcls),
                       new SqlParameter("@mzcldw", ypEntity.mzcldw),
                       new SqlParameter("@zycls", ypEntity.zycls),
                       new SqlParameter("@zycldw", ypEntity.zycldw),
                       new SqlParameter("@zxdw", ypEntity.zxdw),
                       new SqlParameter("@djdw", ypEntity.djdw),
                       new SqlParameter("@lsj", ypEntity.lsj),
                       new SqlParameter("@pfj", ypEntity.pfj),
                       new SqlParameter("@zfbl", ypEntity.zfbl),
                       new SqlParameter("@zfxz", ypEntity.zfxz),
                       new SqlParameter("@dlCode", ypEntity.dlCode),
                       new SqlParameter("@jx", ypEntity.jx),
                       new SqlParameter("@ycmc", ypEntity.ycmc),
                       new SqlParameter("@medid", ypEntity.medid),
                       new SqlParameter("@medextid", ypEntity.medextid),
                       new SqlParameter("@ypbzdm", ypEntity.ypbzdm),
                       new SqlParameter("@nbdl", ypEntity.nbdl),
                       new SqlParameter("@mzzybz", ypEntity.mzzybz),
                       new SqlParameter("@CreatorCode",null),
                       new SqlParameter("@CreateTime", null),
                       new SqlParameter("@LastModifyTime", DateTime.Now),
                       new SqlParameter("@LastModifierCode",OperatorProvider.GetCurrent().UserCode),
                       new SqlParameter("@zt", ypEntity.zt),
                       new SqlParameter("@px", ypEntity.px),
                       new SqlParameter("@lsbz", ypEntity.lsbz),
                       new SqlParameter("@mjzbz", ypEntity.mjzbz),
                       new SqlParameter("@yfCode", ypEntity.yfCode),
                       new SqlParameter("@isKss", ypEntity.isKss),
                       new SqlParameter("@kssId", ypEntity.kssId),
                       new SqlParameter("@jybz", ypEntity.jybz),
                       new SqlParameter("@bz", ypEntity.bz),
                       new SqlParameter("@cxjje", ypEntity.cxjje),
                       new SqlParameter("@tsypbz", ypEntity.tsypbz),
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);

                        //更新药品属性
                        var strSql2 = new StringBuilder();
                        strSql2.Append(@"  UPDATE [NewtouchHIS_Base].dbo.xt_ypsx
SET ypId=@ypId,OrganizeId=@OrganizeId,ypCode=@ypCode,shbz=@shbz,tsbz=@tsbz,jsbz=@jsbz,gzy=@gzy,mzy=@mzy,yljsy=@yljsy,zbbz=@zbbz,zlff=@zlff,sjap=@sjap,yl=@yl,yldw=@yldw,ypgg=@ypgg,ybdm=@ybdm,syts=@syts,dczdjl=@dczdjl,dczdsl=@dczdsl,ljzdjl=@ljzdjl,ljzdsl=@ljzdsl,pzwh=@pzwh,yptssx=@yptssx,ypflCode=@ypflCode,jzlx=@jzlx,mrbzq=@mrbzq,zjtzsj=@zjtzsj,xglx=@xglx,ghdw=@ghdw,ypcd=@ypcd,CreatorCode=@CreatorCode,CreateTime=@CreateTime,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode,zt=@zt,px=@px,xzyy=@xzyy,xzyysm=@xzyysm,LastYBUploadTime=@LastYBUploadTime,mrjl=@mrjl,mrpc=@mrpc,ybbz=@ybbz,xnhybdm=@xnhybdm,gjybdm=@gjybdm,ybmlscrq=@ybmlscrq,gjybmc=@gjybmc,xjbs=@xjbs,dcxl=@dcxl,mbxl=@mbxl,mryf=@mryf,ybgg=@ybgg
where ypId=@ypId");
                        var paraList2 = new DbParameter[]
                        {
                        new SqlParameter("@ypId", ypsxEntity.ypId),
                        new SqlParameter("@OrganizeId", ypsxEntity.OrganizeId),
                        new SqlParameter("@ypCode", ypsxEntity.ypCode),
                        new SqlParameter("@shbz", ypsxEntity.shbz),
                        new SqlParameter("@tsbz", ypsxEntity.tsbz),
                        new SqlParameter("@jsbz", ypsxEntity.jsbz),
                        new SqlParameter("@gzy", ypsxEntity.gzy),
                        new SqlParameter("@mzy", ypsxEntity.mzy),
                        new SqlParameter("@yljsy", ypsxEntity.yljsy),
                        new SqlParameter("@zbbz", ypsxEntity.zbbz),
                        new SqlParameter("@zlff", ypsxEntity.zlff),
                        new SqlParameter("@sjap", ypsxEntity.sjap),
                        new SqlParameter("@yl", ypsxEntity.yl),
                        new SqlParameter("@yldw", ypsxEntity.yldw),
                        new SqlParameter("@ypgg", ypsxEntity.ypgg),
                        new SqlParameter("@ybdm", ypsxEntity.ybdm),
                        new SqlParameter("@syts", ypsxEntity.syts),
                        new SqlParameter("@dczdjl", ypsxEntity.dczdjl),
                        new SqlParameter("@dczdsl", ypsxEntity.dczdsl),
                        new SqlParameter("@ljzdjl", ypsxEntity.ljzdjl),
                        new SqlParameter("@ljzdsl", ypsxEntity.ljzdsl),
                        new SqlParameter("@pzwh", ypsxEntity.pzwh),
                        new SqlParameter("@yptssx", ypsxEntity.yptssx),
                        new SqlParameter("@ypflCode", ypsxEntity.ypflCode),
                        new SqlParameter("@jzlx", ypsxEntity.jzlx),
                        new SqlParameter("@mrbzq", ypsxEntity.mrbzq),
                        new SqlParameter("@zjtzsj", ypsxEntity.zjtzsj),
                        new SqlParameter("@xglx", ypsxEntity.xglx),
                        new SqlParameter("@ghdw", ypsxEntity.ghdw),
                        new SqlParameter("@ypcd", ypsxEntity.ypcd),
                        new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                        new SqlParameter("@CreateTime", DateTime.Now),
                        new SqlParameter("@LastModifyTime", null),
                        new SqlParameter("@LastModifierCode", null),
                        new SqlParameter("@zt", ypsxEntity.zt),
                        new SqlParameter("@px", ypsxEntity.px),
                        new SqlParameter("@xzyy", null),
                        new SqlParameter("@xzyysm", null),
                        new SqlParameter("@LastYBUploadTime", ypsxEntity.LastYBUploadTime),
                        new SqlParameter("@mrjl", ypsxEntity.mrjl),
                        new SqlParameter("@mrpc", ypsxEntity.mrpc),
                        new SqlParameter("@ybbz", ypsxEntity.ybbz),
                        new SqlParameter("@xnhybdm", ypsxEntity.xnhybdm),
                        new SqlParameter("@gjybdm", ypsxEntity.gjybdm),
                        new SqlParameter("@ybmlscrq", null),
                        new SqlParameter("@gjybmc", ypsxEntity.gjybmc),
                        new SqlParameter("@xjbs", null),
                        new SqlParameter("@dcxl", ypsxEntity.dcxl),
                        new SqlParameter("@mbxl", ypsxEntity.mbxl),
                        new SqlParameter("@mryf", ypsxEntity.mryf),
                        new SqlParameter("@ybgg", ypsxEntity.ybgg),
                        };
                        db.ExecuteSqlCommand(strSql2.ToString(), paraList2);
                        db.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException(ex.Message);
                }
            }
            else
            {
                model.ypId = 0;
                //新增
                var ypEntity = new SysMedicineBaseVO();
                var ypsxEntity = new SysMedicinePropertyBaseVO();

                //插入药品
                UpdateEnityProperties(ypEntity, ypsxEntity, model);
                //ypEntity.Create();
                //_sysMedicineRepository.Insert(ypEntity);
                //插入药品属性
                ypsxEntity.ypId = ypEntity.ypId;
                //ypsxEntity.Create();
                //_sysMedicinePropertyRepo.Insert(ypsxEntity);
                try
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {
                        //插入药品
                        var strSql = new StringBuilder();
                        strSql.Append(@"insert into [NewtouchHIS_Base].dbo.xt_yp
                                  (ypCode,ypmc,OrganizeId,ypqz,yphz,spm,py,cfl,cfdw,jl,jldw,bzs,bzdw,mzcls,mzcldw,zycls,zycldw,zxdw,djdw,lsj,pfj,zfbl,zfxz,dlCode,jx,ycmc,medid,medextid,ypbzdm,nbdl,mzzybz,CreatorCode,CreateTime,LastModifyTime,LastModifierCode,zt,px,lsbz,mjzbz,yfCode,isKss,kssId,jybz,bz,cxjje,tsypbz)
                                  values (@ypCode,@ypmc,@OrganizeId,@ypqz,@yphz,@spm,@py,@cfl,@cfdw,@jl,@jldw,@bzs,@bzdw,@mzcls,@mzcldw,@zycls,@zycldw,@zxdw,@djdw,@lsj,@pfj,@zfbl,@zfxz,@dlCode,@jx,@ycmc,@medid,@medextid,@ypbzdm,@nbdl,@mzzybz,@CreatorCode,@CreateTime,@LastModifyTime,@LastModifierCode,@zt,@px,@lsbz,@mjzbz,@yfCode,@isKss,@kssId,@jybz,@bz,@cxjje,@tsypbz);");
                        var paraList = new DbParameter[]
                        {
                       new SqlParameter("@ypCode", ypEntity.ypCode),
                       new SqlParameter("@ypmc", ypEntity.ypmc),
                       new SqlParameter("@OrganizeId", ypEntity.OrganizeId),
                       new SqlParameter("@ypqz", ypEntity.ypqz),
                       new SqlParameter("@yphz", ypEntity.yphz),
                       new SqlParameter("@spm", ypEntity.spm),
                       new SqlParameter("@py", ypEntity.py),
                       new SqlParameter("@cfl", ypEntity.cfl),
                       new SqlParameter("@cfdw", ypEntity.cfdw),
                       new SqlParameter("@jl", ypEntity.jl),
                       new SqlParameter("@jldw", ypEntity.jldw),
                       new SqlParameter("@bzs", ypEntity.bzs),
                       new SqlParameter("@bzdw", ypEntity.bzdw),
                       new SqlParameter("@mzcls", ypEntity.mzcls),
                       new SqlParameter("@mzcldw", ypEntity.mzcldw),
                       new SqlParameter("@zycls", ypEntity.zycls),
                       new SqlParameter("@zycldw", ypEntity.zycldw),
                       new SqlParameter("@zxdw", ypEntity.zxdw),
                       new SqlParameter("@djdw", ypEntity.djdw),
                       new SqlParameter("@lsj", ypEntity.lsj),
                       new SqlParameter("@pfj", ypEntity.pfj),
                       new SqlParameter("@zfbl", ypEntity.zfbl),
                       new SqlParameter("@zfxz", ypEntity.zfxz),
                       new SqlParameter("@dlCode", ypEntity.dlCode),
                       new SqlParameter("@jx", ypEntity.jx),
                       new SqlParameter("@ycmc", ypEntity.ycmc),
                       new SqlParameter("@medid", ypEntity.medid),
                       new SqlParameter("@medextid", ypEntity.medextid),
                       new SqlParameter("@ypbzdm", ypEntity.ypbzdm),
                       new SqlParameter("@nbdl", ypEntity.nbdl),
                       new SqlParameter("@mzzybz", ypEntity.mzzybz),
                       new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                       new SqlParameter("@CreateTime", DateTime.Now),
                       new SqlParameter("@LastModifyTime", null),
                       new SqlParameter("@LastModifierCode",null),
                       new SqlParameter("@zt", ypEntity.zt),
                       new SqlParameter("@px", ypEntity.px),
                       new SqlParameter("@lsbz", ypEntity.lsbz),
                       new SqlParameter("@mjzbz", ypEntity.mjzbz),
                       new SqlParameter("@yfCode", ypEntity.yfCode),
                       new SqlParameter("@isKss", ypEntity.isKss),
                       new SqlParameter("@kssId", ypEntity.kssId),
                       new SqlParameter("@jybz", ypEntity.jybz),
                       new SqlParameter("@bz", ypEntity.bz),
                       new SqlParameter("@cxjje", ypEntity.cxjje),
                       new SqlParameter("@tsypbz", ypEntity.tsypbz),
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);

                        //插入药品属性
                        var strSql2 = new StringBuilder();
                        strSql2.Append(@"insert into [NewtouchHIS_Base].dbo.xt_ypsx
                                  (ypId,OrganizeId,ypCode,shbz,tsbz,jsbz,gzy,mzy,yljsy,zbbz,zlff,sjap,yl,yldw,ypgg,ybdm,syts,dczdjl,dczdsl,ljzdjl,ljzdsl,pzwh,yptssx,ypflCode,jzlx,mrbzq,zjtzsj,xglx,ghdw,ypcd,CreatorCode,CreateTime,LastModifyTime,LastModifierCode,zt,px,xzyy,xzyysm,LastYBUploadTime,mrjl,mrpc,ybbz,xnhybdm,gjybdm,ybmlscrq,gjybmc,xjbs,dcxl,mbxl,mryf,ybgg)
                                  values (@ypId,@OrganizeId,@ypCode,@shbz,@tsbz,@jsbz,@gzy,@mzy,@yljsy,@zbbz,@zlff,@sjap,@yl,@yldw,@ypgg,@ybdm,@syts,@dczdjl,@dczdsl,@ljzdjl,@ljzdsl,@pzwh,@yptssx,@ypflCode,@jzlx,@mrbzq,@zjtzsj,@xglx,@ghdw,@ypcd,@CreatorCode,@CreateTime,@LastModifyTime,@LastModifierCode,@zt,@px,@xzyy,@xzyysm,@LastYBUploadTime,@mrjl,@mrpc,@ybbz,@xnhybdm,@gjybdm,@ybmlscrq,@gjybmc,@xjbs,@dcxl,@mbxl,@mryf,@ybgg);");
                        var paraList2 = new DbParameter[]
                        {
                        new SqlParameter("@ypId", ypsxEntity.ypId),
                        new SqlParameter("@OrganizeId", ypsxEntity.OrganizeId),
                        new SqlParameter("@ypCode", ypsxEntity.ypCode),
                        new SqlParameter("@shbz", ypsxEntity.shbz),
                        new SqlParameter("@tsbz", ypsxEntity.tsbz),
                        new SqlParameter("@jsbz", ypsxEntity.jsbz),
                        new SqlParameter("@gzy", ypsxEntity.gzy),
                        new SqlParameter("@mzy", ypsxEntity.mzy),
                        new SqlParameter("@yljsy", ypsxEntity.yljsy),
                        new SqlParameter("@zbbz", ypsxEntity.zbbz),
                        new SqlParameter("@zlff", ypsxEntity.zlff),
                        new SqlParameter("@sjap", ypsxEntity.sjap),
                        new SqlParameter("@yl", ypsxEntity.yl),
                        new SqlParameter("@yldw", ypsxEntity.yldw),
                        new SqlParameter("@ypgg", ypsxEntity.ypgg),
                        new SqlParameter("@ybdm", ypsxEntity.ybdm),
                        new SqlParameter("@syts", ypsxEntity.syts),
                        new SqlParameter("@dczdjl", ypsxEntity.dczdjl),
                        new SqlParameter("@dczdsl", ypsxEntity.dczdsl),
                        new SqlParameter("@ljzdjl", ypsxEntity.ljzdjl),
                        new SqlParameter("@ljzdsl", ypsxEntity.ljzdsl),
                        new SqlParameter("@pzwh", ypsxEntity.pzwh),
                        new SqlParameter("@yptssx", ypsxEntity.yptssx),
                        new SqlParameter("@ypflCode", ypsxEntity.ypflCode),
                        new SqlParameter("@jzlx", ypsxEntity.jzlx),
                        new SqlParameter("@mrbzq", ypsxEntity.mrbzq),
                        new SqlParameter("@zjtzsj", ypsxEntity.zjtzsj),
                        new SqlParameter("@xglx", ypsxEntity.xglx),
                        new SqlParameter("@ghdw", ypsxEntity.ghdw),
                        new SqlParameter("@ypcd", ypsxEntity.ypcd),
                        new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                        new SqlParameter("@CreateTime", DateTime.Now),
                        new SqlParameter("@LastModifyTime", null),
                        new SqlParameter("@LastModifierCode", null),
                        new SqlParameter("@zt", ypsxEntity.zt),
                        new SqlParameter("@px", ypsxEntity.px),
                        new SqlParameter("@xzyy", null),
                        new SqlParameter("@xzyysm", null),
                        new SqlParameter("@LastYBUploadTime", ypsxEntity.LastYBUploadTime),
                        new SqlParameter("@mrjl", ypsxEntity.mrjl),
                        new SqlParameter("@mrpc", ypsxEntity.mrpc),
                        new SqlParameter("@ybbz", ypsxEntity.ybbz),
                        new SqlParameter("@xnhybdm", ypsxEntity.xnhybdm),
                        new SqlParameter("@gjybdm", ypsxEntity.gjybdm),
                        new SqlParameter("@ybmlscrq", null),
                        new SqlParameter("@gjybmc", ypsxEntity.gjybmc),
                        new SqlParameter("@xjbs", null),
                        new SqlParameter("@dcxl", ypsxEntity.dcxl),
                        new SqlParameter("@mbxl", ypsxEntity.mbxl),
                        new SqlParameter("@mryf", ypsxEntity.mryf),
                        new SqlParameter("@ybgg", ypsxEntity.ybgg),
                        };
                        db.ExecuteSqlCommand(strSql2.ToString(), paraList2);
                        db.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 医保同步修改最后上传时间
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        //public bool YibaoUpload(int ypId, out string error)
        //{
        //    try
        //    {
        //        using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
        //        {
        //            var ypsxEntity = db.IQueryable<SysMedicinePropertyEntity>().Where(p => p.ypId == ypId).FirstOrDefault();
        //            if (ypsxEntity == null)
        //            {
        //                error = "HIS中查无此药";
        //                return false;
        //            }
        //            ypsxEntity.LastYBUploadTime = System.DateTime.Now;
        //            ypsxEntity.Modify();
        //            db.Update(ypsxEntity);
        //            db.Commit();
        //        }
        //        error = "";
        //        return true;
        //    }
        //    catch
        //    {
        //        error = "HIS更新药品同步医保时间失败";
        //        return false;
        //    }
        //}

        #region private methods

        /// <summary>
        /// 拼装值
        /// </summary>
        /// <param name="ypEntity"></param>
        /// <param name="ypsxEntity"></param>
        /// <param name="model"></param>
        public void UpdateEnityProperties(SysMedicineBaseVO modelyp, SysMedicinePropertyBaseVO modelypsx, SysMedicineVO model)
        {
            #region 药品信息表值
            modelyp.bzdw = model.bzdw;
            modelyp.bzs = model.bzs;
            modelyp.cfdw = "";//默认
            modelyp.cfl = decimal.Parse("0.0000");
            //modelyp.cldw = model.cldw;
            //modelyp.cls = model.cls;
            modelyp.djdw = model.djdw;
            modelyp.dlCode = model.dlCode;
            modelyp.jl = model.jl;
            modelyp.jldw = model.jldw; ;
            modelyp.jx = model.jx;//( or liek ) and bgbz='0' and zt='1' 根据代码/姓名/首拼获取剂型信息获得
            modelyp.lsj = model.lsj;
            modelyp.medextid = 0;//默认
            modelyp.medid = 0;//默认
            modelyp.mzcldw = model.mzcldw;
            modelyp.mzcls = model.mzcls;
            modelyp.mzzybz = "1";//门诊住院标志默认1
            modelyp.nbdl = model.nbdl;
            modelyp.pfj = model.pfj;
            modelyp.px = model.px;
            modelyp.py = model.py;
            modelyp.spm = model.spm;
            modelyp.OrganizeId = model.OrganizeId;    //组织机构Id
            modelyp.ycmc = model.ycmc;
            modelyp.ypbzdm = model.ypbzdm;//药品包装代码默认3
            modelyp.bz = model.bz;
            modelyp.cxjje = model.cxjje;//超限价金额
            modelyp.ypId = model.ypId;
            modelyp.ypCode = model.ypCode;
            modelyp.ypmc = model.ypmc;
            modelyp.yphz = "";//默认
            modelyp.ypqz = "";//默认
            modelyp.zfbl = model.zfbl;
            modelyp.zfxz = model.zfxz;
            modelyp.zt = model.zt;
            modelyp.zycldw = model.zycldw;
            modelyp.zycls = model.zycls;
            modelyp.zxdw = model.zxdw;
            modelyp.isKss = model.isKss;
            modelyp.kssId = model.kssId;
            modelyp.jybz = model.jybz;
            modelyp.tsypbz = model.tsypbz;
            #endregion

            #region 药品SX值
            modelypsx.OrganizeId = model.OrganizeId;    //组织机构Id
            modelypsx.dczdjl = model.dczdjl;
            modelypsx.dczdsl = model.dczdsl;
            modelypsx.ghdw = model.ghdw;
            modelypsx.gzy = model.gzy;
            modelypsx.jsbz = model.jsbz;
            modelypsx.jzlx = model.jzlx;
            modelypsx.ljzdjl = model.ljzdjl;
            modelypsx.ljzdsl = model.ljzdsl;
            modelypsx.mrbzq = model.mrbzq;
            modelypsx.mzy = model.mzy;
            //modelypsx.px = model.px;
            modelypsx.pzwh = model.pzwh;
            modelypsx.shbz = model.shbz;
            modelypsx.sjap = model.sjap;
            modelypsx.syts = model.syts;
            modelypsx.tsbz = model.tsbz;
            modelypsx.xglx = model.xglx;
            modelypsx.ybdm = model.ybdm;
            modelypsx.xnhybdm = model.xnhybdm;
            modelypsx.yl = model.yl;
            modelypsx.yldw = model.yldw;
            modelypsx.yljsy = model.yljsy;
            modelypsx.ypcd = model.ypcd;
            modelypsx.ypCode = model.ypCode;
            modelypsx.ypflCode = model.ypflCode;
            modelypsx.ypgg = model.ypgg;
            modelypsx.ypId = model.ypId;
            modelypsx.yptssx = model.yptssx;
            modelypsx.zbbz = model.zbbz;
            modelypsx.zjtzsj = model.zjtzsj;
            modelypsx.zlff = model.zlff;
            modelypsx.mrjl = model.mrjl;
            modelypsx.mrpc = model.mrpc;
            modelypsx.ybbz = model.ybbz;
            modelypsx.gjybdm = model.gjybdm;
            modelypsx.gjybmc = model.gjybmc;
            modelypsx.dcxl = model.dcxl;
            modelypsx.mbxl = model.mbxl;
            modelypsx.mryf = model.mryf;
            modelypsx.ybgg = model.ybgg;
            //modelypsx.zt = model.zt;

            #endregion
        }

        #endregion

        /// <summary>
        /// 获取药品系数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetTradePricePlus(string name)
        {
            string sql = @"select top 1 code from [NewtouchHIS_Base].[dbo].[Sys_ItemsDetail] where name=@name ";
            DbParameter[] par = {
                new SqlParameter("@name", name)
            };
            return FirstOrDefault<string>(sql, par);
        }




        //药品用法
        public IList<SysMedicineUsageVO> GetUsageFloat(string keyword)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_ypyf(nolock) where zt = '1' and (yfCode like @searchKeyword or yfmc like @searchKeyword or py like @searchKeyword) order by py asc";
            return this.FindList<SysMedicineUsageVO>(sql, new SqlParameter[] {
                new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
            });
        }


        #region SysMedicineRepo

        /// <summary>
        /// 获取医保字典库药品List
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<GzybybItemCodeVO> GetYbMedicineList(string OrganizeId, string keyword = null)
        {
            //if (string.IsNullOrWhiteSpace(keyword))
            //{
            //    return null;
            //}
            try
            {
                //                var sql = @"select top 50 yka003 ypmc, yka002 ybdm, aka074 gg, yka601 ycmc
                //from NewtouchHIS_Base..Gzyb_ybItemCode
                //where 1=1
                //and (yka003 like @searchKeyword or yka003 like @searchKeyword or yka389 like @searchKeyword or yka601 like @searchKeyword)
                //order by yka389 asc";
                var sql = @"select top 200  cod.ypmc, cod.ypdm ybdm,  cod.gg,  cod.ycmc,cod.ypxz ybxz,case cod.ypxz when '" + ((int)EnumZFXZv2.J).ToString() +
                    "' then '甲类' when '" + ((int)EnumZFXZv2.Y).ToString() +
                    @"' then '乙类' else '丙类' end  ypxz, cod.py py, cod.ybdj ybdj, cod.gjybdm gjybdm,cod.ypjx jxmc,cod.pzwh pzwh
from NewtouchHIS_Base..Cqyb_ybItemCode cod
where 1=1 and cod.zt=1 and cod.OrganizeId=@orgId";
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sql += " and (cod.ypmc like @searchKeyword  or cod.ypdm like @searchKeyword or cod.py like @searchKeyword or cod.gjybdm like @searchKeyword or cod.pzwh like @searchKeyword or cod.ycmc like @searchKeyword)";

                }

                sql += " order by cod.py desc";
                return this.FindList<GzybybItemCodeVO>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%"),
                new SqlParameter("@orgId",OrganizeId)});
            }
            catch
            {
                //没有这张表
                return null;
            }
        }
        /// <summary>
        /// 查询医保姓名表信息
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<GzybNameCodeVO> GetYbName(string OrganizeId, string lx, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;
            }

            var sql = @"select top 50 id,xmmc,pym,ybxj,dfybdm,gjybdm,lx,gg,dw,dj,pzwh,sccj,dfxmmc from [NewtouchHIS_Base].dbo.Cqyb_ybxm where zt = '1' and  OrganizeId=@OrganizeId 
                and (id LIKE @searchKeyword or
                xmmc like @searchKeyword or
                pym like @searchKeyword  or
                dfybdm like @searchKeyword or
                gjybdm like @searchKeyword or
                pzwh like @searchKeyword or
                sccj like @searchKeyword) ";
            if (lx == "126" || lx == "00000015" || lx == "00000016")
            {
                sql += " and lx='材料'";
            }
            else
            {
                sql += " and lx='项目'";
            }
            return this.FindList<GzybNameCodeVO>(sql, new SqlParameter[] {

                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%"),
                new SqlParameter("@OrganizeId",OrganizeId)
                });
        }

        /// <summary>
        /// 获取医保字典库药品List
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<GzxnhybItemCodeVO> GetYbXNHMedicineList(string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return null;
            }
            try
            {
                var sql = @"SELECT  code ,
        name ,
        dosageForm ,
        CASE WHEN isBase = '0' THEN '否'
             WHEN isBase = '1' THEN '是'
             ELSE ''
        END isBase
FROM    [NewtouchHIS_Base].[dbo].[Gaxnh_S30]
WHERE   (name LIKE @searchKeyword or code like @searchKeyword or pcode like @searchKeyword ) AND zt='1' ";
                return this.FindList<GzxnhybItemCodeVO>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
            }
            catch
            {
                //没有这张表
                return null;
            }
        }
        #endregion

    }
}

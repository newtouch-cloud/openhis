using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class ComDiagnosisRepo : RepositoryBase<ComDiagnosisEntity>, IComDiagnosisRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public ComDiagnosisRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        //获取常用诊断信息--浮层
        public IList<ComDiagnosisVO> GetComDiagnosis(string ys,string ks, string orgId, string type,string keyword,string lx)
        {
            try
            {
                var sql = string.Format(@"select cyzdmc,cyzdbm,icd10,py,(case qxkz when '1' then '个人' else '科室' end) isgr
                from [Newtouch_CIS].dbo.Com_Diagnosis with(nolock) 
                where OrganizeId=@orgId and zt='1' 
                and cyzdtype=@type and ((qxkz='2' and ksCode=@ks) or (qxkz='1' and CreatorCode=@ys)) ");

                IList<SqlParameter> parlist = new List<SqlParameter>();
                parlist.Add(new SqlParameter("@orgId", orgId));
                parlist.Add(new SqlParameter("@ys", ys));
                parlist.Add(new SqlParameter("@ks", ks));
                parlist.Add(new SqlParameter("@type", type));

                if (!string.IsNullOrEmpty(lx) && lx != "0")
                {
                    sql += " and qxkz=@qxkz ";
                    parlist.Add(new SqlParameter("@qxkz", lx));
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += " and (cyzdmc like @keyword or cyzdbm like @keyword or py like @keyword)";
                    parlist.Add(new SqlParameter("@keyword", "%" + keyword + "%"));
                }
                sql += " order by qxkz";
                return this.FindList<ComDiagnosisVO>(sql.ToString(), parlist.ToArray());
            }
            catch (Exception ex)
            {
                throw new FailedException("错误信息："+ex.Message);
            }
        }
        //获取常用诊断信息--维护
        public IList<ComDiagnosisVO> GetComDiagnosisAdmin(string ys, string ks, string orgId, string type, string keyword, string lx)
        {
            try
            {
                var sql = string.Format(@"select cyzdmc,cyzdbm,icd10,py,(case qxkz when '1' then '个人' else '科室' end) isgr
                from [Newtouch_CIS].dbo.Com_Diagnosis with(nolock) 
                where OrganizeId=@orgId and zt='1' 
                and cyzdtype=@type and CreatorCode=@ys ");

                IList<SqlParameter> parlist = new List<SqlParameter>();
                parlist.Add(new SqlParameter("@orgId", orgId));
                parlist.Add(new SqlParameter("@ys", ys));
                parlist.Add(new SqlParameter("@ks", ks));
                parlist.Add(new SqlParameter("@type", type));

                if (!string.IsNullOrEmpty(lx) && lx != "0")
                {
                    sql += " and qxkz=@qxkz ";
                    parlist.Add(new SqlParameter("@qxkz", lx));
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += " and (cyzdmc like @keyword or cyzdbm like @keyword or py like @keyword)";
                    parlist.Add(new SqlParameter("@keyword", "%" + keyword + "%"));
                }
                sql += " order by qxkz";
                return this.FindList<ComDiagnosisVO>(sql.ToString(), parlist.ToArray());
            }
            catch (Exception ex)
            {
                throw new FailedException("错误信息：" + ex.Message);
            }
        }
        //获取诊断记录信息
        public List<ComDiagnosisVO> GetDiagsLists(string orgId, string type, string mzh)
        {
            try
            {
                var sql = "";
                IList<SqlParameter> parlist = new List<SqlParameter>();
                if (type == "1")
                {
                    sql = string.Format(@"select a.zdCode,a.zdmc,b.icd10,b.py from xt_jz jz with(nolock) 
left join xt_xyzd a with(nolock)  
on jz.jzId=a.jzId and a.zt='1' 
left join [NewtouchHIS_Base]..V_S_xt_zd b with(nolock)  
on a.zdCode=b.zdCode and b.zt='1' 
where jz.OrganizeId=@orgId and jz.zt='1' and jz.mzh=@mzh ");
                }
                else
                {
                    sql = string.Format(@"select a.zdCode,a.zdmc,b.icd10,b.py from xt_jz jz with(nolock) 
left join xt_zyzd a with(nolock)  
on jz.jzId=a.jzId and a.zt='1' 
left join [NewtouchHIS_Base]..V_S_xt_zd b with(nolock)  
on a.zdCode=b.zdCode and b.zt='1' 
where jz.OrganizeId=@orgId and jz.zt='1' and jz.mzh=@mzh ");
                }
                parlist.Add(new SqlParameter("@orgId", orgId));
                parlist.Add(new SqlParameter("@mzh", mzh));
                return this.FindList<ComDiagnosisVO>(sql.ToString(), parlist.ToArray());
            }
            catch (Exception ex)
            {

                throw new FailedException("错误信息：" + ex.Message);
            }
            
        }
        //获取诊断拼音
        public List<ComDiagnosisVO> Getzdpy( string zdmc, string zdbm)
        {
            try
            {
                var sql = "select zdmc,zdCode,py from [NewtouchHIS_Base]..V_S_xt_zd with(nolock) where  zdmc=@zdmc and  zdCode=@zdbm and zt='1' ";
                IList<SqlParameter> parlist = new List<SqlParameter>();
                parlist.Add(new SqlParameter("@zdmc", zdmc));
                parlist.Add(new SqlParameter("@zdbm", zdbm));
                return this.FindList<ComDiagnosisVO>(sql.ToString(), parlist.ToArray());
            }
            catch (Exception ex)
            {

                throw new FailedException("错误信息：" + ex.Message);
            }

        }
        //添加常用诊断
        public void SubmitDiag(ComDiagnosisEntity entity)
        {
            try
            {
                ComDiagnosisEntity zdbm = this.FindEntity(p => p.cyzdbm == entity.cyzdbm && p.cyzdtype == entity.cyzdtype && p.ys == entity.ys);
                if (zdbm != null)
                {
                    throw new FailedException("不能添加重复常用诊断");
                }
                else
                {
                    entity.Id = Guid.NewGuid().ToString();
                    entity.CreateTime = DateTime.Now;
                    entity.Create();
                    this.Insert(entity);
                }
            }
            catch (Exception ex)
            {

                throw new FailedException("错误信息：" + ex.Message);
            }



        }
        //删除常用诊断
        public void DelDiagnosticTemplate(string Id)
        {
            try
            {
                ComDiagnosisEntity zdbm = this.FindEntity(p => p.Id == Id);
                this.Delete(zdbm);
            }
            catch (Exception ex)
            {

                throw new FailedException("错误信息：" + ex.Message);
            }



        }
    }
}

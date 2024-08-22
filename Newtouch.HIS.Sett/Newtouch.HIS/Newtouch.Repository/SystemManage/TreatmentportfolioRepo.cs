using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.SystemManage
{
    public class TreatmentportfolioRepo : RepositoryBase<TreatmentportfolioEntity>, ITreatmentportfolioRepo
    {
        public TreatmentportfolioRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// string zhmc, string zhcode, string ord, string zlxm, string zlxmmc, string price, string zlxmpy, string zhje
        /// 
        /// </summary>
        /// <param name="zhmc"></param>
        /// <param name="zhcode"></param>
        /// <param name="ord"></param>
        /// <param name="zlxm"></param>
        /// <param name="zlxmmc"></param>
        /// <param name="price"></param>
        /// <param name="zlxmpy"></param>
        /// <param name="zhje"></param>
        //public void Insert(string OrganizeId, string zhmc, string zhcode, string ord, string zlxm, string zlxmmc, string price, string zlxmpy, string zhje)
        //{
        //    TreatmentportfolioEntity entity = new TreatmentportfolioEntity();
        //    if (!string.IsNullOrEmpty(OrganizeId))
        //    {
        //        entity.OrganizeId = OrganizeId;
        //        //entity.Id = collection["OrganizeId"];
        //        entity.zhmc = zhmc;
        //        entity.zhcode = zhcode;
        //        entity.ord = int.Parse(ord);
        //        entity.zlxm = zlxm;
        //        entity.zlxmmc = zlxmmc;
        //        entity.zt = "";
        //        entity.price = price;
        //        //entity.CreatorCode = "kradmin";
        //        //entity.CreateTime = Convert.ToDateTime(DateTime.Now.ToString());
        //        //entity.LastModifyTime = Convert.ToDateTime(DateTime.Now.ToString());
        //        //entity.LastModifierCode = "";
        //        entity.zlxmpy =zlxmpy;
        //        entity.sfdl = zhje;
        //        entity.Create(true);
        //        this.Insert(entity);
        //    }

        //    //throw new NotImplementedException();
        //}

        public void ADDInsert(TreatmentportfolioEntity entity, string keyValue)
        {
            //entity.Create(true);
            //this.Insert(entity);

            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }

        }



     

        public void ADDrowdata(TreatmentportfolioEntity entity)
        {
            entity.Create(true);
            this.Insert(entity);
            //throw new NotImplementedException();
        }

        public void deleteid(string keyValue)
        {
            this.Delete(p => p.zhcode == keyValue);
            
        }

        public void deletemc(string keyValue,string zhcodemc)
        {
            this.Delete(p => p.zlxmmc == keyValue && p.zhcode==zhcodemc);

        }

        public IList<TreatmentportfolioEntity> ListselectTJ(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<TreatmentportfolioEntity> selectid(TreatmentportfolioEntity entity, string keyValue)
        {
            string id = entity.Id;
            var sql = "select * from mz_gh_zlxmzh where Id='"+keyValue+"'";
            return this.FindList<TreatmentportfolioEntity>(sql);

            //throw new NotImplementedException();
        }

        public IList<TreatmentportfolioEntity> TJchaxun(string zhcodetj, string sfxmmc123)
        {
            var sql = "select * from mz_gh_zlxmzh where zhcode='"+zhcodetj+ "'and zlxmmc='"+sfxmmc123+"'";
            return this.FindList<TreatmentportfolioEntity>(sql);
        }

        public void Updatexg(TreatmentportfolioEntity entity, string keyValue)
        {
            entity.Modify(keyValue);
            this.Update(entity);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
//        IList<TreatmentportfolioEntity> ITreatmentportfolioRepo.Listselect()
//        {
////            var sql =string.Format(@"select a.*,  b.dlmc from [NewtouchHIS_Sett].[dbo].[mz_gh_zlxmzh] a,[NewtouchHIS_Base].[dbo].[xt_sfdl] b
////where a.sfdl = b.dlCode order by a.zhmc");
//            var sql = "select * from mz_gh_zlxmzh order by zhmc desc";
//                return this.FindList<TreatmentportfolioEntity>(sql);
//                //return this.SqlQueryForDataTatable(sql);
            
            
//        }

//        IList<TreatmentportfolioEntity> ITreatmentportfolioRepo.Keyword(string keyword)
//        {
//            var sql = "select * from mz_gh_zlxmzh where zhmc='" + keyword + "' or zhcode='" + keyword + "'";
//            return this.FindList<TreatmentportfolioEntity>(sql);
//        }

        public IList<TreatmentportfolioEntity> Codecx(TreatmentportfolioEntity entity, string zhcode)
        {
            var sql = "select * from mz_gh_zlxmzh where zhcode='" + zhcode + "'";
            return this.FindList<TreatmentportfolioEntity>(sql);
        }

        

//        public IList<TreamentviewVO> Loginview()
//        {
//            var sql = string.Format(@"select a.*,  b.dlmc from [NewtouchHIS_Sett].[dbo].[mz_gh_zlxmzh] a,[NewtouchHIS_Base].[dbo].[xt_sfdl] b
//where a.sfdl = b.dlCode order by a.zhmc");
//            return this.FindList<TreamentviewVO>(sql);
//        }

        public string sfdlcx(string sfxmmc123)
        {
            var sql = "select sfdlCode from [NewtouchHIS_Base].[dbo].[xt_sfxm] where sfxmmc='" + sfxmmc123 + "'";
            var a = FindList<string>(sql)[0].ToString();
            return a;
        }




        public IList<TreamentviewVO> Loginview(Pagination pagination)
        {
            pagination.sidx = "zhmccode";
            var sql = string.Format(@"select a.*,  b.dlmc,a.zhmc+'-'+a.zhcode as zhmccode from [NewtouchHIS_Sett].[dbo].[mz_gh_zlxmzh] a,[NewtouchHIS_Base].[dbo].[xt_sfdl] b
where a.sfdl = b.dlCode");
            string a = this.QueryWithPage<TreamentviewVO>(sql, pagination).ToString();
            return this.QueryWithPage<TreamentviewVO>(sql,pagination);
        }

        public IList<TreamentviewVO> Keyword(Pagination pagination, string keyword)
        {
            pagination.sidx = "zhmccode";
            var sql = string.Format(@"select a.*,  b.dlmc,a.zhmc+'-'+a.zhcode as zhmccode from [NewtouchHIS_Sett].[dbo].[mz_gh_zlxmzh] a,[NewtouchHIS_Base].[dbo].[xt_sfdl] b
where a.sfdl = b.dlCode and (a.zhmc='" + keyword + "' or a.zhcode='" + keyword+"')");
            return this.QueryWithPage<TreamentviewVO>(sql,pagination);
        }



    }
}

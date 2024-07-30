using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 门诊结算大类
    /// </summary>
    public class OutpatientSettlementCategoryRepo : RepositoryBase<OutpatientSettlementCategoryEntity>, IOutpatientSettlementCategoryRepo
    {
        public OutpatientSettlementCategoryRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 根据jsnm查门诊结算大类
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public List<OutpatientSettlementCategoryEntity> SelectMzjsdlByJsnm(int jsnm, string orgId)
        {
            return this.IQueryable().Where(a => a.jsnm == jsnm && a.OrganizeId == orgId).ToList();
        }

        /// <summary>
        /// 获取结算大类
        /// </summary>
        /// <param name="js"></param>
        /// <param name="ghxmList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatientSettlementCategoryEntity> GetJsdl(OutpatientSettlementEntity js, List<OutpatientItemEntity> ghxmList, string orgId)
        {
            var jsdlList = new List<OutpatientSettlementCategoryEntity>();
            #region old Code
            ////医保费用
            //foreach (OutpatientRegistItemEntity yb in ghxmList)
            //{
            //    OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
            //    if (jsdl == null)
            //    {
            //        jsdl = new OutpatientSettlementCategoryEntity();
            //        jsdlList.Add(jsdl);
            //    }

            //    jsdl.jsnm = js.jsnm;
            //    jsdl.dl = yb.dl;//大类
            //    jsdl.flzffy += yb.flzf;//分类自负费用
            //    jsdl.zlfy += yb.zl + yb.sfzl;//自理费用
            //    jsdl.jmfy += yb.jmje;//减免金额
            //    jsdl.jsrq = js.CreateTime;
            //    if (returnValue.ybjsfwze == 0)
            //    {
            //        jsdl.zffy += 0m;
            //        jsdl.kbfy += 0m;
            //    }
            //    else
            //    {
            //        jsdl.zffy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.zffy;//分类金额/分类金额合计*自负金额
            //        jsdl.kbfy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.jzfy;//分类金额/分类金额合计*自负金额
            //    }
            //    jsdl.zje += jsdl.flzffy + jsdl.zlfy + jsdl.jmfy + jsdl.zffy + jsdl.kbfy;
            //}
            //非医保费用
            #endregion
            foreach (var dl in ghxmList.Select(a => a.dl).Distinct())
            {
                var xmList = ghxmList.Where(t => t.dl == dl).ToList();
                var mzjsdl = new OutpatientSettlementCategoryEntity
                {
                    jsnm = js.jsnm,
                    dl = dl,
                    flzffy = 0,
                    zlfy = xmList.Sum(a => a.dj),
                    jmfy = xmList.Sum(a => a.jmje ?? 0),
                    jsrq = js.CreateTime,
                    zffy = 0,
                    kbfy = 0,
                    zje = xmList.Sum(a => a.dj),
                    OrganizeId = orgId
                };
                mzjsdl.Create(true);
                jsdlList.Add(mzjsdl);
            }

            return jsdlList;
        }

    }
}



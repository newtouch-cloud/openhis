using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Newtouch.HIS.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    public class SysPatientChargeAlgorithmRepo : RepositoryBase<SysPatientChargeAlgorithmEntity>, ISysPatientChargeAlgorithmRepo
    {

        public SysPatientChargeAlgorithmRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取所有有效算法
        /// </summary>
        /// <returns></returns>
        public List<SysPatientChargeAlgorithmEntity> getAllMzActive()
        {
            List<SysPatientChargeAlgorithmEntity> sysPatAlgoEntityList = null;
            sysPatAlgoEntityList = this.IQueryable().Where(p => p.zt == "1" && (p.mzzybz == "0" || p.mzzybz == "1")).OrderByDescending(p => p.sfjb).ToList();

            if (sysPatAlgoEntityList != null)
            {
                return sysPatAlgoEntityList;
            }
            else
            {
                return null;
            }
        }

        public void SubmitForm(SysPatientChargeAlgorithmEntity entity, string keyValue)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    entity.Modify(int.Parse(keyValue));
                    db.Update(entity);
                  

                }
                else
                {
                    entity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_brsfsf"));
                    db.Insert(entity);
                }
                db.Commit();
            }
        }

        public List<ChargeItemDetailVO> GetSFXMItemInfoByDlCode(string keyword, string dlCode,string orgId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"EXEC spSelectSFXMItemByDlcode @keyword=@keyword, @dlCode =@dlCode,@OrganizeId=@OrganizeId");
            SqlParameter[] par = {
                new SqlParameter("@keyword",keyword),
                new SqlParameter("@dlCode",dlCode),
                new SqlParameter("@OrganizeId",orgId)
            };

            return this.FindList<ChargeItemDetailVO>(strSql.ToString(), par);
        }
    }
}

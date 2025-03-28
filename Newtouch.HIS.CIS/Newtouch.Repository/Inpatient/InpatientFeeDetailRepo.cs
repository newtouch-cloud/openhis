using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 13:27
    /// 描 述：住院费用明细库
    /// </summary>
    public class InpatientFeeDetailRepo : RepositoryBase<InpatientFeeDetailEntity>, IInpatientFeeDetailRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public InpatientFeeDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(InpatientFeeDetailEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties

                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        /// <summary>
        /// 通过住院号,医嘱性质获取费用信息
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        public List<InpatientFeeDetailEntity> GetListByZyhYzxz(string OrganizeId, string zyh, string yzxz)
        {
            List<SqlParameter> pars = new List<SqlParameter>();
            string lsSQL = @" SELECT * FROM zy_fymxk WITH(NOLOCK) WHERE zyh=@zyh AND OrganizeId = @orgId AND zt ='1' ";
            pars.Add(new SqlParameter("@zyh", zyh));
            pars.Add(new SqlParameter("@orgId", OrganizeId));
            if (!string.IsNullOrWhiteSpace(yzxz))
            {
                lsSQL += " AND yzxz = @yzxz";
                pars.Add(new SqlParameter("@yzxz", yzxz));
            }
            return this.FindList<InpatientFeeDetailEntity>(lsSQL, pars.ToArray());
        }

    }
}
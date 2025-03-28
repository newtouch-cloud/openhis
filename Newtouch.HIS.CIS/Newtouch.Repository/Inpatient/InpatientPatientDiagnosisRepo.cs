using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;

namespace Newtouch.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 13:27
    /// 描 述：住院病人诊断信息
    /// </summary>
    public class InpatientPatientDiagnosisRepo : RepositoryBase<InpatientPatientDiagnosisEntity>, IInpatientPatientDiagnosisRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public InpatientPatientDiagnosisRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(InpatientPatientDiagnosisEntity entity, string keyValue)
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
        /// 获取诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1入院诊断2出院诊断</param>
        /// <param name="zt">1-有效  0-无效</param>
        /// <returns></returns>
        public List<InpatientPatientDiagnosisEntity> SelectData(string zyh, string organizeId, string zdlb, string zt = "1")
        {
            const string sql = @"
SELECT * FROM dbo.zy_PatDxInfo(NOLOCK) 
WHERE zyh=@zyh AND OrganizeId=@OrganizeId AND zt=@zt AND zdlb=@zdlb
";
            var param = new DbParameter[]
            {
                new SqlParameter("@zyh",zyh ),
                new SqlParameter("@OrganizeId",organizeId ),
                new SqlParameter("@zt",zt ),
                new SqlParameter("@zdlb", zdlb)
            };
            return FindList<InpatientPatientDiagnosisEntity>(sql, param);
        }

        public List<WMDiagnosisHtmlVO> SelectDiagData(string zyh, string organizeId, string zdlb, string zt = "1")
        {
            const string sql = @"
            SELECT a.zddm, a.zdmc, b.icd10
            FROM dbo.zy_PatDxInfo a
            left join NewtouchHIS_Base.dbo.xt_zd b
            on a.zddm = b.zdCode and (a.OrganizeId = b.OrganizeId or b.OrganizeId = '*')
            and a.zt = '1' and b.zt = '1' 
            WHERE a.zyh=@zyh AND a.OrganizeId=@OrganizeId AND a.zt=@zt AND a.zdlb=@zdlb and a.zdmc is not null
            ";
            var param = new DbParameter[]
            {
                new SqlParameter("@zyh",zyh ),
                new SqlParameter("@OrganizeId",organizeId ),
                new SqlParameter("@zt",zt ),
                new SqlParameter("@zdlb", zdlb)
            };
            return FindList<WMDiagnosisHtmlVO>(sql, param);
        }
    }
}
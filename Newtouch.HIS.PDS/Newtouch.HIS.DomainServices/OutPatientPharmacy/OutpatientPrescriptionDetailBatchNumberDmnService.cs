using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 门诊处方批号  mz_cfmxph
    /// </summary>
    public class OutpatientPrescriptionDetailBatchNumberDmnService : DmnServiceBase, IOutpatientPrescriptionDetailBatchNumberDmnService
    {

        public OutpatientPrescriptionDetailBatchNumberDmnService(IDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        /// <summary>
        /// 修改归架标志
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        public int UpdateGjztbyCfh(string cfh, string organizeId)
        {
            var param = new DbParameter[]
            {
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return ExecuteSqlCommand("UPDATE dbo.mz_cfmxph SET gjzt='1' WHERE cfh=@cfh AND OrganizeId=@OrganizeId", param.ToArray());
        }

        /// <summary>
        /// 根据处方号和组织机构获取未归架的门诊处方明细批号信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="ypCode"></param>
        /// <param name="gjzt">归架状态：0-未归架  1-已归架</param>
        /// <returns></returns>
        public List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string organizeId, string ypCode = "", string gjzt = "0")
        {
            var sql = new StringBuilder(@"
SELECT * FROM dbo.mz_cfmxph(NOLOCK) 
WHERE cfh=@cfh
AND gjzt=@gjzt
AND OrganizeId=@OrganizeId
AND zt='1' 
");
            if (!string.IsNullOrWhiteSpace(ypCode)) sql.Append("AND yp=@ypCode ");
            var param = new DbParameter[]
            {
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@gjzt", gjzt),
                new SqlParameter("@ypCode", ypCode),
            };
            return FindListNoLog<OutpatientPrescriptionDetailBatchNumberEntity>(sql.ToString(), param);
        }

    }
}

using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects;
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
    public class SysMedicineElectronicPrescriptionDmnService : DmnServiceBase, ISysMedicineElectronicPrescriptionDmnService
    {
        public SysMedicineElectronicPrescriptionDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        /// <summary>
        /// 获取当前组织下的系统药品信息
        /// </summary>
        /// <returns></returns>
        public IList<SysMedicineElectronicPrescriptionVO> GetPaginationList(Pagination pagination, string genname, string medListCodg, string listType, string zt)
        {
            var sql = @"select * from [NewtouchHIS_Sett]..Dzcf_CFYP_output where 1=1 ";
            if (!string.IsNullOrEmpty(genname))
            {
                sql += @" and (genname like @genname or regName like @genname)";
            }
            if (!string.IsNullOrEmpty(medListCodg))
            {
                sql += @" and medListCodg like @medListCodg";
            }
            if (!string.IsNullOrWhiteSpace(listType))
            {
                sql += @" and listType like @listType";
            }
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql += @" and zt like @zt";
            }
            DbParameter[] par = new DbParameter[]
            {
                new SqlParameter("@genname", "%"+genname.Trim()+"%"),
                new SqlParameter("@medListCodg", "%"+medListCodg+"%"),
                new SqlParameter("@listType", "%"+listType.Trim()+"%"),
                new SqlParameter("@zt", "%"+zt+"%"),
            };
            return this.QueryWithPage<SysMedicineElectronicPrescriptionVO>(sql, pagination,par
            );
        }

    }
}

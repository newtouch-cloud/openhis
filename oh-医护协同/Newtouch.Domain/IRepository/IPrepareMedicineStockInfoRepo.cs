using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
    public interface IPrepareMedicineStockInfoRepo : IRepositoryBase<PrepareMedicineStockInfoEntity>
    {
        List<PrepareMedicineStockInfoEntity> SelectData(string ypCode, string ph, string pc, string yfbmCode, string organizeId);

        int UpdateKcslWithTrans(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId, string userCode, Infrastructure.EF.EFDbTransaction db);

    }
}

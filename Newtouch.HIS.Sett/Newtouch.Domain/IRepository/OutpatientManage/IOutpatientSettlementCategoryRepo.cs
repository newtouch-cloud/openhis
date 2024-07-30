using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutpatientSettlementCategoryRepo : IRepositoryBase<OutpatientSettlementCategoryEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientSettlementCategoryEntity> SelectMzjsdlByJsnm(int jsnm, string orgId);
        
        /// <summary>
        /// create tabel
        /// </summary>
        /// <param name="js"></param>
        /// <param name="ghxmList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientSettlementCategoryEntity> GetJsdl(OutpatientSettlementEntity js, List<OutpatientItemEntity> ghxmList, string orgId);
    }
}

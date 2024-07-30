using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.OutpatientManage
{
    public interface IOutpatientRegistScheduleRepo : IRepositoryBase<OutpatientRegistScheduleEntity>
    {

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="financialInvoiceEntity"></param>
        /// <param name="fpdm"></param>
        void SubmitForm(OutpatientRegistScheduleEntity financialInvoiceEntity, int? ghpbId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="fpdm"></param>
        void DeleteForm(int ghpbId, string OrganizeId);
    }
}

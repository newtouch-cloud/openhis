using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.PatientManage
{
    public interface IInpatientReserveDmnService
    {
        /// <summary>
        /// 保存住院账户数据事务
        /// </summary>
        /// <param name="vo"></param>
        void PatZyAccDBProc(PatZyAccDataVO vo, InpatientAccountEntity zh, FinanceReceiptEntity cwsj, string type);

        /// <summary>
        /// 获取住院管理》账户管理》获取账户收支记录信息
        /// </summary>
        /// <returns></returns>
        List<InpatientPatAccPayVO> GetAccPayInfo(int zh, string orgId, string zhxz);
    }
}

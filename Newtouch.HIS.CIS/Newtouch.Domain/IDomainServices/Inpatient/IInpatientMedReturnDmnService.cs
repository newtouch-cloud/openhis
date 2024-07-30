using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices
{
    public interface IInpatientMedReturnDmnService
    {
        /// <summary>
        /// 病区发药患者树
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<InpWardPatTreeVO> GetPatTree(string keyword,string staffId, string orgId,string ReturnZcyMed=null);
        /// <summary>
        /// 查询可退药列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="patInfo"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        IList<InpatientMedicineGrantDto> GetPatMedReturnList(string patInfo, string keyword, string kssj, string jssj,string orgId, string ReturnZcyMed = null);
        /// <summary>
        /// 申请退药
        /// </summary>
        /// <param name="user"></param>
        /// <param name="medList"></param>
        string MedReturnSubmit(OperatorModel user,string medList);








    }
}

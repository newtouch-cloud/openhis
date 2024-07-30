using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain.DTO;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.IDomainServices
{
    public interface IOpArrangementDmnService
    {
        IList<ArrangeListVO> GetArrangeList(QueryArrangeDto dto);
        //IList<ArrangeListVO> GetArrangeList(Pagination pagination, QueryArrangeDto dto);
        string getArrangeIdByApplyId(string ApplyId,string OrganizeId);
        ArrangeListVO GetArrangeObjByApplyId(string ApplyId, string OrganizeId);
        ORApplyInfoEntity GetApplyInfoByKey(string keyValue, string OrganizeId);
    }
}

using Newtouch.Core.Common;
using Newtouch.EMR.APIRequest.Bljgh.Request;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IRepository
{
    public interface Ibl_ElementDomainRepo : IRepositoryBase<bl_ElementDomainEntity>
    {
        List<bl_ElementDomainEntity> GetElementTree(string orgId);

        IList<BlysGridVo> GetElementDetail(Pagination pagination, string OrganizeId, string keyword);

        List<BlysListVo> GetBlys(string keyword, string orgId);

        List<TableColumnVo> GetTabColumnList(string tabName, string keyword);
        List<BlysDeatilVo> GetBlysmx(string keyword, string orgId);

        void SubmitForm(bl_ElementDomainEntity entity, string keyValue);
        void DeleteForm(string keyValue, string orgId, string user);

        #region 病历结构竖表数据转换横表
        void BljghDataDealwith(BljghReq req);

        #endregion
    }
}

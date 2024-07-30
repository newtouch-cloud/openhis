using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysItemsDetailRepository : IRepositoryBase<SysItemsDetailEntity>
    {
        /// <summary>
        /// 获取公共字典项List
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysItemsDetailEntity> GetCommonList(string itemId = null, string keyword = null);

        /// <summary>
        /// 获取 单一组织机构 字典项List
        /// </summary>
        /// <param name="orgId">医疗机构Id，不传时差共享的</param>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysItemsDetailVO> GetListByOrgId(string orgId = null, string itemId = null, string keyword = null);

        /// <summary>
        /// 更新、保存字典项
        /// </summary>
        /// <param name="itemsDetailEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysItemsDetailEntity entity, string keyValue);

        List<ProjectZbVO> GetTreeViewdata(string orgId);
        IList<ProjectMxVO> GetProjectMxVO(string orgId, string kmdm);
        IList<XmandYp> GetXmandYp(string orgId, string kmdm, int? xmlx);
        int UpdateMx(string orgid, string kmdm, string xmdm, string czydm);
        int InsertMx(string orgid, string kmdm, string xmdm,string czydm);
        int DeleteMl(string orgid, string kmdm);
        List<ProjectZbVO> GetMlbyKmdm(string kmdm, string orgid);
        int InsertMl(string kmmc,string kmdm, string sjkmdm,int xmlx,int gmlbz,decimal sl, string orgid, string czydm, int zt);
        int UpdateMl(string kmmc,string kmdm, string sjkmdm,int xmlx,int gmlbz,decimal sl, string orgid, string czydm, int zt);
        List<ProjectZbVO> Getsjdm(string orgid);
    }
}

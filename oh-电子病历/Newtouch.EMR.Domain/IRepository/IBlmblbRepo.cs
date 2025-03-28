using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using Newtouch.Common.Operator;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-09-05 11:34
    /// 描 述：病历模板列表
    /// </summary>
    public interface IBlmblbRepo : IRepositoryBase<BlmblbEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(BlmblbEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);
        BlmblbEntity GetTemplateById(string ID);
        BlmblbEntity GetTemplateByBllxId(string ID, string OrganizeId);
        
        List<BlmblbEntity> GetParentTemplate(string OrganizeId);
        List<BlmblbEntity> GetPagintionList(Pagination pagination, string OrganizeId, string keyword, OperatorModel user);


        #region 护理记录
        void BtnSubmit(OperatorModel user, IList<HljldataVO> data);
        IList<HljldataVO> HljlLoadData(Pagination pagination, string blId,string zyh, string orgId);
        #endregion
        



    }


}
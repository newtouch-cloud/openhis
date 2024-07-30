using Newtouch.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using System;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 13:27
    /// 描 述：住院病人生命体征
    /// </summary>
    public interface IInpatientVitalSignsRepo : IRepositoryBase<InpatientVitalSignsEntity>
    {
        /// <summary>
        /// 护理分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<InpatientVitalSignsDto> GetPaginationList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh,string wardCode, string isShowDelete);

        /// <summary>
        /// 护理查询
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<InpatientVitalSignsEntity> GetValidList(string orgId, DateTime? kssj, DateTime? jssj, string zyh);


        /// <summary>
        /// 护理查询(不分页)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<InpatientVitalSignsDto> GetList(string orgId, DateTime? kssj, DateTime? jssj, string zyh);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(InpatientVitalSignsEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

        IList<TemperatureGraphData> GetTempratureData(string orgId, DateTime? kssj, DateTime? jssj, string zyh,int pagesize =6);

    }
}
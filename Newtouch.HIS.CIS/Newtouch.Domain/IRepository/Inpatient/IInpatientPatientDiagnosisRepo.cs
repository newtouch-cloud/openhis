using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 13:27
    /// 描 述：住院病人诊断信息
    /// </summary>
    public interface IInpatientPatientDiagnosisRepo : IRepositoryBase<InpatientPatientDiagnosisEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(InpatientPatientDiagnosisEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1入院诊断2出院诊断</param>
        /// <param name="zt">1-有效  0-无效</param>
        /// <returns></returns>
        List<InpatientPatientDiagnosisEntity> SelectData(string zyh, string organizeId, string zdlb,
           string zt = "1");
        /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zdlb">1入院诊断2出院诊断</param>
        /// <param name="zt">1-有效  0-无效</param>
        /// <returns></returns>
        List<WMDiagnosisHtmlVO> SelectDiagData(string zyh, string organizeId, string zdlb,
           string zt = "1");

    }
}
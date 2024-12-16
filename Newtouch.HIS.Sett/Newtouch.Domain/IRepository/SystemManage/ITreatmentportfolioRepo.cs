using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.SystemManage
{
    public interface ITreatmentportfolioRepo : IRepositoryBase<TreatmentportfolioEntity>
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        void ADDInsert(TreatmentportfolioEntity entity, string keyValue);

        /// <summary>
        /// 删除收费项目组合
        /// </summary>
        /// <param name="keyValue"></param>
        void deleteid(string keyValue, string orgId);

        /// <summary>
        /// 删除收费组合中明细
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="zhcodemc"></param>
        void deletemc(string keyValue, string zhcodemc, string orgId);


        /// <summary>
        /// 保存收费项目组合
        /// </summary>
        /// <param name="entity"></param>
        void ADDrowdata(TreatmentportfolioEntity entity);
        /// <summary>
        /// 是否已存在收费项目组合
        /// </summary>
        /// <param name="zhcodetj"></param>
        /// <param name="sfxmmc123"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<TreatmentportfolioEntity> TJchaxun(string zhcodetj, string sfxmmc123, string orgId);
        /// <summary>
        /// 获取修改收费项目组合
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zhcode"></param>
        /// <returns></returns>
        IList<TreatmentportfolioEntity> Codecx(string orgId, string zhcode);
        /// <summary>
        /// 查询收费项目组合
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<TreamentviewVO> Keyword(Pagination pagination, string keyword, string orgId);
    }
}

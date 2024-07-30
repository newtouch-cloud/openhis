using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
    public interface IComDiagnosisRepo : IRepositoryBase<ComDiagnosisEntity>
    {
        /// <summary>
        /// 获取常用诊断信息
        /// </summary>
        /// <param name="ys"></param>
        /// <param name="orgId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<ComDiagnosisVO> GetComDiagnosis(string ys,string ks,string orgId,string type,string keyword,string lx);
        /// <summary>
        /// 保存常用诊断
        /// </summary>
        /// <param name="entity"></param>
        void SubmitDiag(ComDiagnosisEntity entity);
        /// <summary>
        /// 获取诊断拼音
        /// </summary>
        /// <param name="zdmc"></param>
        /// <param name="zdbm"></param>
        /// <returns></returns>
        List<ComDiagnosisVO> Getzdpy(string zdmc, string zdbm);
        /// <summary>
        /// 获取诊断记录信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="type"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        List<ComDiagnosisVO> GetDiagsLists(string orgId,string type, string mzh);
        void DelDiagnosticTemplate(string Id);
        /// <summary>
        /// 获取常用诊断信息-管理界面
        /// </summary>
        /// <param name="ys"></param>
        /// <param name="orgId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<ComDiagnosisVO> GetComDiagnosisAdmin(string ys, string ks, string orgId, string type, string keyword, string lx);
    }
}

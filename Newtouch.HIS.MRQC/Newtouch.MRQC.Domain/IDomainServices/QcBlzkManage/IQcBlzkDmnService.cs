using Newtouch.MRQC.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.IDomainServices.QcBlzkManage
{
    public interface IQcBlzkDmnService
    {
        void SubmitForm(ZkxmEntityVo entity);
        /// <summary>
        /// 项目评分列表
        /// </summary>
        /// <param name="bllx"></param>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<ScoreItemVo> Getybbxbldata(string bllx, string orgId, string zyh);
        void SaveScoreDate(List<ScoreEntity> entity, string orgId, string CreatorCode, string bllx, string blmc,string zyh);
    }
}

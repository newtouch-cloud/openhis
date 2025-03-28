using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IRepository
{
    public interface IHljlDataRepo : IRepositoryBase<HljlDataEntity>
    {
        #region 护理记录
        void BtnSubmit(OperatorModel user, IList<HljldataVO> data,string blId,string zyh);

        IList<HljldataVO> HljlLoadData(Pagination pagination, string zyh,string blId,string orgId,string kssj,string jssj);
        #endregion
        /// <summary>
        /// 模板权限控制
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="mbbh"></param>
        /// <param name="orgId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        PatMedRecordTreeVO HljlCtrlQx(string blId, string mbbh, string orgId, string user);
        List<InfordivVO> Infodiv(string zyh,string orgId);

        List<HljldataVO> mbbhselect(string zyh, string mbbh,string orgId, string kssj, string jssj);
        List<HljldataVO> blIdselect(string zyh, string mbbh,string orgId);


    }
}

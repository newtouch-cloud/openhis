using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface.BusinessManage
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRefundApp
    {
        List<MZJSInfo> GetFPHByKh(string kh, string startTime, string endTime, string fph);
        List<MZJS> GetMZJSByJsnm(int jsnm);
        List<GridViewMx> GetGridViewMx(int jsnm);
        decimal getSysl(int patid, int ghnm);

        bool btnReturn(string kh, List<GridViewMx> RefundVOList, int jsnm, out bool isReturnAll);

        #region GRS门诊退费

        /// <summary>
        /// GRS门诊退费
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="GridViewMxList"></param>
        /// <param name="jsnm"></param>
        /// <param name="isReturnAll"></param>
        /// <returns></returns>
        bool btnReturnInAcc(string blh, List<GridViewMx> GridViewMxList, int jsnm, out bool isReturnAll);
        #endregion

        #region 医保退费

        /// <summary>
        /// 退费流程.新的未结明细上传
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="newwjybjsh"></param>
        /// <param name="tmxzje">余下总金额</param>
        /// <param name="yxzje">余下总金额</param>
        void DetailsUploadYb(string orgId, int jsnm
            , Dictionary<int, decimal> tjsxmDict
            , string newwjybjsh
            , out decimal tmxzje
            , out decimal yxzje);


        #endregion

    }
}

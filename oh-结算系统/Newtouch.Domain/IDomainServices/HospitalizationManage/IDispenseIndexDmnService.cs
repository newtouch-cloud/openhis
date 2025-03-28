using Newtouch.Common;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Sett.Request.HospitalizationPharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.HospitalizationManage
{
    //住院发药接口
    public interface IDispenseIndexDmnService
    {
        #region PID 住院发药


        /// <summary>
        /// 住院发药病区
        /// </summary>
        /// <param name="zyyf"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<DispenseBQIndexVO> GetZYFYBQPagList(string zyyf, string organizeId);

        /// <summary>
        /// 住院发药病人
        /// </summary>
        /// <param name="zyyf"></param>
        /// <param name="bq"></param>
        /// <returns></returns>
        IList<DispenseBQRYIndexVO> GetZYFYBRPagList(string zyyf, string bq);


        /// <summary>
        /// 住院发药信息集合
        /// </summary>
        /// <param name="bq"></param>
        /// <param name="Zyh"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <param name="Cl"></param>
        /// <param name="Fyzt"></param>
        /// <returns></returns>
        IList<DispenseBQRYXXIndexVO> GetZYFYXXPagList(WaitingDispenseBRXXMedicineDispenseRequest model);


        /// <summary>
        /// 住院病人病区配药
        /// </summary>
        /// <param name="UserLogin"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <returns></returns>
        IList<DispenseBQBRPYIndexVO> SetZYBRBQPY(WaitingDispenseBQBRPYMedicineDispenseRequest model);

        /// <summary>
        /// 住院病区病人发药退药后修改发药备注状态
        /// </summary>
        /// <param name="UserLogin"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <returns></returns>
        int UpBQPYyzzx(WaitingDispenseBQPYyzzxZTDispenseRequest model);

        #endregion

        #region 住院发药查询
        //住院发药查询
        IList<DispenseBQRYCXXXIndexVO> GetZYFYCXXXPagList(WaitingDispenseBRCXXXMedicineDispenseRequest model);

        #endregion

        #region 住院退药明细信息
        /// <summary>
        /// 获取医生站医嘱ID ，医嘱表ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ModelZyTyYszyzIdInfoVO> GetZYYszysList(WaitingDispenseTYYszyzIdDispenseRequest model);

        IList<DispenseBQRYTYXXIndexVO> GetZYTYXXPagList(WaitingDispenseBRTYXXMedicineDispenseRequest model);
        #endregion
    }
}

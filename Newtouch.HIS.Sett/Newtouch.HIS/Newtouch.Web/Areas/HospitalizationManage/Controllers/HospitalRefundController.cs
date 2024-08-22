using Newtouch.HIS.Application;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Web.Areas.HospitalizationManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HospitalRefundController : ControllerBase
    {
        private readonly IHospitalRefundApp _hospitalRefundApp;

        /// <summary>
        /// 加载新生儿 
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <returns>姓名 婴儿号</returns>
        //public DataTable GetYehByZyh(string zyh)
        //{

        //    DataTable yeh = _hospitalRefundApp.GetYehByZyh(zyh);
        //    return yeh;
        //}
        /// <summary>
        /// 根据住院号获取病人信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetPatInfoByZyh(string zyh)
        {
         
            var name= _hospitalRefundApp.GetPatInfoByZyh(zyh);
            return Success("", name);
        }

        /// <summary>
        /// 查询
        /// </summary>

        public ActionResult BtnQuery( string zyh, string brxm, string yrh, string sfks, string zxks, string xmmc)
       {
            var data = new List<GridZyXmYpjfb>();
            if (string.IsNullOrWhiteSpace(zyh.Trim()))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
              
            }
            data = _hospitalRefundApp.GetZyXmjfbGroup(zyh, zxks, xmmc, yrh);        //项目

            //住院退费配置：是否包含药品 0 不包含药品


            /// 根据代码获取配置信息

            var pzInfo = _hospitalRefundApp.GetPZInfo();
            if (pzInfo == null)
            {
                var msg = "系统未配置编号为：" + Constants.xtmzpz.ZYTFPZ_INCLUDEYP + " 的系统打印配置";
                return Error(msg);  
            }

            if (pzInfo.ToString() != "0")//0 不包含药品
            {
                var ypdata = new List<GridZyXmYpjfb>();
                ypdata = _hospitalRefundApp.GetZyYpjfbGroup(zyh, zxks, xmmc, yrh);        //药品
                data.AddRange(ypdata);
            }
             
            return Success("", data);
        }

        /// <summary>
        /// 根据所选择项获取明细
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="xsebh"></param>
        /// <param name="sfxmbh"></param>
        /// <param name="tdrq"></param>
        /// <param name="is_sfxm"></param>
        /// <returns></returns>

        public ActionResult Detail(string zyh, string xsebh, string sfxm, string CreateTime, string is_sfxm)
        {
           
            var data= LoadZyXmYpItemsjfb(zyh, xsebh, sfxm, CreateTime, is_sfxm);
            return Success("", data);
        }
        private List<gridZyXmYpItemsjfb> LoadZyXmYpItemsjfb(string zyh, string xsebh, string sfxm, string CreateTime, string is_sfxm)
        {
            var data = new List<gridZyXmYpItemsjfb>();
            if (is_sfxm == "0")
            {
                data = _hospitalRefundApp.GetZyYpjfbList(zyh, xsebh, sfxm, CreateTime); //药品收费
            }
            else
            {
                data = _hospitalRefundApp.GetZyXmjfbList(zyh, xsebh, sfxm, CreateTime); //项目收费
            }

            return data;
        }
        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="cxzyjfbbh">编号</param>
        /// <param name="is_sfxm">是否是药品</param>
        /// <param name="refundNum">退数量</param>
        /// <returns></returns>
        public ActionResult btnTuiFei(string cxzyjfbbh, string is_sfxm,int refundNum)
        {
            string result = "退费失败";
            Dictionary<string, decimal> returnDict = new Dictionary<string, decimal>();
            returnDict.Add(cxzyjfbbh, refundNum);
            if (is_sfxm == "0")
            {
                //药品收费                            
                result = _hospitalRefundApp.SaveReturnZyyp(returnDict);
            }
            else
            {
                //项目收费
                result = _hospitalRefundApp.SaveReturnZyxm(returnDict);
            }
         return Success("", result) ;
        }

    }
}
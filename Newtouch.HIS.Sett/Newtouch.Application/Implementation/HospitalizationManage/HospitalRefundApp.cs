/*********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 住院管理》住院退费》
//**********************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.Common.Operator;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class HospitalRefundApp : AppBase, IHospitalRefundApp
    {
        private readonly IHospitalRefundDmnService _hospitalRefundService;
        private readonly ISysConfigRepo _sysConfigRepo; //系统配置
        
        /// <summary>
        /// 住院项目计费分组
        /// </summary>
        /// <param name="zyh">住院编号</param>
        /// <param name="zxks">科室</param>
        /// <param name="xmmc">收费项目</param>
        /// <param name="yrh">新生儿编号</param>
        public List<GridZyXmYpjfb> GetZyXmjfbGroup(string zyh, string zxks, string xmmc, string yrh)
        {
            List<GridZyXmYpjfb> zyXmjfbDt = _hospitalRefundService.GetZyXmjfbGroup(zyh, zxks, xmmc, yrh);

            return zyXmjfbDt;
        }
             /// <summary>
        /// 住院药品计费分组
        /// </summary>
        /// <param name="zyh">住院编号</param>
        /// <param name="zxks">科室</param>
        /// <param name="xmmc">收费项目</param>
        /// <param name="yrh">新生儿编号</param>

        public List<GridZyXmYpjfb> GetZyYpjfbGroup(string zyh, string zxks, string xmmc, string yrh)
        {
            List<GridZyXmYpjfb> ZyYpjfb = _hospitalRefundService.GetZyYpjfbGroup(zyh, zxks, xmmc, yrh);

            return ZyYpjfb;
        }
        /// <summary>
        /// 住院药品计费明细
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="xsebh">新生儿编号</param>
        /// <param name="ypbh">药品编号</param>
        /// <returns></returns>
        public List<gridZyXmYpItemsjfb> GetZyYpjfbList(string zyh, string xsebh, string ypCode, string tdrq)
        {
            List<gridZyXmYpItemsjfb> zyYpjfbDt = _hospitalRefundService.GetZyYpjfbList(zyh, xsebh, ypCode, tdrq);
            //if (zyYpjfbDt.Rows.Count > 0 && loadZyYpjfbListEvent != null)
            //    loadZyYpjfbListEvent(zyYpjfbDt);
            return zyYpjfbDt;
        }
        /// <summary>
        /// 住院项目计费明细
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="xsebh"></param>
        /// <param name="sfxmbh"></param>
        /// <returns></returns>
        public List<gridZyXmYpItemsjfb> GetZyXmjfbList(string zyh, string xsebh, string sfxm, string CreateTime)
        {
            List<gridZyXmYpItemsjfb> zyXmjfbDt = _hospitalRefundService.GetZyXmjfbList(zyh, xsebh, sfxm, CreateTime);

            return zyXmjfbDt;
        }
        /// <summary>
        /// 加载新生儿 
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <returns>姓名 婴儿号</returns>
        //public DataTable GetYehByZyh(string zyh)
        //{

        //    DataTable yeh = _hospitalRefundService.GetYehByZyh(zyh);
        //    return yeh;
        //}
        /// 根据住院号获取病人信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public string GetPatInfoByZyh(string zyh)
        {
          
            return _hospitalRefundService.GetPatInfoByZyh( zyh);
        }
        /// 根据代码获取配置信息
        public string GetPZInfo()
        {
            return _sysConfigRepo.GetValueByCode(Constants.xtmzpz.ZYTFPZ_INCLUDEYP, OperatorProvider.GetCurrent().OrganizeId);

        }
        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="returnDict"></param>
        /// <returns></returns>
        public string SaveReturnZyyp(Dictionary<string, decimal> returnDict)
        {
            return _hospitalRefundService.SaveReturnZyyp(returnDict);
        }
        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="returnDict"></param>
        /// <returns></returns>
        public string SaveReturnZyxm(Dictionary<string, decimal> returnDict)
        {
            return _hospitalRefundService.SaveReturnZyxm(returnDict);
        }
    }
}

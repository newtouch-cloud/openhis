/*********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 住院管理》账户管理》
// Author：HYJ
// CreateDate： 2016/12/20 
//**********************************************************/


using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using System.Collections.Generic;


namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospitalRefundApp
    {
        //DataTable GetYehByZyh(string zyh);
        string GetPatInfoByZyh(string zyh);
        string GetPZInfo();
        List<GridZyXmYpjfb> GetZyXmjfbGroup(string zyh, string zxks, string xmmc, string yrh);
        List<GridZyXmYpjfb> GetZyYpjfbGroup(string zyh, string zxks, string xmmc, string yrh);
        List<gridZyXmYpItemsjfb> GetZyYpjfbList(string zyh, string xsebh, string yp, string CreateTime);
        List<gridZyXmYpItemsjfb> GetZyXmjfbList(string zyh, string xsebh, string sfxm, string CreateTime);
        string SaveReturnZyyp(Dictionary<string, decimal> returnDict);
        string SaveReturnZyxm(Dictionary<string, decimal> returnDict);

    }
}

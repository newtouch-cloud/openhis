using System;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IHospitalRefundDmnService
    {
      //  DataTable GetYehByZyh(string zyh);
        string GetPatInfoByZyh(string zyh);

        List<GridZyXmYpjfb> GetZyXmjfbGroup(string zyh, string zxks, string xmmc, string yrh);
        List<GridZyXmYpjfb> GetZyYpjfbGroup(string zyh, string zxks, string xmmc, string yrh);
        List<gridZyXmYpItemsjfb> GetZyYpjfbList(string zyh, string xsebh, string ypbh, string CreateTime);
        List<gridZyXmYpItemsjfb> GetZyXmjfbList(string zyh, string xsebh, string sfxmbh, string CreateTime);
        string SaveReturnZyyp(Dictionary<string, decimal> returnDict);
        string SaveReturnZyxm(Dictionary<string, decimal> returnDict);
    }

}

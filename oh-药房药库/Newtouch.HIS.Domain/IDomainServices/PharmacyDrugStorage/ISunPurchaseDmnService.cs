using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface ISunPurchaseDmnService
    {
        string getMessageDigest(string content);
        PurchaseHead GetHead();
        string PurchaseInterface(string responsexml, string OrganizeId, string param,string jydm, string jymc);

        //订单填报（YY009）
        string Purchase_YY009(string orgId, string cgId);
        //订单填报确认（YY010）
        string Purchase_YY010(string orgId, string cgId);

        //发票查询并获取(YY004)
        List<OutputStructYY004> Purchase_YY004(string orgId, string yqbm, string fpmxbh);
        //发票验收 (YY019)
        string Purchase_YY019(PurchaseMainYY019 main, string orgId);
        //获取配送单明细数据
        List<OutputStructYY003> Purchase_YY003(string orgId, string yqbm, string psmxbh);
        //配送明细验收
        string Purchase_YY018(PurchaseMainYY018 main, string orgId);
        //退货单填报（YY011）
        string Purchase_YY011(string orgId, string cgId);
        //退货单填报确认（YY012）
        string Purchase_YY012(string orgId, string cgId);

        //医院配送点基础信息传报(YY001)
        string Purchase_YY001(string orgId, string Id, string czlx);
    }
}

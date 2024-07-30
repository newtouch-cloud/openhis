using Newtouch.Herp.Domain.DTO.InputDto.Purchase;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.DTO.OutputDto.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.IDomainServices.Purchase
{
    public interface ISunPurchaseDmnService
    {
        //耗材采购单填报(YY111)
        string Purchase_YY111(string orgId, string cgId);
        //耗材采购单确认(YY112)
        string Purchase_YY112(string orgId, string cgId);

        //耗材按发票明细获取(YY156)
        List<OutputStructYY156> Purchase_YY156(string orgId, string qybm, string fpmxbhcxtj);
        //耗材发票验收确认(YY132)
        string Purchase_YY132(PurchaseMainYY132 main, string orgId);

        //耗材按配送明细获取(YY153)
        List<OutputStructYY153> Purchase_YY153(string orgId, string qybm, string psmxbhcxtj);
        //耗材配送验收确认(YY131)
        string Purchase_YY131(PurchaseMainYY131 main, List<PurchaseStructYY131> st, string orgId);


        //耗材退货单填报(YY113)
        string Purchase_YY113(string orgId, string thId);
        //耗材退货单确认(YY114)
        string Purchase_YY114(string orgId, string thId);

        //耗材发票信息获取(YY157)
        OutputYY157 Purchase_YY157(PurchaseMainYY157 main, string orgId);
        //耗材发票支付确认(YY133)
        string Purchase_YY133(PurchaseMainYY133 main, string orgId);


        //耗材配送点传报(YY101)
        string Purchase_YY101(string orgId, string Id, string czlx);

        #region 查询接口

        //YY154 耗材配送单获取(YY154)
        OutputYY154 Purchase_YY154(PurchaseMainYY154 main, string orgId);
        //YY155 耗材配送明细获取(YY155)
        OutputYY155 Purchase_YY155(PurchaseMainYY155 main, string orgId);
        //YY158 耗材发票明细获取(YY158)
        OutputYY158 Purchase_YY158(PurchaseMainYY158 main, string orgId);

        //YY151 耗材采购明细信息获取(YY151)
        OutputYY151 Purchase_YY151(PurchaseMainYY151 main, string orgId);

        //YY152 耗材退货明细信息获取(YY152)
        OutputYY152 Purchase_YY152(PurchaseMainYY152 main, string orgId);

        //YY159 耗材按采购单获取采购明细状态(YY159)
        OutputYY159 Purchase_YY159(PurchaseMainYY159 main, string orgId);

        //YY160 耗材发票状态获取(YY160)
        OutputYY160 Purchase_YY160(PurchaseMainYY160 main, string orgId);
        //YY161 耗材配送单状态获取(YY161)
        OutputYY161 Purchase_YY161(PurchaseMainYY161 main, string orgId);
        //YY162 耗材按退货单获取退货明细状态(YY162)
        OutputYY162 Purchase_YY162(PurchaseMainYY162 main, string orgId);

        //YY164 企业信息获取(YY164)
        OutputYY164 Purchase_YY164(PurchaseMainYY164 main, string orgId);

        #endregion 
    }
}

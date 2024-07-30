using System;
using System.Collections.Generic;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Hospitalizatiion;
using System.Data.SqlClient;
using System.Web.Http;
using Newtouch.HIS.Domain.Entity.V;

namespace Newtouch.PDS.API.Controllers
{
    [RoutePrefix("api/Hospitalization")]
    public class HospitalizationController : ApiController
    {
        /// <summary>
        /// 住院退药接口
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ZYTYInterface")]
        public ActResult ZYTYInterface(List<DrugRerurnRequest> request)
        {
            ActResult Res = new ActResult();
            if (request == null || request.Count <= 0)
            {
                Res.IsSucceed = false;
                Res.ResultMsg = "请求数据为空";
                Res.ResultCode = (int)ResultCode.ValidationFailure;
                return Res;
            }
            List<DrugReturnResult> resultList = new List<DrugReturnResult>();
            foreach (var par in request)
            {
                var result = new DrugReturnResult();
                result.yzId = par.yzId;
                result.ypCode = par.ypCode;
                result.zh = par.zh;
                result.zxrq = par.zxrq;
                if (string.IsNullOrEmpty(par.yzId))
                {
                    result.IsSucceed = false;
                    result.ErrorCode = (int)ErrorCode.Error1;
                    result.ErrorMsg = "医嘱号不能为空";
                    resultList.Add(result);
                    continue;
                }
                if (string.IsNullOrEmpty(par.ypCode))
                {
                    result.IsSucceed = false;
                    result.ErrorCode = (int)ErrorCode.Error2;
                    result.ErrorMsg = "药品编码不能为空";
                    resultList.Add(result);
                    continue;
                }
                if (string.IsNullOrEmpty(par.tysqr))
                {
                    result.IsSucceed = false;
                    result.ErrorCode = (int)ErrorCode.Error7;
                    result.ErrorMsg = "退药申请人不能为空";
                    resultList.Add(result);
                    continue;
                }
                if (par.zxrq == null || par.zxrq == DateTime.MinValue)
                {
                    result.IsSucceed = false;
                    result.ErrorCode = (int)ErrorCode.Error8;
                    result.ErrorMsg = "执行日期不能为空";
                    resultList.Add(result);
                    continue;
                }
                string error;
                int isOk = IsCouldReturn(par.yzId.Trim(), par.ypCode.Trim(), par.zxrq, out error);
                if (isOk > 0)
                {
                    result.IsSucceed = false;
                    result.ErrorCode = isOk;
                    result.ErrorMsg = error;
                    resultList.Add(result);
                }
                else
                {
                    string strSql = string.Format("update NewtouchHIS_PDS.dbo.zy_ypyzxx set sqtybz = @sqtybz where zxId = @zxId and ypCode = @ypCode and zxrq = @zxrq");
                    var parms = new List<SqlParameter>();
                    parms.Add(new SqlParameter("@sqtybz", "1"));
                    parms.Add(new SqlParameter("@zxId", par.yzId));
                    parms.Add(new SqlParameter("@ypCode", par.ypCode));
                    parms.Add(new SqlParameter("@zxrq", par.zxrq));
                    int count = Tools.DB.DbHelper.ExcuteSQL(strSql, parms.ToArray());
                    if (count <= 0)
                    {
                        result.IsSucceed = false;
                        result.ErrorMsg = "退药失败";
                        resultList.Add(result);
                    }
                }
            }
            if (resultList.Count > 0)
            {
                Res.IsSucceed = false;
            }
            Res.ResultCode = (int)ResultCode.AllFailure;
            Res.Data = resultList;
            return Res;
        }

        /// <summary>
        /// 退药前判断发药记录是否符合退药条件
        /// </summary>
        /// <param name="zxId"></param>
        /// <returns></returns>
        public int IsCouldReturn(string zxId, string ypCode, DateTime? zxrq, out string error)
        {
            const string strSql = @"
select a.fybz,a.sqtybz,b.ypmc 
from NewtouchHIS_PDS.dbo.zy_ypyzxx a 
inner join NewtouchHIS_Base.dbo.V_C_xt_yp b on a.ypCode = b.ypCode and a.organizeId = b.OrganizeId 
where a.zxId = @zxId 
and a.ypCode = @ypCode 
and a.zxrq =@zxrq ";

            var param = new Dictionary<string, object>
            {
                {"zxId", zxId},
                { "ypCode", ypCode},
                { "zxrq", zxrq}
            };
            IList<YzxxVEntity> obj;

            try
            {
                obj = Tools.DB.DbHelper.ExecuteSqlQueryList<YzxxVEntity>(strSql, param);
            }
            catch
            {
                error = "无法查询该医嘱信息";
                return (int)ErrorCode.Error3;
            }

            if (obj != null && obj.Count > 0)
            {
                foreach (var item in obj)
                {
                    var ypmc = item.ypmc;
                    switch (item.fybz)
                    {
                        case "1":
                            error = ypmc + "未发药,禁止退药";
                            return (int)ErrorCode.Error4;
                        case "3":
                            error = ypmc + "药品已经被退过了";
                            return (int)ErrorCode.Error5;
                        default:
                            {
                                if (!"0".Equals(item.sqtybz.ToString()))
                                {
                                    error = ypmc + "已经申请退药,禁止重复操作";
                                    return (int)ErrorCode.Error6;
                                }

                                break;
                            }
                    }
                }
                error = "";
                return (int)ErrorCode.Success;
            }

            error = "无法查询该医嘱信息";
            return (int)ErrorCode.Error3;
        }
    }
}

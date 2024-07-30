using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.AccessManage
{
    public class AccessAuthDmnService:DmnServiceBase, IAccessAuthDmnService
    {
        #region private
        private readonly IAccessAuthRepo _accessAuthRepo;
        private readonly ISysStaffRepo _sysStaffRepo;
        #endregion
        public AccessAuthDmnService(IBaseDatabaseFactory databaseFactory, IAccessAuthRepo accessAuthRepo, ISysStaffRepo sysStaffRepo)
        : base(databaseFactory)
        {
            _accessAuthRepo = accessAuthRepo;
            _sysStaffRepo = sysStaffRepo;
        }

        public bool Authorized(AccessIdentity ident,out string message)
        {
            message = "";
            if (!string.IsNullOrWhiteSpace(ident.OrganizeId) && !string.IsNullOrWhiteSpace(ident.Account) && !string.IsNullOrWhiteSpace(ident.AppId))
            {
                var list = _accessAuthRepo.FindList(ident.Account, ident.OrganizeId);
                if (list != null && list.Count>0)
                {
                    #region AuthorizedLev
                    var ety = list.FirstOrDefault();
                    if (ety.AuthorizedLev == ((int)EnumAuthorizedLev.imitatelogin))
                    {
                        if (string.IsNullOrWhiteSpace(ident.UserCode)|| string.IsNullOrWhiteSpace(ident.UserName))
                        {
                            message = "模拟登录身份信息不完善，请补充。";
                            return false;
                        }
                        var user = _sysStaffRepo.FindEntity(p => p.gh == ident.UserCode && p.Name == ident.UserName && p.OrganizeId == ident.OrganizeId && p.zt == "1");
                        if (user == null)
                        {
                            message = "模拟登录身份信息验证失败，请确认。";
                            return false;
                        }
                    }
                    else if (ety.AuthorizedLev == ((int)EnumAuthorizedLev.saltaccess))
                    {
                        if (string.IsNullOrWhiteSpace(ident.TokenType))
                        {
                            try
                            {
                                //AuthProvider.GetAuthDecrypt(ident.TokenType);
                            }
                            catch
                            {
                                message = "授权信息验证失败，请确认。";
                                return false;
                            }
                        }
                    }
                    else if (ety.AuthorizedLev == ((int)EnumAuthorizedLev.tokenaccess))
                    {
                        
                    }
                    #endregion
                    int? authlev = ety.AuthorizedPeriod;
                    string accesskey = ety.accesskey;
                    if (authlev != null && !string.IsNullOrWhiteSpace(accesskey))
                    {
                        accesskey ="";
                        TimeSpan timesp = new TimeSpan();
                        timesp = DateTime.Now.Subtract(Convert.ToDateTime(accesskey)).Duration();
                        if (ety.AuthorizedPeriod == ((int)EnumAuthorizedPeriod.longtime))
                        {
                            return true;
                        }
                        else if (ety.AuthorizedPeriod == ((int)EnumAuthorizedPeriod.month) && timesp.TotalDays <= 31)
                        {
                            message = "授权剩余天数："+ timesp.TotalDays.ToString();
                            return true;
                        }
                        else if (ety.AuthorizedPeriod == ((int)EnumAuthorizedPeriod.partyear) && timesp.TotalDays <= 180)
                        {
                            message = "授权剩余天数：" + timesp.TotalDays.ToString();
                            return true;
                        }
                        else if (ety.AuthorizedPeriod == ((int)EnumAuthorizedPeriod.year) && timesp.TotalDays <= 365)
                        {
                            message = "授权剩余天数：" + timesp.TotalDays.ToString();
                            return true;
                        }
                        message = "授权信息过期，请重新授权";
                    }
                    else
                    {
                        message = "授权信息异常，请重新授权";
                    }
                }
                else
                {
                    message = "非法访问，应用未授权";
                }
            }
            else {
                message = "关键身份信息不可为空";
            }
            return false;
        }
    }
}

using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// 顶级组织机构Id
        /// </summary>
        private static string _TopOrganizeId;

        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                if (_TopOrganizeId == null)
                {
                    _TopOrganizeId = System.Configuration.ConfigurationManager.AppSettings["Top_OrganizeId"] as string;
                    if (string.IsNullOrWhiteSpace(_TopOrganizeId))
                    {
                        throw new Exception("Top_OrganizeId未配置");
                    }
                }
                return _TopOrganizeId;
            }
        }

        /// <summary>
        /// 应用Id
        /// </summary>
        private static string _AppId;

        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string AppId
        {
            get
            {
                if (_AppId == null)
                {
                    _AppId = System.Configuration.ConfigurationManager.AppSettings["AppId"] as string;
                    if (string.IsNullOrWhiteSpace(_AppId))
                    {
                        //throw new Exception("AppId未配置");
                    }
                }
                return _AppId;
            }
        }

        private static string _LogonDefaultPassword;

        /// <summary>
        /// 登录的默认密码（创建新用户时的的默认密码）
        /// </summary>
        public static string LogonDefaultPassword
        {
            get
            {
                if (_LogonDefaultPassword == null)
                {
                    _LogonDefaultPassword = System.Configuration.ConfigurationManager.AppSettings["LoginUser_Default_Password"] as string;
                    if (string.IsNullOrWhiteSpace(_LogonDefaultPassword))
                    {
                        throw new System.ApplicationException("LoginUser_Default_Password未配置");
                    }
                }
                return _LogonDefaultPassword;
            }
        }

    }
}

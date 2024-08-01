namespace FrameworkBase.Infrastructure
{
    /// <summary>
    /// 系统常量
    /// </summary>
    public sealed class ConstantsBase
    {
        #region

        /// <summary>
        /// 
        /// </summary>
        private static string _AppId;

        /// <summary>
        /// 应用Id
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
                    }
                }
                return _AppId;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static string _AppName;

        /// <summary>
        /// 应用Name
        /// </summary>
        public static string AppName
        {
            get
            {
                if (_AppName == null)
                {
                    _AppName = System.Configuration.ConfigurationManager.AppSettings["AppName"] as string;
                    if (string.IsNullOrWhiteSpace(_AppName))
                    {
                    }
                }
                return _AppName;
            }
        }

        #endregion

        #region

        /// <summary>
        /// 
        /// </summary>
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

        #endregion


    }
}

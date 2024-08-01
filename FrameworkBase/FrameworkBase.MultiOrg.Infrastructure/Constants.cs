using System;

namespace FrameworkBase.MultiOrg.Infrastructure
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
                if (string.IsNullOrWhiteSpace(_AppName))
                {
                    return null;
                }
                return _AppName;
            }
        }

        #endregion

        #region
        /// <summary>
        /// 
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
        #endregion


    }
}

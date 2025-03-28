using System;
using System.Collections.Generic;
using Newtouch.Common.Operator;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class IEntity<TEntity> : Newtouch.Infrastructure.EF.IEntity<TEntity>
    {
        private static readonly IList<string> creatorUserCodePropertyNameList = new List<string> { "CreatorCode" };
        private static readonly IList<string> createTimePropertyNameList = new List<string> { "CreateTime" };
        private static readonly IList<string> lastModifyUserCodePropertyNameList = new List<string> { "LastModifierCode" };
        private static readonly IList<string> lastModifyTimePropertyNameList = new List<string> { "LastModifyTime" };
        private static readonly IList<string> ztPropertyNameList = new List<string> { "zt" };
        private static readonly IList<string> orgIdPropertyNameList = new List<string> { "OrganizeId" };
        private static readonly IList<string> topOrgIdPropertyNameList = new List<string> { "TopOrganizeId" };

        static IEntity()
        {

        }

        /// <summary>
        /// 当创建实体时，更新实体 的 主键字段、创建人员、创建时间
        /// 调用示例
        /// Guid/string主键  Create() 或 Create(true) 或 Create(true, '73FD1267-79BA-4E23-A152-744AF73117E9')
        /// 自增主键  Create()
        /// int/long非自增主键  Create() 或 Create(true, 9999)
        /// </summary>
        public void Create(bool isSetPrimaryKeyValue = false, object keyValue = null, bool isSetOrgId = false, bool isSetTopOrgId = false)
        {
            var loginInfo = OperatorProvider.GetCurrent();
            if (isSetPrimaryKeyValue)
            {
                setPropertyValue("Key", null
                    , (keyValue == null ? Guid.NewGuid().ToString() : keyValue), isPrimaryKey: true);
            }
            if (loginInfo != null)
            {
                setPropertyValue("creatorUserCode", creatorUserCodePropertyNameList, loginInfo.UserCode);
            }
            setPropertyValue("CreateTime", createTimePropertyNameList, DateTime.Now);
            setPropertyValue("zt", ztPropertyNameList, "1");
            if (isSetOrgId)
            {
                setPropertyValue("orgId", orgIdPropertyNameList, loginInfo.OrganizeId);
            }
            if (isSetTopOrgId)
            {
                setPropertyValue("topOrgId", topOrgIdPropertyNameList, Constants.TopOrganizeId);
            }
        }

        /// <summary>
        /// 当更新实体时，更新 当前实体 的 主键字段、更新人、更新时间
        /// 调用示例
        /// Modify() 、 Modify('73FD1267-79BA-4E23-A152-744AF73117E9')、 Modify(9999)
        /// </summary>
        /// <param name="keyValue">!=null更新主键字段，否则不更新主键字段</param>
        public void Modify(object keyValue = null, bool isSetOrgId = false, bool isSetTopOrgId = false)
        {
            var loginInfo = OperatorProvider.GetCurrent();
            if (keyValue != null)
            {
                setPropertyValue("Key", null, keyValue, isPrimaryKey: true);
            }
            if (loginInfo != null)
            {
                setPropertyValue("LastModifyUserCode", lastModifyUserCodePropertyNameList, loginInfo.UserCode);
            }
            setPropertyValue("LastModifyTime", lastModifyTimePropertyNameList, DateTime.Now);
            if (isSetOrgId)
            {
                setPropertyValue("orgId", orgIdPropertyNameList, loginInfo.OrganizeId);
            }
            if (isSetTopOrgId)
            {
                setPropertyValue("topOrgId", topOrgIdPropertyNameList, Constants.TopOrganizeId);
            }
        }

    }
}

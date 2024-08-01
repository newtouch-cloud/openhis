using Newtouch.Common.Operator;
using System;
using System.Collections.Generic;

namespace FrameworkBase.Domain.Entity
{
    /// <summary>
    /// 所有Entity基类
    /// 可根据自己项目特点修改
    /// 比如说项目都有CreatorCode、CreateTime、LastModifierCode、LastModifyTime、zt字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class IEntity<TEntity> : Newtouch.Infrastructure.EF.IEntity<TEntity>
    {
        private static readonly IList<string> creatorUserCodePropertyNameList = new List<string> { "CreatorCode" };
        private static readonly IList<string> createTimePropertyNameList = new List<string> { "CreateTime" };
        private static readonly IList<string> lastModifyUserCodePropertyNameList = new List<string> { "LastModifierCode" };
        private static readonly IList<string> lastModifyTimePropertyNameList = new List<string> { "LastModifyTime" };
        private static readonly IList<string> ztPropertyNameList = new List<string> { "zt" };

        /// <summary>
        /// 静态构造函数
        /// </summary>
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
        /// <param name="isSetPrimaryKeyValue"></param>
        /// <param name="keyValue"></param>
        /// <param name="autoSetZtValid">默认会尝试给zt字段赋值有效，不需要请指定false（可以重写SetZtValid方法）</param>
        public void Create(bool isSetPrimaryKeyValue = false, object keyValue = null, bool? autoSetZtValid = null)
        {
            string curUserCode = "";
            if (IEntityEx._curUserCodeGetter != null)
            {
                curUserCode = IEntityEx._curUserCodeGetter();
            }
            else
            {
                var loginInfo = OperatorProvider.GetCurrent();
                if (loginInfo != null)
                {
                    curUserCode = loginInfo.UserCode;
                }
            }
            if (isSetPrimaryKeyValue)
            {
                setPropertyValue("Key", null
                    , (keyValue == null ? Guid.NewGuid().ToString() : keyValue), isPrimaryKey: true);
            }
            if (!string.IsNullOrEmpty(curUserCode))
            {
                setPropertyValue("creatorUserCode", creatorUserCodePropertyNameList, curUserCode);
            }
            setPropertyValue("CreateTime", createTimePropertyNameList, DateTime.Now);

            SetZtValid(autoSetZtValid);
        }

        /// <summary>
        /// 设置状态有效。可以选择重写
        /// </summary>
        /// <param name="autoSetZtValid"></param>
        public virtual void SetZtValid(bool? autoSetZtValid = null)
        {
            if (autoSetZtValid ?? true)
            {
                setPropertyValue("zt", ztPropertyNameList, "1");
            }
        }

        /// <summary>
        /// 当更新实体时，更新 当前实体 的 主键字段、更新人、更新时间
        /// 调用示例
        /// Modify() 、 Modify('73FD1267-79BA-4E23-A152-744AF73117E9')、 Modify(9999)
        /// </summary>
        /// <param name="keyValue">!=null更新主键字段，否则不更新主键字段</param>
        public void Modify(object keyValue = null)
        {
            string curUserCode = "";
            if (IEntityEx._curUserCodeGetter != null)
            {
                curUserCode = IEntityEx._curUserCodeGetter();
            }
            else
            {
                var loginInfo = OperatorProvider.GetCurrent();
                if (loginInfo != null)
                {
                    curUserCode = loginInfo.UserCode;
                }
            }
            if (keyValue != null)
            {
                setPropertyValue("Key", null, keyValue, isPrimaryKey: true);
            }
            if (!string.IsNullOrEmpty(curUserCode))
            {
                setPropertyValue("LastModifyUserCode", lastModifyUserCodePropertyNameList, curUserCode);
            }
            setPropertyValue("LastModifyTime", lastModifyTimePropertyNameList, DateTime.Now);
        }

    }

    /// <summary>
    /// IEntity扩展
    /// </summary>
    public class IEntityEx
    {
        /// <summary>
        /// 用户姓名 解析器
        /// </summary>
        public static Func<string> _curUserCodeGetter { private set; get; }

        /// <summary>
        /// 注册 底层（Repo、DmnService）如何获取UserCode
        /// </summary>
        /// <param name="curUserCodeGetter"></param>
        public static void RegisterCurUserCodeGetter(Func<string> curUserCodeGetter)
        {
            _curUserCodeGetter = curUserCodeGetter;
        }
    }

}

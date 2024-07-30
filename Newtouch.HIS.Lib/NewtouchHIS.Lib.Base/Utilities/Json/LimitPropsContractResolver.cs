/*******************************************************************************
 * Copyright © 2023 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;



namespace NewtouchHIS.Lib.Base.Utilities.Json
{
    /// <summary>
    /// 内容解析
    /// </summary>
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] _retainProps = null;
        string[] _excludeProps = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="retainProps">需要保留的字段</param>
        public LimitPropsContractResolver(string[] retainProps)
        {
            //指定要序列化属性的清单
            this._retainProps = retainProps;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="retainProps">需要保留的字段</param>
        /// <param name="excludeProps">要排除的字段</param>
        public LimitPropsContractResolver(string[] retainProps, string[] excludeProps)
        {
            //指定要序列化属性的清单
            this._retainProps = retainProps;

            this._excludeProps = excludeProps;
        }

        /// <summary>
        /// 重写
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list =
            base.CreateProperties(type, memberSerialization);
            return list.Where(p =>
            {
                return (_retainProps == null || _retainProps.Length == 0 || _retainProps.Contains(p.PropertyName))            //只保留清单有列出的属性
                    && (_excludeProps == null || _excludeProps.Length == 0 || !_excludeProps.Contains(p.PropertyName))
                    ;
            }).ToList();
        }
    }
}

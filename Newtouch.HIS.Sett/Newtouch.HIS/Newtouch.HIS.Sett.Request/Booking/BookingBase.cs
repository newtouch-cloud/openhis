using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtouch.Common.Operator;
using Newtouch.HIS.API.Common;
using Newtouch.Infrastructure.Attributes;

namespace Newtouch.HIS.Sett.Request.Booking
{
    public abstract class BookingBase<TEntity>:RequestBase
    {
        public BookingBase()
        {
            
        }

        public virtual DefaultResponse FormatCheck(string method)
        {
            DefaultResponse dto = new DefaultResponse();
            dto.code = ResponseResultCode.SUCCESS;
            return FormatCheck<APIRequiredAttribute>(dto);
        }

        public string FormatMedRequest(Dictionary<string, string> reqDto,string para)
        {
            if (reqDto != null)
            {
                foreach(var item in reqDto)
                {
                    if(item.Key==para)
                    {
                        return item.Value;
                    }
                }
            }
            return null;
        }

        private DefaultResponse FormatCheck<T>(DefaultResponse resDto)
            where T : Attribute
        { 
            ForEachAttrProp<T>(this.GetType(), (prop, attr) =>
            {
                var nodeName = prop.Name;
                var propVal = prop.GetValue(this);

                if(propVal==null || propVal.ToString()=="")
                {
                    resDto.code = ResponseResultCode.ERROR;
                    resDto.msg = resDto.msg + nodeName + " 参数不可为空。";
                }
            });

            return resDto;
        }

        /// <summary>
        /// 遍历指定特性的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="ac"></param>
        /// <param name="attrTvalue"></param>
        private static void ForEachAttrProp<T>(Type type, Action<PropertyInfo, T> ac, string inoutType = null, string attrTvalue = null)
            where T : Attribute
        {
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttributes(false).Where(a => a is T).FirstOrDefault();
                if (attr != null)
                {
                    ac(prop, attr as T);
                }
            }
        }
    }
}

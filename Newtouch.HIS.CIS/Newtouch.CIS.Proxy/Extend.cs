using System;

namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class Extend
    {

        /// <summary>
        /// html编码转成html实体
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string HtmlCodingParsing(this string source)
        {
            return string.IsNullOrWhiteSpace(source) ? "" : source.Replace("&lt;", "<").Replace("&#60;", "<").Replace("&gt;", ">").Replace("&#62;", ">");
        }

        /// <summary>
        /// 提取soapXml报文return内的内容
        /// </summary>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public static string ExtractReturnContent(this string envelope)
        {
            if (string.IsNullOrWhiteSpace(envelope)) return "";
            if (envelope.IndexOf("<return>", StringComparison.Ordinal) <= -1) return envelope;
            var tmpStr = envelope.Substring(envelope.IndexOf("<return>", StringComparison.Ordinal) + 8);
            if (tmpStr.Length <= 0) return tmpStr;
            return tmpStr.IndexOf("</return>", StringComparison.Ordinal) > -1 ? tmpStr.Substring(0, tmpStr.IndexOf("</return>", StringComparison.Ordinal)) : "";
        }

        /// <summary>
        /// 提取soapXml报文return内的内容
        /// </summary>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public static string ExtractReturnContentAndParsing(this string envelope)
        {
            return envelope.ExtractReturnContent().HtmlCodingParsing();
        }

        /// <summary>
        /// 判断返回是否正确，判断依据为是否含有"<return>"
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool IsReturnSuccess(this string response)
        {
            if (string.IsNullOrWhiteSpace(response)) return false;
            var tmp = response.Replace(" ", "");
            if (string.IsNullOrWhiteSpace(tmp)) return false;
            tmp = tmp.ToLower();
            return tmp.IndexOf("<return>", StringComparison.Ordinal) > -1;
        }
    }
}
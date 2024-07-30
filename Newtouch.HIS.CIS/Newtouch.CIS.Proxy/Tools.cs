namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// 获取性别代码
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>返回中医对应的性别代码</returns>
        public static string GetGenderCode(string gender)
        {
            switch (gender.Trim())
            {
                case "男":
                case "男姓":
                case "男人":
                    return "1";
                case "女":
                case "女性":
                case "女人":
                    return "2";
                case "":
                    return "9";
                default:
                case "人妖":
                    return "0";
            }
        }

        /// <summary>
        /// 转换成中医对应的证件类型编码
        /// </summary>
        /// <param name="zjlx">证件类型</param>
        /// <returns>中医对应的证件类型编码</returns>
        public static string CardTypeCodeChange(int zjlx)
        {
            switch (zjlx)
            {
                case 1://身份证
                    return "01";
                case 2://护照
                    return "03";
                case 3://军官证
                    return "04";
                case 9://其他
                default:
                    return "99";
            }
        }

        /// <summary>
        /// 获取证件名称
        /// </summary>
        /// <param name="zjlx">cis证件类型</param>
        /// <returns>中医对应的证件类型名称</returns>
        public static string GetCardType(int zjlx)
        {
            var code = CardTypeCodeChange(zjlx);
            switch (code)
            {
                case "01":
                    return "居民身份证";
                case "02":
                    return "居民户口本";
                case "03":
                    return "护照";
                case "04":
                    return "军官证";
                case "05":
                    return "驾驶证";
                case "06":
                    return "港澳居民来往内地通行证";
                case "07":
                    return "太晚居民来往内地通行证";
                case "09":
                default:
                    return "其他法定有效证件";

            }
        }

        /// <summary>
        /// 转换成中医对应的婚姻状况编码
        /// </summary>
        /// <param name="hf"></param>
        /// <returns></returns>
        public static string ChangeMarriedCode(int hf)
        {
            switch (hf)
            {
                case 0://未婚
                    return "10";
                case 1://已婚
                    return "20";
                case 9:
                default:
                    return "90";
            }
        }

        /// <summary>
        /// 转换成中医对应的婚姻状况编码
        /// </summary>
        /// <param name="hf"></param>
        /// <returns></returns>
        public static string GetMarried(int hf)
        {
            var code = ChangeMarriedCode(hf);
            switch (code)
            {
                case "10":
                    return "未婚";
                case "20":
                    return "已婚";
                case "21":
                    return "初婚";
                case "22":
                    return "再婚";
                case "23":
                    return "复婚";
                case "30":
                    return "丧偶";
                case "40":
                    return "离婚";
                case "90":
                default:
                    return "未知";
            }
        }

        /// <summary>
        /// 本地病人性质转中医馆病人性质
        /// </summary>
        /// <param name="brxz"></param>
        /// <returns></returns>
        public static string ChangeBrxzCode(string brxz)
        {
            switch (brxz)
            {
                case "1"://本地 自费
                    return "2";// 中医馆 自费
                case "0"://本地 自费
                default:
                    return "1";// 中医馆 自费
            }
        }
    }
}
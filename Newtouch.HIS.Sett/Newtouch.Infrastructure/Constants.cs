using Newtouch.Common.Operator;
using Newtouch.Core.Redis;
using Newtouch.Infrastructure.Model;
using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                return FrameworkBase.MultiOrg.Infrastructure.ConstantsBase.TopOrganizeId;
            }
        }

        /// <summary>
        /// 应用Id
        /// </summary>
        public static string AppId
        {
            get
            {
                return FrameworkBase.MultiOrg.Infrastructure.ConstantsBase.AppId;
            }
        }

        #region 登录用户当前的药房部门信息 永不过期 不同系统之间会共享

        /// <summary>
        /// 获取当前的药剂部门（药房或药库）
        /// </summary>
        public static LoginUserCurrentYfbmModel CurrentYfbm
        {
            get
            {
                return GetCurrentYfbm(OperatorProvider.GetCurrent().UserId);
            }
        }

        /// <summary>
        /// 获取当前的药剂部门（药房或药库）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static LoginUserCurrentYfbmModel GetCurrentYfbm(string userId)
        {
            return RedisHelper.Get<LoginUserCurrentYfbmModel>(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId))
                ?? new LoginUserCurrentYfbmModel();
        }

        /// <summary>
        /// 设置当前的药剂部门（药房或药库）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCurrentYfbm(string userId, LoginUserCurrentYfbmModel value)
        {
            if (value == null)
            {
                RedisHelper.Remove(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId));
            }
            else
            {
                RedisHelper.Set(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId), value);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public struct xtzypz
        {
            /// <summary>
            /// 住院不上传医保大类
            /// </summary>
            public const string DL_BSCYB = "zy.DL_BSCYB";
            /// <summary>
            /// 一次性材料大类
            /// </summary>
            public const string YCXCLDM = "zy.YCXCLDM";
            /// <summary>
            /// 预算欠费
            /// </summary>
            public const string ZY_BRXZ_FYBL = "ZY_BRXZ_FYBL";
            /// <summary>
            /// 
            /// </summary>
            public const string ZYJFKSDM = "ZYJFKSDM";
            /// <summary>
            /// 验证医保代码的病人性质;住院收费项目中必须含有医保代码才允许收费的病人性质
            /// </summary>
            public const string ZYYBSFBRXZ = "ZYYBSFBRXZ";
            /// <summary>
            /// 大病减负
            /// </summary>
            public const string ZYYZJFXM = "ZYYZJFXM";
            /// <summary>
            /// 住院组合性质中参与医保交易的性质
            /// </summary>
            public const string ZYZHXZYBJYXZ = "ZYZHXZYBJYXZ";

            /// <summary>
            /// 根据病人性质和诊断判断，可保费用的上限，如少儿基金，第一诊断为急性xx阑尾，报销上限为1100
            /// </summary>
            public const string ZY_BRXZ_ZD_BXSX = "zy.ZY_BRXZ_ZD_BXSX";

            ///// <summary>
            ///// 干部离休病人性质住院床位费固定报销金额
            ///// </summary>
            //public const string ZYCWFTSXZBX = "zy.ZYCWFTSXZBX";


        }

        //什么范围内的费用参与自费比例的计算
        //0 可记帐部分参与计算（暂无）
        //1 可记帐＋分类自负（离休、慈善等大多数……）
        //2 可记帐＋分类自负＋自理－绝对自理（离休挂号费0.1时,绝对自理处理离休特需挂号等不可报部分－见系统收费项目.自负性质）
        //“同大类”的“所有级别”的可报范围必须一致

        /// <summary>
        /// 系统病人收费算法 的 费用范围
        /// 什么范围内的费用参与自费比例的计算
        /// </summary>
        public struct xtbrsfsffyfw
        {
            /// <summary>
            /// 
            /// </summary>
            public const string fyfw0 = "0";
            /// <summary>
            /// 
            /// </summary>
            public const string fyfw1 = "1";
            /// <summary>
            /// 
            /// </summary>
            public const string fyfw2 = "2";
            /// <summary>
            /// 
            /// </summary>
            public const string fyfw3 = "3";
        }

        public static string SiteBaseUrl = AppDomain.CurrentDomain.BaseDirectory;

        public static string ReportTemplateDirUrl = SiteBaseUrl + @"\ReportTemplate\";



        /// <summary>
        /// 系统支付方式
        /// </summary>
        public struct xtzffs
        {
            /// <summary>
            /// 现金支付
            /// </summary>
            public const string XJZF = "0";
            /// <summary>
            /// 住院预交账户支付（2018账户预交支付沿用3）
            /// </summary>
            public const string ZYYJZHZF = "3";
            /// <summary>
            /// POS机支付
            /// </summary>
            public const string POS = "1";
            /// <summary>
            /// 银行转账支付
            /// </summary>
            public const string YHZZ = "2";


        }

        //配置门诊
        public struct xtmzpz
        {
            public const string ZYTFPZ_INCLUDEYP = "mz.ZYTFPZ_INCLUDEYP";//住院退费配置：是否包含药品 0--不包含 

            //add by HLF
            public const string BRXZ_ZFJC = "BRXZ_ZFJC";//病人性质 自费家床
            public const string BRXZ_YBJC = "BRXZ_YBJC";//病人性质 医保家床
            public const string SFXM_GHGHF = "mz.SFXM_GHGHF"; //收费项目 不收挂号费 其他
                                                              //门诊有效时长(小时)：科室(ks) -1 表示所有科室；门诊/急诊(mz) -1 表示当天有效，单位小时
                                                              //例如：[{"ks":"-1","mz":"-1","jz":"24"},{"ks":"03.H1","mz":"-1","jz":"24"}]
            public const string MZ_ACTIVE_DURATION = "mz.MZ_ACTIVE_DURATION";
            public const string MZSFCX_DL = "mz.MZSFCX_DL"; //用于门诊收费查询页面的大类合计
            //public const string GH_ZL_GROUP = "mz.GH_ZL_GROUP"; //挂号组
            public const string SFXM_GHCKF = "mz.SFXM_GHCKF"; //磁卡费
            public const string SFXM_GHGBF = "mz.SFXM_GHGBF"; //工本费
            public const string SFXM_ZKD = "SFXM_ZKD";
            public const string DL_BSCYB = "mz.DL_BSCYB";
            public const string SFXM_FWF = "mz.SFXM_FWF";//收费项目服务费
            public const string DL_NOPRINTMX = "mz.DL_NOPRINTMX";//不打印明细大类
            public const string GHLX_ZJGH = "mz.GHLX_ZJGH"; //专家挂号
            public const string S20100 = "mz.S20100"; //门诊排班作息时间
        }

        #region 门诊退费
        public struct ybjylx
        {
            public const string ybjylx0 = "0";
            public const string ybjylx1 = "1";
            public const string ybjylx2 = "2";
            public const string ybjylx3 = "3";
            public const string ybjylx4 = "4";
            public const string ybjylx5 = "5";
            public const string ybjylx6 = "6";
        }
        public enum ybDealLB
        {
            yb_deal_wjy = 0,
            yb_deal_mzgh = 1,
            yb_deal_mzsf = 2,
            yb_deal_dbgh = 3,
            yb_deal_dbsf = 4,
            yb_deal_jcsf = 5,
            yb_deal_zysf = 6,
            yb_deal_gsgh = 7,
            yb_deal_gsmz = 8,
            yb_deal_sssf = 9,
            nb_yb_fyjs_mz_T1 = 10,
            nb_yb_bc_ztjs = 11,
            nb_yb_fyjs_zy_T1 = 12,
            yb_deal_gszy = 13,
            yb_deal_jcjz = 14,
        }
        public enum jsztEnum
        {
            YJ = 1,
            YT = 2,
            BS = 3,
            BSYT = 4,
        }
        public enum zfxzEnum
        {
            KB = 0,
            ZF = 1,
            FLZF = 2,
            JDZF = 3,
        }
        public struct fyfw
        {
            public const string fyfw0 = "0";
            public const string fyfw1 = "1";
            public const string fyfw2 = "2";
            public const string fyfw3 = "3";
        }
        public enum sfZfxzEnum
        {
            ZF = 0,
            ZL = 1,
        }
        #endregion

        #region 系统收费分类
        public struct xtsffl
        {
            public const string fzlfl = "FZLFL"; //门诊排班作息时间
        }
        #endregion

        public static DateTime MinDate = new DateTime(1970, 01, 01);
        public static DateTime MaxDate = new DateTime(2099, 12, 31);
    }
}

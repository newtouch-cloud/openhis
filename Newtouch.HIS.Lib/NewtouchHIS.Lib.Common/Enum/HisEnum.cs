using System.ComponentModel;

namespace NewtouchHIS.Lib.Common
{
    /// <summary>
    /// HIS 通用枚举 
    /// 可用于与第三方交互转换
    /// </summary>
    public static class HisEnum
    {
        /// <summary>
        /// 访问终端类型
        /// </summary>
        public enum MediaTerminalType
        {
            [Description("电脑浏览器")]
            Browser = 0,
            [Description("手机移动端")]
            Mobile = 1,
        }

        public enum EnumMarriageStu
        {
            [Description("未婚")]
            unmarried =1,
            [Description("已婚")]
            married = 2,
            [Description("不详")]
            unknown = 3,

        }
        /// <summary>
        /// 血糖测量方式
        /// </summary>
        public enum EnumXtclfs
        {
            [Description("空腹血糖")]
            xuetang_kf = 1,
            [Description("餐后2小时")]
            xuetang_ch = 2,
            [Description("随机")]
            xuetang_sj,
        }
        /// <summary>
        /// 病人性质
        /// 定义同内部：NewtouchHIS.Domain.Enum.EnumBrxz
        /// </summary>
        public enum EnumBrxz
        {
            /// <summary>
            /// 自费
            /// </summary>
            [Description("自费")]
            zf = 0,
            /// <summary>
            /// 职工医保
            /// </summary>
            [Description("职工医保")]
            zg = 1,
            /// <summary>
            /// 居民医保
            /// </summary>
            [Description("居民医保")]
            jm = 2,
            /// <summary>
            /// 离休
            /// </summary>
            [Description("离休")]
            lx = 3,
            /// <summary>
            /// 普通医保
            /// </summary>
            [Description("普通医保")]
            pt = 11,
        }
        /// <summary>
        /// 远程诊疗状态
        /// </summary>
        public enum EmunRemoteTreatedStu
        {
            [Description("待确认")]
            dqr = 1,
            [Description("就诊中")]
            jzz = 2,
            [Description("就诊结束")]
            yjs = 3,
            [Description("已驳回")]
            yth = 4,
            [Description("已撤销")]
            ycx = 5,
            [Description("已发药")]
            yfy = 6,
        }

        #region 药品相关字典
        /// <summary>
        /// 中药用法
        /// </summary>
        public enum EnumTCMUsage
        {
            [Description("先煎")]
            XJ = 20001,
            [Description("后下")]
            HX = 20002,
            [Description("包煎")]
            BJ = 20003,
            [Description("烊化")]
            YH = 20004,
            [Description("打碎")]
            DS = 20005,
            [Description("足浴")]
            ZY = 20006,
            [Description("另包")]
            LB = 20007,
            [Description("水煎服")]
            SJF = 20011,            
            [Description("外敷")]
            WF = 20603,
            [Description("煎药机煎药")]
            JYJ = 20604,
            [Description("冲服")]
            XWZS = 20620,
            [Description("冲服")]
            CF = 20700,
            [Description("泡服")]
            PF = 20701,
        }
       
        #endregion


        #region 住院
        /// <summary>
        /// 在院标志
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// 入院登记
            /// </summary>
            Xry = 0,
            /// <summary>
            /// 病区中
            /// </summary>
            Bqz = 1,
            /// <summary>
            /// 病区出院（出病区）（待结账）
            /// </summary>
            Djz = 2,
            /// <summary>
            /// 已出院（出院结算）
            /// </summary>
            Ycy = 3,
            /// <summary>
            /// 转区
            /// </summary>
            Zq = 7,

            /// <summary>
            /// 作废记录/取消入院
            /// </summary>
            Wry = 9,
        }
        #endregion


        #region CIS
        /// <summary>
        /// 处方类型
        /// </summary>
        public enum EnumCflx
        {
            /// <summary>
            /// 西药处方
            /// </summary>
            [Description("西药处方")]
            WMPres = 1,
            /// <summary>
            /// 中药处方
            /// </summary>
            [Description("中药处方")]
            TCMPres = 2,
            /// <summary>
            /// 康复处方
            /// </summary>
            [Description("康复处方")]
            RehabPres = 3,
            /// <summary>
            /// 检验处方
            /// </summary>
            [Description("检验处方")]
            InspectionPres = 4,
            /// <summary>
            /// 检查处方
            /// </summary>
            [Description("检查处方")]
            ExaminationPres = 5,
            /// <summary>
            /// 常规项目处方
            /// </summary>
            [Description("常规项目处方")]
            RegularItemPres = 6,

            [Description("电子处方")]
            Dzcf = 7,

        }

        public enum EnumYpyf
        {
            [Description("西医")]
            WM = 1,
            [Description("中医")]
            TCM = 2
        }

        /// <summary>
        /// 诊断类型
        /// </summary>
        public enum EnumZdlx
        {
            /// <summary>
            /// 主诊断
            /// </summary>
            [Description("主诊断")]
            Main = 1,
            /// <summary>
            /// 副诊断
            /// </summary>
            [Description("副诊断")]
            Deputy = 2
        }
        #endregion
    }
}

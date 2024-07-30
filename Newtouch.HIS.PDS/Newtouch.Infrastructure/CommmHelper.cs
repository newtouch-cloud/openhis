using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.Tools;
using System;
using System.Text.RegularExpressions;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class CommmHelper
    {
        /// <summary>
        /// 收费项目 治疗量 换算成 数量
        /// </summary>
        /// <param name="zll"></param>
        /// <param name="dwjls"></param>
        /// <param name="jjcl">枚举EnumSfxmJjcl</param>
        /// <param name="throwExWhenZero">当数量结果为0时 是否抛异常，默认抛</param>
        /// <returns></returns>
        public static int CalcSfxmSl(int? zll, int? dwjls, int? jjcl,
            bool throwExWhenZero = true)
        {
            if ((zll ?? 0) <= 0)
            {
                throw new FailedException("ZLL_IS_REQUIRED", "缺少治疗量");
            }
            if ((dwjls ?? 0) <= 0)
            {
                throw new FailedException("DWJLS_IS_REQUIRED", "缺少单位计量数");
            }
            if ((jjcl ?? 0) <= 0)
            {
                throw new FailedException("JJCL_IS_REQUIRED", "缺少计价策略");
            }
            var res = zll.Value / dwjls.Value;
            if (res <= 0 && throwExWhenZero)
            {
                throw new FailedException("SL_ZERO_IS_NOTALLOWED", "不允许0数量");
            }
            return res;
        }

        /// <summary>
        /// 导出到指定目录
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filepaths">指定盘 例如："D:\\"</param>
        /// <returns></returns>
        public static string GetLocalFilePath(string path,string filepaths)
        {
            //var configFileExportBaseDir = "D:\\";//ConfigurationHelper.GetAppConfigValue("LocalFileBaseDir");
            if (string.IsNullOrWhiteSpace(filepaths))
            {
                filepaths = "D:\\";
            }
            var filePath = (filepaths + path).Replace("\\\\", "\\");

            var iIndex = filePath.LastIndexOf("\\");
            var dirPath = filePath.Substring(0, iIndex);
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }

            return filePath;
        }

        /// <summary>
        /// 获取本地文件对应的网络访问路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetLocalFileNetUrl(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }
            path = path.Replace("\\", "/");

            if (!path.Contains(":"))
            {

                var baseUrl = ConfigurationHelper.GetAppConfigValue("LocalFileBaseNetUrl");
                return (baseUrl.EndsWith("/") ? baseUrl : (baseUrl + "/"))
                    + ("LocalFile/Download?path=" + path.EnBase64()).Replace("//", "/");
            }
            else
            {
                throw new FailedException("PATH_NOT_VALID", "PATH_NOT_VALID");
            }
        }

        #region 身份证校验
        /// <summary>  
        /// 验证身份证合理性  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public static bool CheckIDCard(string idNumber)
        {
            if (idNumber.Length == 18)
            {
                bool check = CheckIDCard18(idNumber);
                return check;
            }
            else if (idNumber.Length == 15)
            {
                bool check = CheckIDCard15(idNumber);
                return check;
            }
            else
            {
                return false;
            }
        }


        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard18(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准  
        }


        /// <summary>  
        /// 16位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard15(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            return true;
        }
        #endregion

        #region 汉字拼音
        //定义拼音区编码数组
        private static int[] getValue = new int[]
            {
        -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
        -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
        -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
        -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
        -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
        -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
        -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
        -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
        -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
        -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
        -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
        -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
        -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
        -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
        -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
        -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
        -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
        -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
        -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
        -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
        -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
        -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
        -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
        -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
        -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
        -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
        -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
        -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
        -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
        -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
        -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
        -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
        -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
            };
        //定义拼音数组
        private static string[] getName = new string[]
            {
        "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
        "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
        "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
        "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
        "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
        "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
        "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
        "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
        "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
        "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
        "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
        "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
        "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
        "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
        "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
        "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
        "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
        "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
        "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
        "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
        "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
        "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
        "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
        "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
        "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
        "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
        "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
        "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
        "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
        "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
        "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
        "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
        "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
           };
        /// <summary>汉字转换成全拼的拼音</summary>
        /// <param name="Chstr">汉字字符串</param>
        /// <returns>转换后的拼音字符串</returns> 
        public static string StrConvertToPinyin(string Chstr,string type)
        {
            Regex reg = new Regex("^[\u4e00-\u9fa5]$");//验证是否输入汉字
            byte[] arr = new byte[2];
            string pystr = "";
            int asc = 0, M1 = 0, M2 = 0;
            char[] mChar = Chstr.ToCharArray();//获取汉字对应的字符数组
            for (int j = 0; j < mChar.Length; j++)
            {
                //如果输入的是汉字
                if (reg.IsMatch(mChar[j].ToString()))
                {
                    arr = System.Text.Encoding.Default.GetBytes(mChar[j].ToString());
                    M1 = (short)(arr[0]);
                    M2 = (short)(arr[1]);
                    asc = M1 * 256 + M2 - 65536;
                    if (asc > 0 && asc < 160)
                    {
                        pystr += mChar[j];
                    }
                    else
                    {
                        switch (asc)
                        {
                            case -9254:
                                pystr += "Zhen"; break;
                            case -8985:
                                pystr += "Qian"; break;
                            case -5463:
                                pystr += "Jia"; break;
                            case -8274:
                                pystr += "Ge"; break;
                            case -5448:
                                pystr += "Ga"; break;
                            case -5447:
                                pystr += "La"; break;
                            case -4649:
                                pystr += "Chen"; break;
                            case -5436:
                                pystr += "Mao"; break;
                            case -5213:
                                pystr += "Mao"; break;
                            case -3597:
                                pystr += "Die"; break;
                            case -5659:
                                pystr += "Tian"; break;
                            default:
                                for (int i = (getValue.Length - 1); i >= 0; i--)
                                {
                                    if (getValue[i] <= asc) //判断汉字的拼音区编码是否在指定范围内
                                    {
                                        if (type == "1")
                                        {
                                            pystr += getName[i].Substring(0, 1).ToUpper();
                                        }
                                        else
                                        {
                                            pystr += getName[i];//如果不超出范围则获取对应的拼音
                                        }
                                        break;
                                    }
                                }
                                break;
                        }
                    }
                }
                else    //如果不是汉字
                {
                    pystr += mChar[j].ToString();//如果不是汉字则返回
                }
            }
            return pystr;//返回获取到的汉字拼音
        }

        /////   <summary> 
        /////   得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母 
        /////   </summary> 
        /////   <param   name="CnChar">单个汉字</param> 
        /////   <returns>单个大写字母</returns> 
        //private static string GetCharSpellCode(string CnChar)
        //{
        //    long iCnChar;
        //    byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

        //    //如果是字母，则直接返回 
        //    if (ZW.Length == 1)
        //    {
        //        return CnChar.ToUpper();
        //    }
        //    else
        //    {
        //        //   get   the     array   of   byte   from   the   single   char    
        //        int i1 = (short)(ZW[0]);
        //        int i2 = (short)(ZW[1]);
        //        iCnChar = i1 * 256 + i2;
        //    }
        //    #region table   of   the   constant   list
        //    //expresstion 
        //    //table   of   the   constant   list 
        //    // 'A';           //45217..45252 
        //    // 'B';           //45253..45760 
        //    // 'C';           //45761..46317 
        //    // 'D';           //46318..46825 
        //    // 'E';           //46826..47009 
        //    // 'F';           //47010..47296 
        //    // 'G';           //47297..47613

        //    // 'H';           //47614..48118 
        //    // 'J';           //48119..49061 
        //    // 'K';           //49062..49323 
        //    // 'L';           //49324..49895 
        //    // 'M';           //49896..50370 
        //    // 'N';           //50371..50613 
        //    // 'O';           //50614..50621 
        //    // 'P';           //50622..50905 
        //    // 'Q';           //50906..51386

        //    // 'R';           //51387..51445 
        //    // 'S';           //51446..52217 
        //    // 'T';           //52218..52697 
        //    //没有U,V 
        //    // 'W';           //52698..52979 
        //    // 'X';           //52980..53640 
        //    // 'Y';           //53689..54480 
        //    // 'Z';           //54481..55289 
        //    #endregion
        //    //   iCnChar match     the   constant 
        //    if ((iCnChar >= 45217) && (iCnChar <= 45252))
        //    {
        //        return "A";
        //    }
        //    else if ((iCnChar >= 45253) && (iCnChar <= 45760))
        //    {
        //        return "B";
        //    }
        //    else if ((iCnChar >= 45761) && (iCnChar <= 46317))
        //    {
        //        return "C";
        //    }
        //    else if ((iCnChar >= 46318) && (iCnChar <= 46825))
        //    {
        //        return "D";
        //    }
        //    else if ((iCnChar >= 46826) && (iCnChar <= 47009))
        //    {
        //        return "E";
        //    }
        //    else if ((iCnChar >= 47010) && (iCnChar <= 47296))
        //    {
        //        return "F";
        //    }
        //    else if ((iCnChar >= 47297) && (iCnChar <= 47613))
        //    {
        //        return "G";
        //    }
        //    else if ((iCnChar >= 47614) && (iCnChar <= 48118))
        //    {
        //        return "H";
        //    }
        //    else if ((iCnChar >= 48119) && (iCnChar <= 49061))
        //    {
        //        return "J";
        //    }
        //    else if ((iCnChar >= 49062) && (iCnChar <= 49323))
        //    {
        //        return "K";
        //    }
        //    else if ((iCnChar >= 49324) && (iCnChar <= 49895))
        //    {
        //        return "L";
        //    }
        //    else if ((iCnChar >= 49896) && (iCnChar <= 50370))
        //    {
        //        return "M";
        //    }

        //    else if ((iCnChar >= 50371) && (iCnChar <= 50613))
        //    {
        //        return "N";
        //    }
        //    else if ((iCnChar >= 50614) && (iCnChar <= 50621))
        //    {
        //        return "O";
        //    }
        //    else if ((iCnChar >= 50622) && (iCnChar <= 50905))
        //    {
        //        return "P";
        //    }
        //    else if ((iCnChar >= 50906) && (iCnChar <= .51386))
        //    {
        //        return "Q";
        //    }
        //    else if ((iCnChar >= 51387) && (iCnChar <= 51445))
        //    {
        //        return "R";
        //    }
        //    else if ((iCnChar >= 51446) && (iCnChar <= 52217))
        //    {
        //        return "S";
        //    }
        //    else if ((iCnChar >= 52218) && (iCnChar <= 52697))
        //    {
        //        return "T";
        //    }
        //    else if ((iCnChar >= 52698) && (iCnChar <= 52979))
        //    {
        //        return "W";
        //    }
        //    else if ((iCnChar >= 52980) && (iCnChar <= 53640))
        //    {
        //        return "X";
        //    }
        //    else if ((iCnChar >= 53689) && (iCnChar <= 54480))
        //    {
        //        return "Y";
        //    }
        //    else if ((iCnChar >= 54481) && (iCnChar <= 55289))
        //    {
        //        return "Z";
        //    }
        //    else return ("?");
        //}
        #endregion

    }
}

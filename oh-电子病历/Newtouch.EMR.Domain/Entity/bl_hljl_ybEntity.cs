using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Newtouch.EMR.Domain.Entity
{

    [Table("bl_hljl_yb")]
    public  class bl_hljl_ybEntity : IEntity<bl_hljl_ybEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string rq { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string sj { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 护理等级 1 特级 2 一级 3 二级 4 三级
        /// </summary>
        public string hldj { get; set; }
        /// <summary>
        /// 护理类型 1基础护理 2特殊护理 3辩证施护 4其他
        /// </summary>
        public string hllx { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public string tz { get; set; }
        /// <summary>
        /// 体温
        /// </summary>
        public string tw { get; set; }
        /// <summary>
        /// 呼吸频率
        /// </summary>
        public string hxpl { get; set; }
        /// <summary>
        /// 脉率
        /// </summary>
        public string ml { get; set; }
        /// <summary>
        /// 收缩压
        /// </summary>
        public string ssy { get; set; }
        /// <summary>
        /// 舒张压
        /// </summary>
        public string szy { get; set; }
        /// <summary>
        /// 血氧饱和度
        /// </summary>
        public string xybhd { get; set; }
        /// <summary>
        /// 吸氧
        /// </summary>
        public string xy { get; set; }
        /// <summary>
        /// 足背动脉搏动
        /// </summary>
        public string zbdmbz { get; set; }
        /// <summary>
        /// 饮食情况 1良好 2一般 3较差
        /// </summary>
        public string ysqk { get; set; }
        /// <summary>
        /// 导管护理描述
        /// </summary>
        public string dghl { get; set; }
        /// <summary>
        /// 气管护理描述
        /// </summary>
        public string qghl { get; set; }
        /// <summary>
        /// 体位护理
        /// </summary>
        public string twhl { get; set; }
        /// <summary>
        /// 皮肤护理
        /// </summary>
        public string pfhl { get; set; }
        /// <summary>
        /// 营养护理
        /// </summary>
        public string yyhl { get; set; }
        /// <summary>
        /// 饮食指导 01普通饮食 02软食 03半流食 04流食 05禁食 06禁食水 07鼻饲饮食 08低盐低脂饮食 09糖尿病饮食 99其他
        /// </summary>
        public string yszd { get; set; }
        /// <summary>
        /// 心理护理 1根据病人心理状况实施心理护理 2家属心理支持
        /// </summary>
        public string xlhl { get; set; }
        /// <summary>
        /// 安全护理 1勤巡视病房 2加床档 3约束四肢
        /// </summary>
        public string aqhl { get; set; }
        /// <summary>
        /// 简要病情
        /// </summary>
        public string jybq { get; set; }
        /// <summary>
        /// 护理观察项目名称
        /// </summary>
        public string hlgcxm { get; set; }
        /// <summary>
        /// 护理观察结果
        /// </summary>
        public string hlgcjg { get; set; }
        /// <summary>
        /// 护理操作名称
        /// </summary>
        public string hlczmc { get; set; }
        /// <summary>
        /// 护理操作项目类目名称
        /// </summary>
        public string hlczxmmc { get; set; }
        /// <summary>
        /// 护理观察结果
        /// </summary>
        public string hlczjg { get; set; }
        /// <summary>
        /// 发出手术安全核对表
        /// </summary>
        public string fcssaqb { get; set; }
        /// <summary>
        /// 收回手术安全核对表
        /// </summary>
        public string shssaqb { get; set; }
        /// <summary>
        /// 发出手术风险评估表
        /// </summary>
        public string fcssfxpgb { get; set; }
        /// <summary>
        /// 收回手术风险评估表
        /// </summary>
        public string shssfxpgb { get; set; }
        /// <summary>
        /// 隔离标志
        /// </summary>
        public string glbz { get; set; }
        /// <summary>
        /// 隔离种类 1消化道隔离 2呼吸道隔离 3接触隔离 4血液-体液隔离 5严密隔离 6昆虫隔离 7保护性隔离
        /// </summary>
        public string glzl { get; set; }
        /// <summary>
        /// 护士签名
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// 护士签名时间 
        /// </summary>
        public string hsqmrqsj { get; set; }
        /// <summary>
        /// 意识
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 瞳孔-左
        /// </summary>
        public string tk_z { get; set; }
        /// <summary>
        /// 瞳孔-右
        /// </summary>
        public string tk_y { get; set; }
        /// <summary>
        /// 对光反应-左
        /// </summary>
        public string dgfs_z { get; set; }
        /// <summary>
        /// 对光反应-右
        /// </summary>
        public string dgfs_y { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
    }
}

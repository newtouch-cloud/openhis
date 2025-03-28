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
        /// ����
        /// </summary>
        public string rq { get; set; }
        /// <summary>
        /// ʱ��
        /// </summary>
        public string sj { get; set; }
        /// <summary>
        /// סԺ��
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// ����ȼ� 1 �ؼ� 2 һ�� 3 ���� 4 ����
        /// </summary>
        public string hldj { get; set; }
        /// <summary>
        /// �������� 1�������� 2���⻤�� 3��֤ʩ�� 4����
        /// </summary>
        public string hllx { get; set; }
        /// <summary>
        /// ����ʷ
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string tz { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string tw { get; set; }
        /// <summary>
        /// ����Ƶ��
        /// </summary>
        public string hxpl { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string ml { get; set; }
        /// <summary>
        /// ����ѹ
        /// </summary>
        public string ssy { get; set; }
        /// <summary>
        /// ����ѹ
        /// </summary>
        public string szy { get; set; }
        /// <summary>
        /// Ѫ�����Ͷ�
        /// </summary>
        public string xybhd { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string xy { get; set; }
        /// <summary>
        /// �㱳��������
        /// </summary>
        public string zbdmbz { get; set; }
        /// <summary>
        /// ��ʳ��� 1���� 2һ�� 3�ϲ�
        /// </summary>
        public string ysqk { get; set; }
        /// <summary>
        /// ���ܻ�������
        /// </summary>
        public string dghl { get; set; }
        /// <summary>
        /// ���ܻ�������
        /// </summary>
        public string qghl { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        public string twhl { get; set; }
        /// <summary>
        /// Ƥ������
        /// </summary>
        public string pfhl { get; set; }
        /// <summary>
        /// Ӫ������
        /// </summary>
        public string yyhl { get; set; }
        /// <summary>
        /// ��ʳָ�� 01��ͨ��ʳ 02��ʳ 03����ʳ 04��ʳ 05��ʳ 06��ʳˮ 07������ʳ 08���ε�֬��ʳ 09������ʳ 99����
        /// </summary>
        public string yszd { get; set; }
        /// <summary>
        /// ������ 1���ݲ�������״��ʵʩ������ 2��������֧��
        /// </summary>
        public string xlhl { get; set; }
        /// <summary>
        /// ��ȫ���� 1��Ѳ�Ӳ��� 2�Ӵ��� 3Լ����֫
        /// </summary>
        public string aqhl { get; set; }
        /// <summary>
        /// ��Ҫ����
        /// </summary>
        public string jybq { get; set; }
        /// <summary>
        /// ����۲���Ŀ����
        /// </summary>
        public string hlgcxm { get; set; }
        /// <summary>
        /// ����۲���
        /// </summary>
        public string hlgcjg { get; set; }
        /// <summary>
        /// �����������
        /// </summary>
        public string hlczmc { get; set; }
        /// <summary>
        /// ���������Ŀ��Ŀ����
        /// </summary>
        public string hlczxmmc { get; set; }
        /// <summary>
        /// ����۲���
        /// </summary>
        public string hlczjg { get; set; }
        /// <summary>
        /// ����������ȫ�˶Ա�
        /// </summary>
        public string fcssaqb { get; set; }
        /// <summary>
        /// �ջ�������ȫ�˶Ա�
        /// </summary>
        public string shssaqb { get; set; }
        /// <summary>
        /// ������������������
        /// </summary>
        public string fcssfxpgb { get; set; }
        /// <summary>
        /// �ջ���������������
        /// </summary>
        public string shssfxpgb { get; set; }
        /// <summary>
        /// �����־
        /// </summary>
        public string glbz { get; set; }
        /// <summary>
        /// �������� 1���������� 2���������� 3�Ӵ����� 4ѪҺ-��Һ���� 5���ܸ��� 6������� 7�����Ը���
        /// </summary>
        public string glzl { get; set; }
        /// <summary>
        /// ��ʿǩ��
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// ��ʿǩ��ʱ�� 
        /// </summary>
        public string hsqmrqsj { get; set; }
        /// <summary>
        /// ��ʶ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// ͫ��-��
        /// </summary>
        public string tk_z { get; set; }
        /// <summary>
        /// ͫ��-��
        /// </summary>
        public string tk_y { get; set; }
        /// <summary>
        /// �ԹⷴӦ-��
        /// </summary>
        public string dgfs_z { get; set; }
        /// <summary>
        /// �ԹⷴӦ-��
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

using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.EMR.Domain.Entity
{

    [Table("bl_bllx")]
    public  class bl_bllxEntity : IEntity<bl_bllxEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// �������ͱ�ʶ
        /// </summary>
        public string bllx { get; set; }
        public string bllxmc { get; set; }       
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// ������λ
        /// </summary>
        public string RelDutys { get; set; }
        /// <summary>
        /// �������ͼ���
        /// </summary>
        public string bllxcode { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// �������ݱ� Ϊ��ʱĬ��
        /// </summary>
        public string relTB { get; set; }
        /// <summary>
        /// Ŀ¼�㼶 ��ʼ�㼶1
        /// </summary>
        public string MenuLev { get; set; }
        /// <summary>
        /// Ŀ¼�㼶���� Ĭ��һ��ʱ�� �ԡ��ָ�
        /// </summary>
        public string MenuLevName { get; set; }
        public string ParentId { get; set; }
        public string IsRoot { get; set; }
        /// <summary>
        /// ����ģ���ʶ 1 ���� 
        /// </summary>
        public string mzbz { get; set; }
    }
}

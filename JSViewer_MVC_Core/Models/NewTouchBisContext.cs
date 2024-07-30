using System;
using System.Configuration;

using JSViewer_MVC_Core.Code;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JSViewer_MVC_Core.Models
{
    public partial class NewTouchBisContext : DbContext
    {
        public NewTouchBisContext()
        {
        }

        public NewTouchBisContext(DbContextOptions<NewTouchBisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SysReportTemplate> SysReportTemplate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                var a = AppSettingHelper.ReadAppSettings("ConnectionStrings", "TemplateConnection");
                //var ddc = ConfigurationManager.ConnectionStrings["TemplateConnection"].ToString();
               
                optionsBuilder.UseSqlServer(AppSettingHelper.ReadAppSettings("ConnectionStrings", "TemplateConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SysReportTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateId)
                    .HasName("PK_SYS_REPORTTEMPLATE");

                entity.ToTable("Sys_ReportTemplate");

                entity.HasComment("系统_报告模板设置");

                entity.Property(e => e.TemplateId)
                    .HasColumnName("TemplateID")
                    .HasComment("模板ID");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建日期");

                entity.Property(e => e.DataSourceType).HasComment("数据源类型（1、Sql语句 2、存储过程）");

                entity.Property(e => e.DefaultDataSource).HasComment("默认数据源");

                entity.Property(e => e.HospitalCode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("医院代码");

                entity.Property(e => e.LastTemplateContent).HasComment("上一次模板内容");

                entity.Property(e => e.OperCode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("操作员代码");

                entity.Property(e => e.OperName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("操作员名称");

                entity.Property(e => e.ReportNameSuffix)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('rdlx')");

                entity.Property(e => e.ReportType).HasDefaultValueSql("((0))");

                entity.Property(e => e.TemplateCode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("模板代码");

                entity.Property(e => e.TemplateContent)
                    .IsRequired()
                    .HasComment("模板内容");

                entity.Property(e => e.TemplateDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("模板描述");

                entity.Property(e => e.TemplateEnName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TemplateType).HasComment("模板类型（1、Stimusoft模板  2、FastReport模板）");

                entity.Property(e => e.UpdateDateTime)
                    .HasColumnType("datetime")
                    .HasComment("修改日期");

                entity.Property(e => e.UpdateOperCode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("修改人代码");

                entity.Property(e => e.UpdateOperName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("修改人名称");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

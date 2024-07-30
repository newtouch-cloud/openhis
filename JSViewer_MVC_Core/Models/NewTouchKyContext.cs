using System.Configuration;

using JSViewer_MVC_Core.Code;
using JSViewer_MVC_Core.Models.OpenSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JSViewer_MVC_Core.Models
{
    public partial class NewTouchKyContext : DbContext
    {
        public NewTouchKyContext()
        {
        }

        public NewTouchKyContext(DbContextOptions<NewTouchKyContext> options)
           : base(options)
        {
        }

        public virtual DbSet<SysReport> SysReport { get; set; }
        public virtual DbSet<Models.OpenSource.SysReportTemplate> SysReportTemplate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

                var a = AppSettingHelper.ReadAppSettings("ConnectionStrings", "KyTemplateConnection");
                //var ddc = ConfigurationManager.ConnectionStrings["KyTemplateConnection"].ToString();
                optionsBuilder.UseSqlServer(AppSettingHelper.ReadAppSettings("ConnectionStrings", "KyTemplateConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysReport>(entity =>
            {
                entity.HasKey(e => e.ReportID)
                    .HasName("PK_SYS_REPORT");

                entity.ToTable("Sys_Report");

                entity.HasComment("系统_模板主表");
            });
            modelBuilder.Entity<Models.OpenSource.SysReportTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateID)
                    .HasName("PK_SYS_REPORTTEMPLATE");

                entity.ToTable("Sys_ReportTemplate");

                entity.HasComment("系统_模板明细表");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

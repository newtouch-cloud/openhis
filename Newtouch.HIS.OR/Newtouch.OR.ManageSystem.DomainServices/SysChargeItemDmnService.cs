using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.Tools;

namespace Newtouch.OR.ManageSystem.DomainServices
{
    public class SysChargeItemDmnService : DmnServiceBase, ISysChargeItemDmnService
    {

        public SysChargeItemDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        /// <summary>
        /// 收费模板页面index list
        /// </summary>
        /// <returns></returns>
        public IList<SysChargeTemplateGridVO> Search(Pagination pagination, string keyword,string ks, string organizeId)
        {
            var sql = @"SELECT  SUM(ISNULL(sfxm.dj, '0.00') * ISNULL(xm.sl, 0)) zje ,
        mb.sfmb ,
        mb.sfmbbh ,
        mb.sfmbmc ,
        mb.mzzybz ,
        mb.zt ,
        mb.ks ,
        mb.CreateTime
FROM    [NewtouchHIS_Sett].dbo.xt_sfmb(NOLOCK) mb
       LEFT JOIN [NewtouchHIS_Sett].dbo.xt_sfmbxm xm ON xm.sfmbbh = mb.sfmbbh AND xm.OrganizeId = mb.OrganizeId
        LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm AND sfxm.OrganizeId = xm.OrganizeId
WHERE mb.OrganizeId = @orgId and mb.zt='1' and mb.ks=@ks
and exists(select 1 from [NewtouchHIS_Base].dbo.V_S_Sys_Department d with(nolock) where mb.ks=d.code  and mb.organizeid =d.organizeid and d.zt='1')";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", organizeId));
            pars.Add(new SqlParameter("@ks", ks));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (sfmbmc like @searchkeyword or sfmb like @searchkeyword)";
                pars.Add(new SqlParameter("@searchkeyword", "%" + keyword + "%"));
            }
            sql += @" GROUP BY mb.sfmbbh ,
                        mb.sfmb ,
                        mb.sfmbmc ,
                        mb.mzzybz ,
                        mb.zt ,
                        mb.ks ,
                        mb.CreateTime";
            return this.QueryWithPage<SysChargeTemplateGridVO>(sql, pagination, pars.ToArray());
        }

        public void ChargeTemplate_SubmitForm(SysChargeTemplateDto entity, string xmListStr,string user)
        {
            string sql = "";
            if (!string.IsNullOrWhiteSpace(entity.sfmbbh))
            {
                sql = @"select sfmbbh
                from[NewtouchHIS_Sett].dbo.xt_sfmb with(nolock)
                where OrganizeId = @orgId and zt = '1' and sfmb = @sfmb and sfmbbh <> @sfmbbh ";

                var ety = this.FindList<SysChargeTemplateGridVO>(sql, new SqlParameter[]
                {
                    new SqlParameter("@orgId", entity.OrganizeId),
                    new SqlParameter("@sfmb", entity.sfmb),
                    new SqlParameter("@sfmbbh", entity.sfmbbh)
                });
                if (ety != null && ety.Count>0)
                {
                    throw new FailedException("编码不可重复");
                }

                sql = "update a set  a.sfmb = @sfmb,a.sfmbmc=@sfmbmc,a.ks=@ks,a.LastModifyTime=getdate(),a.LastModifierCode=@user,a.zt=@zt " +
                      "from [NewtouchHIS_Sett].dbo.xt_sfmb a " +
                      "where a.OrganizeId = @orgId and a.zt = '1' and a.sfmbbh = @sfmbbh ";
                this.ExecuteSqlCommand(sql, new SqlParameter[]
                {
                    new SqlParameter("@sfmbbh", entity.sfmbbh),
                    new SqlParameter("@orgId", entity.OrganizeId),
                    new SqlParameter("@sfmb", entity.sfmb),
                    new SqlParameter("@sfmbmc", entity.sfmbmc),
                    new SqlParameter("@user", user),
                    new SqlParameter("@ks", entity.ks),
                    new SqlParameter("@zt", entity.zt ?? "1")
                });

                sql = @"update a
                set a.zt = '0',a.LastModifyTime=getdate(),a.LastModifierCode=@user
                from[NewtouchHIS_Sett].dbo.xt_sfmbxm  a
                    where a.OrganizeId = @orgId and a.sfmbbh = @sfmbbh and a.zt = '1'";
                this.ExecuteSqlCommand(sql, new SqlParameter[]
                {
                    new SqlParameter("@sfmbbh", entity.sfmbbh),
                    new SqlParameter("@orgId", entity.OrganizeId),
                    new SqlParameter("@user", user)
                });

            }
            else
            {
                string sfmbbh = Comm.GuId();
                entity.sfmbbh = sfmbbh;

                sql = @"select sfmbbh
                from[NewtouchHIS_Sett].dbo.xt_sfmb with(nolock)
                where OrganizeId = @orgId and zt = '1' and sfmb = @sfmb ";
                var mbety = this.FindList<SysChargeTemplateGridVO>(sql, new SqlParameter[]
                {
                    new SqlParameter("@orgId", entity.OrganizeId),
                    new SqlParameter("@sfmb", entity.sfmb)
                });
                if (mbety != null && mbety.Count > 0)
                {
                    throw new FailedException("编码不可重复");
                }

                sql = @"insert into [NewtouchHIS_Sett].dbo.xt_sfmb([sfmbbh],[OrganizeId],[sfmb],[sfmbmc],[mzzybz],[kffw],[zt],
[CreatorCode],[CreateTime],[ks])
values(@sfmbbh,@orgId,@sfmb,@sfmbmc,2,0,'1',@user,getdate(),@ks)";
                this.ExecuteSqlCommand(sql, new SqlParameter[]
                {
                    new SqlParameter("@sfmbbh", entity.sfmbbh),
                    new SqlParameter("@orgId", entity.OrganizeId),
                    new SqlParameter("@sfmb", entity.sfmb),
                    new SqlParameter("@sfmbmc", entity.sfmbmc),
                    new SqlParameter("@user", user),
                    new SqlParameter("@ks", entity.ks)
                });
            }

            string sqlitems = @"INSERT INTO [NewtouchHIS_Sett].dbo.[xt_sfmbxm]
                ([sfmbxmId],[OrganizeId],[sfmbbh],[sfxm],[CreatorCode],[CreateTime],[zt],[sl])
";
            int i = 0;
            if (!string.IsNullOrWhiteSpace(xmListStr))
            {
                var itemList = Json.ToObject<IList<SysChargeItemTemplateVO>>(xmListStr);
                if (itemList.Count > 0)
                {
                    foreach (var item in itemList)
                    {
                        i++;
                        var sl = string.IsNullOrWhiteSpace(item.sl.ToString()) == true ? "null" : item.sl.ToString();
                        if (i == 1)
                        {
                            sqlitems += @"
select newid(),'" + entity.OrganizeId + "','" + entity.sfmbbh + "','" + item.sfxmCode + "','" + user +
                                "',getdate(),'1'," + sl + @"  
";
                        }
                        else
                        {
                            sqlitems += @"
union all select newid(),'" + entity.OrganizeId+"','"+ entity.sfmbbh+"','"+ item.sfxmCode+"','"+user+"',getdate(),'1',"+ sl + @"  
";                            
                        }
                    }
                }

                this.ExecuteSqlCommand(sqlitems);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cnt"></param>
        /// <param name="orgId"></param>
        /// <param name="dl">1药品 2项目 3非治疗项目	多个 逗号分割</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysChargeItemTemplateVO> GetDicChargeItems(int cnt ,string orgId,string dl,string keyword)
        {
            string sql = "exec [NewtouchHIS_Sett].[dbo].[usp_SelectSfxmYp] @topCount=@topCount,@orgId=@orgId,@dllb=@dl ";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@topCount", cnt));
            pars.Add(new SqlParameter("@dl", dl));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += ",@keyword=@keyword ";
                pars.Add(new SqlParameter("@keyword", keyword));
            }

            return this.FindList<SysChargeItemTemplateVO>(sql, pars.ToArray());
        }

        /// <summary>
        /// 获取收费模板的项目列表
        /// </summary>
        /// <param name="sfmbbh"></param>
        /// <returns></returns>
        public IList<SysChargeItemTemplateVO> GetChargeTemplateChargeItemList(string sfmbbh, string orgId)
        {
            var sql = @"select 
b.sfxmId ,
b.sfxmCode ,
b.sfxmmc ,
b.sfdlCode ,
b.badlCode ,
b.nbdlCode ,
b.py ,
b.dw ,
b.dj ,
b.zfbl ,
b.mzzybz ,
b.CreateTime ,
b.ssbz ,
a.sl ,
a.duration ,
a.kflb ,
a.zll ,
a.zxcs ,
a.zxks, a.yzpc,
a.bw
from [NewtouchHIS_Sett].[dbo].xt_sfmbxm(nolock) a
left join NewtouchHIS_Base.[dbo].V_S_xt_sfxm b on a.sfxm = b.sfxmCode and b.OrganizeId = @orgId
                where a.OrganizeId = @orgId and a.sfmbbh = @sfmbbh
                and a.zt = '1' and b.zt = '1'";
            return this.FindList<SysChargeItemTemplateVO>(sql, new[] { new SqlParameter("@sfmbbh", sfmbbh)
                ,new SqlParameter("@orgId", orgId)});
        }

        public SysChargeTemplateInfoVM GetSysChargeTemplateInfo(string keyValue,string orgId)
        {
            //收费项目列表
            string sql = @"select sfmbbh,OrganizeId,sfmb,sfmbmc,ks,px,zt 
from [NewtouchHIS_Sett].[dbo].xt_sfmb with(nolock)
where sfmbbh=@Id
            ";

            var templateEntity = this.FindList<SysChargeTemplateDto>(sql, new SqlParameter[]
            {
                new SqlParameter("@Id",keyValue), 
            }).FirstOrDefault();
            
            if (templateEntity == null || string.IsNullOrWhiteSpace(keyValue) || templateEntity.OrganizeId == null)
            {
                return null;
            }
            return new SysChargeTemplateInfoVM()
            {
                TemplateEntity = templateEntity,
                SysList = GetChargeTemplateChargeItemList(keyValue, templateEntity.OrganizeId)
            };
        }

    }
}

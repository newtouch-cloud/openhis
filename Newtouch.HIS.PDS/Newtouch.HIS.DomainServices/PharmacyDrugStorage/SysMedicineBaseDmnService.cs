using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class SysMedicineBaseDmnService : DmnServiceBase, ISysMedicineBaseDmnService
    {

        public SysMedicineBaseDmnService(IDefaultDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }


        /// <summary>
        /// 根据关键字查询药品大分类
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysMedicineClassificationVO> GetValidList(string keyword = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_ypfl(nolock) where zt = '1' and (ypflCode like @searchKeyword or ypflmc like @searchKeyword or py like @searchKeyword) order by CreateTime desc";
            return this.FindList<SysMedicineClassificationVO>(sql, new SqlParameter[] {
                            new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                        });
        }

        public SysMedicineAntibioticInfoVO GetKssInfo(string Id, string OrganizeId)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_ypKss(nolock) where Id=@Id and organizeId=@organizeId  ";
            return this.FirstOrDefault<SysMedicineAntibioticInfoVO>(sql, new SqlParameter[] {
                            new SqlParameter("@Id", Id),
                            new SqlParameter("@OrganizeId", OrganizeId)
                        });
        }

        public IList<SysItemsDetailVO> GetValidListByItemCode(string code, string keyword, string orgId = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.Sys_ItemsDetail
        where zt = '1'
        and (isnull(@orgId,'') = '' or OrganizeId = '*' or OrganizeId = @orgId)
        and ItemId in (
        select Id from [NewtouchHIS_Base].dbo.Sys_Items where zt = '1' and Code = @code
        )
        and (Name like @keyword or Code like @keyword)
        ";
            return this.FindList<SysItemsDetailVO>(sql, new SqlParameter[] {
                        new SqlParameter("@code", code),
                        new SqlParameter("@orgId", orgId ?? ""),
                        new SqlParameter("@keyword",'%'+ (keyword==null?"":keyword) +'%')
                    });
        }

        /// <summary>
        /// 获得所有列表
        /// </summary>
        public List<SysMedicalOrderFrequencyVO> GetOrderFrequencyList(string orgId, string keyword = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_yzpc(nolock) where organizeId=@organizeId ";

            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " yzpcmc like @keyword or yzpcCode like @keyword";
            }
            else
            {
            }
            return this.FindList<SysMedicalOrderFrequencyVO>(sql, new SqlParameter[] {
                            new SqlParameter("@keyword", "%" + (keyword ?? "") + "%"),
                        new SqlParameter("@organizeId", orgId ?? "")
                        });
        }

        /// <summary>
        /// 收费大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysChargeCategoryVO> GetsfdlValidList(string orgId)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_sfdl(nolock) where zt=1 and organizeId=@organizeId order by px ";
            return this.FindList<SysChargeCategoryVO>(sql, new SqlParameter[] {
                            new SqlParameter("@OrganizeId", orgId)
                        });
        }

        /// <summary>
        /// 药品剂型
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineFormulationVO> GetypjxValidList(string keyword = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_ypjx(nolock) where zt = '1' and (jxCode like @searchKeyword or jxmc like @searchKeyword or py like @searchKeyword) order by py asc";
            return this.FindList<SysMedicineFormulationVO>(sql, new SqlParameter[] {
                        new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    });
        }


        /// <summary>
        /// 查询药品单位
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineUnitVO> GetypdwValidList(string keyword = null)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_ypdw where zt = '1' and ypdwmc like @searchKeyword order by ypdwmc asc";
            return this.FindList<SysMedicineUnitVO>(sql, new SqlParameter[] {
                        new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    });
        }


        public int Insertyfbmyp(SysPharmacyDepartmentOpenMedicineVO entity)
        {

            if (!string.IsNullOrEmpty(entity.Id))
            {
                try
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {

                        var sqlParList = new List<SqlParameter>();
                        var sqla = "";
                        var sqlb = "";
                        if (entity.px != 0 && entity.px != null) {
                            sqla += ",px "; sqlb += ",@px "; sqlParList.Add(new SqlParameter("@px", entity.px));
                        }

                        var sql = @"
insert into NewtouchHIS_Base.dbo.xt_yfbm_yp
(Id,yfbmCode ,dlCode,OrganizeId,
CreatorCode,CreateTime,LastModifyTime,LastModifierCode,zt"
+ sqla + @")
values ( 
newid(),@yfbmCode ,@dlCode,@OrganizeId,
@CreatorCode,getdate(),null,null,'1'"
+ sqlb + "); ";
                        sqlParList.Add(new SqlParameter("@yfbmCode", entity.yfbmCode));
                        sqlParList.Add(new SqlParameter("@dlCode", entity.dlCode));
                        sqlParList.Add(new SqlParameter("@OrganizeId", entity.OrganizeId));
                        sqlParList.Add(new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode));

                        ExecuteSqlCommand(sql, sqlParList.ToArray());
                        db.Commit();


                        //var strSql = new StringBuilder();
                        //strSql.Append(@"insert into [NewtouchHIS_Base].dbo.xt_yfbm_yp
                        //       (Id,yfbmCode ,dlCode,OrganizeId,CreateTime,CreatorCode,zt,px)
                        //          values (newid(),@yfbmCode ,@dlCode,@OrganizeId,@CreateTime,@CreatorCode,@zt,@px);");
                        //var paraList = new DbParameter[]
                        //{
                        //new SqlParameter("@yfbmCode", entity.yfbmCode),
                        //new SqlParameter("@dlCode", entity.dlCode),
                        //new SqlParameter("@OrganizeId", entity.OrganizeId),
                        //new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                        //new SqlParameter("@CreateTime", DateTime.Now),
                        //new SqlParameter("@zt", entity.zt),
                        //new SqlParameter("@px", entity.px),
                        //};
                        //db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        //db.Commit();

                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException(ex.Message);
                }
            }
            return 1;

        }


        /// <summary>
        /// 查看药房部门药品信息(大类)
        /// </summary>
        /// <param name="dlCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<PharmacyDepartmentOpenMedicineRepoVO> SelectDepartmentMedicine(string dlCode, string organizeId)
        {
            const string sql = @"
SELECT * 
FROM [NewtouchHIS_Base].dbo.xt_yfbm_yp(NOLOCK) yfbmyp
WHERE yfbmyp.OrganizeId=@organizeId 
AND yfbmyp.dlCode=@dlCode
AND yfbmyp.zt='1'
";
            DbParameter[] parame = {
                new SqlParameter("@dlCode",dlCode ?? ""),
                new SqlParameter("@organizeId",organizeId)
            };
            return FindList<PharmacyDepartmentOpenMedicineRepoVO>(sql, parame);
        }


        /// <summary>
        /// 提交抗生素类别信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Id</returns>
        public string SubmitForm(SysMedicineAntibioticInfoVO entity)
        {
            string sql = "";

            var sqlParList = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(entity.Id))
            {
                var sqla = "";
                if (entity.kssLevel1TypeId != null)
                {
                    sqla += ",[kssLevel1TypeId]=@kssLevel1TypeId ";
                    sqlParList.Add(new SqlParameter("@kssLevel1TypeId", entity.kssLevel1TypeId));
                }
                if (entity.kssLevel2TypeId != null)
                {
                    sqla += ",[kssLevel2TypeId] =@kssLevel2TypeId";
                    sqlParList.Add(new SqlParameter("@kssLevel2TypeId", entity.kssLevel2TypeId));
                }
                if (entity.jlfwBegin != null)
                {
                    sqla += ",[jlfwBegin]=@jlfwBegin ";
                    sqlParList.Add(new SqlParameter("@jlfwBegin", entity.jlfwBegin));
                }
                if (entity.jlfwEnd != null)
                {
                    sqla += ",[jlfwEnd]=@jlfwEnd ";
                    sqlParList.Add(new SqlParameter("@jlfwEnd", entity.jlfwEnd));
                }
                if (entity.pcfwBegin != null)
                {
                    sqla += ",[pcfwBegin] =@pcfwBegin";
                    sqlParList.Add(new SqlParameter("@pcfwBegin", entity.pcfwBegin));
                }
                if (entity.pcfwEnd != null)
                {
                    sqla += ",[pcfwEnd]=@pcfwEnd ";
                    sqlParList.Add(new SqlParameter("@pcfwEnd", entity.pcfwEnd));
                }
                if (entity.DDDnum != null)
                {
                    sqla += ",[DDDnum] =@DDDnum";
                    sqlParList.Add(new SqlParameter("@DDDnum", entity.DDDnum));
                }
                if (entity.DDDdw != null)
                {
                    sqla += ",[DDDdw] =@DDDdw";
                    sqlParList.Add(new SqlParameter("@DDDdw", entity.DDDdw));
                }
                if (entity.xj != null)
                {
                    sqla += ",[xj]=@xj ";
                    sqlParList.Add(new SqlParameter("@xj", entity.xj));
                }
                if (entity.kssjldw != null)
                {
                    sqla += ",[kssjldw]=@kssjldw ";
                    sqlParList.Add(new SqlParameter("@kssjldw", entity.kssjldw));
                }

                sql = @"update a
		set 
OrganizeId=@OrganizeId
,qxjb=@qxjb
,kp=@kp
,LastModifyTime=getdate()
,LastModifierCode=@LastModifierCode"
+ sqla +
@"
		from [NewtouchHIS_Base].dbo.xt_ypKss a
		where id=@id and OrganizeId=@orgId and a.zt='1'";
            }
            else
            {
                var sqla = "";
                var sqlb = "";
                if (entity.kssLevel1TypeId != null)
                {
                    sqla += ",[kssLevel1TypeId] ";
                    sqlb += ",@kssLevel1TypeId ";
                    sqlParList.Add(new SqlParameter("@kssLevel1TypeId", entity.kssLevel1TypeId));
                }
                if (entity.kssLevel2TypeId != null)
                {
                    sqla += ",[kssLevel2TypeId] ";
                    sqlb += ",@kssLevel2TypeId ";
                    sqlParList.Add(new SqlParameter("@kssLevel2TypeId", entity.kssLevel2TypeId));
                }
                if (entity.jlfwBegin != null)
                {
                    sqla += ",[jlfwBegin] ";
                    sqlb += ",@jlfwBegin ";
                    sqlParList.Add(new SqlParameter("@jlfwBegin", entity.jlfwBegin));
                }
                if (entity.jlfwEnd != null)
                {
                    sqla += ",[jlfwEnd] ";
                    sqlb += ",@jlfwEnd ";
                    sqlParList.Add(new SqlParameter("@jlfwEnd", entity.jlfwEnd));
                }
                if (entity.pcfwBegin != null)
                {
                    sqla += ",[pcfwBegin] ";
                    sqlb += ",@pcfwBegin ";
                    sqlParList.Add(new SqlParameter("@pcfwBegin", entity.pcfwBegin));
                }
                if (entity.pcfwEnd != null)
                {
                    sqla += ",[pcfwEnd] ";
                    sqlb += ",@pcfwEnd ";
                    sqlParList.Add(new SqlParameter("@pcfwEnd", entity.pcfwEnd));
                }
                if (entity.DDDnum != null)
                {
                    sqla += ",[DDDnum] ";
                    sqlb += ",@DDDnum ";
                    sqlParList.Add(new SqlParameter("@DDDnum", entity.DDDnum));
                }
                if (entity.DDDdw != null)
                {
                    sqla += ",[DDDdw] ";
                    sqlb += ",@DDDdw ";
                    sqlParList.Add(new SqlParameter("@DDDdw", entity.DDDdw));
                }
                if (entity.xj != null)
                {
                    sqla += ",[xj] ";
                    sqlb += ",@xj ";
                    sqlParList.Add(new SqlParameter("@xj", entity.xj));
                }
                if (entity.kssjldw != null)
                {
                    sqla += ",[kssjldw] ";
                    sqlb += ",@kssjldw ";
                    sqlParList.Add(new SqlParameter("@kssjldw", entity.kssjldw));
                }



                sql = @"insert into [NewtouchHIS_Base].dbo.xt_ypKss
([Id],[OrganizeId],[qxjb],[kp],
[CreatorCode],[CreateTime],[LastModifyTime],[LastModifierCode],[zt]"
+ sqla +
@")
		select 
newid(),@OrganizeId,@qxjb,@kp
,@CreatorCode,getdate(),null,null,@zt
"
            + sqlb
            ;
            }

            try
            {
                //ExecuteSqlCommand(sql, new SqlParameter[] {
                sqlParList.Add(new SqlParameter("@OrganizeId", entity.OrganizeId));
                //    sqlParList.Add(new SqlParameter("@kssLevel1TypeId",entity.kssLevel1TypeId));
                //sqlParList.Add(new SqlParameter("@kssLevel2TypeId",entity.kssLevel2TypeId));
                sqlParList.Add(new SqlParameter("@qxjb", entity.qxjb));
                //sqlParList.Add(new SqlParameter("@jlfwBegin",entity.jlfwBegin));
                //sqlParList.Add(new SqlParameter("@jlfwEnd",entity.jlfwEnd??0));
                //sqlParList.Add(new SqlParameter("@pcfwBegin",entity.pcfwBegin??0));
                //sqlParList.Add(new SqlParameter("@pcfwEnd",entity.pcfwEnd??0));
                //sqlParList.Add(new SqlParameter("@DDDnum",entity.DDDnum));
                //sqlParList.Add(new SqlParameter("@DDDdw",entity.DDDdw));
                //sqlParList.Add(new SqlParameter("@xj",entity.xj));
                sqlParList.Add(new SqlParameter("@kp", entity.kp));
                sqlParList.Add(new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode));
                sqlParList.Add(new SqlParameter("@LastModifierCode", OperatorProvider.GetCurrent().UserCode));
                sqlParList.Add(new SqlParameter("@zt", entity.zt));
                //sqlParList.Add(new SqlParameter("@kssjldw",entity.kssjldw));
                //});
                ExecuteSqlCommand(sql, sqlParList.ToArray());
            }
            catch (Exception ex)
            {
                throw new FailedException("保存失败！" + ex.Message);
            }
            return entity.Id;
        }


        /// <summary>
        /// 获取医保病人性质 自负/超限比例
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<ShybBrxzblVo> Getybbxbldata(string keyword, string orgId)
        {
            string isczsql = @" select xmcode from NewtouchHIS_Base..[Sh_Ybfyxzbl] where  xmcode=@keyword ";
            var iscz = this.FindList<string>(isczsql, new DbParameter[]
                {
                   new SqlParameter("@keyword", keyword)
                });
            string sql = "";
            var pars = new List<SqlParameter>() { new SqlParameter("@orgId", orgId) };
            if (iscz.Count == 0)
            {
                sql = @" select a.Code,'' xmcode,'' xmmc,a.Id xzId,a.Name xzmc,0.00 zfbl,0.00 fyxe,0.00 cxbl 
from NewtouchHIS_Base..Sys_ItemsDetail a
WHERE  a.OrganizeId=@orgId  
and ItemId in (select id from NewtouchHIS_Base..Sys_Items where code='shybbrxz' and zt=1 ) order by px desc";
            }
            else
            {
                sql = @"SELECT a.Code ,b.xmcode,b.xmmc,a.Id xzId,a.Name xzmc,isnull(b.zfbl,0.00) zfbl,isnull(b.fyxe,0.00) fyxe,isnull(b.cxbl,0.00) cxbl 
                           FROM NewtouchHIS_Base..Sys_ItemsDetail a
                           LEFT JOIN NewtouchHIS_Base..[Sh_Ybfyxzbl] b on a.Id = b.xzId and b.xmcode = @keyword
                           WHERE ItemId = 'b3a4e2ab-e8c5-4c63-8b26-e8c0c8500bf2'  and a.OrganizeId =@orgId  order by px desc";
                pars.Add(new SqlParameter("@keyword", keyword));
            }


            return this.FindList<ShybBrxzblVo>(sql, pars.ToArray());

        }

        public void SaveYbblValue(List<Sh_YbfyxzblVO> entity, string xmbm, string xmmc, string orgId, string CreatorCode)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var checkSql = "select * from [NewtouchHIS_Base].dbo.Sh_Ybfyxzbl where zt=1 and OrganizeId=@organizeId  and xmcode=@xmcode ";
                var parList = new List<SqlParameter>
                {
                    new SqlParameter("@organizeId", orgId ?? ""),
                    new SqlParameter("@xmcode", xmbm ?? ""),
                };
                var checkList = FindList<Sh_YbfyxzblVO>(checkSql, parList.ToArray());
                if (checkList.Count > 0)
                {
                    foreach (var item in checkList)
                    {
                        var deletesql = " delete [NewtouchHIS_Base].dbo.Sh_Ybfyxzbl where id=@id ";

                        ExecuteSqlCommand(deletesql, new SqlParameter[] { new SqlParameter("@id", item.Id) });

                    };
                }

                foreach (var item in entity)
                {
                    var sqlParList = new List<SqlParameter>();

                    var sqla = "";
                    var sqlb = "";
                    if (xmbm != null)
                    {
                        sqla += ",[xmcode] ";
                        sqlb += ",@xmcode ";
                        sqlParList.Add(new SqlParameter("@xmcode", xmbm));
                    }
                    if (xmmc != null)
                    {
                        sqla += ",[xmmc] ";
                        sqlb += ",@xmmc ";
                        sqlParList.Add(new SqlParameter("@xmmc", xmmc));
                    }
                    if (item.xzId != null)
                    {
                        sqla += ",[xzId] ";
                        sqlb += ",@xzId ";
                        sqlParList.Add(new SqlParameter("@xzId", item.xzId));
                    }
                    if (item.xzmc != null)
                    {
                        sqla += ",[xzmc] ";
                        sqlb += ",@xzmc ";
                        sqlParList.Add(new SqlParameter("@xzmc", item.xzmc));
                    }
                    if (item.zfbl != null)
                    {
                        sqla += ",[zfbl] ";
                        sqlb += ",@zfbl ";
                        sqlParList.Add(new SqlParameter("@zfbl", item.zfbl));
                    }
                    if (item.fyxe != null)
                    {
                        sqla += ",[fyxe] ";
                        sqlb += ",@fyxe ";
                        sqlParList.Add(new SqlParameter("@fyxe", item.fyxe));
                    }
                    if (item.cxbl != null)
                    {
                        sqla += ",[cxbl] ";
                        sqlb += ",@cxbl ";
                        sqlParList.Add(new SqlParameter("@cxbl", item.cxbl));
                    }
                    var sql = @"insert into [NewtouchHIS_Base].dbo.Sh_Ybfyxzbl
([OrganizeId],
[CreatorCode],[CreateTime],[LastModifyTime],[LastModifierCode],[zt]"
    + sqla +
    @")
		select 
@OrganizeId
,@CreatorCode,getdate(),null,null,'1'
"
                + sqlb
                ;
                    try
                    {
                        sqlParList.Add(new SqlParameter("@OrganizeId", orgId));
                        sqlParList.Add(new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode));
                        ExecuteSqlCommand(sql, sqlParList.ToArray());
                    }
                    catch (Exception ex)
                    {
                        throw new FailedException("保存失败！" + ex.Message);
                    }

                }

                db.Commit();
            }
        }

    }
}

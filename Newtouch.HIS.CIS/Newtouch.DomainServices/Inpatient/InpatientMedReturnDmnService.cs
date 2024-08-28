using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Newtouch.Tools;
using Newtouch.Domain.DTO.InputDto.Inpatient;

namespace Newtouch.DomainServices
{
    public class InpatientMedReturnDmnService: DmnServiceBase, IInpatientMedReturnDmnService
    {
        public InpatientMedReturnDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
        /// <summary>
        /// 获取病区发药患者树
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public IList<InpWardPatTreeVO> GetPatTree_old(string keyword, string staffId,string orgId,string ReturnZcyMed=null)
        {
            string sql = @"select a.OrganizeId, b.bqCode,c.bqmc,d.zyh,d.hzxm
from [NewtouchHIS_Base].[dbo].[V_C_Sys_UserStaff] a with(nolock),
	[NewtouchHIS_Base].[dbo].[V_S_Sys_StaffWard] b with(nolock),
	[NewtouchHIS_Base].[dbo].[V_S_xt_bq] c with(nolock) 
	left join zy_fyqqk d with(nolock) on c.bqCode=d.WardCode and c.organizeid=d.organizeid and 
d.tybz in("+ (int)EnumMedSTflag.Receive + "," + (int)EnumMedSTflag.PartReturn + @")
inner join zy_brxxk e on e.zyh=d.zyh and e.organizeid=d.organizeid
where a.staffId=@staffId and a.zt=1  and e.zt='1'
and e.zybz not in (" + (int)EnumZYBZ.Ycy + "," + (int)EnumZYBZ.Wry + "," + (int)EnumZYBZ.Djz+@")
and a.OrganizeId=b.organizeid and a.staffid=b.staffid
and b.OrganizeId=c.organizeid and b.bqcode=c.bqcode";
            if (string.IsNullOrWhiteSpace(ReturnZcyMed) || ReturnZcyMed == "0")
            {
                sql += " and not exists(select 1 from zy_lsyz ls with(nolock) where d.yzxh=ls.id and ls.yzlx=" + (int)EnumYzlx.zcy + " )";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (e.zyh like @keyword or e.xm like @keyword)";
               // par.Add(new SqlParameter("@keyword", "%"+keyword+"%"));
            }
            sql += " group by a.OrganizeId, b.bqCode,c.bqmc,d.zyh,d.hzxm";
            return this.FindList<InpWardPatTreeVO>(sql, new SqlParameter[] {
                new SqlParameter("@staffId", staffId),
                new SqlParameter("@keyword", "%"+keyword+"%"),
            });
        }
        public IList<InpWardPatTreeVO> GetPatTree(string keyword, string staffId, string orgId, string ReturnZcyMed = null)
        {
            string sql = @"select b.bqCode,c.bqmc,d.zyh,d.hzxm,e.ryrq,e.sex,e.birth,CAST(FLOOR(DATEDIFF(DY, e.birth, GETDATE()) / 365.25) AS VARCHAR(5)) nl,
CONVERT(VARCHAR(25),CASE DATEDIFF(DAY, e.ryrq,GETDATE()) WHEN 0 THEN 1 else  DATEDIFF(DAY, e.ryrq,GETDATE())END ) inHosDays,cw.BedNo
from [NewtouchHIS_Base].[dbo].[Sys_StaffWard] b with(nolock)
    inner join [NewtouchHIS_Base].[dbo].[xt_bq] c with(nolock) on b.OrganizeId=c.organizeid and b.bqcode=c.bqcode
    left join zy_fyqqk d with(nolock) on c.bqCode=d.WardCode and c.organizeid=d.organizeid and d.tybz in(" + (int)EnumMedSTflag.Receive + "," + (int)EnumMedSTflag.PartReturn + @")
    left join zy_cwsyjlk cw with(nolock) on cw.zyh=d.zyh and c.organizeid=d.organizeid and cw.zt='1'
    inner join zy_brxxk e on e.zyh=d.zyh and e.organizeid=d.organizeid
where b.staffId=@staffId and b.OrganizeId=@orgId and b.zt='1' and e.zt='1'
and e.zybz not in (" + (int)EnumZYBZ.Ycy + "," + (int)EnumZYBZ.Wry + "," + (int)EnumZYBZ.Djz + @")
";
            if (string.IsNullOrWhiteSpace(ReturnZcyMed) || ReturnZcyMed == "0")
            {
                sql += " and not exists(select 1 from zy_lsyz ls with(nolock) where d.yzxh=ls.id and ls.yzlx=" + (int)EnumYzlx.zcy + " )";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (e.zyh like @keyword or e.xm like @keyword)";
                // par.Add(new SqlParameter("@keyword", "%"+keyword+"%"));
            }
            sql += " group by b.bqCode,c.bqmc,d.zyh,d.hzxm,e.ryrq,e.sex,e.birth,cw.BedNo ";
            return this.FindList<InpWardPatTreeVO>(sql, new SqlParameter[] {
                new SqlParameter("@staffId", staffId),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@keyword", keyword + "%")
            });
        }

        public IList<InpatientMedicineGrantDto> GetPatMedReturnList(string patInfo, string keyword, string kssj, string jssj,string orgId,string ReturnZcyMed=null)
        {
            string sql = @"select [Id],a.[OrganizeId],[lyxh],[zyh],[hzxm],[yzxh],[fzxh],[yfdm],[WardCode],[DeptCode],[ysgh]
,[zxrq],[qqrq],[fyrq],[ypdm],a.[ypmc],[ypsl],a.[ypgg],[ypdw],[dwxs],[ykxs],[ypdj],[zxcs],[tybz]
,[fyczyh],[yzxz],[zbbz],[memo],[mcsl],[CreateTime],[CreatorCode],[LastModifyTime],
[LastModifierCode],a.[zt],ypsl-isnull(ytsl,0) tsl,ypsl-isnull(ytsl,0) ktsl
from v_yzisfy a with(nolock) --zy_fyqqk a with(nolock)
left join [NewtouchHIS_Base].[dbo].[V_C_xt_yp] b with(nolock) on a.[ypdm]=b.ypcode and a.OrganizeId=b.OrganizeId and b.zt='1'
where a.OrganizeId=@orgId and a.zt='1' and a.tybz in (" + (int)EnumMedSTflag.Receive + "," + (int)EnumMedSTflag.PartReturn + @") 
and zyh=@zyh and a.fybz in('2','3')--查询已发药和未完全退完的
and not exists(select 1 from zy_tyjl  b with(nolock) 
			    where b.fyid=a.id and b.zt='1') ";
            if (!string.IsNullOrWhiteSpace(kssj) && !string.IsNullOrWhiteSpace(jssj))
            {
                sql += " and zxrq between @kssj and @jssj ";
                jssj = Convert.ToDateTime(jssj).ToString("yyyy-MM-dd")+" 23:59:59";
            }

            if (string.IsNullOrWhiteSpace(ReturnZcyMed) || ReturnZcyMed=="0")
            {
                sql += " and not exists(select 1 from zy_lsyz ls with(nolock) where a.yzxh=ls.id and ls.yzlx="+ (int)EnumYzlx.zcy + " )";
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += "  and charindex(@keyword,a.ypmc)>0 ";
            }

            sql += " order by zxrq,fzxh,b.dlcode ";
            SqlParameter[] para = new SqlParameter[] {
                    new SqlParameter("@zyh", patInfo),
                    new SqlParameter("@keyword", keyword ?? ""),
                    new SqlParameter("@kssj", kssj ?? ""),
                    new SqlParameter("@jssj", jssj ?? ""),
                    new SqlParameter("@orgId", orgId)
            };

            //return this.QueryWithPage<InpatientMedicineGrantEntity>(sql, pagination, para);
            return this.FindList<InpatientMedicineGrantDto>(sql, para);
        }

        public IList<InpatientMedicineGrantEntity> GetMedListbyFYId(string medList)
        {
            string sql = @"select [Id],[OrganizeId],[lyxh],[zyh],[hzxm],[yzxh],[fzxh],[yfdm],[WardCode],[DeptCode],[ysgh]
                            ,[zxrq],[qqrq],[fyrq],[ypdm],[ypmc],[ypsl],[ypgg],[ypdw],[dwxs],[ykxs],[ypdj],[zxcs],[tybz]
                            ,[fyczyh],[yzxz],[zbbz],[memo],[mcsl],[CreateTime],[CreatorCode],[LastModifyTime],[LastModifierCode],[zt]
                            from zy_fyqqk a with(nolock)
                            where a.tybz=@tybz
                            and not exists(select 1 from zy_tyjl  b with(nolock) 
			                where a.Id=b.fyId )
                            and id in(select col from dbo.f_split(@medList,',') where col>'')";
            return this.FindList<InpatientMedicineGrantEntity>(sql, new[] { new SqlParameter("@tybz", EnumMedSTflag.Receive) , new SqlParameter("@medList", medList) });
        }

        //public void MedReturnSubmit(OperatorModel user, string medList)
        //{
        //    IList<InpatientMedicineGrantEntity> fyList = GetMedListbyFYId(medList);
        //    List<InpatientMedicineReturnEntity> returnList = null;
        //    if (fyList != null && medList.Count() > 0)
        //    {
        //        foreach (var Item in fyList)
        //        {
        //            InpatientMedicineReturnEntity ety = new InpatientMedicineReturnEntity();
        //            ety.fyId = Item.Id;
        //            ety.OrganizeId = Item.OrganizeId;
        //            //ety.tyxh = null;
        //            ety.yzxh = Item.yzxh;
        //            ety.zyh = Item.zyh;
        //            ety.hzxm = Item.hzxm;
        //            ety.WardCode = Item.WardCode;
        //            ety.fyqqxh = Item.lyxh;
        //            ety.ypdm = Item.ypdm;
        //            ety.tysl = Item.ypsl;
        //            ety.ktypsl = Item.ypsl;
        //            ety.ypgg = Item.ypgg;
        //            ety.ypdw = Item.ypdw;
        //            ety.ykxs = Item.ykxs;
        //            ety.ypdj = Item.ypdj;
        //            ety.tyqrbz = 0;
        //            ety.tyqrxh = null;
        //            ety.tyczydm = user.rygh;
        //            //ety.yftyczydm = null;
        //            //ety.yftyrq = null;                   
        //        }
        //    }
        //}

        public string MedReturnSubmit(OperatorModel user, string medList)
        {
            string tyxh = "";
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans()) {
                try
                {
                    var medentity = Tools.Json.ToList<InMedReturnRequestslDto>(medList);
                    string sql = "";
                    sql = @"
select Id fyId, yzxh yzId,ypdm ypCode,ypmc,fzxh zh,zxrq,zyh,ypsl tysl,yfdm fyyf,DeptCode ksCode,WardCode bqCode,cast(a.lyxh as bigint) lyxh
from zy_fyqqk a with(nolock)
where a.zt=1 and a.OrganizeId=@orgId and tybz in(select col from dbo.f_split(@tybz,',')) and 
a.id in(select col from dbo.f_split(@medList,',') where col>'')
and isnull(fzxh,'')='' 
union all
select Id fyId, yzxh yzId,ypdm ypCode,ypmc,fzxh zh,zxrq,zyh,ypsl tysl,yfdm fyyf,DeptCode ksCode,WardCode bqCode,cast(b.lyxh as bigint) lyxh
from zy_fyqqk b with(nolock) 
where  b.zt=1 and b.OrganizeId=@orgId and b.tybz in(select col from dbo.f_split(@tybz,',')) 
and exists (
    select 1 -- OrganizeId,fzxh,zyh
    from zy_fyqqk a  with(nolock)
    where a.id in(select col from dbo.f_split(@medList,',') where col>'') 
    and a.OrganizeId=b.OrganizeId and a.fzxh=b.fzxh and b.zyh=b.zyh and a.zxrq=b.zxrq
    --group by OrganizeId,fzxh,zyh
    )
";
                    var medL = string.Join(",", medentity.Select(p => p.Id).Distinct()); List<InpMedReturnNeedDto> yzList = this.FindList<InpMedReturnNeedDto>(sql, new[] { new SqlParameter("@tybz", (int)EnumMedSTflag.Receive+","+ (int)EnumMedSTflag.PartReturn), new SqlParameter("@medList", medL), new SqlParameter("@orgId", user.OrganizeId) });

                    InpMedReturnResultDto apiResp = null;
                    if (yzList != null && yzList.Count > 0)
                    {

                        List<InpMedReturnRequestDto> reqList = yzList.Select(p => new InpMedReturnRequestDto()
                        {
                            yzId = p.yzId,
                            zyh = p.zyh,
                            ypCode = p.ypCode,
                            tysl = medentity.FirstOrDefault(q=>q.Id==p.fyId).tsl,
                            zhyz = null,
                            zh = p.zh,
                            zxrq = p.zxrq,
                            fyyf = p.fyyf,
                            ksCode = p.ksCode,
                            bqCode = p.bqCode,
                            tysqr = user.UserCode,
                            lyxh=p.lyxh
                        }).ToList();
                        var reqobj = new
                        {
                            OrganizeId = user.OrganizeId,
                            ReturnDrugDetail = reqList,
                            ClientNo = Guid.NewGuid(),
                            TimeStamp = DateTime.Now.ToString()
                        };
                        //调用药房药库接口请求退药，返回IsSucceed==true 则全部成功，若false 则认为全部失败 
                        var apires = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                        "/api/ResourcesOperate/HospitalizatiionReturnDispensingMedicine", reqobj, autoAppendToken: false);

                        if (apires.code == APIRequestHelper.ResponseResultCode.SUCCESS)
                        {
                            if (medentity != null && medentity.Count() > 0&&apires.data!=null&& apires.data.ToString() != "")
                            {
                                MedReturnProcess(user, medentity,db,out tyxh, apires.data.ToString());
                                db.Commit();
                            }
                        }
                        else //黄杉杉修改于2019.05.24 药房药库请求返回不支持 （5 药品已退 6 已申请退药） 两种情况，所以不做处理
                        //if (apires.code == APIRequestHelper.ResponseResultCode.FAIL && apires.sub_msg != "")
                        //{

                        //    var data = Tools.Json.ToObject<List<InpMedReturnResultDataDto>>(apires.data.ToString()); //接口返回数据 
                        //medList = "";
                        //if (data != null)
                        //{
                        //    foreach (InpMedReturnResultDataDto item in data)
                        //    {
                        //        if (item.ErrorCode == "5" || item.ErrorCode == "6")
                        //        {
                        //            //5 药品已退 6 已申请退药
                        //            var list = yzList.Where(p => p.yzId == item.yzId).Where(p => p.ypCode == item.ypCode).Where(p => p.zxrq == item.zxrq);
                        //            foreach (InpMedReturnNeedDto fyItem in list)
                        //            {

                        //                medList += fyItem.fyId + ",";
                        //            }
                        //        }

                        //        errMsg += item.ErrorMsg + ";";
                        //    }
                        //}

                        //if (!string.IsNullOrWhiteSpace(medL))
                        //{
                        //    sql = @"select [Id],[OrganizeId],[tyxh],[yzxh],[zyh],[hzxm],[WardCode],[fyqqxh],[ypdm],[tysl]
                        //        ,[ktypsl],[ypgg],[ypdw],[dwxs],[ykxs],[ypdj],[tyqrbz],[tyqrxh],[tyczydm],[yftyczydm]
                        //        ,[yftyrq],[CreateTime],[CreatorCode],[LastModifyTime],[LastModifierCode],[zt],[fyId],[fzxh]
                        //        from zy_tyjl with(nolock)
                        //        where fyid in(select col from dbo.f_split(@medList,',') where col>'') and zt=1 and OrganizeId=@orgId ";
                        //    var hastyjl = this.FindList<InpatientMedicineReturnEntity>(sql, new[] { new SqlParameter("@medList", medL), new SqlParameter("@orgId ", user.OrganizeId) });
                        //    if (hastyjl.Count > 0)
                        //    {
                        //        //更新发药请求库对应医嘱为退药状态
                        //        sql = @"update a
                        //            set a.tybz=@tybz,a.LastModifierCode=@modificode,a.[LastModifyTime]=getdate()
                        //            from zy_fyqqk a 
                        //            where a.id in(select col from dbo.f_split(@medList,',') where col>'') and OrganizeId=@orgId  and tybz<>@tybz ";
                        //        SqlParameter[] para = new SqlParameter[] {
                        //        new SqlParameter("@tybz",EnumMedSTflag.AllReturn),
                        //        new SqlParameter("@modificode",user.UserCode),
                        //        new SqlParameter("@medList",medL),
                        //        new SqlParameter("@orgId",user.OrganizeId)
                        //    };

                        //        db.ExecuteSqlCommand(sql, para);
                        //    }
                        //    else
                        //    {
                        //        MedReturnProcess(user, medentity,db);
                        //        db.Commit();
                        //    }
                        //}
                        //throw new FailedException("退药申请失败，" + errMsg);

                        //}
                        //else
                        {
                            throw new FailedException("退药申请失败，请重试");
                        }
                    }
                }
                catch (FailedException ex)
                {
                    throw new FailedException("退药申请失败，" + ex.Msg);
                }
            }
            return tyxh;
        }

        protected void MedReturnProcess(OperatorModel user, List<InMedReturnRequestslDto> medList, Newtouch.Infrastructure.EF.EFDbTransaction db,out string tyxh,string tydh)
        {
            tyxh = "";
            if (medList!=null&&db!=null)
            {
                try
                {
                    foreach (var item in medList)
                    {
                        InpatientMedicineGrantEntity ime = db.IQueryable<InpatientMedicineGrantEntity>().FirstOrDefault(p => p.Id == item.Id && p.tybz != (int)EnumMedSTflag.AllReturn && p.zt == "1" && p.OrganizeId == user.OrganizeId);
                        if (ime != null)
                        {
                            ime.ytsl = (ime.ytsl ?? 0) + item.tsl;
                            if (ime.ytsl == ime.ypsl)
                            {
                                ime.tybz = (int)EnumMedSTflag.AllReturn;
                            }
                            else if (ime.ytsl > ime.ypsl)
                            {
                                throw new FailedException("退药数量不能大于发药数量");
                            } else if (ime.ytsl < ime.ypsl) {
                                ime.tybz = (int)EnumMedSTflag.PartReturn;
                            }
                            ime.LastModifyTime = DateTime.Now;
                            ime.LastModifierCode = user.UserCode;
                            ime.Modify();
                            db.Update(ime);

                            InpatientMedicineReturnEntity tyjl = new InpatientMedicineReturnEntity
                            {
                                OrganizeId = user.OrganizeId,
                                tyxh = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueIntValue("zy_tyxh", user.OrganizeId),
                                yzxh = ime.yzxh,
                                zyh = ime.zyh,
                                hzxm = ime.hzxm,
                                WardCode = ime.WardCode,
                                fyqqxh = ime.lyxh,
                                ypdm = ime.ypdm,
                                tysl = item.tsl,
                                ktypsl = (ime.ypsl - ime.ytsl).ToInt(),
                                ypgg = ime.ypgg,
                                ypdw = ime.ypdw,
                                dwxs = ime.dwxs,
                                ykxs = ime.ykxs,
                                ypdj = ime.ypdj,
                                tyqrbz = 0,
                                tyczydm = user.UserCode,
                                yftyrq = DateTime.Now,
                                tydh = tydh
                            };
                            tyjl.Create(true);
                            db.Insert(tyjl);

                            if (string.IsNullOrWhiteSpace(tyxh))
                            {
                                tyxh += tyjl.tyxh;
                            }
                            else
                            {
                                tyxh += "," + tyjl.tyxh;
                            }
                        }
                       
                    }
                }
                catch (Exception)
                {
                    throw new FailedException("退药申请失败，请重试");
                }
             
            }

                //int flag = this.ExecuteSqlCommand(sql, para);
                //if (flag != 0)
                //{
                //    //生成退药请求
                //    //生成退药序号
                //    int tyxh = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueIntValue("zy_tyxh", user.OrganizeId);
                //    if (tyxh > 0)
                //    {
                //        sql = @"
                //                INSERT INTO [dbo].[zy_tyjl]
                //                ([Id] ,[OrganizeId] ,[tyxh] ,[yzxh] ,[zyh] ,[hzxm] ,[WardCode] ,[fyqqxh] ,[ypdm] ,[tysl] ,[ktypsl] ,
                //                [ypgg] ,[ypdw] ,[dwxs] ,[ykxs] ,[ypdj] ,[tyqrbz] ,[tyqrxh] ,[tyczydm] ,[yftyczydm] ,[yftyrq] ,
                //                [CreateTime] ,[CreatorCode] ,[LastModifyTime] ,[LastModifierCode] ,[zt] ,[fyId] ,[fzxh])
                //                select newid(),[OrganizeId],@tyxh,[yzxh],[zyh] ,[hzxm] ,[WardCode] ,lyxh,[ypdm],ypsl,ypsl,
                //                ypgg,ypdw,dwxs,ykxs,ypdj,0,null,@czygh,null,null,
                //                getdate(),@creator,null,null,1,id,fzxh
                //                from zy_fyqqk  with(nolock)
                //                where id in(select col from dbo.f_split(@medList,',') where col>'')
                //                ";

                //        para = new SqlParameter[] {
                //                        new SqlParameter("@tyxh",tyxh),
                //                        new SqlParameter("@czygh",user.rygh),
                //                        new SqlParameter("@tybz",EnumMedSTflag.Receive),
                //                        new SqlParameter("@creator",user.UserCode),
                //                        new SqlParameter("@medList",medList)
                //                    };
                //        flag = this.ExecuteSqlCommand(sql, para);

                //    }
                //    else {
                //        throw new FailedException("退药申请失败(序号)，请重试");
                //    }

                //}
                //else
                //{
                //    throw new FailedException("退药申请失败(发药状态)，请重试");
                //}
            //}
        }




        



    }
}

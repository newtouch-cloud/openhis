using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.EMR.APIRequest.Bljgh.Request;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using Newtouch.EMR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Newtouch.EMR.Repository
{
    public class bl_ElementDomainRepo : RepositoryBase<bl_ElementDomainEntity>, Ibl_ElementDomainRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public bl_ElementDomainRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public List<bl_ElementDomainEntity> GetElementTree(string orgId)
        {
            return this.IQueryable().Where(p => p.Zt == 1 && p.OrganizeId == orgId).OrderBy(p => p.Px).ToList();
        }

        public IList<BlysGridVo> GetElementDetail(Pagination pagination, string OrganizeId, string keyword)
        {
            string sql = @" 
     select jgmx.Id
        ,ElementId, Table_Name, Table_Column_No, Table_Column_Code, Table_Colunn_Name
        ,Table_Column_Type, Element_ID, Element_Name, Element_Type, Element_Type_Name, jgmx.Sybz, jgmx.Px
	    ,AreValue,ysmxId,ysmx.ysmxname AreValuemc
     from bl_ElementDomain_Detail jgmx 
	 left join bl_ysmx ysmx on jgmx.ysmxId=ysmx.YsId and ysmx.ysmxcode=arevalue
     where ElementId=@keyword and jgmx.zt=1 and jgmx.OrganizeId=@OrgId
";
            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@OrgId", OrganizeId));
            para.Add(new SqlParameter("@keyword", keyword));
            return this.QueryWithPage<BlysGridVo>(sql, pagination, para.ToArray()).ToList();
        }

        public List<BlysListVo> GetBlys(string keyword, string orgId)
        {
            string sql = @"select top 20 b.ysmc ysdl,a.Id ysId,a.yscode ysdm,a.ysmc,a.yslx,a.BindingPath from bl_ys a
 join bl_ys b on a.yssjid = b.Id
 where a.OrganizeId = @orgId and a.zt = 1 and (a.ysmc like @keyword or a.py like @keyword)
 and b.zt = 1 --and b.yslx is null
";
            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@keyword",'%'+ keyword+'%'));
            return this.FindList<BlysListVo>(sql, para.ToArray()).ToList();
        }

        public List<TableColumnVo> GetTabColumnList(string tabName,string keyword)
        {
            
            string sql = @"select col.column_id,col.name columnName,lx.name columnType,col.max_length,col.scale
  from sys.columns col,sys.types lx
 where col.object_id = object_id(@tabName)
   and col.system_type_id = lx.system_type_id and col.name like @keyword
   order by col.column_id";

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@tabName", tabName));
            para.Add(new SqlParameter("@keyword", '%' + keyword + '%'));
            return this.FindList<TableColumnVo>(sql, para.ToArray()).ToList();
        }
        public List<BlysDeatilVo> GetBlysmx(string keyword, string orgId)
        {
            string sql = @" select ysId,ysmxcode,ysmxName from bl_ysmx where ysId=@ysId and sybz=1 and OrganizeId= @orgId and zt = 1
";
            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@ysId", keyword));
            return this.FindList<BlysDeatilVo>(sql, para.ToArray()).ToList();
        }

        public void SubmitForm(bl_ElementDomainEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int Id = Convert.ToInt32(keyValue);
                var dbEntity = this.FindEntity(Id);
                dbEntity.Px = entity.Px;
                dbEntity.Regex = entity.Regex;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                dbEntity.Modify(Id);
                this.Update(dbEntity);
            }
            else
            {
                var check = this.FindEntity(p => p.Bllx == entity.Bllx && p.Zt == 1 && p.OrganizeId == entity.OrganizeId);
                if (check != null)
                {
                    throw new FailedException("该病历结构表已存在！");
                }
                
                entity.CreateTime = DateTime.Now;
                entity.Zt =1;
                entity.Create();
                this.Insert(entity);
            }
        }

        public void DeleteForm(string keyValue, string orgId, string user)
        {
            if (!string.IsNullOrWhiteSpace(keyValue) && !string.IsNullOrWhiteSpace(orgId))
            {
                int Id = Convert.ToInt32(keyValue);
                bl_ElementDomainEntity ety = this.FindEntity(p => p.Id == Id && p.OrganizeId == orgId && p.Zt ==1);
                if (ety != null)
                {
                    ety.Zt = 0;
                    ety.LastModifierCode = user;
                    ety.LastModifyTime = DateTime.Now;
                    ety.Modify(Id);
                    this.Update(ety);
                }
                else
                {
                    throw new FailedException("病历表不存在或已删除，请重新选择！");
                }
            }
            else
            {
                throw new FailedException("病历表不存在或已删除，请重新选择！");
            }
        }

        #region 病历结构化竖表转横表存储
        public void BljghDataDealwith(BljghReq req)
        {
            //正则匹配结构表
            string bljgTable = "";
            var bljgTab = this.IQueryable(p => p.Zt == 1).ToList();
            foreach (var item in bljgTab)
            {
                Regex regex = new Regex(item.Regex);
                if (regex.IsMatch(req.blmc))
                {
                    string c1 = (regex.Match(req.blmc).Groups[0].ToString());
                    bljgTable = item.Table_EngLish_Name;
                    break;
                }
            }
            if (string.IsNullOrEmpty(bljgTable))
                return;

            if (req.delzt=="0")
            {
                string deletesql = $@" update {bljgTable} set bz='0',LastModifyTime=getdate(),LastModifierCode='{req.czr}' 
   where bl_pzhm='{req.blId}' and organizeid='{req.organizeId}' ";
                this.ExecuteSqlCommand(deletesql, new SqlParameter[] {
                           });
                return;
            }
            //病人信息
            ZybrjbxxEntity patlist = GetZybrjbxx(req.zyh, req.organizeId);
            //病历信息
            bl_bllxEntity blbentity = GetBllxxx(req.bllx,req.organizeId);
            BlwsInfoVo blwsentity = new BlwsInfoVo ();
            if (blbentity!=null)
            {
                blwsentity = GetBljlInfo(blbentity.relTB,req.blId,req.organizeId);
            }
            //明细表结构
            List<ElementTablStructureVo> jgmxEntity = GetElementTableColumn(bljgTable,req.organizeId);
            List<ElementTablStructureVo> jgDataEntity = GetVerticalTableData(req.blId, req.zyh,req.organizeId);

            jgmxEntity = (from a in jgmxEntity
                          join b in jgDataEntity on a.Element_ID equals b.Element_ID into ab
                          from ba in ab.DefaultIfEmpty(new ElementTablStructureVo())
                          select new ElementTablStructureVo
                          {
                              Table_Column_Code =a.Table_Column_Code,
                              Table_Colunn_Name=a.Table_Colunn_Name,
                              Element_ID=a.Element_ID,
                              Element_Name=a.Element_Name,
                              Element_Value=ba.Element_Value
                          }
                       ).ToList();

            byte[] bytes = System.Text.Encoding.Default.GetBytes(blwsentity.xmlConten);
            var xmlcontentBase64 = Convert.ToBase64String(bytes);
            if (IsCrossDataExist(bljgTable, req.zyh, req.organizeId, req.blId) == 0)
            {
                string sqlInsert = "";
                try {
                    string insertCol = $@" insert into {bljgTable} ([OrganizeId],[ZYH],[BL_TYPE],[BL_PZHM],[BL_RECORD_DATE],[BL_TEXT],[BL_BASE64],CreateTime,CreatorCode,bz,";
                    string insertVal = $@" values('{req.organizeId}','{req.zyh}','{req.bllx}','{req.blId}','{blwsentity.blrq}','{blwsentity.xmlConten}','{xmlcontentBase64}',getdate(),'{req.czr}',1,";
                    foreach (var item in jgmxEntity)
                    {
                        insertCol += item.Table_Column_Code + ",";
                        insertVal += string.IsNullOrWhiteSpace(item.Element_Value) ? "NULL" + "," : "'" + item.Element_Value.Replace(" ", " ") + "'" + ",";
                    }
                    insertCol = insertCol.Substring(0, insertCol.Length - 1) + ")";
                    insertVal = insertVal.Substring(0, insertVal.Length - 1) + ")";

                    sqlInsert = insertCol + insertVal;
                    this.ExecuteSqlCommand(sqlInsert, new SqlParameter[] {
                           });
                }
                catch (Exception e)
                {
                    string rc = req.zyh + "|" + req.bllx + "|" + req.blId + "|" + req.blmc;
                    EmrLogger.Info("竖表数据转横表新增发生错误:" + e.Message + ",入参:" + rc + "+SQL语句:" + sqlInsert);
                }
            }
            else
            {
                string updatesql = "";
                try {
                    updatesql = $@" update {bljgTable} set BL_TEXT='{blwsentity.xmlConten}' ,BL_BASE64='{xmlcontentBase64}',LastModifyTime=getdate(),LastModifierCode='{req.czr}', ";
                    foreach (var item in jgmxEntity)
                    {
                        var d = string.IsNullOrWhiteSpace(item.Element_Value)?string.Empty:item.Element_Value.Replace(" ", " ");
                        updatesql += $@"{item.Table_Column_Code} " + $@"='{d}',";
                    }
                    updatesql = updatesql.Substring(0, updatesql.Length - 1);
                    updatesql = updatesql + $@" where bl_pzhm='{req.blId}' and zyh='{req.zyh}' and bz=1";
                    this.ExecuteSqlCommand(updatesql, new SqlParameter[] {
                           });
                }
                catch (Exception e) {
                    string rc = req.zyh + "|" + req.bllx + "|" + req.blId + "|" + req.blmc;
                    EmrLogger.Info("竖表数据转横表更新发生错误:" + e.Message + ",入参:" + rc + "+SQL语句:" + updatesql);
                }
            }

        }

        public BlwsInfoVo GetBljlInfo(string tableName,string blId,string orgId)
        {
            string sql = $@"select dzbl_id
 ,ksmc,ksdm,qmrq,czydm_zzys,a.blrq,a.creatorcode,b.xmlConten from  {tableName}  a
join zy_meddocs_relation b on a.Id=b.blId and a.organizeid=b.organizeid
where a.id=@blId and a.organizeId=@orgId and a.zt='1' ";

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@blId", blId));
            return this.FindList<BlwsInfoVo>(sql, para.ToArray()).FirstOrDefault();
        }

        public ZybrjbxxEntity GetZybrjbxx(string zyh,string orgId)
        {
            string sql = @"select * from zy_brjbxx where zyh=@zyh and organizeId=@orgId and zt=1  ";

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@zyh", zyh));
            return this.FindList<ZybrjbxxEntity>(sql, para.ToArray()).FirstOrDefault();
        }
        public bl_bllxEntity GetBllxxx(string bllx, string orgId)
        {
            string sql = @"select * from bl_bllx where bllx=@bllx and organizeId=@orgId and zt=1  ";

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@bllx", bllx));
            return this.FindList<bl_bllxEntity>(sql, para.ToArray()).FirstOrDefault();
        }

        public int IsCrossDataExist(string tableName,string zyh,string orgId,string blId)
        {
            string sql = $@"select count(1) from {tableName} where zyh=@zyh and organizeId=@orgId and BL_PZHM=@blId and bz=1  ";

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@zyh", zyh));
            para.Add(new SqlParameter("@blId", blId));
            return this.FindList<int>(sql, para.ToArray()).FirstOrDefault();
        }

        public List<ElementTablStructureVo> GetElementTableColumn(string tableName, string orgId)
        {
            string sql = $@"select Table_Column_Code,Table_Colunn_Name,Element_ID,Element_Name,'' Element_Value from bl_ElementDomain jg
join bl_ElementDomain_Detail jgmx on jgmx.ElementId=jg.Id and jgmx.organizeId=jg.organizeId and jgmx.zt=1 and sybz=1 
where jg.Table_EngLish_Name='{tableName}' and jg.organizeId=@orgId and jg.zt=1  ";

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<ElementTablStructureVo>(sql, para.ToArray());
        }

        public List<ElementTablStructureVo> GetVerticalTableData(string blId,string zyh, string orgId)
        {
            string sql = @"select yscode Element_ID,ysvalue Element_Value from [bl_ysjgnr] 
where organizeId=@orgId  and zyh=@zyh and blId=@blId and zt=1 ";

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@blId", blId));
            para.Add(new SqlParameter("@zyh", zyh));
            return this.FindList<ElementTablStructureVo>(sql, para.ToArray());
        }
       
        #endregion


    }
}

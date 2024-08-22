using Newtouch.Common;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Web.Core.Attributes;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.PatientManage.Controllers
{
    /// <summary>
    /// 病历
    /// </summary>
    public class SysPatientMedicalRecordController : ControllerBase
    {
        private readonly ISysPatientMedicalRecordDmnService _sysPatientMedicalRecordDmnService;
        
        [HandlerAuthorizeIgnore]
        public override ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnlyIgnore]
        [HttpGet]
        public ActionResult GetTreeGridJson(string blh)
        {
            var list = _sysPatientMedicalRecordDmnService.GetMedicalRecordList(this.OrganizeId, blh);
            var treeItemList = new List<SysPatientMedicalRecordTreeItemDTO>();
            foreach (var bl in list)
            {
                var treeModel = new SysPatientMedicalRecordTreeItemDTO();
                treeModel.Id = bl.Id;
                treeModel.blh = bl.blh;
                treeModel.rq = bl.rq;
                treeModel.bz = bl.bz;
                treeModel.zt = bl.zt;
                treeModel.CreatorUserName = bl.CreatorUserName;
                treeModel.CreateTime = bl.CreateTime;
                treeItemList.Add(treeModel);
                foreach (var mx in bl.Details)
                {
                    var mxTreeModel = new SysPatientMedicalRecordTreeItemDTO();
                    mxTreeModel.Id = mx.Id;
                    mxTreeModel.attachType = mx.attachType;
                    mxTreeModel.attachName = mx.attachName;
                    mxTreeModel.attachPath = mx.attachPath;
                    mxTreeModel.attachUrl = mx.attachUrl;
                    mxTreeModel.DetailCreateTime = mx.CreateTime;
                    mxTreeModel.DetailCreatorUserName = mx.CreatorUserName;
                    mxTreeModel.ParentId = bl.Id;
                    mxTreeModel.zt = bl.zt;
                    treeItemList.Add(mxTreeModel);
                }
            }
            var treeList = new List<TreeGridModel>();
            foreach (var treeItem in treeItemList.Where(p => p.ParentId == null))
            {
                var treeModel = new TreeGridModel();
                treeModel.id = treeItem.Id;
                treeModel.isLeaf = true;
                treeModel.parentId = null;
                treeModel.expanded = treeItem.zt == "1";
                treeModel.entityJson = treeItem.ToJson();
                treeList.Add(treeModel);
                foreach (var mx in treeItemList.Where(p => p.ParentId == treeItem.Id))
                {
                    var mxTreeModel = new TreeGridModel();
                    mxTreeModel.id = mx.Id;
                    mxTreeModel.isLeaf = false;
                    mxTreeModel.parentId = treeModel.id;
                    mxTreeModel.expanded = false;
                    mxTreeModel.entityJson = mx.ToJson();
                    treeList.Add(mxTreeModel);
                }
            }
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return null;
            }

            var main = _sysPatientMedicalRecordDmnService.GetMedicalRecordById(keyValue);
            var details = _sysPatientMedicalRecordDmnService.GetMedicalRecordDetailListByMainId(keyValue);

            var result = new
            {
                main = main,
                details = details,
            };

            return Content(result.ToJson());
        }

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitForm(string keyValue, string blh
            , DateTime rq
            , string zt, string bz
            , HttpPostedFileBase file_0, string fileName_0, string dbKeyId_0
            , HttpPostedFileBase file_1, string fileName_1, string dbKeyId_1
            , HttpPostedFileBase file_2, string fileName_2, string dbKeyId_2
            , HttpPostedFileBase file_3, string fileName_3, string dbKeyId_3
            , HttpPostedFileBase file_4, string fileName_4, string dbKeyId_4
            , HttpPostedFileBase file_5, string fileName_5, string dbKeyId_5
            , HttpPostedFileBase file_6, string fileName_6, string dbKeyId_6
            , HttpPostedFileBase file_7, string fileName_7, string dbKeyId_7
            , HttpPostedFileBase file_8, string fileName_8, string dbKeyId_8
            , HttpPostedFileBase file_9, string fileName_9, string dbKeyId_9)
        {
            zt = (zt == "true" || zt == "on") ? "1" : "0";

            IList<SysPatientMedicalRecordDetailEntity> existDetails = null;
            IList<string> delDetailIdList = null;

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                existDetails = _sysPatientMedicalRecordDmnService.GetMedicalRecordDetailListByMainId(keyValue);

                delDetailIdList = existDetails.Select(p => p.Id).Where(p => p != dbKeyId_0 && p != dbKeyId_1 && p != dbKeyId_2 && p != dbKeyId_3 && p != dbKeyId_4 && p != dbKeyId_5 && p != dbKeyId_6 && p != dbKeyId_7 && p != dbKeyId_8 && p != dbKeyId_9).ToList();
            }
            var updateDetailEntityList = new List<SysPatientMedicalRecordDetailEntity>();
            var addDetailEntityList = new List<SysPatientMedicalRecordDetailEntity>();

            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_0, fileName_0, dbKeyId_0);
            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_1, fileName_1, dbKeyId_1);
            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_2, fileName_2, dbKeyId_2);
            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_3, fileName_3, dbKeyId_3);
            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_4, fileName_4, dbKeyId_4);
            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_5, fileName_5, dbKeyId_5);
            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_6, fileName_6, dbKeyId_6);
            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_7, fileName_7, dbKeyId_7);
            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_8, fileName_8, dbKeyId_8);
            Detail(addDetailEntityList, updateDetailEntityList, existDetails, blh, file_9, fileName_9, dbKeyId_9);

            _sysPatientMedicalRecordDmnService.SubmitForm(keyValue, blh, rq, zt, bz, delDetailIdList
                , addDetailEntityList, updateDetailEntityList, this.OrganizeId);

            return Content("<script type='text/javascript'>top.frames['PatientMedicalRecordList'].cabck('" + "操作成功" + "');</script>");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="existDetails"></param>
        /// <param name="blh"></param>
        /// <param name="sfile"></param>
        /// <param name="sfileName"></param>
        /// <param name="sdbKeyId"></param>
        /// <returns></returns>
        private SysPatientMedicalRecordDetailEntity Detail(
            List<SysPatientMedicalRecordDetailEntity> willaddDetailEntityList,
            List<SysPatientMedicalRecordDetailEntity> willupdateDetailEntityList,
            IList<SysPatientMedicalRecordDetailEntity> existDetails,
            string blh, HttpPostedFileBase sfile, string sfileName, string sdbKeyId)
        {
            if (sfile != null || !string.IsNullOrWhiteSpace(sfileName) || !string.IsNullOrWhiteSpace(sdbKeyId))
            {
                SysPatientMedicalRecordDetailEntity mEntity = null;
                if (!string.IsNullOrWhiteSpace(sdbKeyId))
                {
                    mEntity = existDetails.Where(p => p.Id == sdbKeyId).FirstOrDefault();
                }
                if (mEntity == null)
                {
                    mEntity = new SysPatientMedicalRecordDetailEntity();
                    willaddDetailEntityList.Add(mEntity);
                }
                else
                {
                    willupdateDetailEntityList.Add(mEntity);
                }
                //附件
                if (sfile != null)
                {
                    var fileName = sfile.FileName;
                    var path = "\\历史病历\\" + blh + DateTime.Now.ToString("\\\\yyyyMMdd\\\\HHmmssfff\\\\") + fileName;
                    var filePath = CommmHelper.GetLocalFilePath(path);
                    sfile.SaveAs(filePath);

                    mEntity.attachPath = path;
                    mEntity.attachName = fileName;
                    mEntity.attachType = (int)Newtouch.Core.Common.Utils.FileHelper.GetTypeByExtension(FileHelper.GetExtension(fileName));
                }
                //
                if (!string.IsNullOrWhiteSpace(sfileName))
                {
                    mEntity.attachName = sfileName; 
                }

                return mEntity;
            }
            return null;
        }

    }
}
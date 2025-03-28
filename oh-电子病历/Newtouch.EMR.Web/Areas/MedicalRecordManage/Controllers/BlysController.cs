using Newtouch.EMR.Domain.Entity;

using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Web;
using System.Collections.Generic;
using Newtouch.Common;
using Newtouch.EMR.Domain.IRepository;
using System.Linq;
using System;
using System.Xml;
using Newtouch.Core.Common.Utils;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage
{

    public class BlysController : OrgControllerBase
    {
        private readonly IBlysRepo _blysRepo;
      
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="blysRepo"></param>
        public BlysController(IBlysRepo blysRepo)
        {
            this._blysRepo = blysRepo;
        }
        public ActionResult YSList()
        {

            return View();
        }

        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyValue)
        {

            if (!string.IsNullOrEmpty(keyValue))
            {
                var elemrnt = _blysRepo.IQueryable(p => p.Id == keyValue && p.zt == "1" && p.OrganizeId == OrganizeId);

                return Content(elemrnt.ToJson());
            }
            var entity = _blysRepo.IQueryable(p => p.zt == "1" && p.OrganizeId == OrganizeId);
            return Content(entity.ToJson());
        }

        public ActionResult GetDataSourceList()
        {
            var dataList = _blysRepo.GetYsTree(OrganizeId, "-1");
            return Content(dataList.ToJson());
        }

        public ActionResult GetYsTree()
        {

            var treeList = new List<TreeViewModel>();
            var dataList = _blysRepo.GetYsTree(OrganizeId, "-1");

            foreach (var data in dataList)
            {

                treeList.Add(new TreeViewModel()
                {
                    id = data.Id,
                    value = "",
                    text = data.ysmc,
                    parentId = null,
                    hasChildren = true,
                    isexpand = true,
                });
                var dataChildren = _blysRepo.GetYsTree(OrganizeId, data.Id);

                foreach (var child in dataChildren)
                {
                    treeList.Add(new TreeViewModel()
                    {
                        id = child.Id,
                        value = child.ysmc,
                        text = child.ysmc,
                        parentId = child.yssjid,
                        hasChildren = false,
                        isexpand = false,
                    });
                }

            }
            var show = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));

        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _blysRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult SubmitForm(BlysEntity entity, string keyValue)
        {
            if(entity!=null)
            {
                if (!string.IsNullOrWhiteSpace(entity.ysmc) && !string.IsNullOrWhiteSpace(entity.DataSource))
                {
                    if (string.IsNullOrWhiteSpace(entity.BindingPath))
                    {
                        entity.BindingPath = entity.yscode;
                    }

                    if (!string.IsNullOrWhiteSpace(keyValue))
                    {
                        entity.LastModifierCode = UserIdentity.UserCode;
                    }
                    else
                    {
                        entity.CreatorCode = UserIdentity.UserCode;
                        entity.CreateTime = DateTime.Now;
                        entity.OrganizeId = OrganizeId;
                        entity.Id = Guid.NewGuid().ToString();
                        entity.zt = "1";
                    }

                    _blysRepo.SubmitForm(entity, keyValue);
                    return Success("操作成功。");
                }

            }
            return Error("操作失败，关键信息不可为空。");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string delYSID)
        {
            _blysRepo.DeleteForm(delYSID);
            return Success("操作成功。");
        }


        public ActionResult toCS()
        {
            string dir = this.Server.MapPath("~\\File\\");
            XMLCreate(dir + "DataSourceDescriptor.xml", OrganizeId);
            return Success("操作成功。");
        }
        public bool XMLCreate(string filePath, string OrganizeId)
        {
            bool result = true;
            try
            {

                XmlDocument xmlDoc = new XmlDocument();//新建XML文件

                //判断文件是否存在
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
        
                }
                XmlNode header = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement BigDataSourceDescriptor = xmlDoc.CreateElement("DataSourceDescriptor");
                XmlElement BigName = xmlDoc.CreateElement("Name");
                BigName.InnerText = "数据源";
                XmlElement bigChildDescriptors = xmlDoc.CreateElement("ChildDescriptors");
                xmlDoc.AppendChild(header);
                BigDataSourceDescriptor.AppendChild(BigName);
                BigDataSourceDescriptor.AppendChild(bigChildDescriptors);
                xmlDoc.AppendChild(BigDataSourceDescriptor);
                var ysList = _blysRepo.GetYsTree(OrganizeId, "-1");
                foreach (var ys in ysList)
                {
                    XmlElement parDescriptor = xmlDoc.CreateElement("Descriptor");
                    XmlElement parName = xmlDoc.CreateElement("Name");
                    parName.InnerText = ys.ysmc;
                    XmlElement parBackgroundText = xmlDoc.CreateElement("BackgroundText");
                    parBackgroundText.InnerText = ys.BackgroundText;
                    parDescriptor.AppendChild(parName);
                    parDescriptor.AppendChild(parBackgroundText);
                    XmlElement parChildDescriptors = xmlDoc.CreateElement("ChildDescriptors");
                    var ysChildren = _blysRepo.GetYsTree(OrganizeId, ys.Id);

                    foreach (var child in ysChildren)
                    {
                        XmlElement ChildDescriptor = xmlDoc.CreateElement("Descriptor");

                        if (child.UserEditable != null)
                        {
                            XmlElement UserEditable = xmlDoc.CreateElement("UserEditable");
                            UserEditable.InnerText = child.UserEditable;
                            ChildDescriptor.AppendChild(UserEditable);
                        }
                        if (child.EditStyle != null)
                        {
                            XmlElement EditStyle = xmlDoc.CreateElement("EditStyle");
                            EditStyle.InnerText = child.EditStyle;
                            ChildDescriptor.AppendChild(EditStyle);
                        }
                        XmlElement ChildValueBinding = xmlDoc.CreateElement("ValueBinding");
                        XmlElement ChildDataSource = xmlDoc.CreateElement("DataSource");
                        ChildDataSource.InnerText = child.DataSource;
                        XmlElement ChildBindingPath = xmlDoc.CreateElement("BindingPath");
                        ChildBindingPath.InnerText = child.BindingPath;
                        XmlElement ChildAutoUpdate = xmlDoc.CreateElement("AutoUpdate");
                        ChildAutoUpdate.InnerText = child.AutoUpdate;
                        XmlElement ChildReadonly = xmlDoc.CreateElement("Readonly");
                        ChildReadonly.InnerText = child.Readonly;
                        ChildValueBinding.AppendChild(ChildDataSource);
                        ChildValueBinding.AppendChild(ChildBindingPath);
                        ChildValueBinding.AppendChild(ChildAutoUpdate);
                        ChildValueBinding.AppendChild(ChildReadonly);
                        ChildDescriptor.AppendChild(ChildValueBinding);

                        XmlElement BackgroundText = xmlDoc.CreateElement("BackgroundText");
                        BackgroundText.InnerText = child.BackgroundText;
                        XmlElement Name = xmlDoc.CreateElement("Name");
                        Name.InnerText = "";
                        if (child.Style != null)
                        {
                            XmlElement DisplayFormat = xmlDoc.CreateElement("DisplayFormat");
                            XmlElement Style = xmlDoc.CreateElement("Style");
                            Style.InnerText = child.Style;
                            XmlElement Format = xmlDoc.CreateElement("Format");
                            Format.InnerText = child.Format;
                            DisplayFormat.AppendChild(Style);
                            DisplayFormat.AppendChild(Format);
                            ChildDescriptor.AppendChild(DisplayFormat);
                        }

                        XmlElement TypeName = xmlDoc.CreateElement("TypeName");
                        TypeName.InnerText = child.TypeName;
                        XmlElement Description = xmlDoc.CreateElement("Description");
                        Description.InnerText = child.ysmc;
                        XmlElement ChildDescriptors = xmlDoc.CreateElement("ChildDescriptors");
                        ChildDescriptor.AppendChild(BackgroundText);
                        ChildDescriptor.AppendChild(Name);
                        ChildDescriptor.AppendChild(TypeName);
                        ChildDescriptor.AppendChild(Description);
                        ChildDescriptor.AppendChild(ChildDescriptors);
                        parChildDescriptors.AppendChild(ChildDescriptor);
                        if (child.ListSource > 0)
                        {
                            XmlElement ListSource = xmlDoc.CreateElement("ListSource");
                            XmlElement SourceName = xmlDoc.CreateElement("SourceName");
                            XmlElement Items = xmlDoc.CreateElement("Items");
                            SourceName.InnerText = child.Description;
                            var List = _blysRepo.GetYsTree(OrganizeId, child.Id);
                            foreach (var data in List)
                            {

                                XmlElement Item = xmlDoc.CreateElement("Item");
                                XmlElement Text = xmlDoc.CreateElement("Text");
                                Text.InnerText = data.ysmc;
                                XmlElement Value = xmlDoc.CreateElement("Value");
                                Value.InnerText = data.Value;
                                Item.AppendChild(Text);
                                Item.AppendChild(Value);
                                Items.AppendChild(Item);
                            }
                            ListSource.AppendChild(SourceName);
                            ListSource.AppendChild(Items);
                            ChildDescriptor.AppendChild(ListSource);
                        }
                    }
                    parDescriptor.AppendChild(parChildDescriptors);
                    bigChildDescriptors.AppendChild(parDescriptor);
                }


                xmlDoc.Save(filePath);


            }
            catch(Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Tools;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Controllers
{
    

    public class TreatmentController :OrgControllerBase
    {

        private readonly ITreatmentportfolioRepo _treatmentportfolio;


        // GET: Treatment
        public ActionResult Index()
        {
            
            return View();
        }
        
        
        public ActionResult AForm() {

            
            return View();
        }

        public ActionResult tianjia(TreatmentportfolioEntity entity, string keyValue) {
            
            //_treatmentportfolio.Insert(collection["OrganizeId"], collection["zhmc"], collection["zhcode"], collection["ord"], collection["zlxm"], collection["zlxmmc"], collection["price"], collection["zlxmpy"], collection["zhje"]);
            _treatmentportfolio.ADDInsert(entity,keyValue);

            return Success("操作成功");
        }


       

        /// <summary>
        /// 页面加载时的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Login( string keyword) {


            //var data = keyword == "" ? _treatmentportfolio.Listselect().ToJson() : _treatmentportfolio.Keyword(keyword).ToJson();
            //var data = keyword == "" ? _treatmentportfolio.Loginview().ToJson() : _treatmentportfolio.Keyword(keyword).ToJson();
            //return Content(data);
            return View();

        }


        public ActionResult loginfy(Pagination pagination,string keyword) {
            
                var data = new
                {
                    rows = keyword == "" ? _treatmentportfolio.Loginview(pagination).ToJson() : _treatmentportfolio.Keyword(pagination,keyword).ToJson(),
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,

        };
            
            return Content(data.rows);
        }


        public ActionResult selectid(TreatmentportfolioEntity entity, string keyValue)
        {
            var data = _treatmentportfolio.selectid(entity, keyValue);
            string a = data[0].ToJson();
            return Content(data[0].ToJson());
        }


        public ActionResult Updatexg(TreatmentportfolioEntity entity, string keyValue) {
            _treatmentportfolio.Updatexg(entity,keyValue);
            return Success("修改成功");
        }


        public ActionResult deleteid(string keyValue) {
            _treatmentportfolio.deleteid(keyValue);
            return Success("删除成功");
        }

        public ActionResult deletemc(string keyValue,string zhcodemc)
        {
            _treatmentportfolio.deletemc(keyValue, zhcodemc);
            return Success("删除成功");
        }


        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult rowdata(string dataset) {
            JArray jar = JArray.Parse(dataset);
            for (int i = 0; i < jar.Count; i++)
            {
                JObject jobjar = JObject.Parse(jar[i].ToString());

                string zhcodetj = jobjar["zhcode"].ToString();
                string sfxmmc123 = jobjar["zlxmmc"].ToString();
                if (_treatmentportfolio.TJchaxun(zhcodetj, sfxmmc123).Count > 0)
                {
                    continue;
                }
                else {

                    //string sfdl = jobjar["sfdl"].ToString();
                    var sfdlsu=_treatmentportfolio.sfdlcx(sfxmmc123);

                    TreatmentportfolioEntity addrow = new TreatmentportfolioEntity
                    {
                        zhmc = jobjar["zhmc"].ToString(),
                        zhcode = jobjar["zhcode"].ToString(),
                        ord = int.Parse(jobjar["ord"].ToString()),
                        zlxm = jobjar["zlxm"].ToString(),
                        zlxmmc = jobjar["zlxmmc"].ToString(),
                        price = decimal.Parse(jobjar["price"].ToString()),
                        zlxmpy = jobjar["zlxmpy"].ToString(),
                        OrganizeId = OrganizeId,
                        sfdl = sfdlsu
                    };
                    _treatmentportfolio.ADDrowdata(addrow);
                }
                
            }
            
            return Success("添加成功");
        }




        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult Tjchaxun(string zhcodetj,string sfxmmc123) {

            var data = _treatmentportfolio.TJchaxun(zhcodetj,sfxmmc123);
            if (data.Count > 0)
            {
                return Content("1");
            }
            else {
                return Content("0");
            }
            
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult Codecx(TreatmentportfolioEntity entity, string zhcode)
        {
            var data = _treatmentportfolio.Codecx(entity,zhcode);
            var str = JsonConvert.SerializeObject(_treatmentportfolio.Codecx(entity, zhcode));
            var a = data.ToJson();
            return Content(str);
        }


    }
}
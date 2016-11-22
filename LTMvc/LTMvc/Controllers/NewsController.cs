using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMvcModels;
using LTMvcBLL;

using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace LTMvc.Controllers
{
    public class NewsController : Controller
    {
        #region 新闻条目编辑
        public ActionResult Index()
        {
            //返回视图
            return View("Index");
        }


        /// <summary>
        /// JSON数据转换
        /// </summary>
        /// <param name="catalogId"></param>
        /// <returns></returns>
        public ActionResult GetNewsList(string catalogId )
        {
            //1.调用模型处理业务
            List<NewsInfo> newsInfo = new NewsInfoManage().GetNewsInfo(catalogId);
            //2.将当前的List对象集合转换成字符串（JSON格式）
            string strList = JsonConvert.SerializeObject(newsInfo);
            //3.返回JSON格式数据
            return Json(strList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取新闻详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetNewsDetails(string id)
        {
            //1.调用模型处理业务
            List<NewsInfo> newsInfo = new NewsInfoManage().GetNewsDetails(id);
            //2.将当前的List对象集合转换成字符串（JSON格式）
            string strList = JsonConvert.SerializeObject(newsInfo[0]);
            //3.返回JSON格式数据
            return Json(strList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Add()
        {
            //1.新增数据
            //获取数据并封装
            NewsInfo objNewsInfo = new NewsInfo() {
                id = Guid.NewGuid().ToString(),
                catalog_name = Request.Params["news_catalog"],
                catalog_id = Request.Params["catalog_id"],
                news_title = Request.Params["news_title"],
                news_summary = Request.Params["news_summary"],
                is_published = Convert.ToBoolean(0),
                publish_date = DateTime.Now,
                write_date = DateTime.Now,
                view_times = 1,
                create_time = DateTime.Now,
                creater_name = "管理员",//目前获取SESSION中的用户名存在问题，暂时用固定字符串代替
                updated_time = DateTime.Now,
                updater_name = "管理员"
            };

            NewsContent objNewsContent = new NewsContent()
            {
                id = Guid.NewGuid().ToString(),
                news_id = objNewsInfo.id,
                news_info = "",
                create_time = DateTime.Now,
                creater_name = "管理员",
                updated_time = DateTime.Now,
                updater_name = "管理员"
            };
            //调用业务逻辑zhi
            int result = new NewsInfoManage().AddNewsInfo(objNewsInfo, objNewsContent);

            return RedirectToAction("GetNewsList", "News");
        }

        public ActionResult Modify()
        {
            //1.修改数据
            //获取数据并封装
            NewsInfo objNewsInfo = new NewsInfo()
            {
                id = Request.Params["id"],
                catalog_name = Request.Params["catalog_name"],
                news_title = Request.Params["news_title"],
                news_summary = Request.Params["news_summary"],
                updated_time = DateTime.Now,
                updater_name = "管理员"
            };
            //调用业务逻辑
            int result = new NewsInfoManage().ModifyNewsInfo(objNewsInfo);

            return RedirectToAction("GetNewsList", "News");
        }

        public ActionResult Del()
        {
            //1.修改数据
            //获取数据并封装
            string id = Request.Params["id"];
            string[] objIdArray = id.Split(new char[] { ',' });

            //调用业务逻辑
            int result = new NewsInfoManage().DelNewsInfo(objIdArray);
            return RedirectToAction("GetNewsList", "News");
        }


        public ActionResult Publish()
        {
            //1.修改数据
            //获取数据并封装
            string id = Request.Params["id"];
            string[] objIdArray = id.Split(new char[] { ',' });

            //调用业务逻辑
            int result = new NewsInfoManage(). PublishNews(objIdArray);
            return RedirectToAction("GetNewsList", "News");
        }
        #endregion

        #region 新闻内容管理
        public ActionResult GetNewsContent(string news_id) 
        {
            NewsContent newsContent = new NewsInfoManage().GetNewsContent(news_id);

            JavaScriptSerializer news_content = new JavaScriptSerializer();
            string strNewsContent = news_content.Serialize(newsContent);

            return Json(strNewsContent, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SaveNewsContent(string newsID, string news_content)
        {
            NewsContent objNewsContent = new NewsContent()
            {
                news_id = newsID,
                news_info = news_content
            };

            new NewsInfoManage().SaveNewsContent(objNewsContent);

            return this.Content("保存成功");
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public string UploadImgFile()
        {
            //上传文件信息
            HttpContextBase context = HttpContext;

            //存储路径
            string uploadedPath = "/UploadedFiles/Img/";

            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            var files = context.Request.Files;
            if (files.Count <= 0)
            {
                return null;
            }

            HttpPostedFileBase file = files[0];
            string result = "";

            if (file == null)
            {
                context.Response.Write("error|file is null");
                return null;
            }
            else
            {
                //获取数据并进行封装
                ImageInfo objImgInfo = new ImageInfo()
                {
                    image_id = Guid.NewGuid().ToString(),
                    image_type = file.FileName.Substring(file.FileName.LastIndexOf('.'), file.FileName.Length - file.FileName.LastIndexOf('.')),
                    image_size = file.ContentLength.ToString(),
                    image_res = "",

                    create_time = DateTime.Now,
                    creater_name = "管理员",
                    updated_time = DateTime.Now,
                    updater_name = "管理员"
                };

                string urlHost = HttpContext.Request.Url.Host + ":"+ HttpContext.Request.Url.Port.ToString();
                objImgInfo.image_name = objImgInfo.image_id + objImgInfo.image_type;
                objImgInfo.image_url = "http://" + urlHost + uploadedPath + objImgInfo.image_name;

                string savePath = context.Server.MapPath("~" + uploadedPath) + objImgInfo.image_name;

                //result = uploadedPath + new NewsInfoManage().UploadImgFile(file, objImgInfo, savePath);
                result =  new NewsInfoManage().UploadImgFile(file, objImgInfo, savePath);
            }

            return result;
        }
        

        

        public ActionResult NewsManage()
        {
            return View("NewsManage");
        }
        #endregion
    }
}
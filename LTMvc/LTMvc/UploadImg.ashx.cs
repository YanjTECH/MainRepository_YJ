using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMvc
{
    /// <summary>
    /// Summary description for UploadImg
    /// </summary>
    public class UploadImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            var files = context.Request.Files;
            if (files.Count <= 0)
            {
                return;
            }

            HttpPostedFile file = files[0];

            if (file == null)
            {
                context.Response.Write("error|file is null");
                return;
            }
            else
            {
                string path = context.Server.MapPath("~/uploadedFiles/");  //存储图片的文件夹
                string originalFileName = file.FileName;
                string fileExtension = originalFileName.Substring(originalFileName.LastIndexOf('.'), originalFileName.Length - originalFileName.LastIndexOf('.'));
                string currentFileName = (new Random()).Next() + fileExtension;  //文件名中不要带中文，否则会出错
                                                                                 //生成文件路径
                string imagePath = path + currentFileName;
                //保存文件
                file.SaveAs(imagePath);

                //获取图片url地址
                string imgUrl = "http://localhost:8888/uploadedFiles/" + currentFileName;

                //返回图片url地址
                context.Response.Write(imgUrl);
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
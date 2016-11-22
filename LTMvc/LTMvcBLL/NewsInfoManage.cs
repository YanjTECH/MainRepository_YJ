using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTMvcDAL;
using LTMvcModels;
using System.Web;

namespace LTMvcBLL
{
    public class NewsInfoManage
    {
        public List<NewsInfo> GetNewsInfo(string catalogId)
        {
            return new NewsInfoService().GetNewsInfo(catalogId);
        }

        public List<NewsInfo> GetNewsDetails(string id)
        {
            return new NewsInfoService().GetNewsDetails(id);
        }

        public int AddNewsInfo(NewsInfo objNewsInfo, NewsContent objNewsContent)
        {
            int result;
            result = new NewsInfoService().AddNewsInfo(objNewsInfo);
            result = new NewsInfoService().AddNewsContent(objNewsContent);

            return result;
        }


        public int ModifyNewsInfo(NewsInfo objNewsInfo)
        {
            return new NewsInfoService().ModifyNewsInfo(objNewsInfo);
        }

        public int DelNewsInfo(string[] objIdArray) {
            return new NewsInfoService().DelNewsInfo(objIdArray);
        }

        public int PublishNews(string[] objIdArray)
        {
            return new NewsInfoService().PublishNews(objIdArray);
        }


        public NewsContent GetNewsContent(string news_id)
        {
            return new NewsInfoService().GetNewsContent(news_id);
        }

        public int SaveNewsContent(NewsContent objNewsContent)
        {
            return new NewsInfoService().SaveNewsContent(objNewsContent);
        }

        public string UploadImgFile(HttpPostedFileBase objFile, ImageInfo objImgInfo, string savePath) {
            return new NewsInfoService().UploadImgFile(objFile, objImgInfo, savePath);
        }



    }

}

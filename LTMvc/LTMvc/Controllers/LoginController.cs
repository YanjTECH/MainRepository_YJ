using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMvcBLL;
using LTMvcModels;

namespace LTMvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("UserLogin");
        }


        public ActionResult UserLogin()
        {
            //获取用户输入的数据
            UserAccount objUser = new UserAccount()
            {
                user_id = Request.Params["user_id"],
                user_pwd = Request.Params["user_pwd"]
            };

            //调用登录业务逻辑
            objUser = new UserAccountManage().UserLogin(objUser);
            if(objUser != null)
            {
                ViewBag.user_name = objUser.user_name;
                return RedirectToAction("Index", "Home");
                //return View("Home/Index");
            }
            else
            {
                ViewBag.account_info = false;
                return View();
            }

            //return View();
        }
    }
}
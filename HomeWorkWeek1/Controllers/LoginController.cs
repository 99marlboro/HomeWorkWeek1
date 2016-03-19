using HomeWorkWeek1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HomeWorkWeek1.Controllers
{
    public class LoginController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(客戶資料登入ViewModel login)
        {
            string szMD5密碼 = base.StrMD5(login.密碼);
            if (repo客戶資料.CheckLogin(login.帳號, szMD5密碼))
            {
                FormsAuthentication.RedirectFromLoginPage(login.帳號, login.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "您輸入的帳密不正確!!");
            return View();
        }       

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
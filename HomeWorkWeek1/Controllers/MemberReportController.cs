using HomeWorkWeek1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWorkWeek1.Controllers
{
    public class MemberReportController : Controller
    {
        private 客戶資料Entities1 db = new 客戶資料Entities1();
        // GET: MemberReport
        public ActionResult Index()
        {
            var data = db.客戶資料總表.ToList().OrderBy(m => m.客戶名稱);
            return View(data);
        }
    }
}
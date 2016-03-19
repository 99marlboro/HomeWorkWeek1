using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWorkWeek1.Controllers
{
    [紀錄Action的執行時間Attribute]
    public class 紀錄Action的執行時間Attribute : ActionFilterAttribute
    {
        private DateTime StTime;
        private DateTime EdTime;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 紀錄開始時間
            StTime = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // 紀錄結束時間
            EdTime = DateTime.Now;
            // 計算執行時間
            //TimeSpan exectuionTime = TimeSpan.FromHours(1);
            TimeSpan exectuionTime = EdTime - StTime;
            filterContext.Controller.ViewBag.執行時間 = exectuionTime.TotalMilliseconds;

            base.OnActionExecuted(filterContext);
        }
    }
}
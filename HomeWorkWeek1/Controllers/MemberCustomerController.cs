using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeWorkWeek1.Models;
using System.Data.Entity.Validation;

namespace HomeWorkWeek1.Controllers
{
    public class MemberCustomerController : Controller
    {
        private 客戶資料Entities1 db = new 客戶資料Entities1();

        #region View
        // GET: MemberCustomer
        public ActionResult Index(int? id, string Name, string Cell, string Email, string KeyWord)
        {
            if (id.HasValue)
            {
                ViewData["iMCid"] = Convert.ToInt16(id);
            }
            else
            {
                return RedirectToAction("Index", "Member");
            }
            string szName = Name;
            string szCell = Cell;
            string szEmail = Email;
            string szKeyWord = KeyWord;

            var data = db.客戶聯絡人.Where(m => m.是否已刪除 == false).OrderBy(m => m.Id).ToList().AsQueryable();
            if (ViewData["iMCid"] != null)
            {
                data = data.Where(m => m.客戶Id == Convert.ToInt16(ViewData["iMCid"]));
            }
            if (!string.IsNullOrEmpty(szName))
            {
                data = data.Where(m => m.姓名.Contains(szName.Trim()));
            }
            if (!string.IsNullOrEmpty(szCell))
            {
                data = data.Where(m => m.手機.Contains(szCell.Trim()));
            }
            if (!string.IsNullOrEmpty(szEmail))
            {
                data = data.Where(m => m.Email.Contains(szEmail.Trim()));
            }
            if (!string.IsNullOrEmpty(szKeyWord))
            {
                data = data.Where(m => m.手機.Contains(szKeyWord.Trim()) || m.姓名.Contains(szKeyWord.Trim()) || m.電話.Contains(szKeyWord.Trim()) || m.職稱.Contains(szKeyWord.Trim()));
            }
            return View(data);
        }
        #endregion

        #region Create
        public ActionResult Create(int icMCid)
        {
            GetDropDownList(icMCid);
            客戶聯絡人 oMemberCustomer = new 客戶聯絡人();
            oMemberCustomer.客戶Id = icMCid;
            oMemberCustomer.客戶資料 = db.客戶資料.Where(m => m.Id == icMCid).FirstOrDefault();
            return View(oMemberCustomer);
        }

        [HttpPost]
        public ActionResult Create(客戶聯絡人 oMemberCustomer)
        {
            //檢察重覆Email
            客戶聯絡人 oCheckMemberCustomer = new 客戶聯絡人();
            oCheckMemberCustomer = db.客戶聯絡人.Where(m => m.客戶Id == oMemberCustomer.客戶Id && m.Email == oMemberCustomer.Email).FirstOrDefault();
            // oCheckMemberCustomer.Any()  //可以用
            if (oCheckMemberCustomer == null)
            {
                db.客戶聯絡人.Add(oMemberCustomer);
                SaveChanges();                
            }
            //else
            //{
            //    TempData["message"] = " Email重覆!!!";
            //    return View();
            //}
            return RedirectToAction("Index", "MemberCustomer");
            
        }
        #endregion 
        
        #region Edit
        public ActionResult Edit(int id)
        {
            var data = db.客戶聯絡人.FirstOrDefault(m => m.Id == id);
            GetDropDownList(data.客戶Id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(客戶聯絡人 oMemberCustomer)
        {
            //檢察重覆Email
            客戶聯絡人 oCheckMemberCustomer = new 客戶聯絡人();
            oCheckMemberCustomer = db.客戶聯絡人.Where(m => m.客戶Id == oMemberCustomer.客戶Id && m.Email == oMemberCustomer.Email).FirstOrDefault();
            if (oCheckMemberCustomer.Id == oMemberCustomer.Id)
            {
                var data = db.客戶聯絡人.Find(oMemberCustomer.Id);
                data.職稱 = oMemberCustomer.職稱;
                data.姓名 = oMemberCustomer.姓名;
                data.Email = oMemberCustomer.Email;
                data.手機 = oMemberCustomer.手機;
                data.電話 = oMemberCustomer.電話;
                data.客戶Id = oMemberCustomer.客戶Id;
                data.是否已刪除 = oMemberCustomer.是否已刪除;
                SaveChanges();
            }
            //else {
            //    TempData["message"] = " Email重覆!!!";
            //    return View();
            //}
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public ActionResult Detail(int id)
        {
            var data = db.客戶聯絡人.FirstOrDefault(m => m.Id == id);
            GetDropDownList(data.客戶Id);
            return View(data);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            var oMemberBank = db.客戶聯絡人.Find(id);
            oMemberBank.是否已刪除 = true;
            SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Other
        private void SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in item.ValidationErrors)
                    {
                        string entityName = item.Entry.Entity.GetType().Name;
                        throw new Exception(entityName + " 類型驗證失敗:" + err.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private void GetDropDownList(int idd)
        {
            //LIST DropDownList
            var data = db.客戶資料.OrderBy(m => m.Id);
            SelectList slMember = new SelectList(data, "Id", "客戶名稱", idd);
            ViewBag.客戶Id = slMember;

        }
        #endregion

        public ActionResult AlertMsg()
        {
            return View();
        }
    }
}
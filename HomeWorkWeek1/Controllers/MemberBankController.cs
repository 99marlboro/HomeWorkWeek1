using System;
using System.Linq;
using System.Web.Mvc;
using HomeWorkWeek1.Models;
using System.Data.Entity.Validation;

namespace HomeWorkWeek1.Controllers
{
    public class MemberBankController : Controller
    {
        private 客戶資料Entities1 db = new 客戶資料Entities1();

        #region View
        // GET: MemberBank
        public ActionResult Index(int? id, string BankName, string AccountNo, string KeyWord)
        {
            if (id.HasValue)
            {
                ViewData["iMid"] = Convert.ToInt16(id);
            }
            else
            {
                return RedirectToAction("Index", "Member");
            }
            string szBankName = BankName;
            string szAccountNo = AccountNo;
            string szKeyWord = KeyWord;

            var data = db.客戶銀行資訊.Where(m => m.是否已刪除 == false).OrderBy(m => m.Id).ToList().AsQueryable();
            if (ViewData["iMid"] != null)
            {
                data = data.Where(m => m.客戶Id == Convert.ToInt16(ViewData["iMid"]));
            }
            if (!string.IsNullOrEmpty(szBankName))
            {
                data = data.Where(m => m.銀行名稱.Contains(szBankName.Trim()));
            }
            if (!string.IsNullOrEmpty(AccountNo))
            {
                data = data.Where(m => m.帳戶號碼.Contains(szAccountNo.Trim()));
            }
            if (!string.IsNullOrEmpty(szKeyWord))
            {
                data = data.Where(m => m.銀行名稱.Contains(szKeyWord.Trim()) || m.帳戶名稱.Contains(szKeyWord.Trim()) || m.帳戶號碼.Contains(szKeyWord.Trim()));
            }
            return View(data);
        }
        #endregion

        #region Create
        public ActionResult Create(int icMid)
        {
            GetDropDownList(icMid);           

            客戶銀行資訊 oMemberBank = new 客戶銀行資訊();
            oMemberBank.客戶Id = icMid;
            oMemberBank.客戶資料 = db.客戶資料.Where(m => m.Id == icMid).FirstOrDefault();
            return View(oMemberBank);
        }
        
        [HttpPost]
        public ActionResult Create(客戶銀行資訊 oMemberBank)
        {
            db.客戶銀行資訊.Add(oMemberBank);
            SaveChanges();
            return RedirectToAction("Index", "MemberBank");
        }
        #endregion 

        #region Edit
        public ActionResult Edit(int id)
        {
            var data = db.客戶銀行資訊.FirstOrDefault(m => m.Id == id);
            GetDropDownList(data.客戶Id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(客戶銀行資訊 oMemberBank)
        {
            var data = db.客戶銀行資訊.Find(oMemberBank.Id);
            data.銀行名稱 = oMemberBank.銀行名稱;
            data.銀行代碼 = oMemberBank.銀行代碼;
            data.帳戶名稱 = oMemberBank.帳戶名稱;
            data.帳戶號碼 = oMemberBank.帳戶號碼;
            data.分行代碼 = oMemberBank.分行代碼;
            data.客戶Id = oMemberBank.客戶Id;
            //data.客戶資料 = oMemberBank.客戶資料;
            data.是否已刪除 = oMemberBank.是否已刪除;
            SaveChanges();  
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public ActionResult Detail(int id)
        {
            var data = db.客戶銀行資訊.FirstOrDefault(m => m.Id == id);
            GetDropDownList(data.客戶Id);
            return View(data);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            var oMemberBank = db.客戶銀行資訊.Find(id);
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
    }
}
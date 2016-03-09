using System;
using System.Linq;
using System.Web.Mvc;
using HomeWorkWeek1.Models;
using System.Data.Entity.Validation;

namespace HomeWorkWeek1.Controllers
{
    public class MemberController : Controller
    {
        private 客戶資料Entities1 db = new 客戶資料Entities1();

        #region View
        // GET: Member
        public ActionResult Index(string MemberName, string MemberNo, string KeyWord)
        {
            string szMemberName = MemberName;
            string szMemberNo = MemberNo;
            string szKeyWord = KeyWord;

            var data = db.客戶資料.Where(m => m.是否已刪除 == false).OrderBy(m => m.Id).ToList().AsQueryable();
            if (!string.IsNullOrEmpty(szMemberName))
            {
                data = data.Where(m => m.客戶名稱.Contains(szMemberName.Trim()));
            }
            if (!string.IsNullOrEmpty(szMemberNo))
            {
                data = data.Where(m => m.統一編號.Contains(szMemberNo.Trim()));
            }
            if (!string.IsNullOrEmpty(szKeyWord))
            {
                data = data.Where(m => m.客戶名稱.Contains(szKeyWord.Trim()) || m.統一編號.Contains(szKeyWord.Trim()) || m.地址.Contains(szKeyWord.Trim()) || m.電話.Contains(szKeyWord.Trim()) || m.傳真.Contains(szKeyWord.Trim()));
            }     
            return View(data);
        }
        #endregion 

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 oMember)
        {
            db.客戶資料.Add(oMember);
            SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion 

        #region Edit
        public ActionResult Edit(int id)
        {
            var data = db.客戶資料.FirstOrDefault(m => m.Id == id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(客戶資料 oMember)
        {
            var data = db.客戶資料.Find(oMember.Id);
            data.客戶名稱 = oMember.客戶名稱;
            data.統一編號 = oMember.統一編號;
            data.電話 = oMember.電話;
            data.傳真 = oMember.傳真;
            data.地址 = oMember.地址;
            data.Email = oMember.Email;
            data.是否已刪除 = oMember.是否已刪除;
            SaveChanges();  
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public ActionResult Detail(int id)
        {
            var data = db.客戶資料.FirstOrDefault(m => m.Id == id);
            return View(data);        
        }
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {            
            var oMember = db.客戶資料.Find(id);
            oMember.是否已刪除 = true;
            //db.客戶銀行資訊.RemoveRange(oMember.客戶銀行資訊);
            //db.客戶聯絡人.RemoveRange(oMember.客戶聯絡人);
            //db.客戶資料.Remove(oMember);
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

        #endregion
    }
}
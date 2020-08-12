using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCorePractice.Models;

namespace NetCorePractice.Controllers
{
    public class MemberController : Controller
    {
        private readonly PracticeContext _context;

        public MemberController(PracticeContext context)
        {
            _context = context;
        }


        //查詢
        public IActionResult Index()
        {
            var query = this._context.Member.ToList();                  



            return View(query);
        }

        public ActionResult PartialAllUser()
        {
            return PartialView();
        }
        #region 註冊
        //註冊
        public IActionResult Insert()
        {          
            return View();
        }
        //註冊
        public virtual JsonResult Registered(string UserID, string Pass, string CName, string Phone, string Tel, string Gender, string Birth)
        {


            string Message = "成功";

            var result =Confirmation(UserID, Pass, CName, Phone, Tel, Gender, Birth);
            if (result.ToString() == "")
            {
                var InsertResult =InsertUser(UserID, Pass, CName, Phone, Tel, Gender, Birth);
                if (InsertResult != true)
                {
                    Message = "新增失敗";
                }
            }
            else
            {
                Message = result.ToString();
            }


            return Json(Message);
        }

        public bool InsertUser(string userID, string pass, string cName, string phone, string tel, string gender, string birth)
        {
            try
            {
                var toDb = new Member
                {
                    Account = userID,
                    Password = pass,
                    Name = cName,
                    Phone = phone,
                    Tel = tel,
                    Gender = gender,
                    Birthday = DateTime.Parse(birth)
                };

                using (var dbContext = _context)
                {
                    dbContext.Member.Add(toDb);
                    dbContext.SaveChanges();

                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

                return false;
            }


        }

        #endregion

        #region 修改
        //修改
        public IActionResult Edit(string key)
        {
            var result = this._context.Member.Where(x => x.Account == key).ToList();
            return View(result);
        }

        public virtual JsonResult EditMember(string UserID, string Pass, string CName, string Phone, string Tel, string Gender, string Birth)
        {


            string Message = "成功";

            var result =Confirmation("pass", Pass, CName, Phone, Tel, Gender, Birth);
            if (result.ToString() == "")
            {
                var InsertResult =UpdateUser(UserID, Pass, CName, Phone, Tel, Gender, Birth);
                if (InsertResult != true)
                {
                    Message = "修改失敗";
                }
            }
            else
            {
                Message = result.ToString();
            }


            return Json(Message);
        }

        public bool UpdateUser(string userID, string pass, string cName, string phone, string tel, string gender, string birth)
        {
            try
            {
                // Query the database for the row to be updated.
                var query =
                    (from mem in _context.Member
                     where mem.Account == userID
                     select mem).ToList();

                // Execute the query, and change the column values
                // you want to change.
                foreach (Member mem in query)
                {
                    mem.Password = pass;
                    mem.Name = cName;
                    mem.Phone = phone;
                    mem.Tel = tel;
                    mem.Gender = gender;
                    mem.Birthday = DateTime.Parse(birth);
                }


                _context.SaveChanges();

                return true;
            }
            catch (DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

                return false;
            }


        }
        #endregion

        #region 刪除
        //刪除
        public IActionResult Delete(string key)
        {
            var result =DeleteUser(key);

            return RedirectToAction("Index", "Member", new { Area = "" });
        }




        //刪除USER
        public bool DeleteUser(string userID)
        {
            try
            {
                var DeMember = _context.Member.Where(x => x.Account == userID).FirstOrDefault();


                using (var dbContext = _context)
                {
                    dbContext.Member.Remove(DeMember);
                    dbContext.SaveChanges();

                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

                return false;
            }


        }

        #endregion

        #region 資料檢查
        //USER資料檢查
        public string Confirmation(string userID, string pass, string cName, string phone, string tel, string gender, string birth)
        {
            string Message = "";

            var IsExisits = _context.Member.Where(x => x.Account == userID).Count();

            if (userID == "" || userID == null)
            {

                Message = "帳號有誤";
                return (Message);
            }
            if (IsExisits >= 1)
            {

                Message = "用戶已註冊";
                return (Message);
            }
            if (pass == "" || pass == null)
            {

                Message = "密碼有誤";
                return (Message);
            }
            if (cName == "" || cName == null)
            {

                Message = "姓名有誤";
                return (Message);
            }
            if (phone == "" || phone == null)
            {

                Message = "電話有誤";
                return (Message);
            }
            //if (tel == "" || tel == null)
            //{

            //    Message = "電話有誤";
            //    return (Message);
            //}
            if (gender.ToUpper() != "M" && gender.ToUpper() != "F")
            {
                Message = "性別有誤";
                return (Message);
            }
            if (ValidateDate(birth) != true)
            {
                Message = "日期有誤";
                return (Message);
            }


            return (Message);
        }
        //日期檢查
        private bool ValidateDate(string stringDateValue)

        {
            DateTime sd;//供判斷暫存之用
            if (String.IsNullOrEmpty(stringDateValue) ||
               !DateTime.TryParse(stringDateValue, out sd))
            {
                return false;
            }
            return true;

        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.POCOModel;
using Test.ViewModel;
using System.Web.Security;

namespace Test.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        static string UName = "";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult UserLogin(string name, string pwd)
        {
            using (var db = new LicenceEntity())
            {
                var user = db.UserInfo.FirstOrDefault(u => u.UserName == name && u.Pwd == pwd);
                UName = user.UserName;
                if (user == null)
                {
                    return Json(new { State = -1, Message = "登录失败" });
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(name, false);
                    return Json(new { State = 1, Message = "登录成功" });
                }
            }
        }
        private List<SelectListItem> GetList()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem { Text="需要",Value="1"},
                new SelectListItem { Text="不需要",Value="0"}
            };
            return list;
        }
        //public ActionResult Default(string Name = "", string IsReview = "", string State = "")
        //{
        //    using (var db = new LicenceEntity())
        //    {
        //        var list = new List<LicenceViewModel>();
        //        bool? view;
        //        if (IsReview != "")
        //        {
        //            view = Convert.ToBoolean(IsReview);
        //        }
        //        else
        //        {
        //            list = db.Licence.Where(l => l.Name.Contains(Name) && l.State.Contains(State)).Select(l => new LicenceViewModel()
        //            {
        //                ID = l.ID,
        //                Name = l.Name,
        //                IssueDate = l.IssueDate,
        //                VaildDate = l.VaildDate,
        //                IsReview = l.IsReview,
        //                ReviewDate = l.ReviewDate,
        //                State = l.State,
        //                LendUserName = l.UserInfo.UserName
        //            }).ToList();
        //            ViewBag.list = GetList();
        //            return View(list);
        //        }
        //        list = db.Licence.Where(l => l.Name.Contains(Name) && l.IsReview == view && l.State.Contains(State)).Select(l => new LicenceViewModel()
        //        {
        //            ID = l.ID,
        //            Name = l.Name,
        //            IssueDate = l.IssueDate,
        //            VaildDate = l.VaildDate,
        //            IsReview = l.IsReview,
        //            ReviewDate = l.ReviewDate,
        //            State = l.State,
        //            LendUserName = l.UserInfo.UserName
        //        }).ToList();
        //        return View(list);
        //    }
        //}
        public ActionResult Default(string Name = "", int IsReview = -1, int State = 0)
        {
            using (var db = new LicenceEntity())
            {
                var list = db.Licence.Where(l => (l.Name.Contains(Name)) && (IsReview == -1 ? true : IsReview == 1 ? l.IsReview == true : l.IsReview == false) &&
                (State == 0 ? true : State == 1 ? l.State.Contains("借出") : l.State.Contains("在库"))).Select(l => new LicenceViewModel()
                {
                    ID = l.ID,
                    Name = l.Name,
                    IssueDate = l.IssueDate,
                    VaildDate = l.VaildDate,
                    IsReview = l.IsReview,
                    ReviewDate = l.ReviewDate,
                    State = l.State,
                    LendUserName = l.UserInfo.UserName
                }).ToList();
                ViewBag.list = GetList();
                return View(list);
            }
        }
        public ActionResult UserGuihuan(int id)
        {
            using (var db = new LicenceEntity())
            {
                var user = db.Licence.FirstOrDefault(l => l.ID == id);
                if (user == null)
                {
                    return Json(new { State = -1, Message = "归还失败" });
                }
                user.State = "在库";
                user.LendUserID = null;
                db.SaveChanges();
                return Json(new { State = 1, Message = "归还成功" });

            }
        }
        public ActionResult UserJiechu(int id)
        {
            using (var db = new LicenceEntity())
            {
                var user = db.Licence.FirstOrDefault(l => l.ID == id);
                if (user == null)
                {
                    return Json(new { State = -1, Message = "借出失败" });
                }
                //user.State = "借出(" + UName + ")";
                user.State = "借出";
                user.LendUserID = db.UserInfo.FirstOrDefault(u => u.UserName == User.Identity.Name).ID;
                db.SaveChanges();
                return Json(new { State = 1, Message = "借出成功" });
            }
        }
        public ActionResult Delete(int id)
        {
            using (var db = new LicenceEntity())
            {
                var user = db.Licence.FirstOrDefault(l => l.ID == id);
                if (user == null)
                {
                    return Json(new { State = -1, Message = "删除失败" });
                }
                db.Licence.Remove(user);
                db.SaveChanges();
                return Json(new { State = 1, Message = "删除成功" });
            }
        }
        private List<SelectListItem> StateList()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem { Text="借出",Value="借出"},
                new SelectListItem { Text="在库",Value="在库"}
            };
            return list;
        }
        private List<SelectListItem> StateList1()
        {
            using (var db = new LicenceEntity())
            {
                var list = db.UserInfo.Select(u => new SelectListItem()
                {
                    Text = u.UserName,
                    Value = u.ID.ToString()
                }).ToList();
                return list;
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.list1 = StateList1();
            ViewBag.list = StateList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(LicenceAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new LicenceEntity())
                {
                    var lic = new Licence()
                    {
                        Name = model.Name,
                        IssueDate = model.IssueDate,
                        VaildDate = model.VaildDate,
                        IsReview = model.IsReview,
                        ReviewDate = model.ReviewDate,
                        State = model.State,
                        LendUserID = model.LendUserID
                    };
                    db.Licence.Add(lic);
                    db.SaveChanges();
                    ViewBag.msg = "添加成功";
                }
            }
            else
            {
                ModelState.AddModelError("", "添加失败");
            }
            ViewBag.list = StateList();
            ViewBag.list1 = StateList1();
            return View();
        }
    }
}
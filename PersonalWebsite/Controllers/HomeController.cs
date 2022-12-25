using PersonalWebsite.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        MyPersonalBlogEntities1 db = new MyPersonalBlogEntities1();
        public ActionResult HomePage()
        {
            var model = db.todolist.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult New()
        {
        
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(todolist list)
        {
            if (list.ID == 0) //for insert
            {
                db.todolist.Add(list);
            }
            else
            {
                var updateData = db.todolist.Find(list.ID);
                if (updateData == null)
                {
                    return HttpNotFound();
                }
                updateData.Tasks = list.Tasks;
            }

            db.SaveChanges();
            return RedirectToAction("HomePage", "Home");
        }

        public ActionResult Update(int id)
        {
            var model = db.todolist.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("New", model);
        }

        public ActionResult Delete(int id)
        {
            var delete = db.todolist.Find(id);
            if (delete == null)
            {
                return HttpNotFound();
            }
            db.todolist.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("HomePage", "Home");
        }



    }
}
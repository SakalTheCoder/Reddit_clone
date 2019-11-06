using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Reddit.Models;

namespace Reddit.Controllers
{
    
    public class newsfeedpostsController : Controller
    {
        private reddit_dbEntities db = new reddit_dbEntities();

        // GET: newsfeedposts
        public ActionResult Index()
        {
            try
            {
                return View(db.newsfeedposts.ToList());
            }catch(Exception e)
            {
                return View();
            }
        }

        // GET: newsfeedposts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            newsfeedpost newsfeedpost = db.newsfeedposts.Find(id);
            if (newsfeedpost == null)
            {
                return HttpNotFound();
            }
            return View(newsfeedpost);
        }

        // GET: newsfeedposts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: newsfeedposts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Caption,Image_src,User_ID")] newsfeedpost newsfeedpost)
        {
            if (ModelState.IsValid)
            {
                db.newsfeedposts.Add(newsfeedpost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsfeedpost);
        }

        // GET: newsfeedposts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            newsfeedpost newsfeedpost = db.newsfeedposts.Find(id);
            if (newsfeedpost == null)
            {
                return HttpNotFound();
            }
            return View(newsfeedpost);
        }

        // POST: newsfeedposts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Caption,Image_src,User_ID")] newsfeedpost newsfeedpost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsfeedpost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsfeedpost);
        }

        // GET: newsfeedposts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            newsfeedpost newsfeedpost = db.newsfeedposts.Find(id);
            if (newsfeedpost == null)
            {
                return HttpNotFound();
            }
            return View(newsfeedpost);
        }

        // POST: newsfeedposts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            newsfeedpost newsfeedpost = db.newsfeedposts.Find(id);
            db.newsfeedposts.Remove(newsfeedpost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

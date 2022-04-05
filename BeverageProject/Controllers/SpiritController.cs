﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities.Products;
using MyDatabase;
using PagedList;

namespace BeverageProject.Controllers
{
    public class SpiritController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Spirit
        public ActionResult Index(string category, string searchSpirit, int? page, int? pSize)
        {
            @ViewBag.searchSpirit = searchSpirit;

            var spirits = db.Spirits.ToList();
            if (!string.IsNullOrEmpty(searchSpirit))
            {
                spirits = spirits.Where(t => t.Name.ToUpper().Contains(searchSpirit.ToUpper())).ToList();
            }

            int pageNumber = page ?? 1;
            int pageSize = pSize ?? 10;

            if (category is null)
            {
                return View(spirits.ToPagedList(pageNumber, pageSize));
            }
            return View(spirits.Where(x => x.Kind == category).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult IndexCollection(string category, string searchSpirit, int? page, int? pSize)
        {
            @ViewBag.searchSpirit = searchSpirit;

            var spirits = db.Spirits.ToList();
            if (!string.IsNullOrEmpty(searchSpirit))
            {
                spirits = spirits.Where(t => t.Name.ToUpper().Contains(searchSpirit.ToUpper())).ToList();
            }

            int pageNumber = page ?? 1;
            int pageSize = pSize ?? 12;
            if (category is null)
            {
                return View(spirits.ToPagedList(pageNumber, pageSize));
            }
            return View(spirits.Where(x => x.Kind == category).ToPagedList(pageNumber, pageSize));
        }

        // GET: Spirit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spirit spirit = db.Spirits.Find(id);
            if (spirit == null)
            {
                return HttpNotFound();
            }
            return View(spirit);
        }

        // GET: Spirit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Spirit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,PhotoUrl")] Spirit spirit)
        {
            if (ModelState.IsValid)
            {
                db.Spirits.Add(spirit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spirit);
        }

        // GET: Spirit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spirit spirit = db.Spirits.Find(id);
            if (spirit == null)
            {
                return HttpNotFound();
            }
            return View(spirit);
        }

        // POST: Spirit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,PhotoUrl")] Spirit spirit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spirit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spirit);
        }

        // GET: Spirit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spirit spirit = db.Spirits.Find(id);
            if (spirit == null)
            {
                return HttpNotFound();
            }
            return View(spirit);
        }

        // POST: Spirit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spirit spirit = db.Spirits.Find(id);
            db.Spirits.Remove(spirit);
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

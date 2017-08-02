using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BarbaraDoces.Context;

namespace BarbaraDoces.Models
{
    public class NovidadeController : Controller
    {
        private Entities db = new Entities();

        // GET: Novidade
        public ActionResult Index()
        {
            return View();
        }

        // GET: Novidade/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // GET: Novidade/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Novidade/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNovidade,TituloNovidade,DescNovidade,DtNovidade")] NovidadeViewModel novidadeViewModel)
        {
            if (ModelState.IsValid)
            {
                //db.NovidadeViewModels.Add(novidadeViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(novidadeViewModel);
        }

        // GET: Novidade/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //NovidadeViewModel novidadeViewModel = db.NovidadeViewModels.Find(id);
            //if (novidadeViewModel == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: Novidade/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNovidade,TituloNovidade,DescNovidade,DtNovidade")] NovidadeViewModel novidadeViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(novidadeViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(novidadeViewModel);
        }

        // GET: Novidade/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //NovidadeViewModel novidadeViewModel = db.NovidadeViewModels.Find(id);
            //if (novidadeViewModel == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: Novidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //NovidadeViewModel novidadeViewModel = db.NovidadeViewModels.Find(id);
            //db.NovidadeViewModels.Remove(novidadeViewModel);
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

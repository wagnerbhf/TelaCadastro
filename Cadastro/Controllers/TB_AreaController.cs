using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cadastro;

namespace Cadastro.Controllers
{
   public class TB_AreaController : Controller
   {
      private GerenciamentoContext db = new GerenciamentoContext();

      // GET: TB_Area
      public ActionResult Index()
      {
         var tB_Area = db.TB_Area.Include(t => t.TB_Empresa);
         return View(tB_Area.ToList());
      }

      // GET: TB_Area/Details/5
      public ActionResult Details(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Area tB_Area = db.TB_Area.Find(id);
         if (tB_Area == null)
         {
            return HttpNotFound();
         }
         return View(tB_Area);
      }

      // GET: TB_Area/Create
      public ActionResult Create()
      {
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial");
         return View();
      }

      // POST: TB_Area/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "IdArea,IdEmpresa,Descricao,Sigla")] TB_Area tB_Area)
      {
         if (ModelState.IsValid)
         {
            IQueryable<TB_Area> areas = db.TB_Area;
            tB_Area.IdArea = areas.Count() + 1;

            tB_Area.TB_Empresa = db.TB_Empresa.First(p => p.IdEmpresa == tB_Area.IdEmpresa);

            db.TB_Area.Add(tB_Area);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Area.IdEmpresa);
         return View(tB_Area);
      }

      // GET: TB_Area/Edit/5
      public ActionResult Edit(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Area tB_Area = db.TB_Area.Find(id);
         if (tB_Area == null)
         {
            return HttpNotFound();
         }
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Area.IdEmpresa);
         return View(tB_Area);
      }

      // POST: TB_Area/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "IdArea,IdEmpresa,Descricao,Sigla")] TB_Area tB_Area)
      {
         if (ModelState.IsValid)
         {
            db.Entry(tB_Area).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Area.IdEmpresa);
         return View(tB_Area);
      }

      // GET: TB_Area/Delete/5
      public ActionResult Delete(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Area tB_Area = db.TB_Area.Find(id);
         if (tB_Area == null)
         {
            return HttpNotFound();
         }
         return View(tB_Area);
      }

      // POST: TB_Area/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         TB_Area tB_Area = db.TB_Area.Find(id);
         db.TB_Area.Remove(tB_Area);
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

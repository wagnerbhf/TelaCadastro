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
   public class TB_EmpresaController : Controller
   {
      private GerenciamentoContext db = new GerenciamentoContext();

      // GET: TB_Empresa
      public ActionResult Index()
      {
         return View(db.TB_Empresa.ToList());
      }

      // GET: TB_Empresa/Details/5
      public ActionResult Details(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Empresa tB_Empresa = db.TB_Empresa.Find(id);
         if (tB_Empresa == null)
         {
            return HttpNotFound();
         }
         return View(tB_Empresa);
      }

      // GET: TB_Empresa/Create
      public ActionResult Create()
      {
         return View();
      }

      // POST: TB_Empresa/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "IdEmpresa,RazaoSocial,CNPJ,Email")] TB_Empresa tB_Empresa)
      {
         if (ModelState.IsValid)
         {
            IQueryable<TB_Empresa> empresas = db.TB_Empresa;
            tB_Empresa.IdEmpresa = empresas.Count() + 1;

            db.TB_Empresa.Add(tB_Empresa);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         return View(tB_Empresa);
      }

      // GET: TB_Empresa/Edit/5
      public ActionResult Edit(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Empresa tB_Empresa = db.TB_Empresa.Find(id);
         if (tB_Empresa == null)
         {
            return HttpNotFound();
         }
         return View(tB_Empresa);
      }

      // POST: TB_Empresa/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "IdEmpresa,RazaoSocial,CNPJ,Email")] TB_Empresa tB_Empresa)
      {
         if (ModelState.IsValid)
         {
            db.Entry(tB_Empresa).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(tB_Empresa);
      }

      // GET: TB_Empresa/Delete/5
      public ActionResult Delete(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Empresa tB_Empresa = db.TB_Empresa.Find(id);
         if (tB_Empresa == null)
         {
            return HttpNotFound();
         }
         return View(tB_Empresa);
      }

      // POST: TB_Empresa/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         TB_Empresa tB_Empresa = db.TB_Empresa.Find(id);
         db.TB_Empresa.Remove(tB_Empresa);
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

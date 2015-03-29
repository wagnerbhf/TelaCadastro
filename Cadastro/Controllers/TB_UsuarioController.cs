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
   public class TB_UsuarioController : Controller
   {
      private GerenciamentoContext db = new GerenciamentoContext();

      private void RedirecionarSeNaoEstiverLogado()
      {
         if (Session["usuario"] == null)
         {
            Response.Redirect(@"~/User/Login");
         }
      }

      // GET: TB_Usuario
      public ActionResult Index()
      {
         var tB_Usuario = db.TB_Usuario.Include(t => t.TB_Empresa);
         return View(tB_Usuario.ToList());
      }

      // GET: TB_Usuario/Details/5
      public ActionResult Details(int? id)
      {
         RedirecionarSeNaoEstiverLogado();

         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Usuario tB_Usuario = db.TB_Usuario.Find(id);
         if (tB_Usuario == null)
         {
            return HttpNotFound();
         }
         return View(tB_Usuario);
      }

      // GET: TB_Usuario/Create
      public ActionResult Create()
      {
         RedirecionarSeNaoEstiverLogado();

         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial");
         return View();
      }

      // POST: TB_Usuario/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "IdUsuario,IdEmpresa,Nome,Login,Senha,Email,Ativo,DataValidade")] TB_Usuario tB_Usuario)
      {
         RedirecionarSeNaoEstiverLogado();

         if (ModelState.IsValid)
         {
            IQueryable<TB_Usuario> usuarios = db.TB_Usuario;
            tB_Usuario.IdUsuario = tB_Usuario.IdUsuario = usuarios.Count() + 1;

            tB_Usuario.TB_Empresa = db.TB_Empresa.First(p => p.IdEmpresa == tB_Usuario.IdEmpresa);

            db.TB_Usuario.Add(tB_Usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Usuario.IdEmpresa);
         return View(tB_Usuario);
      }

      // GET: TB_Usuario/Edit/5
      public ActionResult Edit(int? id)
      {
         RedirecionarSeNaoEstiverLogado();

         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Usuario tB_Usuario = db.TB_Usuario.Find(id);
         if (tB_Usuario == null)
         {
            return HttpNotFound();
         }
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Usuario.IdEmpresa);
         return View(tB_Usuario);
      }

      // POST: TB_Usuario/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "IdUsuario,IdEmpresa,Nome,Login,Senha,Email,Ativo,DataValidade")] TB_Usuario tB_Usuario)
      {
         RedirecionarSeNaoEstiverLogado();

         if (ModelState.IsValid)
         {
            db.Entry(tB_Usuario).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Usuario.IdEmpresa);
         return View(tB_Usuario);
      }

      // GET: TB_Usuario/Delete/5
      public ActionResult Delete(int? id)
      {
         RedirecionarSeNaoEstiverLogado();

         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Usuario tB_Usuario = db.TB_Usuario.Find(id);
         if (tB_Usuario == null)
         {
            return HttpNotFound();
         }
         return View(tB_Usuario);
      }

      // POST: TB_Usuario/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         RedirecionarSeNaoEstiverLogado();

         TB_Usuario tB_Usuario = db.TB_Usuario.Find(id);
         db.TB_Usuario.Remove(tB_Usuario);
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

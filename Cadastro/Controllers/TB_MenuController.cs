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
   public class TB_MenuController : Controller
   {
      private GerenciamentoContext db = new GerenciamentoContext();

      // GET: TB_Menu
      public ActionResult Index()
      {
         var tB_Menu = db.TB_Menu.Include(t => t.TB_Empresa);
         return View(tB_Menu.ToList());
      }

      // GET: TB_Menu/Details/5
      public ActionResult Details(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Menu tB_Menu = db.TB_Menu.Find(id);
         if (tB_Menu == null)
         {
            return HttpNotFound();
         }
         return View(tB_Menu);
      }

      // GET: TB_Menu/Create
      public ActionResult Create()
      {
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial");
         return View();
      }

      // POST: TB_Menu/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "IdMenu,IdEmpresa,Caption,Descricao,Parent_ID,URL,Seguranca,Menu,Ordem")] TB_Menu tB_Menu)
      {
         if (ModelState.IsValid)
         {
            IQueryable<TB_Menu> menus = db.TB_Menu;
            tB_Menu.IdMenu = tB_Menu.IdMenu = menus.Count() + 1;

            tB_Menu.TB_Empresa = db.TB_Empresa.First(p => p.IdEmpresa == tB_Menu.IdEmpresa);

            db.TB_Menu.Add(tB_Menu);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Menu.IdEmpresa);
         return View(tB_Menu);
      }

      // GET: TB_Menu/Edit/5
      public ActionResult Edit(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Menu tB_Menu = db.TB_Menu.Find(id);
         if (tB_Menu == null)
         {
            return HttpNotFound();
         }
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Menu.IdEmpresa);
         return View(tB_Menu);
      }

      // POST: TB_Menu/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "IdMenu,IdEmpresa,Caption,Descricao,Parent_ID,URL,Seguranca,Menu,Ordem")] TB_Menu tB_Menu)
      {
         if (ModelState.IsValid)
         {
            db.Entry(tB_Menu).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Menu.IdEmpresa);
         return View(tB_Menu);
      }

      // GET: TB_Menu/Delete/5
      public ActionResult Delete(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Menu tB_Menu = db.TB_Menu.Find(id);
         if (tB_Menu == null)
         {
            return HttpNotFound();
         }
         return View(tB_Menu);
      }

      // POST: TB_Menu/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         TB_Menu tB_Menu = db.TB_Menu.Find(id);
         db.TB_Menu.Remove(tB_Menu);
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

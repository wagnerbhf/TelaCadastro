using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cadastro;
using System.Data.Entity.Validation;

namespace Cadastro.Controllers
{
   public class TB_GrupoController : Controller
   {
      private GerenciamentoContext db = new GerenciamentoContext();

      private void RedirecionarSeNaoEstiverLogado()
      {
         if (Session["usuario"] == null)
         {
            Response.Redirect(@"~/User/Login");
         }
      }

      // GET: TB_Grupo
      public ActionResult Index()
      {
         RedirecionarSeNaoEstiverLogado();

         var tB_Grupo = db.TB_Grupo.Include(t => t.TB_Area).Include(t => t.TB_Empresa);
         return View(tB_Grupo.ToList());
      }

      // GET: TB_Grupo/Details/5
      public ActionResult Details(int? id)
      {
         RedirecionarSeNaoEstiverLogado();

         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Grupo tB_Grupo = db.TB_Grupo.Find(id);
         if (tB_Grupo == null)
         {
            return HttpNotFound();
         }
         return View(tB_Grupo);
      }

      // GET: TB_Grupo/Create
      public ActionResult Create()
      {
         RedirecionarSeNaoEstiverLogado();

         ViewBag.IdArea = new SelectList(db.TB_Area, "IdArea", "Descricao");
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial");
         return View();
      }

      // POST: TB_Grupo/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "IdGrupo,IdEmpresa,Descricao,Sigla,IdArea")] TB_Grupo tB_Grupo)
      {
         RedirecionarSeNaoEstiverLogado();

         if (ModelState.IsValid)
         {
            IQueryable<TB_Grupo> grupos = db.TB_Grupo;
            tB_Grupo.IdGrupo = grupos.Count() + 1;

            tB_Grupo.TB_Empresa = db.TB_Empresa.First(p => p.IdEmpresa == tB_Grupo.IdEmpresa);
            tB_Grupo.TB_Area = db.TB_Area.First(p => p.IdArea == tB_Grupo.IdArea);

            db.TB_Grupo.Add(tB_Grupo);

            try
            {
               db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
               // Retrieve the error messages as a list of strings.
               var errorMessages = ex.EntityValidationErrors
                       .SelectMany(x => x.ValidationErrors)
                       .Select(x => x.ErrorMessage);

               // Join the list to a single string.
               var fullErrorMessage = string.Join("; ", errorMessages);

               // Combine the original exception message with the new one.
               var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

               // Throw a new DbEntityValidationException with the improved exception message.
               throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return RedirectToAction("Index");
         }

         ViewBag.IdArea = new SelectList(db.TB_Area, "IdArea", "Descricao", tB_Grupo.IdArea);
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Grupo.IdEmpresa);
         return View(tB_Grupo);
      }

      // GET: TB_Grupo/Edit/5
      public ActionResult Edit(int? id)
      {
         RedirecionarSeNaoEstiverLogado();

         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Grupo tB_Grupo = db.TB_Grupo.Find(id);
         if (tB_Grupo == null)
         {
            return HttpNotFound();
         }
         ViewBag.IdArea = new SelectList(db.TB_Area, "IdArea", "Descricao", tB_Grupo.IdArea);
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Grupo.IdEmpresa);
         return View(tB_Grupo);
      }

      // POST: TB_Grupo/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "IdGrupo,IdEmpresa,Descricao,Sigla,IdArea")] TB_Grupo tB_Grupo)
      {
         RedirecionarSeNaoEstiverLogado();

         if (ModelState.IsValid)
         {
            db.Entry(tB_Grupo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         ViewBag.IdArea = new SelectList(db.TB_Area, "IdArea", "Descricao", tB_Grupo.IdArea);
         ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_Grupo.IdEmpresa);
         return View(tB_Grupo);
      }

      // GET: TB_Grupo/Delete/5
      public ActionResult Delete(int? id)
      {
         RedirecionarSeNaoEstiverLogado();

         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         TB_Grupo tB_Grupo = db.TB_Grupo.Find(id);
         if (tB_Grupo == null)
         {
            return HttpNotFound();
         }
         return View(tB_Grupo);
      }

      // POST: TB_Grupo/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         RedirecionarSeNaoEstiverLogado();

         TB_Grupo tB_Grupo = db.TB_Grupo.Find(id);
         db.TB_Grupo.Remove(tB_Grupo);
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

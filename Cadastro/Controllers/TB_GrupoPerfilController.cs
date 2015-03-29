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
    public class TB_GrupoPerfilController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        private void RedirecionarSeNaoEstiverLogado()
        {
           if (Session["usuario"] == null)
           {
              Response.Redirect(@"~/User/Login");
           }
        }

        // GET: TB_GrupoPerfil
        public ActionResult Index()
        {
            var tB_GrupoPerfil = db.TB_GrupoPerfil.Include(t => t.TB_Empresa).Include(t => t.TB_Grupo).Include(t => t.TB_Menu);
            return View(tB_GrupoPerfil.ToList());
        }

        // GET: TB_GrupoPerfil/Details/5
        public ActionResult Details(int? id)
        {
           RedirecionarSeNaoEstiverLogado();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_GrupoPerfil tB_GrupoPerfil = db.TB_GrupoPerfil.Find(id);
            if (tB_GrupoPerfil == null)
            {
                return HttpNotFound();
            }
            return View(tB_GrupoPerfil);
        }

        // GET: TB_GrupoPerfil/Create
        public ActionResult Create()
        {
           RedirecionarSeNaoEstiverLogado();

            ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial");
            ViewBag.IdGrupo = new SelectList(db.TB_Grupo, "IdGrupo", "Descricao");
            ViewBag.IdMenu = new SelectList(db.TB_Menu, "IdMenu", "Caption");
            return View();
        }

        // POST: TB_GrupoPerfil/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdGrupo,IdEmpresa,IdMenu,Bloqueado,Todos,Pesquisa,Inclusao,Alteracao,Delecao,Impressao,Exportacao")] TB_GrupoPerfil tB_GrupoPerfil)
        {
           RedirecionarSeNaoEstiverLogado();

            if (ModelState.IsValid)
            {
               tB_GrupoPerfil.TB_Empresa = db.TB_Empresa.First(p => p.IdEmpresa == tB_GrupoPerfil.IdEmpresa);
               tB_GrupoPerfil.TB_Grupo = db.TB_Grupo.First(p => p.IdGrupo == tB_GrupoPerfil.IdGrupo);
               tB_GrupoPerfil.TB_Menu = db.TB_Menu.First(p => p.IdMenu == tB_GrupoPerfil.IdMenu);

                db.TB_GrupoPerfil.Add(tB_GrupoPerfil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_GrupoPerfil.IdEmpresa);
            ViewBag.IdGrupo = new SelectList(db.TB_Grupo, "IdGrupo", "Descricao", tB_GrupoPerfil.IdGrupo);
            ViewBag.IdMenu = new SelectList(db.TB_Menu, "IdMenu", "Caption", tB_GrupoPerfil.IdMenu);
            return View(tB_GrupoPerfil);
        }

        // GET: TB_GrupoPerfil/Edit/5
        public ActionResult Edit(int? id)
        {
           RedirecionarSeNaoEstiverLogado();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_GrupoPerfil tB_GrupoPerfil = db.TB_GrupoPerfil.Find(id);
            if (tB_GrupoPerfil == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_GrupoPerfil.IdEmpresa);
            ViewBag.IdGrupo = new SelectList(db.TB_Grupo, "IdGrupo", "Descricao", tB_GrupoPerfil.IdGrupo);
            ViewBag.IdMenu = new SelectList(db.TB_Menu, "IdMenu", "Caption", tB_GrupoPerfil.IdMenu);
            return View(tB_GrupoPerfil);
        }

        // POST: TB_GrupoPerfil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGrupo,IdEmpresa,IdMenu,Bloqueado,Todos,Pesquisa,Inclusao,Alteracao,Delecao,Impressao,Exportacao")] TB_GrupoPerfil tB_GrupoPerfil)
        {
           RedirecionarSeNaoEstiverLogado();

            if (ModelState.IsValid)
            {
                db.Entry(tB_GrupoPerfil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpresa = new SelectList(db.TB_Empresa, "IdEmpresa", "RazaoSocial", tB_GrupoPerfil.IdEmpresa);
            ViewBag.IdGrupo = new SelectList(db.TB_Grupo, "IdGrupo", "Descricao", tB_GrupoPerfil.IdGrupo);
            ViewBag.IdMenu = new SelectList(db.TB_Menu, "IdMenu", "Caption", tB_GrupoPerfil.IdMenu);
            return View(tB_GrupoPerfil);
        }

        // GET: TB_GrupoPerfil/Delete/5
        public ActionResult Delete(int? id)
        {
           RedirecionarSeNaoEstiverLogado();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_GrupoPerfil tB_GrupoPerfil = db.TB_GrupoPerfil.Find(id);
            if (tB_GrupoPerfil == null)
            {
                return HttpNotFound();
            }
            return View(tB_GrupoPerfil);
        }

        // POST: TB_GrupoPerfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           RedirecionarSeNaoEstiverLogado();

            TB_GrupoPerfil tB_GrupoPerfil = db.TB_GrupoPerfil.Find(id);
            db.TB_GrupoPerfil.Remove(tB_GrupoPerfil);
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

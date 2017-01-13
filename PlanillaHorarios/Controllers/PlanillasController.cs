using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlanillaHorarios.DAL;
using PlanillaHorarios.Models;
using PlanillaHorarios.ViewModels;

namespace PlanillaHorarios.Controllers
{
    public partial class PlanillasController : Controller
    {
        private PlanillaContext db = new PlanillaContext();

        // GET: Planillas
        public virtual ActionResult Index(PlanillaResumen planillaResumen)
        {
            if (planillaResumen.PersonaId == 0)
            {
                return HttpNotFound();
            }
            int IdPersona = planillaResumen.PersonaId;
            int anio = planillaResumen.Anio;
            int mes = planillaResumen.Mes;

            var planillas = db.Planilla.Where(p => p.PersonaID == IdPersona).Where(p => p.Anio == anio).Where(p => p.Mes == mes).OrderBy(p => p.Dia);

            if (planillas.Count() == 0)
            {
                var DiaMax = new DateTime(anio, mes, 1).AddMonths(1).AddDays(-1).Day ;
                for (int i = 1; i < DiaMax+1; i++)
                {
                    Planilla diaSemana = new Planilla();
                    diaSemana.PersonaID = IdPersona;
                    diaSemana.Anio = anio;
                    diaSemana.Mes = mes;
                    diaSemana.Dia = i;

                    if (ModelState.IsValid)
                    {
                        db.Planilla.Add(diaSemana);
                        db.SaveChanges();
                    }
                }
            }

            var planillas2 = db.Planilla.Where(p => p.PersonaID == IdPersona).Where(p => p.Anio == anio).Where(p => p.Mes == mes).OrderBy(p => p.Dia).ToList();
            var feriados = db.Feriado.Where(f => f.Fecha.Year == anio && f.Fecha.Month == mes).ToList();
            var planillaPersonaResumenes = planillas2.Select(p =>
            {
                var fecha = new DateTime(p.Anio.Value, p.Mes.Value, p.Dia);
                var feriado = feriados.FirstOrDefault(f => f.Fecha == fecha);

                return new PlanillaPersonaResumen
                {
                    Anio = p.Anio,
                    Mes = p.Mes,
                    Dia = p.Dia,
                    DiaSemana = feriado == null ? null : "Feriado: " + feriado.Descripcion,
                    EsFeriado = feriado == null ? false : true,
                    MHoraEntrada = p.MHoraEntrada,
                    MHoraSalida = p.MHoraSalida,
                    Observaciones = p.Observaciones,
                    PersonaID = p.PersonaID,
                    PlanillaId = p.PlanillaId,
                    THoraEntrada = p.THoraEntrada,
                    THoraSalida = p.THoraSalida
                };
            });

            ViewBag.IdPersona = planillaResumen.PersonaId;
            return View(planillaPersonaResumenes);
        }

        // GET: Planillas/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planilla planilla = db.Planilla.Find(id);
            if (planilla == null)
            {
                return HttpNotFound();
            }
            return View(planilla);
        }

        // GET: Planillas/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Planillas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "PlanillaId,PersonaID,Anio,Mes,Dia,Observaciones,MHoraEntrada,MHoraSalida,THoraEntrada,THoraSalida")] Planilla planilla)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Planilla.Add(planilla);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "No se guardaron los datos. Inténtelo de nuevo, y si el problema persiste consulte al administrador de sistema.");
            }

            return View(planilla);
        }

        // GET: Planillas/Edit/5
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planilla planilla = db.Planilla.Find(id);
            if (planilla == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPersona = planilla.PersonaID;
            return View(planilla);
        }

        // POST: Planillas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "PlanillaId,PersonaID,Anio,Mes,Dia,Observaciones,MHoraEntrada,MHoraSalida,THoraEntrada,THoraSalida")] Planilla planilla)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planilla).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.IdPersona = planilla.PersonaID;
                return RedirectToAction("Index", new PlanillaResumen { PersonaId = planilla.PersonaID, Anio = (int)planilla.Anio, Mes = (int)planilla.Mes });
            }
            return View(planilla);
        }

        // GET: Planillas/Delete/5
        public virtual ActionResult Delete(bool? saveChangesError = false, int? id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Falló la eliminación. Inténtelo de nuevo, y si el problema persiste consulte al administrador de sistema.";
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planilla planilla = db.Planilla.Find(id);
            if (planilla == null)
            {
                return HttpNotFound();
            }
            return View(planilla);
        }

        // POST: Planillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }

        public virtual ActionResult PersonaDetails(PlanillaResumen planillaResumen)
        {
            return RedirectToAction("DetailsPlanilla", "Personas", planillaResumen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public virtual ActionResult VolverListado(int id)
        {
            var persona = new PlanillaResumen() { PersonaId = id };
            ViewBag.IdPersona = persona.PersonaId;
            //return RedirectToAction(MVC.Personas.DetailsPlanilla(persona));
            return RedirectToAction("DetailsPlanilla", "Personas", persona);
        }
    }
}


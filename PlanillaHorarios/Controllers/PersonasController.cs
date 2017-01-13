using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PlanillaHorarios.DAL;
using PlanillaHorarios.Models;
using PlanillaHorarios.ViewModels;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Collections.Generic;

namespace PlanillaHorarios.Controllers
{
    public partial class PersonasController : Controller
    {
        private PlanillaContext db = new PlanillaContext();

        // GET: Personas
        public virtual ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Apellido_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Nombre" ? "Nombre_desc" : "Nombre";
            var personas = from s in db.Persona
                           select s;
            switch (sortOrder)
            {
                case "Apellido_desc":
                    personas = personas.OrderByDescending(s => s.Apellido);
                    break;
                case "Nombre":
                    personas = personas.OrderBy(s => s.Nombre);
                    break;
                case "Nombre_desc":
                    personas = personas.OrderByDescending(s => s.Nombre);
                    break;
                default:
                    personas = personas.OrderBy(s => s.Apellido);
                    break;
            }
            return View(personas.ToList());
        }


        // GET: Personas/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "PersonaID,Nombre,Apellido,Cuil,Mail")] Persona persona)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Persona.Add(persona);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            catch
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "No se guardaron los datos. Inténtelo de nuevo, y si el problema persiste consulte al administrador de sistema..");
            }

            return View(persona);
        }

        // GET: Personas/Edit/5
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "PersonaID,Nombre,Apellido,Cuil,Mail")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPersona = persona.PersonaID;
            return View(persona);
        }

        // GET: Personas/Delete/5
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
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Persona persona = db.Persona.Find(id);
                db.Persona.Remove(persona);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }


        public virtual ActionResult DetailsPlanilla(PlanillaResumen pr)
        {
            if (pr.PersonaId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Persona persona = db.Persona.Find(pr.PersonaId);
            if (persona == null)
            {
                return HttpNotFound();
            }

            var planillas = db.Planilla.Where(p => p.PersonaID == pr.PersonaId).Select(l => new PlanillaResumen { PersonaId = l.PersonaID, Anio = l.Anio.Value, Mes = l.Mes.Value }).Distinct().ToList();

            var planilla = planillas.Where(w => w.Anio == DateTime.Now.Year && w.Mes == DateTime.Now.Month).FirstOrDefault();
            if (planilla == null)
            {
                PlanillaResumen p = new PlanillaResumen { PersonaId = (int)pr.PersonaId, Anio = DateTime.Now.Year, Mes = DateTime.Now.Month };
                planillas.Add(p);
            }
            ViewData["PHorarios"] = planillas;
            ViewBag.IdPersona = pr.PersonaId;
            return View(persona);
        }

        public virtual ActionResult Editar(PlanillaResumen planillaResumen)
        {
            ViewBag.IdPersona = planillaResumen.PersonaId;
            return RedirectToAction("Index", "Planillas", planillaResumen);
        }

        public virtual ActionResult ImprimirPlanilla(PlanillaResumen pr)
        {
            //return View("Error");
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "PlanillaMesEmpleado.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            List<Planilla> cm;
            var planillas = db.Planilla.Where(p => p.PersonaID == pr.PersonaId).Where(p => p.Anio == pr.Anio).Where(p => p.Mes == pr.Mes).OrderBy(p => p.Dia);
            cm = planillas.ToList();
            ReportDataSource rd = new ReportDataSource("DS1", cm);
            lr.DataSources.Add(rd);

            List<Persona> cm2;
            var personas = db.Persona.Where(p => p.PersonaID == pr.PersonaId);
            cm2 = personas.ToList();
            ReportDataSource rd2 = new ReportDataSource("DS2", cm2);
            lr.DataSources.Add(rd2);

            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
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


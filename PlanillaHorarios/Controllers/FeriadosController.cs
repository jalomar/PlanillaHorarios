using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PlanillaHorarios.DAL;
using PlanillaHorarios.Models;

namespace PlanillaHorarios.Controllers
{
    public partial class FeriadosController : Controller
    {
        private PlanillaContext db = new PlanillaContext();

        // GET: Feriados
        public virtual ActionResult Index()
        {
            var feriados = from s in db.Feriado
                           select s;
            feriados = feriados.OrderBy(s => s.Fecha);

            return View(feriados.ToList());
        }

        // GET: Feriados/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feriado feriado = db.Feriado.Find(id);
            if (feriado == null)
            {
                return HttpNotFound();
            }
            return View(feriado);
        }

        // GET: Feriados/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Feriados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "FeriadoID,Fecha,Descripcion")] Feriado feriado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Feriado.Add(feriado);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "No se guardaron los datos. Inténtelo de nuevo, y si el problema persiste consulte al administrador de sistema.");
            }
            return View(feriado);
        }

        // GET: Feriados/Edit/5
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feriado feriado = db.Feriado.Find(id);
            if (feriado == null)
            {
                return HttpNotFound();
            }
            return View(feriado);
        }

        // POST: Feriados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "FeriadoID,Fecha,Descripcion")] Feriado feriado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feriado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feriado);
        }

        // GET: Feriados/Delete/5
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
            Feriado feriado = db.Feriado.Find(id);
            if (feriado == null)
            {
                return HttpNotFound();
            }
            return View(feriado);
        }

        // POST: Feriados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Feriado feriado = db.Feriado.Find(id);
                db.Feriado.Remove(feriado);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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

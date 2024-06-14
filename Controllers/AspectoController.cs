using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoTFG_League.Models;
using System.IO;
using System.Linq;

namespace ProyectoTFG_League.Controllers
{
    public class AspectoController : Controller
    {
        public Contexto Contexto { get; }

        public AspectoController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: AspectoController
        public ActionResult Index()
        {
            var aspectos = Contexto.Aspectos.Include(a => a.CampeonNombre).ToList();
            return View(aspectos);
        }

        // GET: AspectoController/Details/5
        public ActionResult Details(int id)
        {
            var aspecto = Contexto.Aspectos.Include(a => a.CampeonNombre).FirstOrDefault(a => a.ID == id);
            if (aspecto == null)
            {
                return NotFound();
            }
            return View(aspecto);
        }

        // GET: AspectoController/Create
        public ActionResult Create()
        {
            ViewBag.Campeones = new SelectList(Contexto.Campeones, "ID", "Nombre");
            return View();
        }

        // POST: AspectoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AspectoModelo aspecto, IFormFile imagen)
        {
            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    aspecto.Imagen = memoryStream.ToArray();
                }
            }

            aspecto.CampeonNombre = Contexto.Campeones.Find(aspecto.CampeonNombre.ID);

            Contexto.Aspectos.Add(aspecto);
            Contexto.Database.EnsureCreated();
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Campeones = new SelectList(Contexto.Campeones, "ID", "Nombre");
                return View(aspecto);
            }
        }

        // GET: AspectoController/Edit/5
        public ActionResult Edit(int id)
        {
            var aspecto = Contexto.Aspectos.Include(a => a.CampeonNombre).FirstOrDefault(a => a.ID == id);
            if (aspecto == null)
            {
                return NotFound();
            }
            ViewBag.Campeones = new SelectList(Contexto.Campeones, "ID", "Nombre");
            return View(aspecto);
        }

        // POST: AspectoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AspectoModelo aspecto, IFormFile imagen)
        {
            if (id != aspecto.ID)
            {
                return BadRequest();
            }

            var existingAspecto = Contexto.Aspectos.Include(a => a.CampeonNombre).FirstOrDefault(a => a.ID == id);
            if (existingAspecto == null)
            {
                return NotFound();
            }

            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    existingAspecto.Imagen = memoryStream.ToArray();
                }
            }

            existingAspecto.Nombre = aspecto.Nombre;
            existingAspecto.CampeonNombre = Contexto.Campeones.Find(aspecto.CampeonNombre.ID);
            existingAspecto.PrecioRP = aspecto.PrecioRP;
            existingAspecto.Fecha = aspecto.Fecha;

            Contexto.Aspectos.Update(existingAspecto);
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Campeones = new SelectList(Contexto.Campeones, "ID", "Nombre");
                return View(existingAspecto);
            }
        }

        // GET: AspectoController/Delete/5
        public ActionResult Delete(int id)
        {
            var aspecto = Contexto.Aspectos.Include(a => a.CampeonNombre).FirstOrDefault(a => a.ID == id);
            if (aspecto == null)
            {
                return NotFound();
            }
            return View(aspecto);
        }

        // POST: AspectoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var aspecto = Contexto.Aspectos.Find(id);
            if (aspecto == null)
            {
                return NotFound();
            }

            Contexto.Aspectos.Remove(aspecto);
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(aspecto);
            }
        }
    }
}

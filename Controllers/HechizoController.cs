using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTFG_League.Models;


namespace ProyectoTFG_League.Controllers
{
    public class HechizoController : Controller
    {
        public Contexto Contexto { get; }

        public HechizoController(Contexto contexto)
        {
            Contexto = contexto;
        }


        // GET: HechizoController
        public ActionResult Index()
        {
            var hechizos = Contexto.Hechizos.ToList();
            return View(hechizos);
        }

        // GET: HechizoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HechizoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HechizoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HechizoModelo hechizo, IFormFile imagen)
        {
            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    hechizo.Imagen = memoryStream.ToArray();
                }
            }

            Contexto.Hechizos.Add(hechizo);
            Contexto.Database.EnsureCreated();
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: HechizoController/Edit/5
        public ActionResult Edit(int id)
        {
            var hechizo = Contexto.Hechizos.Find(id);
            if (hechizo == null)
            {
                return NotFound();
            }
            return View(hechizo);
        }

        // POST: HechizoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HechizoModelo hechizo, IFormFile imagen)
        {
            if (id != hechizo.ID)
            {
                return BadRequest();
            }

            var existingHechizo = Contexto.Hechizos.Find(id);
            if (existingHechizo == null)
            {
                return NotFound();
            }

            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    existingHechizo.Imagen = memoryStream.ToArray();
                }
            }

            existingHechizo.Nombre = hechizo.Nombre;
            existingHechizo.DescripcionH = hechizo.DescripcionH;
            existingHechizo.Enfriamiento = hechizo.Enfriamiento;
            existingHechizo.ModoH = hechizo.ModoH;
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<HechizoModelo> entityEntry = Contexto.Hechizos.Update(existingHechizo);
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(existingHechizo);
            }
        }

        // GET: HechizoController/Delete/5
        public ActionResult Delete(int id)
        {
            var hechizo = Contexto.Hechizos.Find(id);
            if (hechizo == null)
            {
                return NotFound();
            }
            return View(hechizo);
        }

        // POST: HechizoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var hechizo = Contexto.Hechizos.Find(id);
            if (hechizo == null)
            {
                return NotFound();
            }

            Contexto.Hechizos.Remove(hechizo);
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(hechizo);
            }
        }
    }
}

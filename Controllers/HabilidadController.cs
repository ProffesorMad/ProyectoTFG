using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoTFG_League.Models;

namespace ProyectoTFG_League.Controllers
{
    public class HabilidadController : Controller
    {
        public Contexto Contexto { get; }

        public HabilidadController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: HabilidadController
        public ActionResult Index()
        {
            var habilidades = Contexto.Habilidades.Include(h => h.CampeonNombre).ToList();
            return View(habilidades);
        }

        // GET: HabilidadController/Details/5
        public ActionResult Details(int id)
        {
            var habilidad = Contexto.Habilidades.Include(h => h.CampeonNombre).FirstOrDefault(h => h.ID == id);
            if (habilidad == null)
            {
                return NotFound();
            }
            return View(habilidad);
        }

        // GET: HabilidadController/Busqueda
        public ActionResult Busqueda(string tipo, string nombreHabilidad)
        {
            ViewBag.Tipos = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pasiva", Text = "Pasiva" },
                new SelectListItem { Value = "Q", Text = "Q" },
                new SelectListItem { Value = "W", Text = "W" },
                new SelectListItem { Value = "E", Text = "E" },
                new SelectListItem { Value = "R", Text = "R" }
            };

            var habilidadesQuery = Contexto.Habilidades.Include(h => h.CampeonNombre).AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
            {
                habilidadesQuery = habilidadesQuery.Where(h => h.Tipo == tipo);
            }

            if (!string.IsNullOrEmpty(nombreHabilidad))
            {
                habilidadesQuery = habilidadesQuery.Where(h => h.Nombre.Contains(nombreHabilidad));
            }

            var habilidades = habilidadesQuery.ToList();

            ViewBag.TipoSeleccionado = tipo;
            ViewBag.NombreHabilidad = nombreHabilidad;

            return View(habilidades);
        }

        // GET: HabilidadController/Create
        public ActionResult Create()
        {
            ViewBag.Campeones = new SelectList(Contexto.Campeones, "ID", "Nombre");
            ViewBag.Tipos = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Pasiva", Text = "Pasiva" },
                    new SelectListItem { Value = "Q", Text = "Q" },
                    new SelectListItem { Value = "W", Text = "W" },
                    new SelectListItem { Value = "E", Text = "E" },
                    new SelectListItem { Value = "R", Text = "R" }
                };
            return View();
        }

        // POST: HabilidadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HabilidadModelo habilidad, IFormFile imagen)
        {
            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    habilidad.Imagen = memoryStream.ToArray();
                }
            }

            habilidad.CampeonNombre = Contexto.Campeones.Find(habilidad.CampeonNombre.ID);
            Contexto.Habilidades.Add(habilidad);
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                ViewBag.Campeones = new SelectList(Contexto.Campeones, "ID", "Nombre");
                return View(habilidad);
            }
        }

        // GET: HabilidadController/Edit/5
        public ActionResult Edit(int id)
        {
            var habilidad = Contexto.Habilidades.Include(h => h.CampeonNombre).FirstOrDefault(h => h.ID == id);
            if (habilidad == null)
            {
                return NotFound();
            }
            ViewBag.Campeones = new SelectList(Contexto.Campeones, "ID", "Nombre");
            ViewBag.Tipos = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pasiva", Text = "Pasiva" },
                new SelectListItem { Value = "Q", Text = "Q" },
                new SelectListItem { Value = "W", Text = "W" },
                new SelectListItem { Value = "E", Text = "E" },
                new SelectListItem { Value = "R", Text = "R" }
            };
            return View(habilidad);
        }

        // POST: HabilidadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HabilidadModelo habilidad, IFormFile imagen)
        {
            if (id != habilidad.ID)
            {
                return BadRequest();
            }

            var existingHabilidad = Contexto.Habilidades.Include(h => h.CampeonNombre).FirstOrDefault(h => h.ID == id);
            if (existingHabilidad == null)
            {
                return NotFound();
            }

            existingHabilidad.Tipo = habilidad.Tipo;
            existingHabilidad.Nombre = habilidad.Nombre;
            existingHabilidad.DescripicionH = habilidad.DescripicionH;
            existingHabilidad.CampeonNombre = Contexto.Campeones.Find(habilidad.CampeonNombre.ID);

            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    existingHabilidad.Imagen = memoryStream.ToArray();
                }
            }

            Contexto.Habilidades.Update(existingHabilidad);
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Campeones = new SelectList(Contexto.Campeones, "ID", "Nombre");
                return View(existingHabilidad);
            }
        }

        // GET: HabilidadController/Delete/5
        public ActionResult Delete(int id)
        {
            var habilidad = Contexto.Habilidades.Include(h => h.CampeonNombre).FirstOrDefault(h => h.ID == id);
            if (habilidad == null)
            {
                return NotFound();
            }
            return View(habilidad);
        }

        // POST: HabilidadController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var habilidad = Contexto.Habilidades.Find(id);
            if (habilidad == null)
            {
                return NotFound();
            }

            Contexto.Habilidades.Remove(habilidad);
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(habilidad);
            }
        }
    }
}

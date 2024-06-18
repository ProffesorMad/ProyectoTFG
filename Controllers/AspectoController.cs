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
        public ActionResult Index(string sortOrder)
        {
            ViewBag.FechaSortOrder = sortOrder == "fecha_desc" ? "fecha_asc" : "fecha_desc";
            ViewBag.PrecioSortOrder = sortOrder == "precio_desc" ? "precio_asc" : "precio_desc";

            IQueryable<AspectoModelo> aspectosQuery = Contexto.Aspectos.Include(a => a.CampeonNombre).AsQueryable();

            switch (sortOrder)
            {
                case "fecha_asc":
                    aspectosQuery = aspectosQuery.OrderBy(a => a.Fecha);
                    break;
                case "fecha_desc":
                    aspectosQuery = aspectosQuery.OrderByDescending(a => a.Fecha);
                    break;
                case "precio_asc":
                    aspectosQuery = aspectosQuery.OrderBy(a => a.PrecioRP);
                    break;
                case "precio_desc":
                    aspectosQuery = aspectosQuery.OrderByDescending(a => a.PrecioRP);
                    break;
            }

            var aspectos = aspectosQuery.ToList();
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

        // GET: AspectoController/Busqueda
        public ActionResult Busqueda(string nombreAspecto, int? campeon, int? precio, string sortOrder)
        {
            ViewBag.FechaSortOrder = sortOrder == "fecha_desc" ? "fecha_asc" : "fecha_desc";
            ViewBag.PrecioSortOrder = sortOrder == "precio_desc" ? "precio_asc" : "precio_desc";

            var campeones = Contexto.Campeones.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Nombre
            }).ToList();

            campeones.Insert(0, new SelectListItem { Value = "0", Text = "Todos" });

            ViewBag.Campeones = campeones;

            var precios = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Cualquier Precio" },
                new SelectListItem { Value = "750", Text = "750 RP" },
                new SelectListItem { Value = "975", Text = "975 RP" },
                new SelectListItem { Value = "1350", Text = "1350 RP" },
                new SelectListItem { Value = "1820", Text = "1820 RP" },
                new SelectListItem { Value = "3250", Text = "3250 RP" }
            };

            ViewBag.Precios = precios;

            IQueryable<AspectoModelo> aspectosQuery = Contexto.Aspectos.Include(a => a.CampeonNombre).AsQueryable();

            if (!string.IsNullOrEmpty(nombreAspecto))
            {
                aspectosQuery = aspectosQuery.Where(a => a.Nombre.Contains(nombreAspecto));
            }

            if (campeon.HasValue && campeon.Value != 0)
            {
                aspectosQuery = aspectosQuery.Where(a => a.CampeonNombre.ID == campeon.Value);
            }

            if (precio.HasValue && precio.Value != 0)
            {
                aspectosQuery = aspectosQuery.Where(a => a.PrecioRP == precio.Value);
            }

            switch (sortOrder)
            {
                case "fecha_asc":
                    aspectosQuery = aspectosQuery.OrderBy(a => a.Fecha);
                    break;
                case "fecha_desc":
                    aspectosQuery = aspectosQuery.OrderByDescending(a => a.Fecha);
                    break;
                case "precio_asc":
                    aspectosQuery = aspectosQuery.OrderBy(a => a.PrecioRP);
                    break;
                case "precio_desc":
                    aspectosQuery = aspectosQuery.OrderByDescending(a => a.PrecioRP);
                    break;
            }

            var aspectos = aspectosQuery.ToList();

            ViewBag.NombreAspecto = nombreAspecto;
            ViewBag.CampeonSeleccionado = campeon;
            ViewBag.PrecioSeleccionado = precio;

            if (!aspectos.Any())
            {
                ViewBag.NoDataMessage = "No Existen Datos Con Esas Características";
            }

            return View(aspectos);
        }



        // GET: AspectoController/Create
        public ActionResult Create()
        {
            ViewBag.Campeones = new SelectList(Contexto.Campeones, "ID", "Nombre");
            ViewBag.Precios = new List<SelectListItem>
                {
                    new SelectListItem { Value = "750", Text = "750" },
                    new SelectListItem { Value = "975", Text = "975" },
                    new SelectListItem { Value = "1350", Text = "1.350" },
                    new SelectListItem { Value = "1820", Text = "1.820" },
                    new SelectListItem { Value = "3250", Text = "3.250" }
                };
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
                return RedirectToAction(nameof(Create));
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
            ViewBag.Precios = new List<SelectListItem>
                {
                    new SelectListItem { Value = "750", Text = "750" },
                    new SelectListItem { Value = "975", Text = "975" },
                    new SelectListItem { Value = "1350", Text = "1.350" },
                    new SelectListItem { Value = "1820", Text = "1.820" },
                    new SelectListItem { Value = "3250", Text = "3.250" }
                };
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

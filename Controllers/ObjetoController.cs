using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ProyectoTFG_League.Models;

namespace ProyectoTFG_League.Controllers
{
    public class ObjetoController : Controller
    {
        public Contexto Contexto { get; }

        public ObjetoController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: ObjetoController
        public ActionResult Index(string sortOrder, string currentFilter)
        {
            ViewBag.CurrentFilter = currentFilter;

            ViewBag.CosteAzulSortOrder = sortOrder == "costeAzul_asc" ? "costeAzul_desc" : "costeAzul_asc";

            var objetos = Contexto.Objetos.AsQueryable();

            switch (sortOrder)
            {
                case "costeAzul_desc":
                    objetos = objetos.OrderByDescending(o => o.Coste);
                    break;
                case "costeAzul_asc":
                    objetos = objetos.OrderBy(o => o.Coste);
                    break;
            }

            return View(objetos.ToList());
        }
        // GET: ObjetoController/Busqueda
        public ActionResult Busqueda(string nombre, string tipo, string modo, string sortOrder, string currentFilter)
        {
            var tipos = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Todos" },
                new SelectListItem { Value = "Iniciales", Text = "Iniciales" },
                new SelectListItem { Value = "Consumibles", Text = "Consumibles" },
                new SelectListItem { Value = "Wards", Text = "Wards" },
                new SelectListItem { Value = "Distribuidos", Text = "Distribuidos" },
                new SelectListItem { Value = "Botas", Text = "Botas" },
                new SelectListItem { Value = "Basicos", Text = "Basicos" },
                new SelectListItem { Value = "Epicos", Text = "Epicos" },
                new SelectListItem { Value = "Legendarios", Text = "Legendarios" },
                new SelectListItem { Value = "Exclusivos", Text = "Exclusivos" }
            };

            var modos = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Cualquier Modo" },
                new SelectListItem { Value = "Grieta del Invocador", Text = "Grieta del Invocador" },
                new SelectListItem { Value = "ARAM", Text = "ARAM" }
            };

            ViewBag.Tipos = tipos;
            ViewBag.Modos = modos;
            ViewBag.CurrentFilter = currentFilter;

            var objetosQuery = Contexto.Objetos.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                objetosQuery = objetosQuery.Where(o => o.Nombre.Contains(nombre));
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                objetosQuery = objetosQuery.Where(o => o.Tipo == tipo);
            }

            if (!string.IsNullOrEmpty(modo))
            {
                objetosQuery = objetosQuery.AsEnumerable().Where(o => o.Modo.Any(m => m.Contains(modo))).AsQueryable();
            }

            switch (sortOrder)
            {
                case "coste_asc":
                    objetosQuery = objetosQuery.OrderBy(o => o.Coste);
                    break;
                case "coste_desc":
                    objetosQuery = objetosQuery.OrderByDescending(o => o.Coste);
                    break;
            }

            var objetos = objetosQuery.ToList();

            ViewBag.Nombre = nombre;
            ViewBag.TipoSeleccionado = tipo;
            ViewBag.ModoSeleccionado = modo;
            ViewBag.SortOrder = sortOrder;

            if (!objetos.Any())
            {
                ViewBag.NoDataMessage = "No Existen Datos Con Esas Características";
            }

            return View(objetos);
        }

        // GET: ObjetoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ObjetoController/Create
        public ActionResult Create()
        {
            ViewBag.Tipos = new SelectList(new[]
            {
                "Iniciales", "Consumibles", "Wards", "Distribuidos", "Botas", "Basicos", "Epicos", "Legendarios", "Exclusivos"
            });

            ViewBag.Modos = new SelectList(new[]
            {
                "Grieta del Invocador", "ARAM"
            });

            return View();
        }

        // POST: ObjetoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ObjetoModelo objeto, IFormFile imagen)
        {
            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    objeto.Imagen = memoryStream.ToArray();
                }
            }

            Contexto.Objetos.Add(objeto);
            Contexto.Database.EnsureCreated();
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: ObjetoController/Edit/5
        public ActionResult Edit(int id)
        {
            var objeto = Contexto.Objetos.Find(id);
            if (objeto == null)
            {
                return NotFound();
            }

            ViewBag.Tipos = new SelectList(new[]
            {
                "Iniciales", "Consumibles", "Wards", "Distribuidos", "Botas", "Basicos", "Epicos", "Legendarios", "Exclusivos"
            }, objeto.Tipo);

            return View(objeto);
        }

        // POST: ObjetoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ObjetoModelo objeto, IFormFile imagen)
        {
            if (id != objeto.ID)
            {
                return BadRequest();
            }

            var existingObjeto = Contexto.Objetos.Find(id);
            if (existingObjeto == null)
            {
                return NotFound();
            }

            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    existingObjeto.Imagen = memoryStream.ToArray();
                }
            }

            existingObjeto.Nombre = objeto.Nombre;
            existingObjeto.Tipo = objeto.Tipo;
            existingObjeto.Modo = objeto.Modo;
            existingObjeto.Coste = objeto.Coste;
            existingObjeto.Estadisticas = objeto.Estadisticas;

            Contexto.Objetos.Update(existingObjeto);
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(existingObjeto);
            }
        }

        // GET: ObjetoController/Delete/5
        public ActionResult Delete(int id)
        {
            var objeto = Contexto.Objetos.Find(id);
            if (objeto == null)
            {
                return NotFound();
            }
            return View(objeto);
        }

        // POST: ObjetoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var objeto = Contexto.Objetos.Find(id);
            if (objeto == null)
            {
                return NotFound();
            }

            Contexto.Objetos.Remove(objeto);
            Contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

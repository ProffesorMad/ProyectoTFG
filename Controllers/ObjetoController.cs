using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ActionResult Index()
        {
            var objetos = Contexto.Objetos.ToList();
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

            ViewBag.Modos = new MultiSelectList(new[]
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
                return RedirectToAction(nameof(Create));
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

            ViewBag.Modos = new MultiSelectList(new[]
            {
                "Grieta del Invocador", "ARAM"
            }, objeto.Modo);

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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        // GET: ObjetoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ObjetoController/Create
        public ActionResult Create()
        {
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
            return View();
        }

        // POST: ObjetoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ObjetoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ObjetoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTFG_League.Models;

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
            return View();
        }

        // GET: AspectoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AspectoController/Create
        public ActionResult Create()
        {
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

            Contexto.Aspectos.Add(aspecto);
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

        // GET: AspectoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AspectoController/Edit/5
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

        // GET: AspectoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AspectoController/Delete/5
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

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
            return View();
        }

        // POST: HechizoController/Edit/5
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

        // GET: HechizoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HechizoController/Delete/5
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

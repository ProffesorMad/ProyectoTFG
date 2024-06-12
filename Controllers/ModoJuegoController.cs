using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTFG_League.Models;

namespace ProyectoTFG_League.Controllers
{
    public class ModoJuegoController : Controller
    {
        public Contexto Contexto { get; }

        public ModoJuegoController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: ModoJuegoController
        public ActionResult Index()
        {
            var modosJuego = Contexto.ModosJuegos.ToList();
            return View(modosJuego);
        }

        // GET: ModoJuegoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ModoJuegoController/Create
        public ActionResult Create()
        {
            ViewBag.Tipos = new List<string> { "Especial", "Permanente" };
            return View();
        }

        // POST: ModoJuegoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModoJuegoModelo modoJuego)
        {
            if (ModelState.IsValid)
            {
                Contexto.ModosJuegos.Add(modoJuego);
                Contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Tipos = new List<string> { "Especial", "Permanente" };
            return View(modoJuego);
        }

        // GET: ModoJuegoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModoJuegoController/Edit/5
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

        // GET: ModoJuegoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModoJuegoController/Delete/5
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

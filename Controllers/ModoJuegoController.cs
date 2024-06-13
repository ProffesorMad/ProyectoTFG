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
            var modoJuego = Contexto.ModosJuegos.Find(id);
            if (modoJuego == null)
            {
                return NotFound();
            }
            ViewBag.Tipos = new List<string> { "Especial", "Permanente" };
            return View(modoJuego);
        }

        // POST: ModoJuegoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ModoJuegoModelo modoJuego)
        {
            if (id != modoJuego.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Contexto.ModosJuegos.Update(modoJuego);
                Contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Tipos = new List<string> { "Especial", "Permanente" };
            return View(modoJuego);
        }

        // GET: ModoJuegoController/Delete/5
        public ActionResult Delete(int id)
        {
            var modoJuego = Contexto.ModosJuegos.Find(id);
            if (modoJuego == null)
            {
                return NotFound();
            }
            return View(modoJuego);
        }

        // POST: ModoJuegoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var modoJuego = Contexto.ModosJuegos.Find(id);
            if (modoJuego == null)
            {
                return NotFound();
            }

            Contexto.ModosJuegos.Remove(modoJuego);
            Contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

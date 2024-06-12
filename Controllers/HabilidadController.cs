using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        // GET: HabilidadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HabilidadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HabilidadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: HabilidadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HabilidadController/Edit/5
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

        // GET: HabilidadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HabilidadController/Delete/5
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

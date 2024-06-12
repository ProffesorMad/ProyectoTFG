using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoTFG_League.Models;

namespace ProyectoTFG_League.Controllers
{
    public class CampeonController : Controller
    {
        public Contexto Contexto { get; }

        public CampeonController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: CampeonController
        public ActionResult Index()
        {
            var campeones = Contexto.Campeones.Include(c => c.NombreRol).ToList();
            return View(campeones);
        }


        // GET: CampeonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CampeonController/Create
        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(Contexto.Roles, "ID", "Nombre");
            return View();
        }

        // POST: AspectoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CampeonModelo campeon, IFormFile imagen)
        {
            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    campeon.Imagen = memoryStream.ToArray();
                }
            }

            campeon.NombreRol = Contexto.Roles.Find(campeon.NombreRol.ID);

            Contexto.Campeones.Add(campeon);
            Contexto.Database.EnsureCreated();
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Roles = new SelectList(Contexto.Roles, "ID", "Nombre");
                return View(campeon);
            }
        }

        // GET: CampeonController/Edit/5
        public ActionResult Edit(int id)
        {
            var campeon = Contexto.Campeones.Include(c => c.NombreRol).FirstOrDefault(c => c.ID == id);
            if (campeon == null)
            {
                return NotFound();
            }
            ViewBag.Roles = new SelectList(Contexto.Roles, "ID", "Nombre", campeon.NombreRol.ID);
            return View(campeon);
        }

        // POST: CampeonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CampeonModelo campeon, IFormFile imagen)
        {
            if (id != campeon.ID)
            {
                return BadRequest();
            }

            var existingCampeon = Contexto.Campeones.Find(id);
            if (existingCampeon == null)
            {
                return NotFound();
            }

                if (imagen != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imagen.CopyTo(memoryStream);
                        existingCampeon.Imagen = memoryStream.ToArray();
                    }
                }

                campeon.NombreRol = Contexto.Roles.Find(campeon.NombreRol.ID);

                existingCampeon.Nombre = campeon.Nombre;
                existingCampeon.Posicion = campeon.Posicion;
                existingCampeon.Recurso = campeon.Recurso;
                existingCampeon.Gama = campeon.Gama;
                existingCampeon.Fecha = campeon.Fecha;
                existingCampeon.CosteRP = campeon.CosteRP;
                existingCampeon.CosteAzul = campeon.CosteAzul;
                existingCampeon.NombreRol = campeon.NombreRol;

                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<CampeonModelo> entityEntry = Contexto.Campeones.Update(existingCampeon);
                Contexto.SaveChanges();
            
            ViewBag.Roles = new SelectList(Contexto.Roles, "ID", "Nombre", campeon.NombreRol.ID);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(campeon);
            }
        }

        // GET: CampeonController/Delete/5
        public ActionResult Delete(int id)
        {
            var campeon = Contexto.Campeones.Include(c => c.NombreRol).FirstOrDefault(c => c.ID == id);
            if (campeon == null)
            {
                return NotFound();
            }
            return View(campeon);
        }

        // POST: CampeonController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var campeon = Contexto.Campeones.Find(id);
            if (campeon != null)
            {
                Contexto.Campeones.Remove(campeon);
                Contexto.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

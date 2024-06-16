using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTFG_League.Models;

namespace ProyectoTFG_League.Controllers
{
    public class RolController : Controller
    {
        public Contexto Contexto { get; }

        public RolController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: RolController
        public ActionResult Index()
        {
            var roles = Contexto.Roles.ToList();
            return View(roles);
        }

        // GET: RolController/Details/5
        public ActionResult Details(int id)
        {
            var rol = Contexto.Roles
                                .Include(r => r.Campeones)
                                .FirstOrDefault(r => r.ID == id);

            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // GET: RolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RolModelo rol, IFormFile imagen)
        {
            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    rol.Imagen = memoryStream.ToArray();
                }
            }

            Contexto.Roles.Add(rol);
            Contexto.Database.EnsureCreated();
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(rol);
            }
        }

        // GET: RolController/Edit/5
        public ActionResult Edit(int id)
        {
            var rol = Contexto.Roles.Find(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RolModelo rol, IFormFile imagen)
        {
            if (id != rol.ID)
            {
                return BadRequest();
            }

            var existingRol = Contexto.Roles.Find(id);
            if (existingRol == null)
            {
                return NotFound();
            }

            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imagen.CopyTo(memoryStream);
                    existingRol.Imagen = memoryStream.ToArray();
                }
            }

            existingRol.Nombre = rol.Nombre;
            existingRol.DescripcionR = rol.DescripcionR;

            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<RolModelo> entityEntry = Contexto.Roles.Update(existingRol);
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(existingRol);
            }
        }

        // GET: RolController/Delete/5
        public ActionResult Delete(int id)
        {
            var rol = Contexto.Roles.Find(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: RolController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rol = Contexto.Roles.Find(id);
            if (rol == null)
            {
                return NotFound();
            }

            Contexto.Roles.Remove(rol);
            Contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

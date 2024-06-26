﻿using Microsoft.AspNetCore.Http;
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
        public ActionResult Index(string sortOrder, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortOrder = string.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
            ViewBag.FechaSortOrder = sortOrder == "fecha" ? "fecha_desc" : "fecha";
            ViewBag.CosteRPSortOrder = sortOrder == "costeRP" ? "costeRP_desc" : "costeRP";
            ViewBag.CosteAzulSortOrder = sortOrder == "costeAzul" ? "costeAzul_desc" : "costeAzul";

            IQueryable<CampeonModelo> campeonesQuery = Contexto.Campeones.Include(c => c.NombreRol);

            if (!string.IsNullOrEmpty(currentFilter))
            {
                campeonesQuery = campeonesQuery.Where(c => c.Nombre.Contains(currentFilter));
            }

            switch (sortOrder)
            {
                case "nombre_desc":
                    campeonesQuery = campeonesQuery.OrderByDescending(c => c.Nombre);
                    break;
                case "fecha":
                    campeonesQuery = campeonesQuery.OrderBy(c => c.Fecha);
                    break;
                case "fecha_desc":
                    campeonesQuery = campeonesQuery.OrderByDescending(c => c.Fecha);
                    break;
                case "costeRP":
                    campeonesQuery = campeonesQuery.OrderBy(c => c.CosteRP);
                    break;
                case "costeRP_desc":
                    campeonesQuery = campeonesQuery.OrderByDescending(c => c.CosteRP);
                    break;
                case "costeAzul":
                    campeonesQuery = campeonesQuery.OrderBy(c => c.CosteAzul);
                    break;
                case "costeAzul_desc":
                    campeonesQuery = campeonesQuery.OrderByDescending(c => c.CosteAzul);
                    break;
                default:
                    campeonesQuery = campeonesQuery.OrderBy(c => c.Nombre);
                    ViewBag.NombreSortOrder = "nombre_desc";
                    break;
            }

            var campeones = campeonesQuery.ToList();
            ViewBag.CurrentFilter = currentFilter;

            return View(campeones);
        }

        // GET: CampeonController/Busqueda
        public ActionResult Busqueda(string sortOrder, string posicion, string nombre)
        {
            var campeonesQuery = Contexto.Campeones.Include(c => c.NombreRol).AsQueryable();

            if (!string.IsNullOrEmpty(posicion))
            {
                campeonesQuery = campeonesQuery.Where(c => c.Posicion.Contains(posicion));
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                campeonesQuery = campeonesQuery.Where(c => c.Nombre.Contains(nombre));
            }

            ViewBag.Posiciones = new List<SelectListItem>
    {
        new SelectListItem { Text = "Todas las posiciones", Value = "" },
        new SelectListItem { Text = "Top", Value = "Top" },
        new SelectListItem { Text = "Jungla", Value = "Jungla" },
        new SelectListItem { Text = "Medio", Value = "Medio" },
        new SelectListItem { Text = "ADC", Value = "ADC" },
        new SelectListItem { Text = "Support", Value = "Support" }
    };

            switch (sortOrder)
            {
                case "nombre_desc":
                    campeonesQuery = campeonesQuery.OrderByDescending(c => c.Nombre);
                    break;
                case "nombre_asc":
                    campeonesQuery = campeonesQuery.OrderBy(c => c.Nombre);
                    break;
                case "fecha_desc":
                    campeonesQuery = campeonesQuery.OrderByDescending(c => c.Fecha);
                    break;
                case "fecha_asc":
                    campeonesQuery = campeonesQuery.OrderBy(c => c.Fecha);
                    break;
                case "costeRP_desc":
                    campeonesQuery = campeonesQuery.OrderByDescending(c => c.CosteRP);
                    break;
                case "costeRP_asc":
                    campeonesQuery = campeonesQuery.OrderBy(c => c.CosteRP);
                    break;
                case "costeAzul_desc":
                    campeonesQuery = campeonesQuery.OrderByDescending(c => c.CosteAzul);
                    break;
                case "costeAzul_asc":
                    campeonesQuery = campeonesQuery.OrderBy(c => c.CosteAzul);
                    break;
                default:
                    campeonesQuery = campeonesQuery.OrderBy(c => c.Nombre);
                    sortOrder = "nombre_desc";
                    break;
            }

            var campeones = campeonesQuery.ToList();

            ViewBag.Posicion = posicion;
            ViewBag.Nombre = nombre;
            ViewBag.SortOrder = sortOrder;

            // Añadimos el mensaje de error si no hay datos
            if (!campeones.Any())
            {
                ViewBag.NoDataMessage = "No Existen Datos Con Esas Características";
            }

            return View(campeones);
        }

        // GET: CampeonController/Details/5
        public ActionResult Details(int id)
        {
            var campeon = Contexto.Campeones
                                     .Include(c => c.NombreRol)
                                     .Include(c => c.Habilidades)
                                     .FirstOrDefault(c => c.ID == id);

            if (campeon == null)
            {
                return NotFound();
            }

            return View(campeon);
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

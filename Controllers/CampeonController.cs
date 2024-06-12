﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        // GET: CampeonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CampeonController/Create
        public ActionResult Create()
        {
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

            Contexto.Campeones.Add(campeon);
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

        // GET: CampeonController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CampeonController/Edit/5
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

        // GET: CampeonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CampeonController/Delete/5
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

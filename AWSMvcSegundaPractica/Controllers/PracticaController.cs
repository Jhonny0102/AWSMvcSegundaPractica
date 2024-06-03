using AWSMvcSegundaPractica.Models;
using AWSMvcSegundaPractica.Services;
using Microsoft.AspNetCore.Mvc;

namespace AWSMvcSegundaPractica.Controllers
{
    public class PracticaController : Controller
    {
        private ServicePractica service;
        public PracticaController(ServicePractica service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Eventos()
        {
            List<Evento> eventos = await this.service.GetEventosAsync();
            return View(eventos);
        }

        public async Task<IActionResult> Categorias()
        {
            List<Categoria> categorias = await this.service.GetCategoriasAsync();
            return View(categorias);
        }

        public async Task<IActionResult> EventosCategorias()
        {
            List<Categoria> categorias = await this.service.GetCategoriasAsync();
            ViewData["CATEGORIAS"] = categorias;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EventosCategorias(int idcategoria)
        {
            List<Categoria> categorias = await this.service.GetCategoriasAsync();
            ViewData["CATEGORIAS"] = categorias;
            List<Evento> eventos = await this.service.GetEventosCategoriaAsync(idcategoria);
            return View(eventos);
        }

    }
}

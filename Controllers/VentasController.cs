using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : Controller
    {
        private VentasRepositories repository = new VentasRepositories();

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<Venta> lista = repository.listarVentas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

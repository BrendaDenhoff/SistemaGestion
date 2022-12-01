using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : Controller
    {
        private ProductosRepositories repository = new ProductosRepositories();

       [HttpGet]
       public IActionResult Get()
       {
            try
            {
                List<Producto> lista = repository.listarProducto();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

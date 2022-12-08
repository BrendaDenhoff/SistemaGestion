using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoVendidoController : Controller
    {
        private ProductosVendidosRepositories repository = new ProductosVendidosRepositories();

        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<ProductoVendido> lista = repository.listarProductoVendido();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ProductoVendido> Get(int id)
        {
            try
            {
                ProductoVendido? productoVendido = repository.obtenerProductoVendido(id);
                if (productoVendido != null)
                {
                    return Ok(productoVendido);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] int id)
        {
            try
            {
                bool seElimino = repository.eliminarProductoVendido(id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductoVendido productoVendido)
        {
            try
            {
                repository.crearProductoVendido(productoVendido);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ProductoVendido> Put(int id, [FromBody] ProductoVendido productoVendidoAActualizar)
        {
            try
            {
                ProductoVendido? productoVendidoActualizado = repository.actualizarProductoVendido(id, productoVendidoAActualizar);
                if (productoVendidoActualizado != null)
                {
                    return Ok(productoVendidoActualizado);
                }
                else
                {
                    return NotFound("El producto vendido no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

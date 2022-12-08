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
       public ActionResult<List<Producto>> Get()
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

        [HttpGet("{id}")]
        public ActionResult<Producto> Get(int id)
        {
            try
            {
                Producto? producto = repository.obtenerProducto(id);
                if (producto != null)
                {
                    return Ok(producto);
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
                bool seElimino = repository.eliminarProducto(id);
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
        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                repository.crearProducto(producto);
                return Ok();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Producto> Put(int id, [FromBody] Producto productoAActualizar)
        {
            try
            {
                Producto? productoActualizado = repository.actualizarProducto(id, productoAActualizar);
                if (productoActualizado != null)
                {
                    return Ok(productoActualizado);
                }
                else
                {
                    return NotFound("El producto no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

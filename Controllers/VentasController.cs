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
        public ActionResult<List<Venta>> Get()
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

        [HttpGet("{id}")]
        public ActionResult<Venta> Get(int id)
        {
            try
            {
                Venta? venta= repository.obtenerVenta(id);
                if (venta != null)
                {
                    return Ok(venta);
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
                bool seElimino = repository.eliminaVenta(id);
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
        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                repository.crearVenta(venta);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Venta> Put(int id, [FromBody] Venta ventaAActualizar)
        {
            try
            {
                Venta? ventaActualizado = repository.actualizarVenta(id, ventaAActualizar);
                if (ventaActualizado != null)
                {
                    return Ok(ventaActualizado);
                }
                else
                {
                    return NotFound("La venta no fue encontrada");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private UsuariosRepositories repository = new UsuariosRepositories();

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Usuario> lista = repository.listarUsuario();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

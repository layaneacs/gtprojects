using System.Threading.Tasks;
using gtp.api.Database;
using gtp.api.Models;
using gtp.api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gtp.api.Controllers
{

    [ApiController]
    [Route("v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarUsuario(
                    [FromServices] DataContext context,
                    [FromBody]Usuario model)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            try
            {
                context.Usuarios.Add(model);
                await context.SaveChangesAsync();
                
                return Ok(model);
            }
            catch (System.Exception)
            {
               return StatusCode(500, "Não foi possível incluir o usuário");
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(
                    [FromServices] DataContext context,
                    [FromBody]Usuario model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await context.Usuarios.FirstOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);

                if (user == null)
                    return StatusCode(404, "Usuário ou senha inválidos");

                var token = TokenService.GenerateToken(model);
                model.Password = "";
                return Ok(new
                {
                    user = model,
                    token = token
                });
            }
            catch
            {
                return StatusCode(500, "Falha na autenticação");
            }
        }
    }
}
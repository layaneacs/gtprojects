using System.Threading.Tasks;
using gtp.api.Database;
using gtp.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gtp.api.Controllers
{
    public class ProjetoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult> Get([FromServices] DataContext context)
        {
            try
            {
                var user = User.Identity.Name ?? "";
                var result = await context.Projetos.AsNoTracking().ToListAsync(); 
               
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode(500,"Não foi posssível listar as tarefas");            
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Create(
            [FromServices] DataContext context, 
            [FromBody] Projeto model
        )
        {
            if(!ModelState.IsValid) BadRequest(ModelState);

            try
            {
                model.Usuario = User.Identity.Name;
                context.Projetos.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (System.Exception)
            {
                return StatusCode(500,"Não foi posssível incluir a tarefa");            
            }
        }
    }
}
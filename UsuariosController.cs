using APIUsuarios.DAO;
using APIUsuarios.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            var dao = new UsuarioDAO();
            var usuarios = dao.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar(UsuarioDTO usuario)
        {
            var dao = new UsuarioDAO();
            dao.CadastrarUsuario(usuario);

            return Ok("Usuário cadastrado com sucesso!");
        }

        [HttpPut]
        [Route("atualizar/{id}")]
        public IActionResult Atualizar([FromRoute] int id, [FromBody] UsuarioDTO usuario)
        {
            var dao = new UsuarioDAO();
            var linhasAfetadas = dao.AtualizarUsuario(id, usuario);

            if (linhasAfetadas == 0)
                return NotFound("Usuário não encontrado.");

            return Ok("Usuário atualizado com sucesso!");
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            var dao = new UsuarioDAO();
            var linhasAfetadas = dao.DeletarUsuario(id);

            if (linhasAfetadas == 0)
                return NotFound("Usuário não encontrado.");

            return Ok("Usuário excluído com sucesso!");
        }
    }
}


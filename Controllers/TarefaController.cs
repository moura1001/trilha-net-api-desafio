using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Services;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Context;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _service;

        public TarefaController(OrganizadorContext _context)
        {
            _service = new TarefaService(_context);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _service.ObterPorId(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _service.ObterTodos();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefas = _service.ObterPorTitulo(titulo);
            return Ok(tarefas);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefas = _service.ObterPorData(data);
            return Ok(tarefas);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefas = _service.ObterPorStatus(status);
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            try
            {
                var tarefaBanco = _service.Criar(tarefa);
                return CreatedAtAction(nameof(ObterPorId), new { id = tarefaBanco.Id }, tarefaBanco);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            try
            {
                var tarefaBanco = _service.Atualizar(id, tarefa);

                if (tarefaBanco == null)
                    return NotFound();

                return Ok(tarefaBanco);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            bool sucesso = _service.Deletar(id);

            if (!sucesso)
                return NotFound();
            
            return NoContent();
        }
    }
}

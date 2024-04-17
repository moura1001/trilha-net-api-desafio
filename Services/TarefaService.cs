using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services
{
    public class TarefaService
    {
        private readonly OrganizadorContext _context;

        public TarefaService(OrganizadorContext context)
        {
            _context = context;
        }

        public Tarefa ObterPorId(int id)
        {
            return _context.Tarefas.Find(id);
        }
    }
}
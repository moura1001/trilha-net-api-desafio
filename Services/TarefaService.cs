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

        public List<Tarefa> ObterTodos()
        {
            return _context.Tarefas.ToList();
        }

        public List<Tarefa> ObterPorTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                return null;

            titulo = titulo.ToLower();
            return _context.Tarefas.Where(tarefa => tarefa.Titulo.ToLower().Contains(titulo)).ToList();
        }

        public List<Tarefa> ObterPorData(DateTime data)
        {
            return _context.Tarefas.Where(tarefa => DateTime.Equals(tarefa.Data, data)).ToList();
        }

        public List<Tarefa> ObterPorStatus(EnumStatusTarefa status)
        {
            return _context.Tarefas.Where(tarefa => tarefa.Status == status).ToList();
        }

        public Tarefa Criar(Tarefa tarefa)
        {
            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
                throw new Exception("A tarefa precisa de um título");

            if (tarefa.Data == DateTime.MinValue || tarefa.Data < DateTime.Now)
                throw new Exception($"O prazo de conclusão da tarefa precisa ser depois do horário atual: {DateTime.Now:yyyy-MM-dd HH:mm}");
            
            if (tarefa.Status != EnumStatusTarefa.Pendente)
                tarefa.Status = EnumStatusTarefa.Pendente;

            _context.Add(tarefa);
            _context.SaveChanges();
            return tarefa;
        }
    }
}
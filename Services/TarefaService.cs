using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.Services
{
    public class TarefaService
    {
        private List<Tarefa> tarefas = new();
        private int proximoId = 1;

        public void AdicionarTarefa(Tarefa tarefa)
        {
            tarefa.Id = proximoId++;
            tarefa.Concluida = false;
            tarefas.Add(tarefa);
            Console.WriteLine("Tarefa adicionada com sucesso!");
        }

        public List<Tarefa> ListarTarefas()
        {
            return tarefas;
        }

        public Tarefa? BuscarPorId(int id)
        {
            return tarefas.FirstOrDefault(t => t.Id == id);
        }

        public void MarcarComoConcluida(int id)
        {
            var tarefa = BuscarPorId(id);
            if (tarefa != null)
            {
                tarefa.Concluida = true;
                Console.WriteLine("Tarefa marcada como concluída!");
            }
            else
            {
                Console.WriteLine("Tarefa não encontrada.");
            }
        }

        public void RemoverTarefa(int id)
        {
            var tarefa = BuscarPorId(id);
            if (tarefa != null)
            {
                tarefas.Remove(tarefa);
                Console.WriteLine("Tarefa removida com sucesso!");
            }
            else
            {
                Console.WriteLine("Tarefa não encontrada.");
            }
        }
    }
}

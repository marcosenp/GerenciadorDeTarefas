using System;
using System.Collections.Generic;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            char opcao;

            do
            {
                Console.WriteLine("---- Gerenciador de Tarefas ----");
                Console.WriteLine("Escolha uma opção abaixo:");
                Console.WriteLine("1. Adicionar tarefa");
                Console.WriteLine("2. Editar tarefa");
                Console.WriteLine("3. Excluir tarefa");
                Console.WriteLine("4. Listar tarefas");
                Console.WriteLine("5. Sair");
                opcao = char.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case '1':
                        Console.Write("Informe o título para a tarefa: ");
                        string titulo = Console.ReadLine();
                        Console.Write("Informe a descrição para a tarefa: ");
                        string descricao = Console.ReadLine();
                        tarefas.Add(new Tarefa { Titulo = titulo, Descricao = descricao });
                        break;

                    case '2':
                        Console.Write("Escolha o número da tarefa a ser editada: ");
                        int numeroEditar = int.Parse(Console.ReadLine()) - 1;
                        if (numeroEditar >= 0 && numeroEditar < tarefas.Count)
                        {
                            Console.Write("Novo título: ");
                            tarefas[numeroEditar].Titulo = Console.ReadLine();
                            Console.Write("Nova descrição: ");
                            tarefas[numeroEditar].Descricao = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Tarefa não encontrada.");
                        }
                        break;

                    case '3':
                        Console.Write("Escolha o número da tarefa a ser excluída: ");
                        int numeroExcluir = int.Parse(Console.ReadLine()) - 1;
                        if (numeroExcluir >= 0 && numeroExcluir < tarefas.Count)
                        {
                            tarefas.RemoveAt(numeroExcluir);
                            Console.WriteLine("Tarefa excluída com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Tarefa não encontrada.");
                        }
                        break;

                    case '4':
                        string todasTarefas = "";
                        for (int i = 0; i < tarefas.Count; i++)
                        {
                            todasTarefas += $"tarefa {i + 1}: {tarefas[i].Titulo} - {tarefas[i].Descricao}{Environment.NewLine}";
                        }
                        Console.WriteLine("Tarefas:");
                        Console.WriteLine(todasTarefas);
                        break;

                    case '5':
                        Console.WriteLine("Saindo...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

            } while (opcao != '5');
        }
    }

    public class Tarefa
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}




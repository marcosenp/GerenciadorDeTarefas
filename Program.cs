using GerenciadorTarefas.Models;
using GerenciadorTarefas.Services;

TarefaService tarefaService = new();

bool executando = true;

while (executando)
{
    Console.Clear();
    Console.WriteLine("=== GERENCIADOR DE TAREFAS ===");
    Console.WriteLine("1 - Adicionar tarefa");
    Console.WriteLine("2 - Listar tarefas");
    Console.WriteLine("3 - Marcar tarefa como concluída");
    Console.WriteLine("4 - Remover tarefa");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
    string opcao = Console.ReadLine() ?? "";

    Console.Clear();

    switch (opcao)
    {
        case "1":
            AdicionarTarefa();
            break;
        case "2":
            ListarTarefas();
            break;
        case "3":
            MarcarComoConcluida();
            break;
        case "4":
            RemoverTarefa();
            break;
        case "0":
            executando = false;
            Console.WriteLine("Encerrando o programa...");
            break;
        default:
            Console.WriteLine("Opção inválida. Pressione ENTER para continuar...");
            Console.ReadLine();
            break;
    }
}

void AdicionarTarefa()
{
    Console.Write("Título da tarefa: ");
    string titulo = Console.ReadLine() ?? "";

    Console.Write("Descrição: ");
    string descricao = Console.ReadLine() ?? "";

    Console.Write("Data de vencimento (dd/mm/aaaa): ");
    string dataStr = Console.ReadLine() ?? "";

    if (DateTime.TryParse(dataStr, out DateTime dataVencimento))
    {
        var novaTarefa = new Tarefa
        {
            Titulo = titulo,
            Descricao = descricao,
            DataVencimento = dataVencimento
        };

        tarefaService.AdicionarTarefa(novaTarefa);
    }
    else
    {
        Console.WriteLine("Data inválida. Pressione ENTER para continuar...");
    }

    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}

void ListarTarefas()
{
    var tarefas = tarefaService.ListarTarefas();

    if (tarefas.Count == 0)
    {
        Console.WriteLine("Nenhuma tarefa cadastrada.");
    }
    else
    {
        foreach (var t in tarefas)
        {
            Console.WriteLine($"ID: {t.Id}");
            Console.WriteLine($"Título: {t.Titulo}");
            Console.WriteLine($"Descrição: {t.Descricao}");
            Console.WriteLine($"Data de Vencimento: {t.DataVencimento:dd/MM/yyyy}");
            Console.WriteLine($"Status: {(t.Concluida ? "Concluída" : "Pendente")}");
            Console.WriteLine("-----------------------------------");
        }
    }

    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}

void MarcarComoConcluida()
{
    Console.Write("Digite o ID da tarefa: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        tarefaService.MarcarComoConcluida(id);
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }

    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}

void RemoverTarefa()
{
    Console.Write("Digite o ID da tarefa: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        tarefaService.RemoverTarefa(id);
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }

    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}

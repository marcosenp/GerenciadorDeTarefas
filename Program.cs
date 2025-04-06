using GerenciadorTarefas.Models;
using GerenciadorTarefas.Services;
using GerenciadorTarefas.Data;

DatabaseSetup.Inicializar();

TarefaService tarefaService = new();

bool executando = true;

while (executando)
{
    Console.Clear();
    Console.WriteLine("=== GERENCIADOR DE TAREFAS ===");
    Console.WriteLine("1 - Adicionar tarefa");
    Console.WriteLine("2 - Listar todas as tarefas");
    Console.WriteLine("3 - Marcar tarefa como concluída");
    Console.WriteLine("4 - Remover tarefa");
    Console.WriteLine("5 - Listar tarefas pendentes");
    Console.WriteLine("6 - Listar tarefas concluídas");
    Console.WriteLine("7 - Listar tarefas próximas do vencimento (até 3 dias)");
    Console.WriteLine("8 - Listar tarefas atrasadas");
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
            ListarTarefas(tarefaService.ListarTarefas(), "TODAS");
            break;
        case "3":
            MarcarComoConcluida();
            break;
        case "4":
            RemoverTarefa();
            break;
        case "5":
            ListarTarefas(tarefaService.ListarPendentes(), "PENDENTES");
            break;
        case "6":
            ListarTarefas(tarefaService.ListarConcluidas(), "CONCLUÍDAS");
            break;
        case "7":
            ListarTarefas(tarefaService.ListarProximasDoVencimento(3), "A VENCER (3 dias)");
            break;
        case "8":
            ListarTarefas(tarefaService.ListarAtrasadas(), "ATRASADAS");
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
            DataVencimento = dataVencimento,
            Concluida = false
        };

        tarefaService.AdicionarTarefa(novaTarefa);
        Console.WriteLine("Tarefa adicionada com sucesso!");
    }
    else
    {
        Console.WriteLine("Data inválida.");
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
        Console.WriteLine("Tarefa marcada como concluída.");
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
        Console.WriteLine("Tarefa removida.");
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }

    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}

void ListarTarefas(List<Tarefa> tarefas, string tipo)
{
    Console.WriteLine($"=== TAREFAS {tipo} ===");

    if (tarefas.Count == 0)
    {
        Console.WriteLine("Nenhuma tarefa encontrada.");
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

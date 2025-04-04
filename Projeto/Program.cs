using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Gerenciador de Tarefas");

        List<Tarefa> tarefas = new List<Tarefa>();

        tarefas.Add(new Tarefa { Titulo = "Fazer compras.", Descricao = "maçã, banana, morango e abacaxi" });

        foreach (var tarefa in tarefas)
        {
            Console.WriteLine($"Tarefa: {tarefa.Titulo} - {tarefa.Descricao}");
        }
    }
}

public class Tarefa
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorTarefas.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace GerenciadorTarefas.Data
{
    public class TarefaRepository
    {
        public void Adicionar(Tarefa tarefa)
        {
            using var connection = DatabaseConfig.GetConnection();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Tarefas (Titulo, Concluida)
                VALUES ($titulo, $concluida);
            ";

            command.Parameters.AddWithValue("$titulo", tarefa.Titulo);
            command.Parameters.AddWithValue("$concluida", tarefa.Concluida ? 1 : 0);

            command.ExecuteNonQuery();
        }

        public List<Tarefa> Listar()
        {
            var tarefas = new List<Tarefa>();

            using var connection = DatabaseConfig.GetConnection();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Titulo, Concluida FROM Tarefas;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var tarefa = new Tarefa
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Concluida = reader.GetInt32(2) == 1
                };

                tarefas.Add(tarefa);
            }

            return tarefas;
        }

        public void Atualizar(Tarefa tarefa)
        {
            using var connection = DatabaseConfig.GetConnection();
            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Tarefas 
                SET Titulo = $titulo, Concluida = $concluida
                WHERE Id = $id;
            ";

            command.Parameters.AddWithValue("$id", tarefa.Id);
            command.Parameters.AddWithValue("$titulo", tarefa.Titulo);
            command.Parameters.AddWithValue("$concluida", tarefa.Concluida ? 1 : 0);

            command.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var connection = DatabaseConfig.GetConnection();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Tarefas WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", id);

            command.ExecuteNonQuery();
        }
    }
}

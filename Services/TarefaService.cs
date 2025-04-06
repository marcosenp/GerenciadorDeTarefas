using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GerenciadorTarefas.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace GerenciadorTarefas.Services
{
    public class TarefaService
    {
        private const string connectionString = "Data Source=tarefas.db";

        public void AdicionarTarefa(Tarefa tarefa)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"INSERT INTO Tarefas (Titulo, Descricao, DataVencimento, Concluida)
              VALUES ($titulo, $descricao, $dataVencimento, $concluida)";
            command.Parameters.AddWithValue("$titulo", tarefa.Titulo);
            command.Parameters.AddWithValue("$descricao", tarefa.Descricao);
            command.Parameters.AddWithValue("$dataVencimento", tarefa.DataVencimento);
            command.Parameters.AddWithValue("$concluida", tarefa.Concluida);

            command.ExecuteNonQuery();
        }

        public List<Tarefa> ListarTarefas()
        {
            var tarefas = new List<Tarefa>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Titulo, Descricao, DataVencimento, Concluida FROM Tarefas";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tarefas.Add(new Tarefa
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    DataVencimento = reader.GetDateTime(3),
                    Concluida = reader.GetBoolean(4)
                });
            }

            return tarefas;
        }

        public void MarcarComoConcluida(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Tarefas SET Concluida = 1 WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            command.ExecuteNonQuery();
        }

        public void RemoverTarefa(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Tarefas WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            command.ExecuteNonQuery();
        }

        public List<Tarefa> ListarPendentes()
        {
            var tarefas = new List<Tarefa>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Titulo, Descricao, DataVencimento, Concluida FROM Tarefas WHERE Concluida = 0";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tarefas.Add(new Tarefa
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    DataVencimento = reader.GetDateTime(3),
                    Concluida = reader.GetBoolean(4)
                });
            }

            return tarefas;
        }

        public List<Tarefa> ListarConcluidas()
        {
            var tarefas = new List<Tarefa>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Titulo, Descricao, DataVencimento, Concluida FROM Tarefas WHERE Concluida = 1";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tarefas.Add(new Tarefa
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    DataVencimento = reader.GetDateTime(3),
                    Concluida = reader.GetBoolean(4)
                });
            }

            return tarefas;
        }

        public List<Tarefa> ListarProximasDoVencimento(int dias)
        {
            var tarefas = new List<Tarefa>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT Id, Titulo, Descricao, DataVencimento, Concluida 
        FROM Tarefas 
        WHERE DataVencimento <= $dataLimite AND Concluida = 0";
            command.Parameters.AddWithValue("$dataLimite", DateTime.Now.AddDays(dias));

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tarefas.Add(new Tarefa
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    DataVencimento = reader.GetDateTime(3),
                    Concluida = reader.GetBoolean(4)
                });
            }

            return tarefas;
        }

        public List<Tarefa> ListarAtrasadas()
        {
            var tarefas = new List<Tarefa>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
        SELECT Id, Titulo, Descricao, DataVencimento, Concluida 
        FROM Tarefas 
        WHERE DataVencimento < $hoje AND Concluida = 0";
            command.Parameters.AddWithValue("$hoje", DateTime.Now);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tarefas.Add(new Tarefa
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    DataVencimento = reader.GetDateTime(3),
                    Concluida = reader.GetBoolean(4)
                });
            }

            return tarefas;
        }

    }
}


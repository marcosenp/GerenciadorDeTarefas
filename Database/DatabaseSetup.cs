using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.Sqlite;

namespace GerenciadorTarefas.Data
{
    public static class DatabaseSetup
    {
        public static void Inicializar()
        {
            using var connection = DatabaseConfig.GetConnection();

            var command = connection.CreateCommand();
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Tarefas (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Titulo TEXT NOT NULL,
            Descricao TEXT,
            DataVencimento TEXT,
            Concluida INTEGER NOT NULL
            );
        ";


            command.ExecuteNonQuery();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.Sqlite;

namespace GerenciadorTarefas.Data
{
    public static class DatabaseConfig
    {
        public static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection("Data Source=tarefas.db");
            connection.Open();
            return connection;
        }
    }
}

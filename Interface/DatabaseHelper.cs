using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Data;

namespace Interface
{
    internal class DatabaseHelper
    {
        private string connectionString;
        private NpgsqlConnection conn;

        public DatabaseHelper()
        {
            // Initialize ConfigurationBuilder to read the connection string from appsettings.json
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Access the connection string in the "AppSettings" section with key "aivenDB"
            this.connectionString = config.GetSection("AppSettings")["aivenDB"];
            conn = new NpgsqlConnection(connectionString);
        }

        public void TestConnection()
        {

            try
            {
                conn.Open();
                Console.WriteLine("Connection successful");

                conn.Close();
                Console.WriteLine("Connection closed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
         
        }

        public NpgsqlDataReader executeQuery(string query)
        {
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                return command.ExecuteReader(CommandBehavior.CloseConnection); // Close connection when reader is closed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}

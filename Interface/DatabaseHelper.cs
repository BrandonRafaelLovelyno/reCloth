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
        private NpgsqlConnection Conn;
        private NpgsqlDataReader Reader;

        public DatabaseHelper()
        {
            // Initialize ConfigurationBuilder to read the connection string from appsettings.json
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Access the connection string in the "AppSettings" section with key "aivenDB"
            this.connectionString = config.GetSection("AppSettings")["aivenDB"];
            Conn = new NpgsqlConnection(connectionString);
        }

        public void TestConnection()
        {

            try
            {
                Conn.Open();
                Console.WriteLine("Connection successful");

                Conn.Close();
                Console.WriteLine("Connection closed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
         
        }

        public T? extractValue<T>(string key)
        {
            try
            {
                if (Reader == null || !Reader.Read())
                {
                    throw new Exception($"Cannot read {key} property");
                }

                object value = Reader[key];

                // Attempt to cast the value to the specified type
                if (value is T typedValue)
                {
                    return typedValue;
                }
                else
                {
                    throw new InvalidCastException($"The value for key '{key}' cannot be cast to type {typeof(T)}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return default; // Returns null if T is a reference type or default(T) for value types
            }
        }

        public NpgsqlDataReader executeQuery(string query)
        {
            try
            {
                Conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, Conn);
                NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                Reader = reader;
                return command.ExecuteReader(CommandBehavior.CloseConnection); // Close connection when reader is closed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return ex;
            }
        }
    }
}

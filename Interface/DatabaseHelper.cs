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
        private NpgsqlDataReader? Reader;

        public DatabaseHelper()
        {
            // Initialize ConfigurationBuilder to read the connection string from appsettings.json
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Access the connection string in the "AppSettings" section with key "aivenDB"
            string? temp = config.GetSection("AppSettings")["aivenDB"];
            if(temp == null)
            {
                throw new Exception("Database URI not found");
            }

            this.connectionString = temp;
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

        public T convertObject<T>(object obj)
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot cast {obj.GetType().Name} to {typeof(T).Name}");
            }
        }

        public void executePostQuery(string query, params NpgsqlParameter[] parameters)
        {
            Conn.Open();
            using (var command = new NpgsqlCommand(query, Conn))
            {
                // add parameters to the command to prevent SQL injection 
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.ExecuteNonQuery(); 
            }
            Conn.Close();
        }

        public void executePostQueryImage(string query, byte[] imageBytes, params NpgsqlParameter[] parameters)
        {
            Conn.Open() ;
            using (var command = new NpgsqlCommand(query, Conn))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.Parameters.Add(new NpgsqlParameter("image", imageBytes));

                command.ExecuteNonQuery();
            }
            Conn.Close();
        }

     

        public List<Dictionary<string, object>> executeGetQuery(string query, params string[] keys)
        {
            var results = new List<Dictionary<string, object>>();

            Console.WriteLine(query);

            Conn.Open();
            using (var command = new NpgsqlCommand(query, Conn))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    foreach (var key in keys)
                    {
                        row[key] = reader[key];
                    }
                    results.Add(row); 
                }
            }
            Conn.Close();
            return results;
        }
    }
}

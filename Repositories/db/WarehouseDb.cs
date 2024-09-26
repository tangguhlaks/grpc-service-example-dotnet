using Dapper;
using GrpcServiceExample.Model;
using MySql.Data.MySqlClient;

namespace GrpcServiceExample.Repositories.db
{
    public interface IWarehouseDb
    {
        Warehouse? GetWarehouseById(int id);
        List<Warehouse>? GetWarehouse();
        bool CreateWarehouse(Warehouse warehouse);
    }
    public class WarehouseDb : IWarehouseDb
    {
        private string _connection;
        private string _table;
        public WarehouseDb(IConfiguration configuration) { 
            _connection = Environment.GetEnvironmentVariable("connection_grpc_example");
            _table = "mst_warehouse";
        }
        public bool CreateWarehouse(Warehouse warehouse)
        {
            try
            {
                string query = $"insert into {_table} (Id,Code,Name) values (@Id,@Code,@Name)";
                using (var connection = new MySqlConnection(_connection))
                {
                    connection.Open();
                    connection.ExecuteAsync(query,warehouse);
                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public List<Warehouse>? GetWarehouse()
        {
            try
            {
                List<Warehouse>? list = null;
                string query = $"select * from {_table}";
                using (var connection = new MySqlConnection(_connection))
                {
                    connection.Open();
                    list = connection.Query<Warehouse>(query).ToList();
                    connection.Close();
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public Warehouse? GetWarehouseById(int id)
        {
            try
            {
                Warehouse? data = null;
                string query = $"select * from {_table} where id= @Id";
                using (var connection = new MySqlConnection(_connection))
                {
                    connection.Open();
                    data = connection.Query<Warehouse>(query,new { Id= id}).FirstOrDefault();
                    connection.Close();
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

using System.Data;
using Microsoft.Data.SqlClient;
using OrderManagementSystem.DTOs;

namespace OrderManagementSystem.Repository.CustomerRepo
{
    public class CutomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CutomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<CustomerDTO> CreateCustomerAsync(CustomerDTO customerDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Customer_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", customerDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", customerDTO.LastName);
                    command.Parameters.AddWithValue("@Email", customerDTO.Email);
                    command.Parameters.AddWithValue("@Phone", customerDTO.Phone);
                    command.Parameters.AddWithValue("@RegistrationDate", customerDTO.RegistrationDate);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                return customerDTO;
            }
        }
        public async Task<string> DeleteCustomerAsync(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Customer_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", Id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                return "Customer Deleted Successfully";
            }
        }
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var list = new List<CustomerDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Customer_GetAll", connection))
                {
                    await connection.OpenAsync();
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var customer = new CustomerDTO()
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate"))

                            };
                            list.Add(customer);
                        }
                        return list;

                    }
                }
            }
        }
        public async Task<CustomerDTO> GetCustomerByIdAsync(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Customer_GetById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", Id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new CustomerDTO()
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate"))
                            };
                        }
                    }
                    return null;
                }
            }
        }
        public async Task<int> GetCustomerCountAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Customer_Count", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }
        public async Task<CustomerDTO> UpdateCustomerAsync(int Id, CustomerDTO customerDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Customer_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", Id);
                    command.Parameters.AddWithValue("@FirstName", customerDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", customerDTO.LastName);
                    command.Parameters.AddWithValue("@Email", customerDTO.Email);
                    command.Parameters.AddWithValue("@Phone", customerDTO.Phone);
                    command.Parameters.AddWithValue("@RegistrationDate", customerDTO.RegistrationDate);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                return customerDTO;
            }
        }

    }
}

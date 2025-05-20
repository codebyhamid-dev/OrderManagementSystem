using Microsoft.Data.SqlClient;
using System.Data;
using OrderManagementSystem.DTOs;

namespace OrderManagementSystem.Repository.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<ProductDTO> CreateProductAsync(ProductDTO productDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Product_Create", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductName", productDTO.ProductName);
                    command.Parameters.AddWithValue("@Description", productDTO.Description);
                    command.Parameters.AddWithValue("@Price", productDTO.Price);
                    command.Parameters.AddWithValue("@StockQuantity", productDTO.StockQuantity);
                    command.Parameters.AddWithValue("@Category", productDTO.Category);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                return productDTO;
            }
        }

        public async Task<string> DeleteProductAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Product_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductID", id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                return "Product Deleted Successfully";
            }
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var list = new List<ProductDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Product_GetAll", connection))
                {
                    await connection.OpenAsync();
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var product = new ProductDTO
                            {
                                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity")),
                                Category = reader.GetString(reader.GetOrdinal("Category"))
                            };
                            list.Add(product);
                        }
                        return list;
                    }
                }
            }
        }

        public async Task<ProductDTO> GetProductByIdAsync(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Product_GetById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductID", Id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new ProductDTO()
                            {
                                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity")),
                                Category = reader.GetString(reader.GetOrdinal("Category"))
                            };
                        }
                    }
                    return null;
                }
            }
        }

        public async Task<int> GetProductCountAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Product_Count", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task<List<ProductDTO>> SearchProductsAsync(string searchTerm)
        {
            var list = new List<ProductDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Product_Search", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var product = new ProductDTO
                            {
                                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity")),
                                Category = reader.GetString(reader.GetOrdinal("Category"))
                            };
                            list.Add(product);
                        }
                    }
                }
            }
            return list;
        }

        public async Task<ProductDTO> UpdateProductAsync(int Id, ProductDTO productDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_Product_Update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductID", Id);
                    command.Parameters.AddWithValue("@ProductName", productDTO.ProductName);
                    command.Parameters.AddWithValue("@Description", productDTO.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Price", productDTO.Price);
                    command.Parameters.AddWithValue("@StockQuantity", productDTO.StockQuantity);
                    command.Parameters.AddWithValue("@Category", productDTO.Category ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                return productDTO;
            }
        }
    }
}

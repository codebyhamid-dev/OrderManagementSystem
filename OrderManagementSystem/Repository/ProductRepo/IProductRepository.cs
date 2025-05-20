using OrderManagementSystem.DTOs;

namespace OrderManagementSystem.Repository.ProductRepo
{
    public interface IProductRepository
    {
        Task<ProductDTO> CreateProductAsync(ProductDTO productDTO);
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int Id);
        Task<ProductDTO> UpdateProductAsync(int Id, ProductDTO productDTO);
        Task<string> DeleteProductAsync(int id);
        Task<List<ProductDTO>> SearchProductsAsync(string searchTerm);
        Task<int> GetProductCountAsync();



    }
}

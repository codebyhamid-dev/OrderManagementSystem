using OrderManagementSystem.DTOs;

namespace OrderManagementSystem.Repository.CustomerRepo
{
    public interface ICustomerRepository
    {
        Task<CustomerDTO> CreateCustomerAsync(CustomerDTO customerDTO);
        Task<List<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int Id);
        Task<CustomerDTO> UpdateCustomerAsync(int Id, CustomerDTO customerDTO);
        Task<string> DeleteCustomerAsync(int Id);
        Task<int> GetCustomerCountAsync();
    }
}

using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.DTOs
{
    public class CustomerDTO
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}

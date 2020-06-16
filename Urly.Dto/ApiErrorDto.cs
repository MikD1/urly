using System.ComponentModel.DataAnnotations;

namespace Urly.Dto
{
    public class ApiErrorDto
    {
        [Required]
        public string? Message { get; set; }
    }
}

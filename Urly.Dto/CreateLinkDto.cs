using System.ComponentModel.DataAnnotations;

namespace Urly.Dto
{
    public class CreateLinkDto
    {
        [Required]
        public string? FullUrl { get; set; }
    }
}

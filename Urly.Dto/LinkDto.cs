using System.ComponentModel.DataAnnotations;

namespace Urly.Dto
{
    public class LinkDto
    {
        [Required]
        public string? FullUrl { get; set; }

        [Required]
        public string? ShortCode { get; set; }
    }
}

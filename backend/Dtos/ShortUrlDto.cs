using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public class ShortUrlDto
    {
        [Required]
        [Url]
        public string OriginalUrl { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public class AlgorithmDto
    {
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
    }
}

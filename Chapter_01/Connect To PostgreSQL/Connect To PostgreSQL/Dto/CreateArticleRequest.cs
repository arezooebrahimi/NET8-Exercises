using System.ComponentModel.DataAnnotations;

namespace Connect_To_PostgreSQL.Dto
{
    public class CreateArticleRequest
    {
        [Required]
        [StringLength(100)]
        public required string Title { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateArticleRequest() : CreateArticleRequest
    {

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogoApi.Model;
[Table("Categorias")]
public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    
    //PK
    [JsonIgnore]
    public ICollection<Produto>? Produtos { get; set; }
}
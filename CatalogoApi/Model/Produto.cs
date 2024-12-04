using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CatalogoApi.Model;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O nome tem que tem até 80 caracteres", MinimumLength = 5)]
    public string? Nome { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    public int? CategoriaId { get; set; }
    
    //PK
    //[JsonIgnore]
    public Categoria? Categoria { get; set; }

    // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    // {   
    //     if (!string.IsNullOrEmpty(this.Nome))
    //     {
    //         var primeiraLetraMaiuscula = this.Nome.ToString()[0].ToString();
    //
    //         if (primeiraLetraMaiuscula != primeiraLetraMaiuscula.ToUpper())
    //         {
    //             yield return new
    //                 ValidationResult("A primiera letra do nome do produto deve ser maiúscula", 
    //                     new[] { nameof(this.Nome) });
    //         }
    //     }
    //
    //     if (this.Estoque > 0.0)
    //     {
    //         yield return new
    //             ValidationResult("Estoque tem que ser maior a 0", 
    //                 new[] { nameof(this.Nome) });
    //     }
    // }
}
using System.ComponentModel.DataAnnotations;

namespace Desapeguei.Api.Models
{
    public class Produto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe um título para o anúncio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Escreva uma descrição para o produto.")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "A descrição deve ter entre 5 e 1000 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione uma categoria.")]
        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o preço.")]
        [Range(0.01, 999999, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public bool Disponivel { get; set; } = true;

        public string? CaminhoVideo { get; set; }

        public string? ImagemUrl { get; set; }

        // ID do usuário logado (Identity) que publicou o produto.
        // Usado em "Minhas vendas".
        public string? VendedorId { get; set; }

        // ID do vendedor em formato Guid (antigo "VendedorId" da API,
        // renomeado para não conflitar com o VendedorId string acima).
        public Guid? VendedorGuid { get; set; }

        // Nome do vendedor exibido no site e no admin.
        public string? CriadoPor { get; set; }
    }
}

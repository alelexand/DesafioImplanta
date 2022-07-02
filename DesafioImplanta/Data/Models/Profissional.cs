using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioImplanta.Models
{
    public class Profissional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(300, ErrorMessage = "O nome deve ter no máximo 300 caracteres")]
        public string NomeCompleto { get; set; }

        public DateTime DataNascimento { get; set; }

        [Required]
        public char Sexo { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "O CPF deve ter 11 caracteres")]
        public string CPF { get; set; }

        public int NumeroRegistro { get; set; }

        public bool Ativo { get; set; }

        [MaxLength(8, ErrorMessage = "O CEP deve ter 8 caracteres")]
        public string CEP { get; set; }

        [MaxLength(300, ErrorMessage = "A cidade deve ter no máximo 300 caracteres")]
        public string Cidade { get; set; } 

        public decimal? ValorRenda { get; set; } 

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}

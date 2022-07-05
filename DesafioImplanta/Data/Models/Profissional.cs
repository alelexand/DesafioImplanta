using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DesafioImplanta.Models
{
    public class Profissional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(300, ErrorMessage = "O nome deve ter no máximo 300 caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório")]
        public char Sexo { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [MaxLength(11, ErrorMessage = "O CPF deve ter 11 caracteres")]
        public string CPF { get; set; }

        [JsonIgnore]
        public int NumeroRegistro { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        [MaxLength(8, ErrorMessage = "O CEP deve ter 8 caracteres")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória")]
        [MaxLength(300, ErrorMessage = "A cidade deve ter no máximo 300 caracteres")]
        public string Cidade { get; set; } 

        public decimal? ValorRenda { get; set; }

        [JsonIgnore]
        public DateTime DataCriacao { get; set; }
    }
}

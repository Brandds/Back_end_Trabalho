using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Etapa_1.Model
{

    public class Cliente
    {
        public Cliente()
        {
            Alugueis = new HashSet<Aluguel>();
        }

        [Key]
        public int ID_Cliente { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefone { get; set; }

        [JsonIgnore]
        public virtual ICollection<Aluguel> Alugueis { get; set; }
    }
}
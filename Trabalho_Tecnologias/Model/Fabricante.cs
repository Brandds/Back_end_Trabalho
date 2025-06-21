using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Etapa_1.Model
{
    public class Fabricante
    {
        public Fabricante()
        {
            Veiculos = new HashSet<Veiculo>();
        }

        [Key]
        public int ID_Fabricante { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [JsonIgnore]
        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }
}
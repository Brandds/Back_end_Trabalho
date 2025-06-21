using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Etapa_1.Model
{
    public class Veiculo
    {
        public Veiculo()
        {
            Alugueis = new HashSet<Aluguel>();
        }

        [Key]
        public int ID_Veiculo { get; set; }

        [Required]
        [StringLength(150)]
        public string Modelo { get; set; }

        public int Ano { get; set; }

        public int Quilomentragens { get; set; }

        public int ID_Fabricante { get; set; }

        [ForeignKey("ID_Fabricante")]
        public virtual Fabricante Fabricante { get; set; }

        [JsonIgnore]
        public virtual ICollection<Aluguel> Alugueis { get; set; }
    }
}
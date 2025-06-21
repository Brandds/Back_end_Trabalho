using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Etapa_1.Model
{
    public class Aluguel
    {
        [Key]
        public int ID_Aluguel { get; set; }

        [Required]
        public DateTime Data_Inicio { get; set; }

        [Required]
        public DateTime Data_Fim { get; set; }

        [Column(TypeName = "decimal(18,2)")] 
        public decimal Valor_Total { get; set; }

        public int ID_Cliente { get; set; }

        public int ID_Veiculo { get; set; }

        [ForeignKey("ID_Cliente")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("ID_Veiculo")]
        public virtual Veiculo Veiculo { get; set; }
    }
}
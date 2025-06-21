using System.ComponentModel.DataAnnotations;

namespace Trabalho_Tecnologias
{
    public class AluguelCreateDTO
    {
        [Required]
        public DateTime Data_Inicio { get; set; }

        [Required]
        public DateTime Data_Fim { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        public decimal Valor_Total { get; set; }

        [Required]
        public int ID_Cliente { get; set; }

        [Required]
        public int ID_Veiculo { get; set; }
    }
}

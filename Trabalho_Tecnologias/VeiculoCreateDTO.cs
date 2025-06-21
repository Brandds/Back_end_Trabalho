using System.ComponentModel.DataAnnotations;

namespace Trabalho_Tecnologias
{
    public class VeiculoCreateDTO
    {
        [Required]
        [StringLength(150)]
        public string Modelo { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "A quilometragem não pode ser negativa.")]
        public int Quilomentragens { get; set; }

        [Required]
        public int ID_Fabricante { get; set; }
    }
}

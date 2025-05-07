using System.ComponentModel.DataAnnotations;

namespace WebJogos.Models
{
    public enum Status
    {
        [Display(Name = "Completo")]
        Completo,

        [Display(Name = "Parcial")]
        Parcial,

        [Display(Name = "Não iniciado")]
        NaoIniciado
    }
}

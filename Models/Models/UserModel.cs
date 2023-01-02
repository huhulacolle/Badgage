using System.ComponentModel.DataAnnotations;

namespace Badgage.Models.Models
{
    public class UserModel
    {
        [Required(AllowEmptyStrings = true)]
        public int IdUtil { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string AdresseMail { get; set; } = null!;
        public string Mdp { get; set; } = null!;
        public DateTime DateNaiss { get; set; }
    }
}

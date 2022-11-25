namespace Badgage.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string AdresseMail { get; set; } = null!;
        public string Mdp { get; set; } = null!;
        public DateTime DateNaiss { get; set; }
    }
}

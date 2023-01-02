namespace Badgage.Models.Models
{
    public class TeamModel
    {
        public int? IdTeam { get; set; }
        public string Nom { get; set; } = null!;
        public int ByUser { get; set; }
        public int NbTeam { get; set; }
    }
}

namespace Badgage.Models
{
    public class ProjectModel
    {
        public int? IdProject { get; set; }
        public string ProjectName { get; set; } = null!;
        public int IdTeam { get; set; }
        public int ByUser { get; set; }
    }
}

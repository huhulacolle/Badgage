namespace Badgage.Models
{
    public class TaskModel
    {
        public int? IdTache { get; set; }
        public int IdProjet { get; set; }
        public int IdUtil { get; set; }
        public string NomDeTache { get; set; }
        public string Description { get; set; }
        public DateTime DateFin { get; set; }
        public DateTime DateCreation { get; set; }
    }
}


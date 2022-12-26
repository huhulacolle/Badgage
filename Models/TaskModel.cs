namespace Badgage.Models
{
    public class TaskModel
    {
        public int? IdTache { get; set; }
        public string NomDeTache { get; set; }
        public string Descripton { get; set; }
        public DateTime DateFin { get; set; }
        public DateTime DateCreation { get; set; }
    }
}


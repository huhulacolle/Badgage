namespace Badgage.Models.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public int IdUser { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
    }
}

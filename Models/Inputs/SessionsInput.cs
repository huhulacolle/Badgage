namespace Badgage.Models.Inputs
{
    public class SessionInput
    {
        public int IdTask { get; set; }
        public int IdUser { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }
    }
}

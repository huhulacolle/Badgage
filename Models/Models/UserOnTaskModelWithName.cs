namespace Badgage.Models.Models
{
    public class UserOnTaskModelWithName
    {
        public int IdUser { get; set; }
        public string Email { get; set; } = null!;
        public int IdTask { get; set; }
    }
}

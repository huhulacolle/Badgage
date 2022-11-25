namespace Badgage.Exceptions
{
    [Serializable]
    public class EmailNotValidException : Exception
    {
        public EmailNotValidException()
            : base("Email non valide") { }
    }
}

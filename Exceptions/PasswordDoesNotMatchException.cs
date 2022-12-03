namespace Badgage.Exceptions
{
    [Serializable]
    public class PasswordDoesNotMatchException : Exception
    {
        public PasswordDoesNotMatchException() 
            : base("Mot de passe incorrect") { }
    }
}

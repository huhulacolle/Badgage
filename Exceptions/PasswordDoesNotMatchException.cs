namespace Badgage.Exceptions
{
    [Serializable]
    public class PasswordDoesNotMatchException : Exception
    {
        public PasswordDoesNotMatchException() 
            : base("les mots de passes ne correspondent pas") { }
    }
}

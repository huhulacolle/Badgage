namespace Badgage.Exceptions
{
    [Serializable]
    public class PasswordDoesNotMatchException : Exception
    {
        public PasswordDoesNotMatchException()
            : base("les deux mots de passes ne correspondent pas") { }
    }
}

namespace CQRS.Application.Exceptions
{
    public sealed class ValidateException : Exception
    {
        public ValidateException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base()
            => ErrorsDictionary = errorsDictionary;

        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}

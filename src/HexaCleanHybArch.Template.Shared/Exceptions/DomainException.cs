namespace HexaCleanHybArch.Template.Shared.Exceptions
{
    public class DomainException: BaseException
    {
        public override string Code => "domain_error";
        public int ErrorCode { get; } = 400;

        public DomainException(string message) : base(message) { }

        public DomainException(string message, Exception? innerException)
            : base(message, innerException) { }
    }
}

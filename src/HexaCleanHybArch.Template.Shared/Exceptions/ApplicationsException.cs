namespace HexaCleanHybArch.Template.Shared.Exceptions
{
    public class ApplicationsException: BaseException
    {
        public override string Code => "application_error";
        public int ErrorCode { get; private set; } = 400;

        public ApplicationsException(string message) : base(message) { }

        public ApplicationsException(string message, int ErrorCode) : base(message)
        {
            this.ErrorCode = ErrorCode;
        }

        public ApplicationsException(string message, Exception? innerException)
            : base(message, innerException) { }
    }
}

namespace HexaCleanHybArch.Template.Infra.Interfaces
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception? ex = null);
        void LogDebug(string message);
    }
}

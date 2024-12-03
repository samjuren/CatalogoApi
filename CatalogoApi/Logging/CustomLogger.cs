namespace CatalogoApi.Logging;

public class CustomLogger : ILogger
{
    readonly string loggerName;
    private readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }
    
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(message);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminho = $"/Users/samjuren/Logs/{loggerName}.txt";
        string caminhoArquivoLog = $@"{Directory.GetCurrentDirectory()}\logs\{loggerName}.txt";
        using (StreamWriter sw = new StreamWriter(caminho, true))
        {
            try
            {
                sw.WriteLine(mensagem);
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
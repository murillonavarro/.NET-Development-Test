namespace Development_Test.Models
{
    public sealed class Logger
    {
        // Inicialização Lazy para garantir o Singleton thread-safe
        private static readonly Lazy<Logger> lazyInstance = new Lazy<Logger>(() => new Logger());

        // Objeto de bloqueio para garantir thread safety ao escrever no arquivo
        private static readonly object lockObject = new object();

        private Logger() { }

        public static Logger Instance => lazyInstance.Value;

        public async Task LogAsync(LogLevel level, string message)
        {
            string logEntry = $"[{DateTime.Now}] [{level}] {message}{Environment.NewLine}";

            try
            {
                await Task.Run(() =>
                {
                    lock (lockObject)
                    {
                        using (StreamWriter writer = File.AppendText("log.txt"))
                        {
                            writer.Write(logEntry);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro durante o registro, registra-o no console
                Console.WriteLine($"Erro durante o registro: {ex.Message}");
            }
        }
    }
}

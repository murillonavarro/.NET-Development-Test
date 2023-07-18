using Development_Test.Enums;

namespace Development_Test.Models
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> lazyInstance = new Lazy<Logger>(() => new Logger());
        private static readonly object lockObject = new object();

        private Logger() { }

        public static Logger Instance => lazyInstance.Value;

        public async Task LogAsync(LogLevel level, string message)
        {
            string logEntry = $"[{DateTime.Now}] [{level}] {message}{Environment.NewLine}";
            string logDirectory = "logs";

            try
            {
                // Verifica se a pasta 'logs' existe e cria-a se não existir
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                await Task.Run(() =>
                {
                    lock (lockObject)
                    {
                        // Cria o caminho completo para o arquivo de log dentro da pasta 'logs'
                        string logFilePath = Path.Combine(logDirectory, "log.txt");

                        using (StreamWriter writer = File.AppendText(logFilePath))
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

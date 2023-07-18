using Development_Test.Enums;
using Development_Test.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Exemplo de uso do logger
        await Logger.Instance.LogAsync(LogLevel.Info, "Essa é uma mensagem de informação.");
        await Logger.Instance.LogAsync(LogLevel.Warning, "Essa é uma mensagem de aviso.");
        await Logger.Instance.LogAsync(LogLevel.Error, "Essa é uma mensagem de erro.");

        // Aguarda a conclusão das tarefas assíncronas de log
        Task[] tasks = { Logger.Instance.LogAsync(LogLevel.Info, "Essa é uma mensagem de informação.") };
        await Task.WhenAll(tasks);

        // Aguarda uma tecla para encerrar o programa
        Console.WriteLine("Log concluído. Pressione qualquer tecla para fechar...");
        Console.ReadKey();
    }
}
namespace ETicaretWebApi.Services
{
    public class ConsoleLoggerService : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[ConsoleLogger] "+message);
        }
    }
}

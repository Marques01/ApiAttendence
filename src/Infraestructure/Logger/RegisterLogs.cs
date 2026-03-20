namespace Infrastructure.Logger
{
    public class RegisterLogs
    {
        public async static Task CreateAsync(string message, string model)
        {
            string path = @$"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory)}\{model}-{DateTime.Now.ToString("dd-MM-yy")}.log";

            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                await streamWriter.WriteLineAsync("\n");

                await streamWriter.WriteLineAsync("------------------------------------ ");

                await streamWriter.WriteLineAsync($"Model: {model}");

                await streamWriter.WriteLineAsync($"Error: {message}");

                await streamWriter.WriteLineAsync($"Hour: {DateTime.Now}");

                await streamWriter.WriteLineAsync("\n");

                await streamWriter.WriteLineAsync("------------------------------------ ");
            }
        }
    }
}

using System.Text.Json;

namespace Domain.Utils
{
    public static class JsonUtils
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = false
        };

        public static string Serialize(object? obj)
        {
            if (obj is null)
                return string.Empty;

            return JsonSerializer.Serialize(obj, _options);
        }
    }

}

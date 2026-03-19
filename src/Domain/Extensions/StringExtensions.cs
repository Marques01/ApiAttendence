using System.Text.RegularExpressions;

namespace Domain.Extensions
{
    public static class StringExtensions
    {
        public static string CleanInput(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var regex = new Regex(@"[^a-zA-Z0-9\s\u00C0-\u00FF]");

            string cleanValue = regex.Replace(input, string.Empty);

            return cleanValue.Trim();
        }

        public static string FormatCPF(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return string.Empty;

            // Remove qualquer caractere que não seja número
            cpf = Regex.Replace(cpf, @"[^\d]", "");

            if (cpf.Length != 11)
                return cpf; // Retorna o CPF original se não tiver 11 dígitos

            // Formata o CPF
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public static string FormatCellPhoneNumber(this string number)
        {
            // Remove todos os caracteres não numéricos
            number = Regex.Replace(number, @"\D", "");

            // Verifica se o número tem 11 dígitos
            if (number.Length == 11)
                return Regex.Replace(number, @"(\d{2})(\d{5})(\d{4})", "($1) $2-$3");

            throw new ArgumentException("O número de celular deve conter 11 dígitos.");
        }

        public static string CapitalizeFirstLetters(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var words = text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }

            string formatedValue = string.Join(" ", words);
            return formatedValue.Trim();
        }
    }
}
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelloWorldApp.Generated
{
    /// <summary>
    /// Behandelt die Begrüßungslogik für den Nutzer.
    /// </summary>
    public static class GreetingHandler
    {
        private static readonly Regex ValidUsernamePattern = new("^[a-zA-Z0-9 _-]+$", RegexOptions.Compiled);
        private const int MaxUsernameLength = 50;

        /// <summary>
        /// Validiert und formatiert den Nutzernamen.
        /// Erlaubt alphanumerische Zeichen, Leerzeichen, Binde- und Unterstriche.
        /// Maximale Länge: 50 Zeichen.
        /// </summary>
        /// <param name="username">Der zu validierende und formatierende Nutzername</param>
        /// <returns>Die formatierte Begrüßungsnachricht</returns>
        /// <exception cref="ArgumentNullException">Wenn der Nutzername null ist</exception>
        /// <exception cref="ArgumentException">Wenn der Nutzername ungültig ist</exception>
        public static string ProcessGreeting(string username)
        {
            if (username is null)
                throw new ArgumentNullException(nameof(username), "Nutzername darf nicht null sein.");

            var trimmed = username.Trim();

            if (string.IsNullOrEmpty(trimmed))
                throw new ArgumentException("Nutzername darf nicht leer sein.", nameof(username));

            if (trimmed.Length > MaxUsernameLength)
                throw new ArgumentException($"Nutzername darf höchstens {MaxUsernameLength} Zeichen lang sein.", nameof(username));

            if (!ValidUsernamePattern.IsMatch(trimmed))
                throw new ArgumentException("Nutzername enthält ungültige Zeichen.", nameof(username));

            var formattedName = char.ToUpper(trimmed[0]) + trimmed[1..];
            return $"Hallo {formattedName}! Willkommen bei Hello-World.";
        }
    }
}
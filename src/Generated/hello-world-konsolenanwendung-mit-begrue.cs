// src/greeting.js
"use strict";

/**
 * Validiert und formatiert den Nutzernamen.
 * Erlaubt alphanumerische Zeichen, Leerzeichen, Binde- und Unterstriche.
 * Maximale Länge: 50 Zeichen.
 *
 * @param  {string} username
 * @returns {string} Der bereinigte Nutzername
 * @throws {Error} Wenn der Nutzername ungültig ist
 */
function greeting(username) {
  if (typeof username !== "string") {
    throw new TypeError("Nutzername muss ein String sein.");
  }

  const trimmed = username.trim();
  if (!trimmed) {
    throw new Error("Nutzername darf nicht leer sein.");
  }

  if (trimmed.length > 50) {
    throw new RangeError("Nutzername darf höchstens 50 Zeichen lang sein.");
  }

  const isValid = /^[a-zA-Z0-9 _-]+$/.test(trimmed);
  if (!isValid) {
    throw new RangeError("Nutzername enthält ungültige Zeichen.");
  }

  // Erstes Zeichen groß, Rest so belassen
  return `Hallo ${trimmed.charAt(0).toUpperCase()}${trimmed.slice(1)}! Willkommen bei Hello-World.`;
}

module.exports = { greeting };
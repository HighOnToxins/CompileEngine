namespace ParseEngine.Scanning;

public sealed record Token<TToken>(TToken Category, int Index, string Lexeme) where TToken : notnull;
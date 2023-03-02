namespace ParseEngine.Scanning;

public sealed record Token<TToken>(TToken Category, int Index, object? Lexeme) where TToken : notnull;
namespace ParseEngine.Scanning;

public sealed record Token<TSymbol>(TSymbol Category, int Index, object? Lexeme) where TSymbol : notnull;
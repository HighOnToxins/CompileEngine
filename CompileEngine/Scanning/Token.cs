namespace ParseEngine.Scanning;

public class Token<TSymbol> where TSymbol : notnull {
    public TSymbol Category { get; private init; }
    public int Index { get; private init; }

    internal Token(TSymbol category, int index) {
        Category = category;
        Index = index;
    }
}

public sealed class Token<TSymbol, T>: Token<TSymbol> where TSymbol : notnull {

    public T Lexeme { get; private init; }

    internal Token(TSymbol category, int index, T lexeme) : base(category, index) {
        Lexeme = lexeme;
    }
}


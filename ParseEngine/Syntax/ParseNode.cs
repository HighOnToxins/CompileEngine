
using ParseEngine.Scanning;

namespace ParseEngine.Syntax; 

public sealed record ParseNode<TSymbol> where TSymbol : notnull {

    public TSymbol Symbol { get; set; }
    public IReadOnlyList<Token<TSymbol>> Tokens { get; set; }
    public IReadOnlyList<ParseNode<TSymbol>> Children { get; set; }

    public ParseNode(TSymbol symbol, IReadOnlyList<Token<TSymbol>> tokens, IReadOnlyList<ParseNode<TSymbol>> children) {
        Symbol = symbol;
        Tokens = tokens;
        Children = children;
    }
}

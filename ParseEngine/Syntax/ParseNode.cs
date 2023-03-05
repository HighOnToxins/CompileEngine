
using ParseEngine.Scanning;

namespace ParseEngine.Syntax; 

public sealed class ParseNode<TSymbol> where TSymbol : notnull{

    public TSymbol? Symbol { get; private init; }
    public IReadOnlyList<ParseNode<TSymbol>> SubNodes { get; private init; }
    public IReadOnlyList<Token<TSymbol>> Tokens { get; private init; }

    internal ParseNode(TSymbol? symbol, IReadOnlyList<ParseNode<TSymbol>> subNodes, IReadOnlyList<Token<TSymbol>> tokens) {
        Symbol = symbol;
        SubNodes = subNodes;
        Tokens = tokens;
    }

    public ParseNode(TSymbol? symbol, IReadOnlyList<ParseNode<TSymbol>> subNodes) : this(symbol, subNodes, Array.Empty<Token<TSymbol>>()){}

    public ParseNode(TSymbol? symbol, IReadOnlyList<Token<TSymbol>> tokens) : this(symbol, Array.Empty<ParseNode<TSymbol>>(), tokens) {}

    public ParseNode(TSymbol? symbol) : this(symbol, Array.Empty<ParseNode<TSymbol>>(), Array.Empty<Token<TSymbol>>()) { }

    public ParseNode(IReadOnlyList<ParseNode<TSymbol>> subNodes) : this(default, subNodes, Array.Empty<Token<TSymbol>>()) { }

    public ParseNode(IReadOnlyList<Token<TSymbol>> tokens) : this(default, Array.Empty<ParseNode<TSymbol>>(), tokens) { }

}
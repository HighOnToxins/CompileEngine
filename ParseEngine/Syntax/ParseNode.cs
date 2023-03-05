
using ParseEngine.Scanning;

namespace ParseEngine.Syntax; 

public sealed class ParseNode<TSymbol> where TSymbol : notnull{

    public TSymbol Symbol { get; private init; }

    public List<ParseNode<TSymbol>> SubNodes { get; private init; }
    public List<Token<TSymbol>> Tokens { get; private init; }

    internal ParseNode(TSymbol symbol, List<ParseNode<TSymbol>> subNodes, List<Token<TSymbol>> tokens) {
        Symbol = symbol;
        SubNodes = subNodes;
        Tokens = tokens;
    }

}
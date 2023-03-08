
using ParseEngine.Scanning;

namespace ParseEngine.Syntax;

public abstract class ParseNode<TSymbol> where TSymbol : notnull {

}

public sealed class ConcatenationNode<TSymbol>: ParseNode<TSymbol> where TSymbol : notnull {

    public IReadOnlyList<ParseNode<TSymbol>> SubNodes { get; private init; }

    internal ConcatenationNode(IReadOnlyList<ParseNode<TSymbol>> subNodes) {
        SubNodes = subNodes;
    }

    internal ConcatenationNode(params ParseNode<TSymbol>[] subNodes) {
        SubNodes = subNodes;
    }

    internal ConcatenationNode() {
        SubNodes = Array.Empty<ParseNode<TSymbol>>();
    }
}

public sealed class NonTerminalNode<TSymbol>: ParseNode<TSymbol> where TSymbol : notnull {

    public TSymbol Symbol { get; private init; }

    public ParseNode<TSymbol> SubNode { get; private init; }

    internal NonTerminalNode(TSymbol symbol, ParseNode<TSymbol> subNode) {
        Symbol = symbol;
        SubNode = subNode;
    }
}

public sealed class TerminalNode<TSymbol>: ParseNode<TSymbol> where TSymbol : notnull {

    public Token<TSymbol> Token { get; private init; }

    internal TerminalNode(Token<TSymbol> tokens) {
        Token = tokens;
    }

}
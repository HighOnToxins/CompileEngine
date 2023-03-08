
using ParseEngine.Scanning;

namespace ParseEngine.Syntax;

public abstract class ParseNode<TSymbol> where TSymbol : notnull {

}

public sealed class ComplimentNode<TSymbol>: ParseNode<TSymbol> where TSymbol : notnull {

    public IReadOnlyList<ParseNode<TSymbol>> SubNodes { get; private init; }

    internal ComplimentNode(IReadOnlyList<ParseNode<TSymbol>> subNodes) {
        SubNodes = subNodes;
    }

    internal ComplimentNode(params ParseNode<TSymbol>[] subNodes) {
        SubNodes = subNodes;
    }

    internal ComplimentNode() {
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
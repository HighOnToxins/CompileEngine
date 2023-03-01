
namespace ParseEngine.Syntax; 

public sealed record ParseTree<TSymbol> where TSymbol : notnull {
    public ParseNode<TSymbol> Root { get; }

    public ParseTree(ParseNode<TSymbol> root) {
        Root = root;
    } 
}
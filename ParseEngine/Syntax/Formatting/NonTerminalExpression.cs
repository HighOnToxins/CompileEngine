
namespace ParseEngine.Syntax.Formatting;

internal sealed class NonTerminalExpression<TSymbol>: ProductionExpression<TSymbol> where TSymbol : notnull {

    private readonly TSymbol _nonterminal;

    public NonTerminalExpression(TSymbol lhs) {
        _nonterminal = lhs;
    }

    internal override ParseNode<TSymbol> Match(Parser<TSymbol> parser) {
        return parser.Parse(_nonterminal);
    }
}

namespace ParseEngine.Syntax.Formatting;

public sealed class TerminalExpression<TSymbol>: ProductionExpression<TSymbol> where TSymbol : notnull {

    private readonly TSymbol _terminal;

    public TerminalExpression(TSymbol token) {
        _terminal = token;
    }

    internal override ParseNode<TSymbol> Match(Parser<TSymbol> parser) {
        return new TerminalNode<TSymbol>(parser.Expect(_terminal));
    }

}
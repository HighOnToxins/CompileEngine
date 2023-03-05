
namespace ParseEngine.Syntax.Formatting;

public sealed class SymbolExpression<TSymbol>: ProductionExpression<TSymbol> where TSymbol : notnull {

    private readonly TSymbol _symbol;

    public SymbolExpression(TSymbol symbol) {
        _symbol = symbol;
    }

    internal override ParseNode<TSymbol> Parse(Parser<TSymbol> parser) {
        if(parser.IsTerminal(_symbol)) {
            return parser.Parse(_symbol);
        } else {
            return new TerminalNode<TSymbol>(parser.Expect(_symbol));
        }
    }

}
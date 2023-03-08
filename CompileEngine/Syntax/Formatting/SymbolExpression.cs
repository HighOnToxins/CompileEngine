
namespace ParseEngine.Syntax.Formatting;

public sealed class SymbolExpression<TSymbol>: ProductionExpression<TSymbol> where TSymbol : notnull {

    private readonly TSymbol _symbol;

    public SymbolExpression(TSymbol symbol) {
        _symbol = symbol;
    }

    internal override ParseNode<TSymbol> Parse(Parser<TSymbol> parser) =>
        parser.Match(_symbol);

}
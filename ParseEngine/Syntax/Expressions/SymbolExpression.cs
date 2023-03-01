
namespace ParseEngine.Syntax.Expressions;

public sealed class SymbolExpression<TSymbol>: ParseExpression<TSymbol> where TSymbol : notnull {

    private TSymbol _symbol;

    public SymbolExpression(TSymbol symbol){
        _symbol = symbol;
    }

    internal override ParseExpression<TSymbol> Simplify() => this;
}
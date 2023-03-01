
namespace ParseEngine.Syntax.Expressions;

public sealed class SymbolExpression<TSymbol>: ParseExpression<TSymbol> where TSymbol : notnull {
    internal override IReadOnlyList<ParseNode<TSymbol>> Parse() {
        throw new NotImplementedException();
    }
}
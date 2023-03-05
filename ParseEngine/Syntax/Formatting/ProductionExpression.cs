
namespace ParseEngine.Syntax.Formatting;

public abstract class ProductionExpression<TSymbol> where TSymbol : notnull {

    protected IReadOnlyList<ProductionExpression<TSymbol>> Operands { get; private init; }

    public ProductionExpression() {
        Operands = Array.Empty<ProductionExpression<TSymbol>>();
    }

    public ProductionExpression(IReadOnlyList<ProductionExpression<TSymbol>> operands) {
        Operands = operands;
    }

    //TODO: Add simplify/extract function.

    internal abstract ParseNode<TSymbol> Match(Parser<TSymbol> parser);

}
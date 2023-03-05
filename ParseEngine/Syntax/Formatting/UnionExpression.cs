
namespace ParseEngine.Syntax.Formatting;

public sealed class UnionExpression<TSymbol>: ProductionExpression<TSymbol> where TSymbol : notnull {

    private readonly IReadOnlyList<ProductionExpression<TSymbol>> _operands;

    //TODO: Add empty check for union.

    public UnionExpression(IReadOnlyList<ProductionExpression<TSymbol>> operands) {
        _operands = operands;
    }

    public UnionExpression(params ProductionExpression<TSymbol>[] operands) {
        _operands = operands;
    }

    internal override ParseNode<TSymbol> Parse(Parser<TSymbol> parser) {
        throw new NotImplementedException(); //TODO: add fork function to parser.
    }
}
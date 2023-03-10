
namespace CompileEngine.Syntax.Formatting;

public sealed class UnionExpression<TSymbol>: ProductionExpression<TSymbol> where TSymbol : notnull {

    private readonly IReadOnlyList<ProductionExpression<TSymbol>> _operands;

    public UnionExpression(IReadOnlyList<ProductionExpression<TSymbol>> operands) {
        _operands = operands;

        if(_operands.Count == 0) throw new ArgumentException("Operand size can not be zero.");
    }

    public UnionExpression(params ProductionExpression<TSymbol>[] operands) {
        _operands = operands;

        if(_operands.Count == 0) throw new ArgumentException("Operand size can not be zero.");
    }

    internal override ParseNode<TSymbol> Parse(Parser<TSymbol> parser) =>
        parser.Pick(_operands);
}
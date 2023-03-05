
namespace ParseEngine.Syntax.Formatting;

public sealed class ConcatenationExpression<TSymbol>: ProductionExpression<TSymbol> where TSymbol : notnull {

    private readonly IReadOnlyList<ProductionExpression<TSymbol>> _operands;

    //TODO: Add empty check.

    public ConcatenationExpression(IReadOnlyList<ProductionExpression<TSymbol>> operands){
        _operands = operands;
    }

    public ConcatenationExpression(params ProductionExpression<TSymbol>[] operands) {
        _operands = operands;
    }

    internal override ParseNode<TSymbol> Parse(Parser<TSymbol> parser) {
        List<ParseNode<TSymbol>> subNodes = new();

        for(int i = 0; i < _operands.Count; i++) {
            subNodes.Add(_operands[i].Parse(parser));
        }

        return new ConcatenationNode<TSymbol>(subNodes);
    }
}

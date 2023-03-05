
namespace ParseEngine.Syntax.Formatting;

public sealed class ConcatenationExpression<TSymbol>: ProductionExpression<TSymbol> where TSymbol : notnull {

    //TODO: Add empty check.
    public ConcatenationExpression(IReadOnlyList<ProductionExpression<TSymbol>> operands) : 
        base(operands) {}

    public ConcatenationExpression(params ProductionExpression<TSymbol>[] operands) :
       base(operands) { }

    internal override ParseNode<TSymbol> Match(Parser<TSymbol> parser) {
        List<ParseNode<TSymbol>> subNodes = new();

        for(int i = 0; i < Operands.Count; i++) {
            subNodes.Add(Operands[i].Match(parser));
        }

        return new ConcatenationNode<TSymbol>(subNodes);
    }
}

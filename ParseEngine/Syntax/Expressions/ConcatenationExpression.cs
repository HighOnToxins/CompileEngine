
namespace ParseEngine.Syntax.Expressions;

public sealed class ConcatenationExpression<TSymbol> : ParseExpression<TSymbol> where TSymbol : notnull{

    public ConcatenationExpression(params ParseExpression<TSymbol>[] operands) : base(operands.ToList()){}

    public ConcatenationExpression(IReadOnlyList<ParseExpression<TSymbol>> operands) : base(operands) { }

    internal override ParseExpression<TSymbol> Simplify() {
        return new ConcatenationExpression<TSymbol>(Extract<ConcatenationExpression<TSymbol>>().ToArray());
    }
}
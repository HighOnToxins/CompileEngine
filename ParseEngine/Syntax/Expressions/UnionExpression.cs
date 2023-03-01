
namespace ParseEngine.Syntax.Expressions;

public sealed class UnionExpression<TSymbol> : ParseExpression<TSymbol> where TSymbol : notnull{

    private readonly IReadOnlyList<ParseExpression<TSymbol>> _operands;

    public UnionExpression(params ParseExpression<TSymbol>[] operands) : this(operands.ToList()) { }

    public UnionExpression(IReadOnlyList<ParseExpression<TSymbol>> operands) {
        _operands = operands;
    }

    internal override ParseExpression<TSymbol> Simplify() =>
        new UnionExpression<TSymbol>(Extract<UnionExpression<TSymbol>>().ToArray());
}
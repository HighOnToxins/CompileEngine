
using ParseEngine.Scanning;
using ParseEngine.Syntax.Expressions;

namespace ParseEngine.Syntax; 

public sealed class Production<TSymbol> where TSymbol : notnull {

    private readonly TSymbol _symbol;
    private readonly ParseExpression<TSymbol> _expression;
    private IReadOnlySet<TSymbol> _firstTokens;

    public Production(TSymbol symbol, ParseExpression<TSymbol> expression) {
        _symbol = symbol;
        _expression = expression;
        _firstTokens = new HashSet<TSymbol>();
    }

    public ParseNode<TSymbol> Parse(in Grammar<TSymbol> grammar, ref int index, IReadOnlyList<Token<TSymbol>> soruce) {
        throw new NotImplementedException();
    } 

}

using ParseEngine.Scanning;
using ParseEngine.Syntax.Expressions;
using System.Collections;
using System.Linq.Expressions;

namespace ParseEngine.Syntax; 

public sealed class Grammar<TSymbol> : IEnumerable<Production<TSymbol>> where TSymbol : notnull {

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, Production<TSymbol>> _productions;

    public Grammar(TSymbol startingSymbol) {
        _startingSymbol = startingSymbol;
        _productions = new();
    }

    //TODO: Add labels later for easy rewrite.
    //TODO: Add/specify precedence and associativity. 
    public void Add(TSymbol symbol, params TSymbol[] symbols) {
        SymbolExpression<TSymbol>[] symbolExpressions = symbols
                .Select(s => new SymbolExpression<TSymbol>(s)).ToArray();
        ConcatenationExpression<TSymbol> concatenation = new(symbolExpressions);
        Add(symbol, concatenation);
    }

    public void Add(TSymbol symbol, ParseExpression<TSymbol> expression) {
        if(_productions.TryGetValue(symbol, out Production<TSymbol>? production)) {
            UnionExpression<TSymbol> union = new(expression, production.Expression);
            _productions[symbol] = new(symbol, union.Simplify());
        } else {
            _productions.Add(symbol, new(symbol, expression.Simplify()));
        }
    }

    internal IReadOnlySet<TSymbol> GetFirstsOf(TSymbol symbol) {
        throw new NotImplementedException();
    }

    public ParseTree<TSymbol> Parse(IReadOnlyList<Token<TSymbol>> source) {
        int tokenIndex = 0;
        ParseNode<TSymbol> node = RecursiveParse(ref tokenIndex, source, _startingSymbol);
        return new ParseTree<TSymbol>(node);
    }

    internal ParseNode<TSymbol> RecursiveParse(ref int index, IReadOnlyList<Token<TSymbol>> source, TSymbol currentSymbol) {
        throw new NotImplementedException();
    }

    IEnumerator<Production<TSymbol>> IEnumerable<Production<TSymbol>>.GetEnumerator() => 
        _productions
            .Select(k => k.Value)
            .GetEnumerator();

    public IEnumerator GetEnumerator() =>
        _productions
            .Select(k => k.Value)
            .GetEnumerator();

}
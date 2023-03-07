
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ParseEngine.Syntax;

public sealed class Grammar<TSymbol>: IEnumerable<KeyValuePair<TSymbol, ProductionExpression<TSymbol>>> where TSymbol : notnull {

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, ProductionExpression<TSymbol>> _productions;

    public Grammar(TSymbol startingSymbol) {
        _startingSymbol = startingSymbol;
        _productions = new();
    }

    public void Add(TSymbol symbol, ProductionExpression<TSymbol> production) {
        if(_productions.TryGetValue(symbol, out ProductionExpression<TSymbol>? expression)) {
            _productions[symbol] = new UnionExpression<TSymbol>(production, expression);
        } else {
            _productions.Add(symbol, production);
        }
    }

    public void Add(TSymbol symbol, params TSymbol[] symbols) =>
        Add(symbol, new ConcatenationExpression<TSymbol>(symbols
            .Select(s => new SymbolExpression<TSymbol>(s)).ToArray()));

    //TODO: Add labeling.

    public ParseNode<TSymbol> Parse(IReadOnlyList<Token<TSymbol>> source) {
        return new Parser<TSymbol>(this, source).Parse(_startingSymbol);
    }

    internal ProductionExpression<TSymbol> GetProduction(TSymbol symbol) => _productions[symbol];

    internal bool TryGetProduction(TSymbol symbol, [NotNullWhen(true)] out ProductionExpression<TSymbol>? production) =>
        _productions.TryGetValue(symbol, out production);

    internal bool IsNonTerminal(TSymbol symbol) => _productions.ContainsKey(symbol);

    IEnumerator<KeyValuePair<TSymbol, ProductionExpression<TSymbol>>> IEnumerable<KeyValuePair<TSymbol, ProductionExpression<TSymbol>>>.GetEnumerator() =>
        _productions.GetEnumerator();

    public IEnumerator GetEnumerator() =>
        throw new NotImplementedException();

    internal bool Lookahead(ProductionExpression<TSymbol> productionExpression, Token<TSymbol> token) {
        throw new NotImplementedException();
    }
}
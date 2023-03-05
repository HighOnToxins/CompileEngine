
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ParseEngine.Syntax; 

public sealed class Grammar<TSymbol> : IGrammar<TSymbol>, IEnumerable<KeyValuePair<TSymbol, ProductionExpression<TSymbol>>> where TSymbol : notnull  {

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, ProductionExpression<TSymbol>> _productions;

    public Grammar(TSymbol startingSymbol) {
        _startingSymbol = startingSymbol;
        _productions = new();
    }

    //TODO: Add union operator.
    public void Add(TSymbol symbol, ProductionExpression<TSymbol> production) {
        if(_productions.ContainsKey(symbol)) {
            _productions[symbol] = production;
        } else {
            _productions.Add(symbol, production);
        }
    }

    //TODO: Write recursive parse.

    //TODO: Read tokens.
    //TODO: Read symbols.
    //TODO: Read subsymbols.
    //TODO: Read other formatting.
    //TODO: Add labeling.

    public ParseNode<TSymbol> Parse(IReadOnlyList<Token<TSymbol>> source) {
        return new Parser<TSymbol>(this, source).Parse(_startingSymbol);
    }

    internal ProductionExpression<TSymbol> GetProduction(TSymbol symbol) => _productions[symbol];

    bool IGrammar<TSymbol>.TryGetProduction(TSymbol symbol, [NotNullWhen(true)] out ProductionExpression<TSymbol>? production) =>
        _productions.TryGetValue(symbol, out production);

    IEnumerator<KeyValuePair<TSymbol, ProductionExpression<TSymbol>>> IEnumerable<KeyValuePair<TSymbol, ProductionExpression<TSymbol>>>.GetEnumerator() =>
        _productions.GetEnumerator();

    public IEnumerator GetEnumerator() =>
        throw new NotImplementedException();

}
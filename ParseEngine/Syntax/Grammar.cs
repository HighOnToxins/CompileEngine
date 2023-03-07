
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ParseEngine.Syntax;

public sealed class Grammar<TSymbol>: IEnumerable<KeyValuePair<TSymbol, ProductionExpression<TSymbol>>> where TSymbol : notnull {

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, ProductionExpression<TSymbol>> _productions;
    private readonly int _maxLookahead;

    public Grammar(TSymbol startingSymbol, int maxLookahead = 1) {
        _startingSymbol = startingSymbol;
        _productions = new();
        _maxLookahead = maxLookahead;
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
        return new Parser<TSymbol>(this, source, _maxLookahead).Parse(_startingSymbol);
    }


    internal bool TryGetProduction(TSymbol nonterminal, [NotNullWhen(true)] out ProductionExpression<TSymbol>? production) =>
        _productions.TryGetValue(nonterminal, out production);

    internal bool IsNonTerminal(TSymbol symbol) => _productions.ContainsKey(symbol);


    internal IReadOnlySet<Token<TSymbol>> Lookahead(IReadOnlyList<TSymbol> symbols, Token<TSymbol> token, int look) {

        /* Pseudocode: Looking ahead the desired amount, and caching the result.
            
            if is cached 
                return cached
            
            Queue (path, tokens) frontier = add all productionExpressions.
            HashSet descovered

            while frontier.count > 0
        


            cache answer

        */

        throw new NotImplementedException();
    }


    IEnumerator<KeyValuePair<TSymbol, ProductionExpression<TSymbol>>> IEnumerable<KeyValuePair<TSymbol, ProductionExpression<TSymbol>>>.GetEnumerator() =>
        _productions.GetEnumerator();

    public IEnumerator GetEnumerator() => _productions.GetEnumerator();

}
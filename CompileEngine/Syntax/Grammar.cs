
using ParseEngine.Scanning;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ParseEngine.Syntax;

public class Union<TSymbol>: List<Compliment<TSymbol>> { }
public class Compliment<TSymbol>: List<TSymbol> {
    public Compliment(TSymbol[] symbols) : base(symbols) { }
}
public class Digit<TSymbol>: List<TSymbol> {}


public sealed class Grammar<TSymbol>: IEnumerable where TSymbol : notnull {

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, Union<TSymbol>> _productions;
    private readonly int _maxLookahead;

    private readonly Dictionary<TSymbol, List<Digit<TSymbol>>> _lookaheadTable; 

    public Grammar(TSymbol startingSymbol, int maxLookahead = 1) {
        _startingSymbol = startingSymbol;
        _productions = new();
        _maxLookahead = maxLookahead;

        _lookaheadTable = new();
    }


    public void Add(TSymbol symbol, params TSymbol[] symbols) {
        Compliment<TSymbol> concatenation = new(symbols);
        if(_productions.TryGetValue(symbol, out Union<TSymbol>? union)) {
            union.Add(concatenation);
        } else {
            _productions.Add(symbol, new Union<TSymbol> { concatenation });
        }
    }


    //TODO: Add labeling.

    public ParseNode<TSymbol> Parse(IReadOnlyList<Token<TSymbol>> source) {
        return new Parser<TSymbol>(this, source, _maxLookahead).Parse(_startingSymbol);
    }


    internal bool IsNonTerminal(TSymbol symbol) => _productions.ContainsKey(symbol);

    internal bool TryGetProduction(TSymbol nonterminal, [NotNullWhen(true)] out Union<TSymbol>? union) =>
        _productions.TryGetValue(nonterminal, out union);

    public IEnumerator GetEnumerator() => throw new NotImplementedException();

}
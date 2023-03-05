
using ParseEngine.Exceptions;
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;
using System.Collections;

namespace ParseEngine.Syntax; 

public sealed class Grammar<TSymbol> : IEnumerable<KeyValuePair<TSymbol, Production<TSymbol>>> where TSymbol : notnull  {

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, Production<TSymbol>> _productions;

    public Grammar(TSymbol startingSymbol) {
        _startingSymbol = startingSymbol;
        _productions = new();
    }

    //TODO: Change to adding union operator.
    public void Add(TSymbol symbol, Production<TSymbol> production) {
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
        int tokenIndex = 0;
        return RecursiveParse(_startingSymbol, ref tokenIndex, source);
    }

    internal ParseNode<TSymbol> RecursiveParse(TSymbol currentLHS, ref int index, IReadOnlyList<Token<TSymbol>> source) {
        if(_productions.TryGetValue(currentLHS, out Production<TSymbol>? production)) {
            return production.Parse(this, ref index, source);
        } else {
            throw new UnexpectedException<TSymbol>(index, currentLHS);
        }
    }

    IEnumerator<KeyValuePair<TSymbol, Production<TSymbol>>> IEnumerable<KeyValuePair<TSymbol, Production<TSymbol>>>.GetEnumerator() =>
        _productions.GetEnumerator();

    public IEnumerator GetEnumerator() =>
        throw new NotImplementedException();

}
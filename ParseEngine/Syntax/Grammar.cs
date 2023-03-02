
using ParseEngine.Scanning;
using System.Collections;

namespace ParseEngine.Syntax; 

public sealed class Grammar<TToken, TSymbol> : IEnumerable<Production<TToken, TSymbol>> where TToken : notnull where TSymbol : notnull {

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, Production<TToken, TSymbol>> _productions;

    public Grammar(TSymbol startingSymbol) {
        _startingSymbol = startingSymbol;
        _productions = new();
    }

    //The objects of format, is either a TToken or a TSymbol
    //The objects of instansiateFunction are the output type.

    //TODO: Add formatting.

    public void Add(TSymbol symbol, ParseOptions parseOptions, Func<object[], object> instansiateFunction, params object[] format) {
        if(_productions.TryGetValue(symbol, out Production<TToken, TSymbol>? production)) {
            throw new NotImplementedException();
        } else {
            _productions.Add(symbol, new(instansiateFunction, format));
        }
    }

    public void Add(TSymbol symbol, Func<object[], object> instansiateFunction, params object[] format) {
        if(_productions.TryGetValue(symbol, out Production<TToken, TSymbol>? production)) {
            throw new NotImplementedException();
        } else {
            _productions.Add(symbol, new(instansiateFunction, format));
        }
    }

    //TODO: Write recursive parse search.

    public object Parse(IReadOnlyList<Token<TToken>> source) {

        throw new NotImplementedException();

    }

    IEnumerator<Production<TToken, TSymbol>> IEnumerable<Production<TToken, TSymbol>>.GetEnumerator() =>
        throw new NotImplementedException();

    public IEnumerator GetEnumerator() =>
        throw new NotImplementedException();

}
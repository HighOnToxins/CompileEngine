
using ParseEngine.Scanning;
using System.Collections;

namespace ParseEngine.Syntax; 

public sealed class Grammar<TToken, TSymbol> : IEnumerable<Production<TToken>> where TToken : notnull where TSymbol : notnull {

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, Production<TToken>> _productions;

    public Grammar(TSymbol startingSymbol) {
        _startingSymbol = startingSymbol;
        _productions = new();
    }

    //The objects of format, is either a TToken or a TSymbol
    //The objects of instansiateFunction are the output type.
    public void Add(TSymbol symbol, Func<object[], object> instansiateFunction, params object[] format) {
        if(_productions.TryGetValue(symbol, out Production<TToken>? production)) {
            throw new NotImplementedException();
        } else {
            _productions.Add(symbol, new(instansiateFunction, format));
        }
    }

    public object Parse(IReadOnlyList<Token<TToken>> source) {

        throw new NotImplementedException();

    }

    IEnumerator<Production<TToken>> IEnumerable<Production<TToken>>.GetEnumerator() =>
        throw new NotImplementedException();

    public IEnumerator GetEnumerator() =>
        throw new NotImplementedException();

}
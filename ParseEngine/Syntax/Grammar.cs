
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;
using System.Collections;

namespace ParseEngine.Syntax; 

public sealed class Grammar<TToken, TSymbol> : IEnumerable<Production<TToken, TSymbol>> where TToken : notnull where TSymbol : notnull {

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, Production<TToken, TSymbol>> _productions;

    public Grammar(TSymbol startingSymbol) {
        _startingSymbol = startingSymbol;
        _productions = new();
    }

    //The objects of format, is either a TToken, a TSymbol or other format.
    //  -> Each object counts as an input object given to the instansiateFunction.
    //The objects of instansiateFunction are the output type.

    public void Add(TSymbol symbol, ParseOptions parseOptions, Func<object?[], object> instansiateFunction, params ParseFormat<TToken, TSymbol>[] format) {
        if(_productions.TryGetValue(symbol, out Production<TToken, TSymbol>? production)) {
            throw new NotImplementedException();
        } else {
            _productions.Add(symbol, new(instansiateFunction, format));
        }
    }

    public void Add(TSymbol symbol, ParseOptions parseOptions, Func<object?[], object> instansiateFunction, params object[] objectFormat) {
        ParseFormat<TToken, TSymbol>[] format = new ParseFormat<TToken, TSymbol>[objectFormat.Length];

        for(int i = 0; i < objectFormat.Length; i++) {
            format[i] = objectFormat[i] switch {
                ParseFormat<TToken, TSymbol> f => f,
                TToken t => new TokenFormat<TToken, TSymbol>(t),
                TSymbol s => new SymbolFormat<TToken, TSymbol>(s),
                _ => throw new ArgumentException(),
            };
        }

        Add(symbol, parseOptions, instansiateFunction, format);
    }

    public void Add(TSymbol symbol, Func<object?[], object> instansiateFunction, params ParseFormat<TToken, TSymbol>[] format) =>
        Add(symbol, default, instansiateFunction, format);

    public void Add(TSymbol symbol, Func<object?[], object> instansiateFunction, params object[] format) =>
        Add(symbol, default, instansiateFunction, format);

    //TODO: Write recursive parse search.

    public object Parse(IReadOnlyList<Token<TToken>> source) {

        //TODO: Read tokens.
        //TODO: Read symbols.
        //TODO: Read subsymbols.
        //TODO: Read other formatting.

        throw new NotImplementedException();

    }

    IEnumerator<Production<TToken, TSymbol>> IEnumerable<Production<TToken, TSymbol>>.GetEnumerator() =>
        throw new NotImplementedException();

    public IEnumerator GetEnumerator() =>
        throw new NotImplementedException();

}
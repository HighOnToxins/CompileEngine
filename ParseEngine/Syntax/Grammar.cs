
using ParseEngine.Scanning;
using System.Collections;
using System.Linq.Expressions;

namespace ParseEngine.Syntax; 

public sealed class Grammar<TToken> : IEnumerable<Production<TToken>> where TToken : notnull {

    private readonly object _startingType;
    private readonly Dictionary<object, Production<TToken>> _productions;

    public Grammar(object startingType) {
        _startingType = startingType;
        _productions = new();
    }

    public object Parse(IReadOnlyList<Token<TToken>> source) {

        throw new NotImplementedException();

    }

    IEnumerator<Production<TToken>> IEnumerable<Production<TToken>>.GetEnumerator() =>
        throw new NotImplementedException();

    public IEnumerator GetEnumerator() =>
        throw new NotImplementedException();

}
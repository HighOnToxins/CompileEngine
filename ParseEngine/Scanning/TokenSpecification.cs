
using System.Text.RegularExpressions;

namespace ParseEngine.Scanning;

public sealed class TokenSpecification<TSymbol> where TSymbol : notnull {

    private readonly Regex _regex;
    private readonly TSymbol? _category;
    private readonly Func<string, object?>? _lexemeFunction;
    private readonly bool _ignore;

    internal TokenSpecification(string regex, TSymbol? category, Func<string, object?>? lexemeFunction) {
        _regex = new Regex(regex);
        _category = category;
        _lexemeFunction = lexemeFunction;
    }

    internal TokenSpecification(string regex, TSymbol? category) : this(regex, category, null){
    }

    internal TokenSpecification(string regex) : this(regex, default, null){
        _ignore = true;
    }

    internal bool MatchAt(string s, int index, out int length, out Token<TSymbol>? token) {
        token = null;
        Match match = _regex.Match(s, index);

        if(match.Index != index || !match.Success) {
            length = 0;
            return false;
        }

        token = _category == null || _ignore ? null : new Token<TSymbol>(_category, index, _lexemeFunction?.Invoke(match.Value));
        length = match.Length;
        return true;
    }
}

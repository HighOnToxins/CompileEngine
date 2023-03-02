
using System.Text.RegularExpressions;

namespace ParseEngine.Scanning;

public sealed class TokenSpecification<TToken> where TToken : notnull {

    private readonly TToken? _category;
    private readonly Regex _regex;
    private readonly Func<string, object?>? _lexemeFunction;

    internal TokenSpecification(TToken? category, string regex, Func<string, object?>? lexemeFunction) {
        _category = category;
        _regex = new Regex(regex);
        _lexemeFunction = lexemeFunction;
    }

    internal bool MatchAt(string s, int index, out int length, out Token<TToken>? token) {
        token = null;
        Match match = _regex.Match(s, index);

        if(match.Index != index || !match.Success) {
            length = 0;
            return false;
        }

        token = _category == null ? null : new Token<TToken>(_category, index, _lexemeFunction?.Invoke(match.Value));
        length = match.Length;
        return true;
    }
}

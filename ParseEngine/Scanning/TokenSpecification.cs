
using System.Text.RegularExpressions;

namespace ParseEngine.Scanning;

public sealed class TokenSpecification<TToken> where TToken : notnull
{

    private readonly TToken _category;
    private readonly Regex _regex;
    private readonly bool _isIgnored;

    internal TokenSpecification(TToken category, string regex, bool isIgnored = false)
    {
        _category = category;
        _regex = new Regex(regex);
        _isIgnored = isIgnored;
    }

    internal bool MatchAt(string s, int index, out int length, out Token<TToken>? token)
    {
        token = null;
        Match match = _regex.Match(s, index);

        if (match.Index != index || !match.Success)
        {
            length = 0;
            return false;
        }

        if (!_isIgnored)
        {
            token = new Token<TToken>(_category, index, match.Value);
        }

        length = match.Length;
        return true;
    }
}
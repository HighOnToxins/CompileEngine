
using ParseEngine.Exceptions;
using System.Collections;

namespace ParseEngine.Scanning;

public sealed class Tokenizer<TToken>: IEnumerable<TokenSpecification<TToken>> where TToken : notnull {

    private readonly List<TokenSpecification<TToken>> _tokenSpecifications;

    public Tokenizer() {
        _tokenSpecifications = new();
    }

    public void Add(string regex) =>
        _tokenSpecifications.Add(new TokenSpecification<TToken>(regex, default, s => null));

    public void Add(string regex, TToken? category) =>
        _tokenSpecifications.Add(new TokenSpecification<TToken>(regex, category,  s => null));

    public void Add(string regex, TToken? category, Func<string, object?>? lexemeFunction) =>
        _tokenSpecifications.Add(new TokenSpecification<TToken>(regex, category, lexemeFunction));

    public IReadOnlyList<Token<TToken>> GetTokensOf(string s) {
        List<Token<TToken>> tokens = new();

        int index = 0;
        while(index < s.Length) {
            if(TryGetNextToken(ref index, s, out Token<TToken>? token)) {
                if(token != null) tokens.Add(token);
            } else {
                throw new UndefinedTokenException(index, s);
            }
        }

        return tokens;
    }

    private bool TryGetNextToken(ref int index, string s, out Token<TToken>? token) {

        foreach(TokenSpecification<TToken> tokenSpecification in _tokenSpecifications) {
            if(tokenSpecification.MatchAt(s, index, out int length, out token)) {
                index += length;
                return true;
            }
        }

        token = null;
        return false;
    }

    public IEnumerator<TokenSpecification<TToken>> GetEnumerator() => _tokenSpecifications.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
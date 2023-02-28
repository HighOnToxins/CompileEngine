
using ParseEngine.Exceptions;
using System.Collections;

namespace ParseEngine.Scanning;

public sealed class Tokenizer<TToken>: IEnumerable<TokenSpecification<TToken>> where TToken : notnull {

    private readonly List<TokenSpecification<TToken>> _tokenSpecifications;

    public Tokenizer() {
        _tokenSpecifications = new();
    }

    public void Add(TToken category, string regex, bool ignoreString = false) =>
        _tokenSpecifications.Add(new TokenSpecification<TToken>(category, regex, ignoreString));

    public IReadOnlyList<Token<TToken>> GetTokensOf(string s) {
        List<Token<TToken>> tokens = new();

        int index = 0;
        while(index < s.Length) {
            if(TryGetNextToken(ref index, s, out Token<TToken>? token)) {
                if(token != null) tokens.Add(token);
            } else {
                throw new UndefinedTokenException(index);
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
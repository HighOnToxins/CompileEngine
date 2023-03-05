
using ParseEngine.Exceptions;
using System.Collections;

namespace ParseEngine.Scanning;

public sealed class Tokenizer<TSymbol>: IEnumerable<TokenSpecification<TSymbol>> where TSymbol : notnull {

    private readonly List<TokenSpecification<TSymbol>> _tokenSpecifications;

    public Tokenizer() {
        _tokenSpecifications = new();
    }

    public void Add(string regex) =>
        _tokenSpecifications.Add(new TokenSpecification<TSymbol>(regex));

    public void Add(string regex, TSymbol? category) =>
        _tokenSpecifications.Add(new TokenSpecification<TSymbol>(regex, category));

    public void Add(string regex, TSymbol? category, Func<string, object?>? lexemeFunction) =>
        _tokenSpecifications.Add(new TokenSpecification<TSymbol>(regex, category, lexemeFunction));

    public IReadOnlyList<Token<TSymbol>> GetTokensOf(string s) {
        List<Token<TSymbol>> tokens = new();

        int index = 0;
        while(index < s.Length) {
            if(TryGetNextToken(ref index, s, out Token<TSymbol>? token)) {
                if(token != null) tokens.Add(token);
            } else {
                throw new UndefinedTokenException(index, s);
            }
        }

        return tokens;
    }

    private bool TryGetNextToken(ref int index, string s, out Token<TSymbol>? token) {

        foreach(TokenSpecification<TSymbol> tokenSpecification in _tokenSpecifications) {
            if(tokenSpecification.MatchAt(s, index, out int length, out token)) {
                index += length;
                return true;
            }
        }

        token = null;
        return false;
    }

    public IEnumerator<TokenSpecification<TSymbol>> GetEnumerator() => _tokenSpecifications.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
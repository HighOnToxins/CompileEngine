
using ParseEngine.Exceptions;
using System.Collections;

namespace ParseEngine.Scanning;

public sealed class Tokenizer<TSymbol>: IEnumerable<TokenSpecification> where TSymbol : notnull {

    private readonly List<TokenSpecification> _tokenSpecifications;

    public Tokenizer() {
        _tokenSpecifications = new();
    }

    public void Add(string regex) =>
        _tokenSpecifications.Add(new TokenSpecification(regex));

    public void Add(string regex, TSymbol category) =>
        _tokenSpecifications.Add(new TokenSpecification<TSymbol>(regex, category));

    public void Add<TLexeme>(string regex, TSymbol category, Func<string, TLexeme> lexemeFunction) =>
        _tokenSpecifications.Add(new TokenSpecification<TSymbol, TLexeme>(regex, category, lexemeFunction));

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

        foreach(TokenSpecification tokenSpecification in _tokenSpecifications) {
            if(tokenSpecification is TokenSpecification<TSymbol> symbolSpecification) {
                if(symbolSpecification.MatchAt(s, index, out int length0, out token)) {
                    index += length0;
                    return true;
                }

                continue;
            }

            if(tokenSpecification.MatchAt(s, index, out int length1)) {
                index += length1;
                token = null;
                return true;
            }
        }

        token = null;
        return false;
    }

    public IEnumerator<TokenSpecification> GetEnumerator() => _tokenSpecifications.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
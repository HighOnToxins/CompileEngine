
using System.Text.RegularExpressions;

namespace ParseEngine.Scanning;

public class TokenSpecification {

    protected Regex Regex { get; private init; }

    internal TokenSpecification(string regex) {
        Regex = new Regex(regex);
    }

    internal virtual bool MatchAt(string s, int index, out int length) {
        Match match = Regex.Match(s, index);

        if(match.Index != index || !match.Success) {
            length = 0;
            return false;
        }

        length = match.Length;
        return true;
    }
}

public class TokenSpecification<TSymbol> : TokenSpecification where TSymbol : notnull {

    protected TSymbol Category { get; private init; }

    internal TokenSpecification(string regex, TSymbol category) : base(regex){
        Category = category;
    }

    internal virtual bool MatchAt(string s, int index, out int length, out Token<TSymbol>? token) {
        token = null;
        Match match = Regex.Match(s, index);

        if(match.Index != index || !match.Success) {
            length = 0;
            return false;
        }

        token = new Token<TSymbol>(Category, index);

        length = match.Length;
        return true;
    }
}

public sealed class TokenSpecification<TSymbol, TLexeme> : TokenSpecification<TSymbol> where TSymbol : notnull {

    private readonly Func<string, TLexeme> _lexemeFunction;

    internal TokenSpecification(string regex, TSymbol category, Func<string, TLexeme> lexemeFunction) : base(regex, category){
        _lexemeFunction = lexemeFunction;
    }

    internal override bool MatchAt(string s, int index, out int length, out Token<TSymbol>? token) {
        token = null;
        Match match = Regex.Match(s, index);

        if(match.Index != index || !match.Success) {
            length = 0;
            return false;
        }

        TLexeme lexeme = _lexemeFunction.Invoke(match.Value);
        token = new Token<TSymbol, TLexeme>(Category, index, lexeme);

        length = match.Length;
        return true;
    }
}

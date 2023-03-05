
using ParseEngine.Scanning;

namespace ParseEngine.Syntax.Formatting;

public sealed class TokenProduction<TSymbol>: Production<TSymbol> where TSymbol : notnull {

    private readonly TSymbol _token;

    public TokenProduction(TSymbol token) {
        _token = token;
    }

    //match function
    public override ParseNode<TSymbol> Parse(in Grammar<TSymbol> grammar, ref int index, IReadOnlyList<Token<TSymbol>> source) {

        if(_token.Equals(source[index].Category)) { //peek function
            return new ParseNode<TSymbol>(new Token<TSymbol>[] { source[index++] });
        } else {
            throw new UnexpectedException<TSymbol>(index, _token, source[index].Category);
        }

    }
}
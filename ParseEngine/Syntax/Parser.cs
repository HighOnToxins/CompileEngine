
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;

namespace ParseEngine.Syntax;

internal sealed class Parser<TSymbol> where TSymbol : notnull{

    private readonly IGrammar<TSymbol> _grammar;
    private readonly IReadOnlyList<Token<TSymbol>> _source;

    private int _index;

    public Parser(IGrammar<TSymbol> grammar, IReadOnlyList<Token<TSymbol>> source) {
        _grammar = grammar;
        _source = source;
        _index = 0;
    }

    public Token<TSymbol> Peek() {
        return _source[_index];
    }

    public Token<TSymbol> Expect(TSymbol symbol) {
        if(symbol.Equals(Peek().Category)) {
            Token<TSymbol> token = Peek();
            _index++;
            return token;
        } else {
            throw new UnexpectedException<TSymbol>(_index, symbol, Peek().Category);
        }
    }

    public ParseNode<TSymbol> Parse(TSymbol symbol) {
        if(_grammar.TryGetProduction(symbol, out ProductionExpression<TSymbol>? production)) {
            return new NonTerminalNode<TSymbol>(symbol, production.Match(this));
        } else {
            throw new UnexpectedException<TSymbol>(_index, symbol);
        }
    }
}
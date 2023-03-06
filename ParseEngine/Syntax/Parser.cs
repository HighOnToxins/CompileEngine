
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;

namespace ParseEngine.Syntax;

//Left-left (top-down) Parser
internal sealed class Parser<TSymbol> where TSymbol : notnull {

    private readonly Grammar<TSymbol> _grammar;
    private readonly IReadOnlyList<Token<TSymbol>> _source;

    private int _index;

    public Parser(Grammar<TSymbol> grammar, IReadOnlyList<Token<TSymbol>> source) {
        _grammar = grammar;
        _source = source;
        _index = 0;
    }

    internal bool IsTerminal(TSymbol symbol) => _grammar.IsTerminal(symbol);

    public Token<TSymbol> Peek() {
        return _source[_index];
    }

    public Token<TSymbol> Expect(TSymbol terminal) {
        if(terminal.Equals(Peek().Category)) {
            Token<TSymbol> token = Peek();
            _index++;
            return token;
        } else {
            throw new UnexpectedException<TSymbol>(_index, terminal, Peek().Category);
        }
    }

    public ParseNode<TSymbol> Parse(TSymbol nonterminal) {
        if(_grammar.TryGetProduction(nonterminal, out ProductionExpression<TSymbol>? production)) {
            return new NonTerminalNode<TSymbol>(nonterminal, production.Parse(this));
        } else {
            throw new UnexpectedException<TSymbol>(_index, nonterminal);
        }
    }

    public ParseNode<TSymbol> Pick(IReadOnlyList<ProductionExpression<TSymbol>> options) {

        //TODO: add/use lookahead function/method.
        //TODO: add/use backtracking/checkpointing.

        throw new NotImplementedException();
    }
}
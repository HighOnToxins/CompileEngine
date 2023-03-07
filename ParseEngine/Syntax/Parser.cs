
using ParseEngine.Exceptions;
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;
using System.Diagnostics.CodeAnalysis;

namespace ParseEngine.Syntax;

//Left-left (top-down) Parser
internal sealed class Parser<TSymbol> where TSymbol : notnull {

    private readonly Grammar<TSymbol> _grammar;
    private readonly IReadOnlyList<Token<TSymbol>> _source;

    private readonly int _maxLookahead;
    private int _index;

    public Parser(Grammar<TSymbol> grammar, IReadOnlyList<Token<TSymbol>> source, int maxLookahead = 1) {
        _grammar = grammar;
        _source = source;
        _maxLookahead = maxLookahead;
        _index = 0;
    }

    public Token<TSymbol> Peek(int i = 0) => _source[_index + i];

    public ParseNode<TSymbol> Match(TSymbol symbol) {
        if(_grammar.IsNonTerminal(symbol)) {
            return Parse(symbol);
        } else {
            return Expect(symbol);
        }
    }

    public TerminalNode<TSymbol> Expect(TSymbol terminal) {
        Token<TSymbol> token = Peek();
        if(terminal.Equals(token.Category)) {
            _index++;
            return new TerminalNode<TSymbol>(token);
        } else {
            throw new UnexpectedException<TSymbol>(_index, terminal, Peek().Category);
        }
    }

    public NonTerminalNode<TSymbol> Parse(TSymbol nonterminal) {
        if(_grammar.TryGetProduction(nonterminal, out ProductionExpression<TSymbol>? production)) {
            return new NonTerminalNode<TSymbol>(nonterminal, production.Parse(this));
        } else {
            throw new UnexpectedException<TSymbol>(_index, nonterminal);
        }
    }

    public ParseNode<TSymbol> Pick(IReadOnlyList<ProductionExpression<TSymbol>> options) {
        return Track(Look(options));
    }

    private IReadOnlyList<ProductionExpression<TSymbol>> Look(IReadOnlyList<ProductionExpression<TSymbol>> options) {
        List<ProductionExpression<TSymbol>> paths = options.ToList();
        for(int lookahead = 0; lookahead < _maxLookahead && paths.Count > 1; lookahead++) {
            for(int i = 0; i < paths.Count && paths.Count > lookahead; i++) {
                if(!_grammar.Lookahead(paths[i], lookahead).Contains(Peek(lookahead))) {
                    paths.RemoveAt(i);
                    i--;
                }
            }
        }
        return paths;
    }

    private ParseNode<TSymbol> Track(IReadOnlyList<ProductionExpression<TSymbol>> paths) {
        for(int i = 0; i < paths.Count; i++) {
            try {
                return paths[i].Parse(this);
            } catch(ParseException e) {
                if(i == paths.Count - 1) {
                    throw e;
                }
            }
        }

        throw new ArgumentException("No path available and no exception thrown.");
    }

}
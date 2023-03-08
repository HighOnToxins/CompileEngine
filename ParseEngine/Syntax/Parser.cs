
using ParseEngine.Exceptions;
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;
using System.Diagnostics.CodeAnalysis;

namespace ParseEngine.Syntax;

//Left-left (top-down) Parser
internal sealed class Parser<TSymbol> where TSymbol : notnull {

    private readonly Grammar<TSymbol> _grammar;
    private readonly IReadOnlyList<Token<TSymbol>> _source;

    private readonly int _maxLook;
    private int _index;

    public Parser(Grammar<TSymbol> grammar, IReadOnlyList<Token<TSymbol>> source, int maxLookahead = 1) {
        _grammar = grammar;
        _source = source;
        _maxLook = maxLookahead;
        _index = 0;
    }

    public NonTerminalNode<TSymbol> Parse(TSymbol nonterminal) {
        if(_grammar.TryGetProduction(nonterminal, out Union<TSymbol>? union)) {
            return new NonTerminalNode<TSymbol>(nonterminal, Pick(union));
        } else {
            throw new InvalidOperationException("Symbol was not nonterminal.");
        }
    }

    public ParseNode<TSymbol> Pick(Union<TSymbol> union) {
        if(union.Count <= 1) {
            return Loop(union[0]);
        }

        List<Compliment<TSymbol>> paths = union.ToList();
        
        //look
        for(int l = 0; l < _maxLook; l++) {
            Token<TSymbol> peekedToken = Peek(l);
            for(int i = 0; i < paths.Count && paths.Count > 1; i++) {
                if(!_grammar.Lookahead(paths[i], l).Contains(peekedToken.Category)) {
                    paths.RemoveAt(i);
                    i--;
                }
            }
        }

        //track/depth
        for(int i = 0; i < paths.Count; i++) {
            try {
                return Loop(paths[i]);
            } catch(ParseException e) {
                if(i == paths.Count - 1) {
                    throw e;
                }
            }
        }

        throw new ArgumentException("No path available and no exception thrown.");
    }

    public ParseNode<TSymbol> Loop(Compliment<TSymbol> compliment) {
        if(compliment.Count <= 1) {
            return Match(compliment[0]);
        }

        ParseNode<TSymbol>[] nodes = compliment
            .Select(s => Match(s))
            .ToArray();

        return new ComplimentNode<TSymbol>(nodes);
    }

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
            throw new UnexpectedException<TSymbol>(_index, terminal, token.Category);
        }
    }

    public Token<TSymbol> Peek(int i = 0) => _source[_index + i];


}
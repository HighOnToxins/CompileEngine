
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

        //track/depth
        for(int i = 0; i < union.Count; i++) {
            try {
                return Loop(union[i]);
            } catch(CompileException e) {
                if(i == union.Count - 1) {
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
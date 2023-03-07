
using ParseEngine.Scanning;
using ParseEngine.Syntax.Formatting;
using System.Diagnostics.CodeAnalysis;

namespace ParseEngine.Syntax;

//Left-left (top-down) Parser
internal sealed class Parser<TSymbol> where TSymbol : notnull {

    private readonly Grammar<TSymbol> _grammar;
    private readonly IReadOnlyList<Token<TSymbol>> _source;

    private int _maxLookahead;
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

        //TODO: add/use lookahead function/method.

        /* Psudocode: breath-search through all paths to determine firsts
        
            ParseException? e = null;
            

            //remove options (auto-break when option count is less-than or equal to 1
            
            for lookahead = 0 to maxLookahead && while options > 1:
                foreach(option in options  && while options > 1:
                    if(!grammar.Lookahead(option, Peek(lookahead))) :
                        options.remove(option)
            

            //try all remaining options
            
                


            if e != null :
                throw e;
            else 
                throw new ParseException();
        */

        //TODO: add/use backtracking/checkpointing.

        throw new NotImplementedException();
    }
}
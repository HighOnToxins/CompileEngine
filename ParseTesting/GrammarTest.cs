using ParseEngine.Scanning;
using ParseEngine.Syntax;

namespace ParseTesting;

public class GrammarTest {

    private enum Symbol {
        number,
        seperator,
        start,
        end,

        Values,
    }

    private static Tokenizer<Symbol> CreateScanner() => new() {
        {"[0-9]+",  Symbol.number,    s => int.Parse(s)},
        {"\\,",     Symbol.seperator},
        {"\\(",     Symbol.start},
        {"\\)",     Symbol.end},
        {"\\s+"},
    };

    [Test]
    public void CanReadTokens() {

        Tokenizer<Symbol> scanner = CreateScanner();

        Grammar<Symbol> grammar = new(Symbol.Values) {
            {Symbol.Values, Symbol.start, Symbol.number, Symbol.seperator, Symbol.number, Symbol.end}
        };

        string str = "(27 , 5)";

        IReadOnlyList<Token<Symbol>> tokens = scanner.GetTokensOf(str);
        ParseNode<Symbol> container = grammar.Parse(tokens);

        Symbol[] expectedTokens = new Symbol[] {
            Symbol.start,
            Symbol.number,
            Symbol.seperator,
            Symbol.number,
            Symbol.end
        };

        if(container is NonTerminalNode<Symbol> nonterminal && nonterminal.Symbol == Symbol.Values &&
            nonterminal.SubNode is ConcatenationNode<Symbol> concatenation) {

            Assert.That(concatenation.SubNodes, Has.Count.EqualTo(5));

            for(int i = 0; i < concatenation.SubNodes.Count; i++) {
                if(concatenation.SubNodes[i] is TerminalNode<Symbol> terminal) {
                    Assert.That(terminal.Token.Category, Is.EqualTo(expectedTokens[i]));
                } else {
                    Assert.Fail();
                }
            }

        } else {
            Assert.Fail();
        }

    }

    [Test]
    public void CanReadSymbols() {

        Tokenizer<Symbol> scanner = CreateScanner();

        Grammar<Symbol> grammar = new(Symbol.Values) {
            {Symbol.Values, Symbol.start, Symbol.Values, Symbol.seperator, Symbol.Values, Symbol.end},
            {Symbol.Values, Symbol.number},
        };

        string str = "((1 , 2), 3)";

        IReadOnlyList<Token<Symbol>> tokens = scanner.GetTokensOf(str);
        ParseNode<Symbol> tree = grammar.Parse(tokens);



    }

}
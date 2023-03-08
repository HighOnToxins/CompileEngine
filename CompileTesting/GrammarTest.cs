using ParseEngine.Scanning;
using ParseEngine.Syntax;

namespace ParseTesting;

public class GrammarTest {

    private enum Symbol {
        number,
        seperator,
        start,
        end,

        ValueValues,
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

        Grammar<Symbol> grammar = new(Symbol.ValueValues) {
            {Symbol.ValueValues, Symbol.Values, Symbol.seperator, Symbol.Values},
            {Symbol.Values, Symbol.start, Symbol.number, Symbol.seperator, Symbol.number, Symbol.end}
        };

        string str = "(1 , 2), (3, 4)";

        IReadOnlyList<Token<Symbol>> tokens = scanner.GetTokensOf(str);
        ParseNode<Symbol> tree = grammar.Parse(tokens);

        int[] expectedValues = new int[] { 1, 2, 3, 4 };

        if(tree is NonTerminalNode<Symbol> nonterminal && nonterminal.Symbol == Symbol.ValueValues &&
            nonterminal.SubNode is ConcatenationNode<Symbol> concatenation) {

            Assert.That(concatenation.SubNodes, Has.Count.EqualTo(3));

            for(int i = 0; i < concatenation.SubNodes.Count; i += 2) {
                if(concatenation.SubNodes[i] is NonTerminalNode<Symbol> nonterminal2 &&
                    nonterminal2.SubNode is ConcatenationNode<Symbol> concatenation2) {

                    for(int j = 1; j < concatenation2.SubNodes.Count; j += 2) {
                        if(concatenation2.SubNodes[j] is TerminalNode<Symbol> terminal &&
                            terminal.Token is Token<Symbol, int> token) {
                            Assert.That(token.Lexeme, Is.EqualTo(expectedValues[i + j / 2]));
                        } else {
                            Assert.Fail();
                        }
                    }

                } else {
                    Assert.Fail();
                }
            }

        } else {
            Assert.Fail();
        }
    }

    [Test]
    public void CanReadRecursiveSymbols() {
        Tokenizer<Symbol> scanner = CreateScanner();

        Grammar<Symbol> grammar = new(Symbol.Values) {
            {Symbol.Values, Symbol.start, Symbol.Values, Symbol.seperator, Symbol.Values, Symbol.end},
            {Symbol.Values, Symbol.number},
        };

        string str = "((1 , 2) , 3)";

        IReadOnlyList<Token<Symbol>> tokens = scanner.GetTokensOf(str);
        ParseNode<Symbol> tree = grammar.Parse(tokens);

        if(tree is NonTerminalNode<Symbol> nonterminal && nonterminal.Symbol == Symbol.Values &&
            nonterminal.SubNode is ConcatenationNode<Symbol> concatenation) {

            Assert.That(concatenation.SubNodes, Has.Count.EqualTo(3));



        } else {
            Assert.Fail();
        }
    }

}
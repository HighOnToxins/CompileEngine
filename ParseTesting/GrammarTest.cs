using ParseEngine.Scanning;
using ParseEngine.Syntax;
using ParseEngine.Syntax.Formatting;

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

    //[Test]
    //public void CanReadSubSymbols() {

    //    Tokenizer<ExpToken> scanner = CreateExpScanner();

    //    Grammar<ExpToken, ExpSymbol> grammar = new(ExpSymbol.Exp) {
    //        { ExpSymbol.Exp, ParseOptions.LeftRight, o => new Add((Exp[])o), ExpSymbol.Exp, ExpToken.Add, ExpSymbol.Exp},
    //        { ExpSymbol.Exp, ParseOptions.RightLeft, o => new Mult((Exp[])o), ExpSymbol.Exp, ExpToken.Mult, ExpSymbol.Exp},
    //        { ExpSymbol.Exp, o => new Val((int)o[0]), ExpToken.Number},
    //    };

    //    string str = "2 * 27 + 5";

    //    IReadOnlyList<Token<ExpToken>> tokens = scanner.GetTokensOf(str);
    //    Exp exp = (Exp) grammar.Parse(tokens);

    //    if(exp is Add add && add.Operands[0] is Mult mult && mult.Operands[0] is Val v0 && mult.Operands[1] is Val v1 && exp.Operands[1] is Val v2) {
    //        Assert.Multiple(() => {
    //            Assert.That(v0.Value, Is.EqualTo(2));
    //            Assert.That(v1.Value, Is.EqualTo(27));
    //            Assert.That(v2.Value, Is.EqualTo(5));
    //        });
    //    } else {
    //        Assert.Fail();
    //    }
    //}

    //private abstract class Exp {
    //    public Exp[] Operands;

    //    public Exp(Exp[] operands) {
    //        Operands = operands;
    //    }
    //    public Exp() {
    //        Operands = Array.Empty<Exp>();
    //    }
    //}

    //private class Add: Exp { public Add(Exp[] operands) : base(operands) { } }
    //private class Mult: Exp { public Mult(Exp[] operands) : base(operands) { } }
    //private class Val: Exp { public int Value { get; } public Val(int value) {Value = value;} }
}
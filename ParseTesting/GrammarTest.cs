using ParseEngine.Scanning;
using ParseEngine.Syntax;
using ParseEngine.Syntax.Formatting;

namespace ParseTesting;

public class GrammarTest {

    private enum Symbol {
        NumberToken,
        SeperatorToken,
        StartToken,
        EndToken,

        ValuesLHS,
    }

    private static Tokenizer<Symbol> CreateExpScanner() => new() {
        {"[0-9]+",  Symbol.NumberToken,    s => int.Parse(s)},
        {"\\,",     Symbol.SeperatorToken},
        {"\\(",     Symbol.StartToken},
        {"\\)",     Symbol.EndToken},
        {"\\s+"},
    };

    [Test]
    public void CanReadTokens() {

        Tokenizer<Symbol> scanner = CreateExpScanner();

        Grammar<Symbol> grammar = new(Symbol.ValuesLHS) {
            {Symbol.ValuesLHS,
                new Concatenation<Symbol>( // "(" number "," number ")" 
                    new TokenProduction<Symbol>(Symbol.StartToken),
                    new TokenProduction<Symbol>(Symbol.NumberToken),
                    new TokenProduction<Symbol>(Symbol.SeperatorToken),
                    new TokenProduction<Symbol>(Symbol.NumberToken),
                    new TokenProduction<Symbol>(Symbol.EndToken)
                )}
        };

        string str = "(27 , 5)";

        IReadOnlyList<Token<Symbol>> tokens = scanner.GetTokensOf(str);
        ParseNode<Symbol> container = grammar.Parse(tokens);

    }

    //[Test]
    //public void CanReadSymbols() {

    //    Tokenizer<ExpToken> scanner = CreateExpScanner();

    //    Grammar<ExpToken, ExpSymbol> grammar = new(ExpSymbol.Exp) {
    //        { ExpSymbol.Add, ParseOptions.LeftRight, o => new Add((Exp[])o), ExpSymbol.Mult, ExpToken.Add, ExpSymbol.Mult},
    //        { ExpSymbol.Val, o => new Val((int)o[0]), ExpToken.Number},
    //    };

    //    string str = "27 + 5";

    //    IReadOnlyList<Token<ExpToken>> tokens = scanner.GetTokensOf(str);
    //    Exp exp = (Exp)grammar.Parse(tokens);

    //    if(exp is Add add && add.Operands[0] is Val v1 && add.Operands[1] is Val v2) {
    //        Assert.Multiple(() => {
    //            Assert.That(v1.Value, Is.EqualTo(27));
    //            Assert.That(v2.Value, Is.EqualTo(5));
    //        });
    //    } else {
    //        Assert.Fail();
    //    }
    //}

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
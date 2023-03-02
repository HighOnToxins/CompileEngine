using ParseEngine.Scanning;
using ParseEngine.Syntax;

namespace ParseTesting;

public class GrammarTest {

    private enum ExpToken {
        Number,
        Add,
    }

    private enum ExpSymbol {
        Add,
    }

    [Test]
    public void CanReadSingleOperator() {

        Tokenizer<ExpToken> scanner = new() {
            {ExpToken.Number, "[0-9]+", s => int.Parse(s)},
            {ExpToken.Add, "\\+"},
            {"\\s+"},
        };

        Grammar<ExpToken, ExpSymbol> grammar = new(ExpSymbol.Add) {
            { ExpSymbol.Add, o => new int[]{(int)o[0], (int)o[1] }, ExpToken.Number, ExpToken.Add, ExpToken.Number},
        };

        string str = "27 + 5";

        IReadOnlyList<Token<ExpToken>> tokens = scanner.GetTokensOf(str);
        int[] add = (int[]) grammar.Parse(tokens);

        Assert.That(add[0], Is.EqualTo(27));
        Assert.That(add[1], Is.EqualTo(5));
    }

}
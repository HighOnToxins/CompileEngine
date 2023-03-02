using ParseEngine.Scanning;
using ParseEngine.Syntax;

namespace ParseTesting;

public class GrammarTest {

    private enum Exp {
        NumberToken,
        AddToken,
    }

    [Test]
    public void CanReadSingleOperator() {

        Tokenizer<Exp> scanner = new() {
            {Exp.NumberToken, "[0-9]+", s => int.Parse(s)},
            {Exp.AddToken, "\\+"},
            {"\\s+"},
        };

       Grammar<Exp> grammar = new(Exp.ExpSymbol) {
            { Exp.ExpSymbol, Exp.NumberToken, Exp.AddToken, Exp.NumberToken}, 
        };

        string str = "27 + 5";

        IReadOnlyList<Token<Exp>> tokens = scanner.GetTokensOf(str);
        ParseTree<Exp> tree = grammar.Parse(tokens);

        Token<Exp>[] array = new Token<Exp>[tree.Root.Tokens.Count];
        for(int i = 0; i < array.Length; i++) {
            array[i] = tree.Root.Tokens[i];
        }

        Assert.That(array, Is.EqualTo(new Token<Exp>[] {
            new Token<Exp>(Exp.NumberToken, 0, "27"),
            new Token<Exp>(Exp.AddToken, 3, "+"),
            new Token<Exp>(Exp.NumberToken, 5, "5")
        }));
    }

    private class Add {
        public int LeftOperand { get; }
        public int RightOperand { get; }    

        public Add(int leftOperand, int rightOperand) {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }
    }

}
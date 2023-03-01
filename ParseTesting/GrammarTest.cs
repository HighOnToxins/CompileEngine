using ParseEngine.Scanning;
using ParseEngine.Syntax;
using ParseEngine.Syntax.Expressions;

namespace ParseTesting;

public class GrammarTest {

    private enum SymbolType {
        NumberToken,
        AddToken,
        Space,

        ExpSymbol,
    }

    [Test]
    public void CanReadSingleOperator() {

        Tokenizer<SymbolType> scanner = new() {
            {SymbolType.NumberToken, "[0-9]+"},
            {SymbolType.AddToken, "\\+"},
            {SymbolType.Space, "\\s+", true},
        };

       Grammar<SymbolType> grammar = new(SymbolType.ExpSymbol) {
            { SymbolType.ExpSymbol, SymbolType.NumberToken, SymbolType.AddToken, SymbolType.NumberToken}, 
        };

        string str = "27 + 5";

        IReadOnlyList<Token<SymbolType>> tokens = scanner.GetTokensOf(str);
        ParseTree<SymbolType> tree = grammar.Parse(tokens);

        Token<SymbolType>[] array = new Token<SymbolType>[tree.Root.Tokens.Count];
        for(int i = 0; i < array.Length; i++) {
            array[i] = tree.Root.Tokens[i];
        }

        Assert.That(array, Is.EqualTo(new Token<SymbolType>[] {
            new Token<SymbolType>(SymbolType.NumberToken, 0, "27"),
            new Token<SymbolType>(SymbolType.AddToken, 3, "+"),
            new Token<SymbolType>(SymbolType.NumberToken, 5, "5")
        }));

    }

}
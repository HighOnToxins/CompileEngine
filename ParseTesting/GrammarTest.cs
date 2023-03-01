using ParseEngine.Scanning;
using ParseEngine.Syntax;
using ParseEngine.Syntax.Expressions;

namespace ParseTesting;

public class GrammarTest {

    private enum TokenType {
        NumberToken,
        AddToken,
        Space,

        ExpSymbol,
    }

    [Test]
    public void CanReadSingleOperator() {

        Tokenizer<TokenType> scanner = new() {
            {TokenType.NumberToken, "[0-9]+"},
            {TokenType.AddToken, "\\+"},
            {TokenType.Space, "\\s+", true},
        };

        //TODO: Add labels later for easy rewrite.
        Grammar<TokenType> grammar = new(TokenType.ExpSymbol) {
            { TokenType.ExpSymbol, new SymbolExpression<TokenType>()}, 
        };

        string str = "27 + 5";

        IReadOnlyList<Token<TokenType>> tokens = scanner.GetTokensOf(str);
        ParseTree<TokenType> tree = grammar.Parse(tokens);

        Token<TokenType>[] array = new Token<TokenType>[tree.Root.Tokens.Count];
        for(int i = 0; i < array.Length; i++) {
            array[i] = tree.Root.Tokens[i];
        }

        Assert.That(array, Is.EqualTo(new Token<TokenType>[] {
            new Token<TokenType>(TokenType.NumberToken, 0, "27"),
            new Token<TokenType>(TokenType.AddToken, 3, "+"),
            new Token<TokenType>(TokenType.NumberToken, 5, "27")
        }));

    }

}
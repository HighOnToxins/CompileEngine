
using ParseEngine.Scanning;

namespace ParseTesting;

public class ScannerTest {

    private enum Word {
        Word,
        Special,
        Number,
    };

    private static Tokenizer<Word> CreateWordScanner() {
        return new() {
            {"\\s+"},
            { "[a-zA-Z]+",       Word.Word,     s => s},
            {"[^0-9a-zA-Z\\s]+", Word.Special,  s => s},
            {"[0-9]+",           Word.Number,   s => int.Parse(s)},
        };
    }

    [Test]
    public void ScannerWorks() {

        Tokenizer<Word> scanner = CreateWordScanner();
        string s = "This is a String.";

        string[] expected = { "This", "is", "a", "String", "." };

        Token<Word>[] tokens = scanner.GetTokensOf(s).ToArray();

        Assert.That(tokens, Has.Length.EqualTo(expected.Length));
        for(int i = 0; i < tokens.Length; i++) {
            if(tokens[i] is Token<Word, string> spelledTokens) {
                Assert.That(spelledTokens.Lexeme, Is.EqualTo(expected[i]));
            } else {
                Assert.Fail();
            }
        }

    }

}
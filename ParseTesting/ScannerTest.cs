
using ParseEngine.Scanning;

namespace ParseTesting;

public class ScannerTest {

    private enum Word {
        Word,
        Special,
        Number,
        Whitespace,
    };

    private static Tokenizer<Word> CreateWordScanner() {
        return new() {
            { Word.Whitespace, "\\s+", true},
            { Word.Word, "[a-zA-Z]+"},
            { Word.Special, "[^0-9a-zA-Z\\s]+"},
            { Word.Number, "[0-9]+"},
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
            if(tokens[i] is Token<Word> spelledTokens) {
                Assert.That(spelledTokens.Lexeme, Is.EqualTo(expected[i]));
            } else {
                Assert.Fail();
            }
        }

    }

}
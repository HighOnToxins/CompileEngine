
namespace ParseEngine.Exceptions;

[Serializable]
public sealed class UndefinedTokenException: CompileException {

    public int Index;
    public string String;

    public UndefinedTokenException(int index, string s) : base($"Unexpected character '{s[index]}' at index {index}") {
        Index = index;
        String = s;
    }
}
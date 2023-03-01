
namespace ParseEngine.Exceptions; 

public sealed class UndefinedTokenException : ParseException{

    public int Index;
    public string String;

    public UndefinedTokenException(int index, string s) : base($"Unexpected character '{s[index]}' at {index}"){
        Index = index;
        String = s;
    }
}
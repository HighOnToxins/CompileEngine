
namespace ParseEngine.Exceptions; 
public sealed class UndefinedTokenException : Exception{

    public int Index;

    public UndefinedTokenException(int index) {
        Index = index;
    }

}
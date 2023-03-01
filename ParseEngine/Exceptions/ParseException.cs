
namespace ParseEngine.Exceptions; 

public class ParseException : Exception{

    public ParseException() : base("The source could not be parsed properly.") {}

    public ParseException(string message) : base(message) {}

}
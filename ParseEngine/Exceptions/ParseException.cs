
//TODO: Rename project from ParserEngine to CompilerEngine

using System.Runtime.Serialization;

namespace ParseEngine.Exceptions;

[Serializable]
public abstract class ParseException: Exception {

    public ParseException() : base("The source could not be parsed properly.") { }

    public ParseException(string? message) : base(message) {
    }

    public ParseException(string? message, Exception? innerException) : base(message, innerException) {
    }

    protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) {
    }
}

//TODO: Rename project from ParserEngine to CompilerEngine

using System.Runtime.Serialization;

namespace ParseEngine.Exceptions;

[Serializable]
public abstract class CompileException: Exception {

    public CompileException() : base("The source could not be parsed properly.") { }

    public CompileException(string? message) : base(message) {
    }

    public CompileException(string? message, Exception? innerException) : base(message, innerException) {
    }

    protected CompileException(SerializationInfo info, StreamingContext context) : base(info, context) {
    }
}
using ParseEngine.Exceptions;
using System.Runtime.Serialization;

namespace ParseEngine.Syntax.Formatting;

[Serializable]
internal class UnexpectedException: ParseException {
    public int Index { get; private init; }
    public string Expected { get; private init; }

    public UnexpectedException(int index, string expected) : base($"Expected {expected} at index {index}.") {
        Index = index;
        Expected = expected;
    }

    public UnexpectedException(int index) : base($"Unexpected at index {index}.") {
        Index = index;
        Expected = "";
    }
}
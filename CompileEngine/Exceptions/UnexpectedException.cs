using ParseEngine.Exceptions;

namespace ParseEngine.Syntax.Formatting;

[Serializable]
internal class UnexpectedException: CompileException {
    public int Index { get; private init; }

    public UnexpectedException(int index) : base($"Unexpected at index {index}.") {
        Index = index;
    }
    public UnexpectedException(int index, string? message) : base(message) {
        Index = index;
    }
}

[Serializable]
internal class UnexpectedException<TSymbol>: UnexpectedException {
    public TSymbol? Expected { get; private init; }
    public TSymbol? Actual { get; private init; }

    public UnexpectedException(int index, TSymbol expected, TSymbol actual) : base(index, $"Expected {expected} at index {index}, but was {actual}.") {
        Expected = expected;
    }

    public UnexpectedException(int index, TSymbol expected) : base(index, $"Expected {expected} at index {index}.") {
        Expected = expected;
    }
}
using ParseEngine.Scanning;

namespace ParseEngine.Syntax.Formatting;

public abstract class Production<TSymbol> where TSymbol : notnull {

    protected IReadOnlyList<Production<TSymbol>> Operands { get; private init; }

    public Production() {
        Operands = Array.Empty<Production<TSymbol>>();
    }

    public Production(IReadOnlyList<Production<TSymbol>> operands) {
        Operands = operands;
    }

    public abstract ParseNode<TSymbol> Parse(in Grammar<TSymbol> grammar, ref int index, IReadOnlyList<Token<TSymbol>> source);

}
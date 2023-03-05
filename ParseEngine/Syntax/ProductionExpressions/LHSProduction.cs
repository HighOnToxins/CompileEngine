
using ParseEngine.Scanning;

namespace ParseEngine.Syntax.Formatting;

internal sealed class LHSProduction<TSymbol>: Production<TSymbol> where TSymbol : notnull {

    private readonly TSymbol _lhs;

    public LHSProduction(TSymbol lhs) {
        _lhs = lhs;
    }

    public override ParseNode<TSymbol> Parse(in Grammar<TSymbol> grammar, ref int index, IReadOnlyList<Token<TSymbol>> source) {
        return grammar.RecursiveParse(_lhs, ref index, source);
    }
}
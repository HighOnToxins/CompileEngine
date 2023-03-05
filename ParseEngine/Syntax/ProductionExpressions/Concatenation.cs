
using ParseEngine.Scanning;

namespace ParseEngine.Syntax.Formatting;

public sealed class Concatenation<TSymbol>: Production<TSymbol> where TSymbol : notnull {

    //TODO: Add empty check.
    public Concatenation(IReadOnlyList<Production<TSymbol>> operands) : 
        base(operands) {}

    public Concatenation(params Production<TSymbol>[] operands) :
       base(operands) { }

    public override ParseNode<TSymbol> Parse(in Grammar<TSymbol> grammar, ref int index, IReadOnlyList<Token<TSymbol>> source) {
        
        List<ParseNode<TSymbol>> subNodes = new();

        for(int i = 0; i < Operands.Count; i++) {
            subNodes.Add(Operands[i].Parse(grammar, ref index, source));
        }

        return new ParseNode<TSymbol>(subNodes);
    }
}

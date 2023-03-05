
using ParseEngine.Scanning;

namespace ParseEngine.Syntax.Formatting;

internal sealed class Concatenation<TSymbol>: Production<TSymbol> where TSymbol : notnull {



    public Concatenation(params TSymbol[] RHSSymbols) {
        throw new NotImplementedException();
    }

    public override ParseNode<TSymbol> Parse(in Grammar<TSymbol> grammar, ref int index, IReadOnlyList<Token<TSymbol>> soruce) {
        throw new NotImplementedException();
        //List<Token<TSymbol>> tokens = new();
        //List<ParseNode<TSymbol>> children = new();

        //int originalIndex = index;

        //for(int i = 0; i < _RHSSymbols.Length; i++) {
        //    if(_RHSSymbols[i].Equals(code[index].Category)) { //TODO: Find a way to determine if a TSymbol is a token and create an expect function with exception.
        //        tokens.Add(code[index]);
        //        index++;
        //    } else if(grammar.TryRecursiveParse(code, _RHSSymbols[i], ref index, out ParseNode<TSymbol>? childChildNode)) {
        //        children.Add(childChildNode);
        //    } else {
        //        index = originalIndex;
        //        parseNode = null;
        //        return false;
        //    }
        //}

        //parseNode = new ParseNode<TSymbol>(_LHSSymbol, children, tokens);
        //return true;
    }
}
namespace ParseEngine.Syntax.Expressions; 

public abstract class ParseExpression<TSymbol> where TSymbol : notnull {

    //internal abstract IReadOnlyList<ParseNode<TSymbol>> Parse();

    internal IReadOnlyList<ParseExpression<TSymbol>> Operands { get; init; }

    internal ParseExpression(IReadOnlyList<ParseExpression<TSymbol>> operands) {
        Operands = operands;
    }

    internal ParseExpression() {
        Operands = Array.Empty<ParseExpression<TSymbol>>();
    }

    internal abstract ParseExpression<TSymbol> Simplify();

    internal IEnumerable<ParseExpression<TSymbol>> Extract<E>() where E : ParseExpression<TSymbol> {
        if(this is E) {
            foreach(ParseExpression<TSymbol> expression in Operands.SelectMany(o => o.Extract<E>())) {
                yield return expression;
            }
        } else {
            yield return this;
        }
    }

}
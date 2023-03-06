
namespace ParseEngine.Syntax.Formatting;

public abstract class ProductionExpression<TSymbol> where TSymbol : notnull {

    //TODO: Add simplify/extract function.


    //TODO: Rewrite production expressions such that they collect tokens rather than piling them up.
    internal abstract ParseNode<TSymbol> Parse(Parser<TSymbol> parser);

}
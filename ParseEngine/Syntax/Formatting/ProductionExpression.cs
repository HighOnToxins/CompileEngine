
namespace ParseEngine.Syntax.Formatting;

public abstract class ProductionExpression<TSymbol> where TSymbol : notnull {

    //TODO: Add simplify/extract function.

    internal abstract ParseNode<TSymbol> Parse(Parser<TSymbol> parser);

}
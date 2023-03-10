
namespace CompileEngine.Syntax.Formatting;

public abstract class ProductionExpression<TSymbol> where TSymbol : notnull {

    //TODO: Add simplify/extract function.

    //TODO: Add left/right associativity and extract/split for abstract grammars.

    //TODO: Rewrite production expressions such that they collect tokens rather than piling them up.
    internal abstract ParseNode<TSymbol> Parse(Parser<TSymbol> parser);

}
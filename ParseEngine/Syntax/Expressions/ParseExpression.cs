namespace ParseEngine.Syntax.Expressions; 

public abstract class ParseExpression<TSymbol> where TSymbol : notnull {

    internal abstract IReadOnlyList<ParseNode<TSymbol>> Parse();

}
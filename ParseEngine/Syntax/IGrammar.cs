
using ParseEngine.Syntax.Formatting;
using System.Diagnostics.CodeAnalysis;

namespace ParseEngine.Syntax;

internal interface IGrammar<TSymbol> where TSymbol : notnull {
    internal bool TryGetProduction(TSymbol symbol, [NotNullWhen(true)] out ProductionExpression<TSymbol>? production);
}
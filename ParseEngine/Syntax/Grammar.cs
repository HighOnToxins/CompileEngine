
using ParseEngine.Scanning;
using ParseEngine.Syntax.Expressions;
using System.Collections;

namespace ParseEngine.Syntax; 

public sealed class Grammar<TSymbol> : IEnumerable<Production<TSymbol>> where TSymbol : notnull {

    private record SymbolDefinition {
        public Production<TSymbol> Production { get; set; }
        public List<TSymbol> FirstTokens { get; set; }

        public SymbolDefinition(Production<TSymbol> production, List<TSymbol> firstTokens) {
            Production = production;
            FirstTokens = firstTokens;
        }
    }

    private readonly TSymbol _startingSymbol;
    private readonly Dictionary<TSymbol, SymbolDefinition> _productions;

    public Grammar(TSymbol startingSymbol) {
        _startingSymbol = startingSymbol;
        _productions = new();
    }

    //TODO: Add/specify precedence and associativity. 
    //TODO: Add combination?
    public void Add(TSymbol symbol, ParseExpression<TSymbol> expression) =>
        _productions.Add(symbol, new SymbolDefinition(new(symbol, expression), new List<TSymbol>()));

    public void DetermineFirsts() {
        throw new NotImplementedException();
    }

    internal IReadOnlySet<TSymbol> GetFirstsOf(TSymbol symbol) {
        throw new NotImplementedException();
    }

    public ParseTree<TSymbol> Parse(IReadOnlyList<Token<TSymbol>> source) {
        int tokenIndex = 0;
        ParseNode<TSymbol> node = RecursiveParse(ref tokenIndex, source, _startingSymbol);
        return new ParseTree<TSymbol>(node);
    }

    internal ParseNode<TSymbol> RecursiveParse(ref int index, IReadOnlyList<Token<TSymbol>> source, TSymbol currentSymbol) {
        throw new NotImplementedException();
    }

    IEnumerator<Production<TSymbol>> IEnumerable<Production<TSymbol>>.GetEnumerator() => 
        _productions
            .Select(k => k.Value)
            .Select(l => l.Production)
            .GetEnumerator();

    public IEnumerator GetEnumerator() =>
        _productions
            .Select(k => k.Value)
            .Select(l => l.Production)
            .GetEnumerator();

}
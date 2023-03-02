namespace ParseEngine.Syntax.Formatting;

public abstract class ParseFormat<TToken, TSymbol> where TToken : notnull where TSymbol : notnull {

}

public sealed class TokenFormat<TToken, TSymbol>: ParseFormat<TToken, TSymbol> where TToken : notnull where TSymbol : notnull {

    public TokenFormat(TToken token) {

    }

}

public sealed class SymbolFormat<TToken, TSymbol>: ParseFormat<TToken, TSymbol> where TToken : notnull where TSymbol : notnull {

    public SymbolFormat(TSymbol token) {

    }

}

using ParseEngine.Scanning;

namespace ParseEngine.Syntax; 

public sealed class Production<TToken, TSymbol> where TToken : notnull where TSymbol : notnull {

    private readonly Func<object[], object> _instansiateFunction;
    private readonly object[] _format; //TODO: Replace format with proper class.

    //objects of format are either TToken or TSymbol or other format 
    //TODO: add UnrecognizedFormatObject exception
    public Production(Func<object[], object> instFunc, params object[] format) {
        _instansiateFunction = instFunc;
        _format = format;
    }

    public object Parse(in Grammar<TToken, TSymbol> grammar, ref int index, IReadOnlyList<Token<TToken>> soruce) {
        
        throw new NotImplementedException();

    } 

}

using ParseEngine.Scanning;

namespace ParseEngine.Syntax; 

public sealed class Production<TToken> where TToken : notnull{

    private readonly Func<object[], object> _instansiateFunction;
    private readonly object[] _format;

    public Production(Func<object[], object> instFunc, params object[] format) {
        _instansiateFunction = instFunc;
        _format = format;
    }

    public object Parse(in Grammar<TToken> grammar, ref int index, IReadOnlyList<Token<TToken>> soruce) {
        
        throw new NotImplementedException();

    } 

}
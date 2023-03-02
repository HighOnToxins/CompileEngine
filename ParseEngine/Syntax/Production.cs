
using ParseEngine.Scanning;

namespace ParseEngine.Syntax; 

public sealed class Production<TToken> where TToken : notnull{

    public Production(/*TODO: Find a way to take a format as input.*/) {
    }

    public object Parse(in Grammar<TToken> grammar, ref int index, IReadOnlyList<Token<TToken>> soruce) {
        
        throw new NotImplementedException();

    } 

}
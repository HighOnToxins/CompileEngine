
namespace ParseEngine.Exceptions; 

internal sealed class TreeNotFoundException : ParseException{

    public TreeNotFoundException() : base("Production could not be expaneded popperly."){ }

}
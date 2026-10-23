namespace InventoryManagement.Api.ResponseHandlers;

public enum ErrorType
{
    NotFound = 14560,
    UniqueConstraint = 14896,

}




public record Error(string Code, string Description)
{
    public static Error NotFound => new Error(ErrorType.NotFound.ToString(), "Entity you want not found");
}






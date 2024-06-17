namespace WMS.Core.Domain.Shared.Errors;

public static class ErrorMessages
{
    public static class Product
    {
        public static string NotFound(string columnName, string value) =>
            $"The Product with column {columnName} and value = {value} was not found";
    }
}
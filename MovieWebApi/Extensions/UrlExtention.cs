public static class UrlExtensions
{
    public static string GenerateUrl<T>(this T entity, LinkGenerator linkGenerator, HttpContext httpContext, string routeName)
    {
        // Use reflection to get the Id property value from the entity
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null) return null;

        var idValue = idProperty.GetValue(entity);
        return linkGenerator.GetUriByName(httpContext, routeName, new { id = idValue });
    }
}

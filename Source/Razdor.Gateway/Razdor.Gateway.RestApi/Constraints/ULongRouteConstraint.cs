using System.Globalization;

namespace Razdor.RestApi.Constraints;

public class ULongRouteConstraint : IRouteConstraint
{
    public static string Name = "ulong";

    public bool Match(
        HttpContext? httpContext,
        IRouter? route,
        string routeKey,
        RouteValueDictionary values,
        RouteDirection routeDirection
    )
    {
        if (!values.TryGetValue(routeKey, out object? routeValue))
            return false;

        switch (routeValue)
        {
            case null:
                return false;
            case ulong:
                return true;
            default:
                string? valueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);
                return ulong.TryParse(valueString, out _);
        }
    }
}
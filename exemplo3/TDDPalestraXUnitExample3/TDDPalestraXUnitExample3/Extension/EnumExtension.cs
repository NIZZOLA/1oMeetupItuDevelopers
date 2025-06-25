using System.ComponentModel;
using System.Net;
using TDDPalestraXUnitExample3.ErrorHelpers;

namespace TDDPalestraXUnitExample3.Extension;

public static class EnumExtension
{
    public static string Description(this Enum value, params object[] parameters)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        var field = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes == null || attributes.Length == 0)
        {
            return value.ToString();
        }

        return parameters == null ? attributes[0].Description : string.Format(attributes[0].Description, parameters);
    }

    public static MetaError MetaErrorDescription(this Enum errorItem, HttpStatusCode statusCode, params string[] parameters)
    {
        string description = errorItem.Description(parameters);

        string message = string.Empty;

        if (parameters == null)
        {
            message = description;
        }
        else
        {
            message = string.Format(description, parameters);
        }

        return new MetaError(
            new Error(message, errorItem.GetHashCode().ToString("000")),
            statusCode
        );
    }
}

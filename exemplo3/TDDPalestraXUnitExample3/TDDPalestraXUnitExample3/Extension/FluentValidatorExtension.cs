using FluentValidation;
using FluentValidation.Results;
using System.Net;
using TDDPalestraXUnitExample3.ErrorHelpers;

namespace TDDPalestraXUnitExample3.Extension;

public static class FluentValidatorExtension
{
    public static IRuleBuilderOptions<T, TProperty> WithCustomError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Enum errorCode, params object[] parameters)
    {
        rule.WithErrorCode(errorCode.GetHashCode().ToString("000"));
        rule.WithMessage(errorCode.Description(parameters));
        return rule;
    }

    public static MetaError ToMetaError(this ValidationFailure error, HttpStatusCode protocolCode = HttpStatusCode.Conflict)
    {
        var err = new Error(error.ErrorMessage, error.ErrorCode);
        var meta = new MetaError(err, protocolCode);
        return meta;
    }
}

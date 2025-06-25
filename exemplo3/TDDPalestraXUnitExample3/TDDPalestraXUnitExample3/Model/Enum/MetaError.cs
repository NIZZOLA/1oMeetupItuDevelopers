using System.Net;
using TDDPalestraXUnitExample3.ErrorHelpers;

namespace TDDPalestraXUnitExample3.Model.Enum;

public class MetaError
{
    public MetaError(Error error, HttpStatusCode protocolCode)
    {
        Error = error;
        ProtocolCode = protocolCode;
    }
    public Error Error { get; set; }

    public HttpStatusCode ProtocolCode { get; set; }
}

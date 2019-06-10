using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TDDPalestraXUnitExample3.ErrorHelpers
{
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
}

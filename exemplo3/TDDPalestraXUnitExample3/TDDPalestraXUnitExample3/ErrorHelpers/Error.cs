namespace TDDPalestraXUnitExample3.ErrorHelpers;

public class Error
{
    public Error(string message, string code)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; set; }
    public string Message { get; set; }
}

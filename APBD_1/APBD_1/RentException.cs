namespace APBD_1;

public class RentException : Exception
{
    public string message { get; }

    public RentException(string _message)
    {
        this.message = _message;
    }
}
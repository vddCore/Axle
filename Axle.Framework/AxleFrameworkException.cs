namespace Axle.Framework;

public class AxleFrameworkException(string message, Exception? innerException)
    : Exception(message, innerException)
{
    public AxleFrameworkException(string message) 
        : this(message, null)
    {
    }
}
using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class CarServiceException : Xeption
    {
        public CarServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
   
}

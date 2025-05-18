// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Xeptions;

namespace Ikrini.Core.API.Models.Foundations.Cars.Exceptions
{
    public class CarServiceException : Xeption
    {
        public CarServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
   
}

// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class CarValidationException : Xeption
    {
        public CarValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}

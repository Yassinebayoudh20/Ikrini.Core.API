// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class InvalidCarException : Xeption
    {
        public InvalidCarException(string message)
            : base(message)
        { }
    }
}

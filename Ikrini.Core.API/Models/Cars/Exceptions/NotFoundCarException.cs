// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class NotFoundCarException : Xeption
    {
        public NotFoundCarException(string message)
            : base(message)
        { }
    }
}

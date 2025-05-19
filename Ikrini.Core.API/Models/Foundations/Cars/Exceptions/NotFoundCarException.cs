// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Xeptions;

namespace Ikrini.Core.API.Models.Foundations.Cars.Exceptions
{
    public class NotFoundCarException : Xeption
    {
        public NotFoundCarException(string message)
            : base(message)
        { }
    }
}

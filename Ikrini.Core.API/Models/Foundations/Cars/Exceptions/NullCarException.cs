// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class NullCarException : Xeption
    {
        public NullCarException(string message)
            : base(message)
        { }
    }
}

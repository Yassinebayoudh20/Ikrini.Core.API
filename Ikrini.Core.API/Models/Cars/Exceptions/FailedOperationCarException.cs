// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class FailedOperationCarException : Xeption
    {
        public FailedOperationCarException(string message, Exception innerException)
           : base(message, innerException)
        { }
    }
}

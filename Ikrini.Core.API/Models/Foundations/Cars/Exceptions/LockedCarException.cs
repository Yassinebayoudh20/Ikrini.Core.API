// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Ikrini.Core.API.Models.Foundations.Cars.Exceptions
{
    public class LockedCarException : Xeption
    {
        public LockedCarException(string message, Exception innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}

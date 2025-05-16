// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class AlreadyExistsCarException : Xeption
    {
        public AlreadyExistsCarException(string message, Exception innerException, IDictionary data)
           : base(message, innerException, data)
        { }
    }
}

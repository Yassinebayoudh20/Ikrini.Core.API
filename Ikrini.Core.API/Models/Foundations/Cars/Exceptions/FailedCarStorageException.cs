// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Ikrini.Core.API.Models.Foundations.Cars.Exceptions
{
    public class FailedCarStorageException : Xeption
    {
        public FailedCarStorageException(string message , Exception innerException)
            : base(message, innerException)
        { }
    }
}

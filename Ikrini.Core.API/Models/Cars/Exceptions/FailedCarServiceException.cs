using System;
using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class FailedCarServiceException : Xeption
    {
        public FailedCarServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

using System;
using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class FailedCarStorageException : Xeption
    {
        public FailedCarStorageException(string message , Exception innerException)
            : base(message, innerException)
        { }
    }
}

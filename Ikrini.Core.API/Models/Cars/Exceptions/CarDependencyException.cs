using System;
using Xeptions;

namespace Ikrini.Core.API.Models.Cars.Exceptions
{
    public class CarDependencyException : Xeption
    {
        public CarDependencyException(string message , Xeption innerException)
            : base(message, innerException)
        { }
    }
}

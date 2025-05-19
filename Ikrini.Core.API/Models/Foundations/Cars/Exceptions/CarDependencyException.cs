// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Ikrini.Core.API.Models.Foundations.Cars.Exceptions
{
    public class CarDependencyException : Xeption
    {
        public CarDependencyException(string message , Xeption innerException)
            : base(message, innerException)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curse.Exceptions
{
    public class CustomExceptionBase : Exception
    {
        public CustomExceptionBase()
        {

        }

        public CustomExceptionBase(String message) : base(message)
        {

        }

        public CustomExceptionBase(String message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
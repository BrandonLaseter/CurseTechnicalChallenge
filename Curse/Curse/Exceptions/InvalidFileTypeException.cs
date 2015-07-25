using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curse.Exceptions
{
    public class InvalidFileTypeException: CustomExceptionBase
    {
        public InvalidFileTypeException()
        {

        }

        public InvalidFileTypeException (String message) : base(message)
	    {

	    }

        public InvalidFileTypeException (String message, Exception innerException) : base (message, innerException)
	    {

	    }
    }
}
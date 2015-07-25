using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curse.Exceptions
{
    public class GeneralException :CustomExceptionBase
    {
            public GeneralException()
        {

        }

        public GeneralException (String message) : base(message)
	    {

	    }

        public GeneralException(String message, Exception innerException)
            : base(message, innerException)
	    {

	    }
    }
}
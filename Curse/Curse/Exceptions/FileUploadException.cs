using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curse.Exceptions
{
    public class FileUploadException : CustomExceptionBase
    {
        public FileUploadException()
        {

        }

        public FileUploadException(String message)
            : base(message)
        {

        }

        public FileUploadException(String message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
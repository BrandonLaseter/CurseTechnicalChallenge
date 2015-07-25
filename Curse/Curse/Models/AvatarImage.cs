using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Curse.Models
{
    public abstract class AvatarImage
    {
        private String _name;
        public String Name
        {
            get { return _name; }
        }

        private HttpServerUtilityBase _server;
        public HttpServerUtilityBase Server
        {
            get
            {
                return _server;
            }
        }

        public AvatarImage(HttpServerUtilityBase server, String name)
        {
            _name = name;
            _server = server;
        }

        public abstract String RelativePath {get;} //used for clients to embed in the IMG tag.
        public abstract String AbsolutePath {get;} //used for server side processing

        public bool Exists
        {
            get
            {
                bool exists = false;

                try
                {
                    exists = File.Exists(AbsolutePath);
                }
                catch (Exception e)
                {
                    //...
                }

                return exists;
            }
        }
    }
}
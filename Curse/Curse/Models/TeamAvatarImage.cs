using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curse.Models
{
    public class TeamAvatarImage : AvatarImage
    {
        public TeamAvatarImage(HttpServerUtilityBase server, String name) : base (server, name)
        {

        }

        public override String RelativePath
        {
            get
            {
                return String.Format("/Images/Avatars/Team/{0}", base.Name);
            }
        }

        public override String AbsolutePath
        {
            get
            {
                return base.Server.MapPath(String.Format("~/Images/Avatars/Team/{0}", base.Name));
            }
        }
    }
}
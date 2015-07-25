using Curse.Exceptions;
using Curse.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Curse.Helpers
{
    public static class AvatarUploader
    {
        /// <summary>
        /// This method uploads an avatar image
        /// </summary>
        /// <param name="server">HttpServerUtilityBase object. Used to map the server path</param>
        /// <param name="avatar">Contains the file bytes to save</param>
        /// <param name="id">The id to use for the image</param>
        /// <param name="avatarType">Currently canbe either Team or Player</param>
        /// <returns></returns>
        public static AvatarImage UploadFile2(HttpServerUtilityBase server, HttpPostedFileBase avatar, int id, AvatarTypeEnum avatarType)
        {
            try
            {
                String fileType = Path.GetExtension(avatar.FileName);
                String name = id + fileType;
                AvatarImage image = null;

                switch (avatar.ContentType)
                {
                    //Only accepct these content types
                    case "image/gif":
                    case "image/png":
                    case "image/jpg":
                    case "image/jpeg":
                        break;

                    //invalid file type
                    default:
                        throw new InvalidFileTypeException("Invalid File Type");
                }

                switch (avatarType)
                {
                    case AvatarTypeEnum.Team:
                        image = new TeamAvatarImage(server, name);
                        break;

                    case AvatarTypeEnum.Player:
                        image = new PlayerAvatarImage(server, name);
                        break;
                }

                try
                {
                    String absPath = image.AbsolutePath;
                    avatar.SaveAs(absPath);
                }
                catch (Exception e)
                {
                    //log
                    throw new FileUploadException("There was an error uploading the file");
                }

                return image;
            }
            catch (Exception e)
            {
                throw new GeneralException();
            }
        }

    }
}
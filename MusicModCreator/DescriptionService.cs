using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicModCreator
{
    public class DescriptionService
    {
        public static string CreateDescription(string modName, string modPath, string supportedVersion)
        {
            var description = new StringBuilder();

            description.Append(string.Format("name=\"{0}\"\r\n", modName));
            description.Append(string.Format("path=\"mod/{0}\"\r\n", modPath));
            description.Append("tags={\r\n\t\"Sound\"\r\n}\r\n");
            description.Append(string.Format("supported_version=\"{0}\"\r\n", supportedVersion));

            return description.ToString();
        }

        public static string CreateSongListing(string musicPath, string volume, bool additionalInfo)
        {
            var oggFiles = Directory.GetFiles(musicPath, "*.ogg").Select(path => Path.GetFileName(path)).ToArray();

            if (!oggFiles.Any())
            {
                throw new ArgumentException(string.Format("No ogg files found in {0}", musicPath));
            }

            var sb = new StringBuilder();

            foreach (var oggFile in oggFiles)
            {
                if (additionalInfo)
                {
                    sb.Append("music = {\r\n");
                    sb.Append(string.Format("\tname = \"{0}\"\r\n", oggFile.Replace(".ogg", "")));
                    sb.Append(string.Format("\tfile = \"{0}\"\r\n", oggFile));
                    sb.Append(string.Format("\tvolume = {0}\r\n", volume));
                }
                else
                {
                    sb.Append("song = {\r\n");
                    sb.Append(string.Format("\tname = \"{0}\"\r\n", oggFile.Replace(".ogg", "")));
                }

                sb.Append("}\r\n\r\n");
            }

            return sb.ToString();
        }

    }
}
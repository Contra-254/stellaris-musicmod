using System;
using System.IO;
using System.Linq;

namespace MusicModCreator
{
    public static class IoService
    {
        public static bool CreateModSubfolder(string path)
        {
            if (!CheckForExistingDirectory(path)) return false;

            try
            {
                Directory.CreateDirectory(path);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        private static bool CheckForExistingDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Console.WriteLine("Mod already exists, overwrite? [y/n]");
                if (Console.Read() != 'y' && Console.Read() != 'Y')
                {
                    return false;
                }
                
                ClearDirectory(path);
            }
            return true;
        }

        private static void ClearDirectory(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            try
            {
                foreach (var file in dirInfo.GetFiles("*.ogg"))
                {
                    file.Delete();
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void CopyMusicFiles(string musicPath, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            var oggFiles = Directory.GetFiles(musicPath, "*.ogg");
            if (!oggFiles.Any())
            {
                throw new ArgumentException(string.Format("No ogg files found in {0}", musicPath));
            }

            Console.WriteLine("Copying {0} files to {1}", oggFiles.Count(), targetPath);
            foreach (var oggFile in oggFiles)
            {
                if (oggFile != null)
                {
                    Console.WriteLine("Copying {0}...", Path.GetFileName(oggFile));
                    var targetFileName = Path.Combine(targetPath, Path.GetFileName(oggFile));
                    try
                    {
                        File.Copy(oggFile, targetFileName, true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: {0}", e.Message);
                    }
                    
                }
            }

        }
    }
}
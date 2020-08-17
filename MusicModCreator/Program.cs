using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace MusicModCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<Options>(args).MapResult((opts) => RunOptionsAndReturnExitCode(opts), //in case parser sucess
                errs => HandleParseError(errs)); //in  case parser fail;



        }

        static int RunOptionsAndReturnExitCode(Options o)
        {

            if ( string.IsNullOrWhiteSpace(o.ModName)
               || String.IsNullOrWhiteSpace(o.MusicPath) || !CheckModFolder(o))
            {
                return -2;
            }
            var targetMusicPath = Path.Combine(o.ModPath, o.ModName.Replace(" ", string.Empty).ToLower(), "music");
            if (!IoService.CreateModSubfolder(targetMusicPath))
            {
                Console.WriteLine("Error: Couldn't create Target Folder");
                return -2;
            }


            if (!CreateDescriptions(o, targetMusicPath))
            {
                Console.WriteLine("Error: Couldn't create all mod description files");
                return -2;
            }

            IoService.CopyMusicFiles(o.MusicPath, targetMusicPath);

            var exitCode = 0;
            return exitCode;
        }

        static int HandleParseError(IEnumerable<Error> errs)
        {
            var result = -2;
            Console.WriteLine("errors {0}", errs.Count());
            if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
                result = -1;
            Console.WriteLine("Exit code {0}", result);
            return result;
        }

        private static bool CreateDescriptions(Options result, string targetMusicPath)
        {
            var modName = result.ModName;
            var modDescription = DescriptionService.CreateDescription(modName, modName.Replace(" ", string.Empty).ToLower(), result.Version);

            try
            {
                var songListing = DescriptionService.CreateSongListing(result.MusicPath, result.Volume, false);
                var assetListing = DescriptionService.CreateSongListing(result.MusicPath, result.Volume, true);

                var descriptionPath = Path.Combine(result.ModPath, string.Format("{0}.mod", modName.Replace(" ", string.Empty).ToLower()));
                File.WriteAllText(descriptionPath, modDescription);

                var musicModPath = Path.Combine(result.ModPath, modName.Replace(" ", string.Empty).ToLower());

                var descriptorPath = Path.Combine(musicModPath, "descriptor.mod");
                Console.WriteLine("Creating file {0}", descriptorPath);
                File.WriteAllText(descriptorPath, modDescription);

                var songsTxt = Path.Combine(targetMusicPath, string.Format("{0}.txt", modName.Replace(" ", string.Empty).ToLower()));
                Console.WriteLine("Creating file {0}", songsTxt);
                File.WriteAllText(songsTxt, songListing);

                var songsAsset = Path.Combine(targetMusicPath, string.Format("{0}.asset", modName.Replace(" ", string.Empty).ToLower()));
                Console.WriteLine("Creating file {0}\r\n", songsAsset);
                File.WriteAllText(songsAsset, assetListing);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }



        private static bool CheckModFolder(Options result)
        {
            if (string.IsNullOrWhiteSpace(result.ModPath))
            {
                result.ModPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Paradox Interactive", "Stellaris", "mod");
            }

            if (!Directory.Exists(result.ModPath))
            {
                Console.WriteLine(string.Format("Mod-Folder does not exist: {0}", result.ModPath));
                return false;
            }

            return true;
        }
    }
}

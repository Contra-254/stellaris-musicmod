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
            var result = Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.Any() || string.IsNullOrWhiteSpace(result.Value.ModName) 
                || String.IsNullOrWhiteSpace(result.Value.MusicPath) || !CheckModFolder(result))
            {
                return;
            }


            var targetMusicPath = Path.Combine(result.Value.ModPath, result.Value.ModName.Replace(" ", string.Empty).ToLower(), "music");
            if (!IoService.CreateModSubfolder(targetMusicPath))
            {
                return;
            }


            if (!CreateDescriptions(result, targetMusicPath))
            {
                Console.WriteLine("Error: Couldn't create all mod description files");
                return;
            }

            IoService.CopyMusicFiles(result.Value.MusicPath, targetMusicPath);
        }

        private static bool CreateDescriptions(ParserResult<Options> result, string targetMusicPath)
        {
            var modName = result.Value.ModName;
            var modDescription = DescriptionService.CreateDescription(modName, modName.Replace(" ", string.Empty).ToLower(), result.Value.Version);

            try
            {
                var songListing = DescriptionService.CreateSongListing(result.Value.MusicPath, result.Value.Volume, false);
                var assetListing = DescriptionService.CreateSongListing(result.Value.MusicPath, result.Value.Volume, true);

                var descriptionPath = Path.Combine(result.Value.ModPath, string.Format("{0}.mod", modName.Replace(" ", string.Empty).ToLower()));
                File.WriteAllText(descriptionPath, modDescription);

                var musicModPath = Path.Combine(result.Value.ModPath, modName.Replace(" ", string.Empty).ToLower());
                
                var descriptorPath = Path.Combine(musicModPath, "descriptor.mod");
                Console.WriteLine("Creating file {0}", descriptorPath);
                File.WriteAllText(descriptorPath, modDescription);

                var songsTxt = Path.Combine(targetMusicPath, "songs.txt");
                Console.WriteLine("Creating file {0}", songsTxt);
                File.WriteAllText(songsTxt, songListing);

                var songsAsset = Path.Combine(targetMusicPath, "songs.asset");
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



        private static bool CheckModFolder(ParserResult<Options> result)
        {
            if (string.IsNullOrWhiteSpace(result.Value.ModPath))
            {
                result.Value.ModPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Paradox Interactive", "Stellaris", "mod");
            }

            if (!Directory.Exists(result.Value.ModPath))
            {
                Console.WriteLine(string.Format("Mod-Folder does not exist: {0}", result.Value.ModPath));
                return false;
            }

            return true;
        }
    }
}

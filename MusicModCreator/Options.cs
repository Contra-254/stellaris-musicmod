using System;
using System.IO;
using CommandLine;
using CommandLine.Text;

namespace MusicModCreator
{
    public class Options
    {
        [Option('m', "musicpath", Required = true, HelpText = "Path containing music (.ogg) files")]
        public string MusicPath { get; set; }
        
        [Option('n', "modname", Required = true, HelpText = "Name of the mod. Can contain whitespaces")]
        public string ModName { get; set; }

        [Option("version", HelpText = "Mod will support this version of Stellaris", DefaultValue = "1.1.0")]
        public string Version { get; set; }

        [Option("volume", HelpText = "Volume of every song", DefaultValue = "0.62")]
        public string Volume { get; set; }

        [Option("modpath", HelpText = "Location where the mod will be created")]
        public string ModPath { get; set; }
    }
}
# Stellaris Music-Mod Creator

## What is this?
It is a Commandline tool to easily create a music mod with your own music for the game Stellaris. 

The music-mod creator only supports .ogg-files and cannot convert other file formats to .ogg. 

## Usage

  -m, --musicpath    Required. Path containing music (.ogg) files

  -n, --modname      Required. Name of the mod. Can contain whitespaces

  --version          (Default: 1.0.3) Mod will support this version of
                     Stellaris

  --volume           (Default: 0.62) Volume of every song

  --modpath          Location where the mod will be created

Example: MusicModCreator.exe -m "C:\MyMusic" -n "CoolMusicMod"

The Mod will be created in 'modpath'. This defaults to the Stellaris mod folder (e.g. C:\Users\<Username>\Documents\Paradox Interactive\Stellaris\mod)


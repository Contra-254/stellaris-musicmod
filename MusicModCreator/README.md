# Stellaris Music-Mod Creator

## What is the Stellaris Music-Mod Creator?
It is a Commandline tool that helps to create a music mod with music of your choice for the game Stellaris by [Paradox Interactive](https://www.paradoxplaza.com/stellaris). 



## Usage

  -m, --musicpath    Required. Path containing music (.ogg) files

  -n, --modname      Required. Name of the mod. Can contain whitespaces

  --version          (Default: 1.0.3) Mod will support this version of
                     Stellaris

  --volume           (Default: 0.62) Volume of every song

  --modpath          Location where the mod will be created

Example: MusicModCreator.exe -m "C:\MyMusic" -n "Cool Music Mod"

+ The Mod will be created in 'modpath'. The default path for this is the Stellaris mod folder (e.g. C:\Users\<Username>\Documents\Paradox Interactive\Stellaris\mod)
+ For the tool to work there needs to be a folder present with all of the .ogg-files you want to add to the mod. 
+ The music-mod creator only supports .ogg-files and cannot convert other file formats to .ogg. 




# Pokemon-Cards
A simple project made in unity that renders the pokemon data fetched from [Pokemon-API](https://pokeapi.co/).
## Prerequisite
The project requires unity version `2021.3.0f1` with `Standalone` build support for `Windows, Mac and Linux`. The project itself was created on windows.
## Input
The project expects an input text file named `pokemon.txt` inside `StreamingAssets` folder. This is how the content of the file looks like...
```
charizard
blastoise
venusaur
pidgeot
butterfree
beedrill
raticate
charmeleon
wartortle
ivysaur
```
Each pokemon name is expected to be in a new line. On starting game it will read and sanitize the input file and display pokemon cards. In case file is not present game will not load anything and display an appropriate message.
## Creating build and running inside unity
To run the project load the scene named `Main` inside unity located in `Levels/Scenes` folder. Make sure the resolution is `1920x1080` both inside the editor and during build creation for optimal view. Make sure to select standalone build for your target platform and hit build.
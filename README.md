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
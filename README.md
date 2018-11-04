# fluffy-adventure

Ceci est un projet personnel pour pratiquer un peu de développement.
L'objectif est de réaliser un jeu multijoueur sur navigateur. 
Le jeu utilise une grille avec une île généré aléatoirement.

## FluffyClient
L'interface web est géré par un serveur Node.js.
La page web utilise une websocket pour communiquer avec le serveur de jeu.

## FluffyServer

Un serveur en C# qui s'occupe de la logique du jeu.
Il utilise des websockets pour une communication bidirectionnelle avec un navigateur.

### Pourquoi fluffy-adventure ?

Github qui propose un nom quand j'ai pas d'idée.

# fluffy-adventure

Ceci est un projet personnel pour pratiquer un peu de développement.
L'objectif est de réaliser un jeu multijoueur sur navigateur. 
Le jeu utilise une grille avec une île générée aléatoirement.

## Le Jeu

Le joueur se trouve sur une île déserte.

Il peut se déplacer d'une case en suivant l'une des quatre directions : nord, est, sud, ouest.

L'île est découpée en un quadrillage. Chaque case est dominé d'un terrain qui détermine la difficulté de franchissement. Plus le terrain est difficile, plus il faut d'énergie pour s'y rendre. Une case possède des ressources limitées et dépendant de son terrains. Le joueur peut récolter des ressources moyennant une somme d'énergie selon la ressource.

![alt text](https://github.com/Monzein/fluffy-adventure/blob/master/Presentation/FindResources.PNG)

Le joueur aperçois sa case et ses alentours. Il peut déplier une carte qui permet de voir toutes les cases découvertes.

![alt text](https://github.com/Monzein/fluffy-adventure/blob/master/Presentation/MapExploration.PNG)

L'inventaire permet d'utiliser des objets et s'équiper. L'inventaire et limité en masse et espace.

![alt text](https://github.com/Monzein/fluffy-adventure/blob/master/Presentation/Inventory.PNG)

Il est possible de créer des objets à partir de ressources disponible dans l'inventaire.

![alt text](https://github.com/Monzein/fluffy-adventure/blob/master/Presentation/Crafting.PNG)

Un joueur peut se retrouver sur la même case qu'un autre personnage. Il peut alors déclencher un combat. Une fenêtre s'ouvre pour effectuer des actions de combats. Le combat se termine par la mort ou la fuite d'un des personnages.
Si le combat est lancé contre un autre joueur, la fenêtre s'ouvre pour le second joueur. Le combat prend fin pour les mêmes conditions.

![alt text](https://github.com/Monzein/fluffy-adventure/blob/master/Presentation/Battle.PNG)

La carte est générée aléatoirement à partir d'une graine. La même graine donne exactement la même carte. La carte est générée selon le principe de génération par îlots. Voici quelques exemples de générations automatiques.

![alt text](https://github.com/Monzein/fluffy-adventure/blob/master/Presentation/GeneratedMap_1.PNG) ![alt text](https://github.com/Monzein/fluffy-adventure/blob/master/Presentation/GeneratedMap_2.PNG) ![alt text](https://github.com/Monzein/fluffy-adventure/blob/master/Presentation/GeneratedMap_3.PNG)

## FluffyClient

L'interface web est géré par un serveur Node.js.
La page web utilise une websocket pour communiquer avec le serveur de jeu.

### Installation (Windows 10)

* Installer Node.js et npm
* Lancer une console et se rendre dans le répertoire *fluffy-adventure\FluffyClient*
* Lancer la commande *npm install*
* Lancer la commande *node server*
* Accepter la fenêtre de sécurité du pare-feu Windows
* Éteindre le serveur CTRL + C

## FluffyServer

Un serveur en C# qui s'occupe de la logique du jeu.
Il utilise des websockets pour une communication bidirectionnelle avec un navigateur.

### Installation (Windows 10)

* Installer Visual Studio avec les outils de développements .Net
* Lancer Visual Studio, ouvrir un projet et choisir le fichier *fluffy-adventure\FluffyServ\FluffyServ\FluffyServ.csproj*
* Enregsitrer le projet dans une solution au niveau du dossier *fluffy-adventure\FluffyServ*
* Fermer Visual Studio, relancer le et ouvrir la solution précédement enregistrée
* Sélectionner en projet de démarrage *FluffyServ*
* Gérer les packages NuGet et vérifier qu'ils sont tous mis à jour
* Lancer le projet
* Accepter la fenêtre de sécurité du pare-feu Windows
* Éteindre le serveur en fermant la console ou en appuyant sur une touche.

## Lancer le jeu (Windows 10)

* Installer les deux composants
* Lancer le serveur. Le build Debug se trouve *fluffy-adventure\FluffyServ\FluffyServ\bin\Debug\FluffyServ.exe*
* Lancer le serveur client. Lancer une console et se rendre dans le répertoire *fluffy-adventure\FluffyClient* puis lancer la commande *node server*
* Ouvrir un navigateur. *ex: Firefox 71.0*
* Taper dans la barre de recherche *localhost:3000*
* Cliquer sur le bouton connecter

Il est possible de se connecter depuis une autre machine du même réseau. Il faut utiliser l'adresse ip de la machine suivi de son port.

* Lancer une console sur la machine serveur
* Taper *ipconfig*
* Chercher l'adresse IPv4
* Sur la machine cliente, lancer un navigateur et taper l'adresse IPV4 de la machine serveur suivi du port. *ex : 192.168.1.99:3000*

Pour se connecter depuis une machine hors du réseau, il faut configurer le routeur pour rediriger vers la machine serveur.

## Pourquoi fluffy-adventure ?

Github m'a proposé un nom alors que je n'avais pas d'idée.

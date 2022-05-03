# euhMDR

Projet de Julien CROYNAR, Victoria BEKKER et Gaspard ESSEVAZ-ROULET


Le jeu est sous forme d'executable dans le dossier "JeuDemo" vous pouvez le lancer de cet endroit normalement.

Sinon le reste du code intéressant est dans le dossier "Assets", il se décompose comme suit : 

	- Dans "Decors", il y a juste des assets pour les arbres
	- "Enemis" malgré le nom, contien le code du personnage principal, avec ses textures notamment et dans "Scripts", les scripts bien sûr
	- "Material" contien quelques textures 
	- "Personnage" est la même chose que "Enemis" mais pour les ennemis pour le coup...
	- Le reste se sont des fichier des assets mais pas du code écrit par nous
	
Dans "Enemis/Scripts" on retrouve ce qui permet de déplacer les bras, gérer la caméra, les contrôles, le déplacement et bien sûr, l'animation procédurale.
Dans "Personnage/Scripts" on retrouve ce qui permet aux NPC de tirer, se déplacer, être animés, et mourir notamment.

Commandes de jeu : 

	- Déplacement -> WASD ou flèches directionnelles 
	- Saut -> Barre espace
	- Caméra -> Souris 
	- Tirs simples -> I
	- Tirs destructeurs de décor -> O
	- Super tir -> P
	
Pour quitter le jeu, il faut Alt+F4
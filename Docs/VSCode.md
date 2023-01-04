Installation du projet avec Visual Studio Code :
=============

## Prérequis :
- Node JS (LTS).
- SDK .NET 6.
- Visual Studio Code.

## Installation des prérequis :

### <ins>Installation de Node JS :</ins>
- Via le site officiel : https://nodejs.org/en/ 
    #### Ou
- En passant par [NVM](https://github.com/nvm-sh/nvm) (Mac et Linux uniquement)

<span style="color:red"> Attention à bien installer la dernière version LTS de Node.js </span>

### <ins>Installation du SDK .NET 6</ins>

- Windows et Mac : Sélectionner l'option adaptée au système et à l'architecture du processeur.
<img src="https://raw.githubusercontent.com/huhulacolle/Badgage/doc/installation/Docs/img/dotnet%20download.png">
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

- (Linux) Ubuntu ou distribution basée sur Ubuntu : ```sudo apt-get update && \ sudo apt-get install -y dotnet-sdk-6.0```

- (Linux) Autre distribution <span style="color:red">Attention à bien installer la version 6 et non une version inférieure ou supérieure </span> : https://learn.microsoft.com/fr-fr/dotnet/core/install/linux

### <ins>Installation de Visual Studio Code :</ins>
- https://code.visualstudio.com/
<br>

## Lancement du projet :
- Récupérez le code en clonant le projet ou en téléchargeant le zip. <br>

- Installer l'extension C# dans Visual Studio Code.
<img src="https://raw.githubusercontent.com/huhulacolle/Badgage/doc/installation/Docs/img/extension%20c%23.png">

- Ouvrir le dossier Badgage avec Visual Studio Code -> aller dans le menu "Executer et debugger" -> cliquer sur "create a launch.json file" et sélectionner ".NET 5+ and .Net Core".
<img src="https://raw.githubusercontent.com/huhulacolle/Badgage/doc/installation/Docs/img/create%20launch.json.png">

    <span style="color:red">Attention</span>
- Enlever le texte pour désactiver le lancement automatique du navigateur au lancement de l'api + front.
<img src="https://raw.githubusercontent.com/huhulacolle/Badgage/doc/installation/Docs/img/ne%20pas%20ouvrir%20le%20navigateur.png">

<br><br>
- Executer l'api (+ front) en cliquant sur le bouton "run and debug".
<img src="https://raw.githubusercontent.com/huhulacolle/Badgage/doc/installation/Docs/img/run.png">


- URL Local : https://localhost:7106/
- URL Swagger : https://localhost:7106/swagger
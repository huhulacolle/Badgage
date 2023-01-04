Installation du projet via CLI :
=============

## Prérequis :
- Node JS (LTS).
- SDK .NET 6.

## Installation des prérequis :

### <ins>Installation de Node JS :</ins>
- Via le site officiel : https://nodejs.org/en/ <br>
Ou
- En passant par [NVM](https://github.com/nvm-sh/nvm) (Mac et Linux uniquement).

<span style="color:red"> Attention à bien installer la dernière version LTS de Node.js </span>

### <ins>Installation du SDK .NET 6</ins>

- Windows et Mac : Sélectionner l'option adaptée au système et à l'architecture du processeur.
<img src="https://raw.githubusercontent.com/huhulacolle/Badgage/Master/Docs/img/dotnet%20download.png">
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

- (Linux) Ubuntu ou distribution basée sur Ubuntu : ```sudo apt-get update && \ sudo apt-get install -y dotnet-sdk-6.0```

- (Linux) Autre distribution <span style="color:red">Attention à bien installer la version 6 et non une version inférieure ou supérieure </span> : https://learn.microsoft.com/fr-fr/dotnet/core/install/linux


## Lancement du projet :
- Récupérez le code en clonant le projet ou en téléchargeant le zip. <br>

- Via le terminal naviguer dans le dossier ```Badgage```.

- Executer l'api en mode debug : ```dotnet watch run```.


- URL Local : https://localhost:7106/ .
- URL Swagger : https://localhost:7106/swagger .
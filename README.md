# Test technique Stereograph

## Suivi du développement

### `0ba3102` et `2821fe4`

> Initialisation du repo et de la solution sur Visual Studio 2022

### `04bf25a`

> Ajout de l’utilisation de la `ConnectionString` dans `appsettings.json`.

### `6b7d213`

> J’ai créé un modèle `Person` qui suit la «structure» du CSV (avec un ID en plus), l’ai ajouté dans l’`ApplicationDBContext`, puis exécuté les commandes suivantes pour mettre notre modèle de données en place en base.
>
> - `dotnet ef migrations add InitialCreate`
>
>   Pour générer les migrations initiales à partir du modèle de données actuel.
>
> - `dotnet ef database update`
>
>   Pour lancer la migration et appliquer ses modifications (et donc créer la table `Person`).

### `8e8017f`

> J’ajoute ce fichier que je tiendrai à jour au cours de mon exercice.
>
> Ensuite, je me rends compte que j’ai appelé la table `People` plutôt que `Persons` comme l’indiquait le squelette de `PersonController`… Je prends donc la liberté de renommer la route en `api/people`.
>
> Je me lance donc dans la création de routes classiques :
>
> - un GET pour tout le monde
> - un GET pour une personne avec son ID
> - un POST pour créer une personne
>
> Elles ont l’air de bien fonctionner quand testées sur le Swagger à la main.

### `d57937f`

> J’ajoute les routes suivantes au `PersonController` :
>
> - un PUT pour mettre à jour les infos d’une personne
> - un DELETE pour supprimer une personne
>
> J’ai aussi réparé la méthode POST (de création) car l’appel `CreatedAtAction` avait un paramètre invalide, ce qui faisait planter la méthode après la création de l’objet, nous empêchant d’avoir un retour propre.

### `d5aa6fe`

> J’ai rajouté une classe de mapping `PersonMap` dans mon modèle `Person` pour proprement récupérer et stocker les infos du fichier CSV avec CsvHelper.
>
> Ensuite, j’ai rédigé la méthode POST `ImportCSV` qui prend en paramètre un fichier de données CSV. Je l’utilise pour lire et stocker en base les données du fichier importé.
>
> J’ai mis quelques sécurités classiques comme la gestion du cas où des données manquent dans les headers du CSV (si une colonne est manquante ou mal nommée).

### `HEAD`

> J’ai constitué quelques fichiers CSV de test de situations problématiques ou non.
>
> Ensuite, j’ai modifié ma méthode `ImportCSV` pour éviter d’ajouter des personnes déjà existantes dans la base en utilisant leur adresse e-mail comme attribut unique. Je pense qu’en utilisant quelque-chose comme un `HashSet<Person>`, il aurait été possible de s’éviter du LINQ qui pourrait prendre un peu de temps sur un éventuel import massif.
>
> Il pourrait aussi être possible, si besoin, de mettre à jour les personnes déjà existantes plutôt que de simplement les ignorer.

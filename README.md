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

### ` HEAD`

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

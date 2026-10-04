# Portfolio – Bertrand Beaujard

Hello visiteur, ici, tu trouveras le code de mon portfolio + le hosting sur github directement.


🔗 **https://bertrandbj.github.io**

## Stack

- .NET 8, Blazor WebAssembly
- CSS sans framework
- GitHub Actions pour la compilation et le déploiement


## Architecture

```
Portfolio/
├── Models/        Modèles de données 
├── Services/      Accès aux données derrière une interface 
├── Extensions/     Méthodes d'extension 
├── Components/    Composants de présentation (modal par exemple).
├── Pages/         Page d'accueil, en code-behind
└── wwwroot/data/  Contenu des projets en JSON
```

- **Séparation des responsabilités** : les données vivent dans `projects.json`, le service les charge, et les composants ne font que les afficher. Ajouter un projet revient à ajouter une entrée au JSON.
- **Injection de dépendances** : la page dépend de `IProjectService`, ce qui permettrait de remplacer le JSON par une API sans toucher aux composants.
- **Modale pilotée par l'URL** : chaque projet a sa propre adresse (`/projets/{slug}`), partageable, compatible avec le bouton Retour.


## Lancer en local

```bash
dotnet watch --project Portfolio
```

## Déploiement

Chaque merge sur `main` déclenche une GitHub Action qui publie le projet en Release et le déploie sur GitHub Pages.
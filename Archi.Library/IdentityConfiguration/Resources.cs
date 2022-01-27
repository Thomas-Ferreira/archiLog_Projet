using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archi.Library.IdentityConfiguration
{
    // enregistrer les ressources pour IdentityServer4
    // sont des étendues de connexion d'ID ouvertes standard, propres à un utilisateur particulier,
    // pour qu'Identity Server prenne en charge.
    // j'ai ajouté des champs d'application standard comme OpenId,
    // profile & Email ainsi qu'un rôle de champ d'application personnalisé qui contient et renvoie les revendications
    // de rôle pour l'utilisateur authentifié.

    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResource
        {
            Name = "role",
            UserClaims = new List<string> {"role"}
        }
    };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {

        //Les ressources API sont utilisées pour définir l'API que le serveur d'identité protège,
        //l'API est sécurisée à l'aide d'un serveur d'identité
        //C'est l'API Météo qui est protégée à l'aide du serveur d'identité.

        new ApiResource
        {
            Name = "weatherApi",
            DisplayName = "Weather Api",
            Description = "Allow the application to access Weather Api on your behalf",
            Scopes = new List<string> { "weatherApi.read", "weatherApi.write"},
            ApiSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
            UserClaims = new List<string> {"role"}
        }
    };
        }
    }
    
}

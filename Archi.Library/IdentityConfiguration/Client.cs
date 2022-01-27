using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace Archi.Library.Core
{ 
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
    {
        // OAuth
        // un identifiant et un secret client pour autoriser l'accès.
        // client est uniquement autorisé à demander l'autorisation d'accès en lecture au serveur d'identité en spécifiant l'ID et le secret du client.
        new Client
        {
            ClientId = "weatherApi",
            ClientName = "ASP.NET Core Weather Api",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
            AllowedScopes = new List<string> {"weatherApi.read"}
        },

        // OpenID Connect qui utilise le flux de code d'autorisation avec clé de preuve pour l'échange de code (PKCE).
        // nécessite un identifiant et un secret client pour autoriser l'accès.
        //  Il dispose également d'une URL de redirection (Application Client URL) pour envoyer les résultats de
        //  l'authentification du serveur d'identité à l'application cliente. 
        // L'identifiant et le secret du client seront utilisés pour autoriser l'accès.
        // PKCE est utilisé pour garantir que l'application cliente demandant des jetons en échange de code est la même
        // application qui avait initialement demandé ce code.

        new Client
        {
            ClientId = "oidcMVCApp",
            ClientName = "Sample ASP.NET Core MVC Web App",
            ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},

            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = new List<string> {"https://localhost:44346/signin-oidc"},
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "role",
                "weatherApi.read"
            },
            RequirePkce = true,
            AllowPlainTextPkce = false
        }
    };
        }
    }
    
}

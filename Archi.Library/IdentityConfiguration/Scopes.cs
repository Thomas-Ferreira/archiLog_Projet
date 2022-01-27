using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archi.Library.IdentityConfiguration
{
    
    public class Scopes
    {
        // Les étendues d' API sont utilisées pour spécifier les actions que l'utilisateur autorisé peut effectuer au niveau de l'API. 
        // Une API CRUD avec différentes étendues comme la lecture, l'écriture et la création et les étendues d'API peuvent être utilisées
        // pour contrôler quelles étendues sur une API sont autorisées pour l'utilisateur autorisé

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
        new ApiScope("weatherApi.read", "Read Access to Weather API"),
        new ApiScope("weatherApi.write", "Write Access to Weather API"),
    };
        }
    }
    
}

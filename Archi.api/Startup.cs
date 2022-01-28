using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Archi.api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Archi.Library.Token;

namespace Archi.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    // Validez le serveur qui génère le token
                    ValidateIssuer = false,
                    // Valider pour le destinataire du token sois autorisé à recevoir le token
                    ValidateAudience = false,
                    // Expiration du token
                    ValidateLifetime = true,
                    // Spécifications des valeurs pour l'émetteur
                    ValidIssuer = "https://localhost:44306",
                    // Spécifications des valeurs pour l'audience
                    ValidAudience = "https://localhost:44306",
                    // Spécifications des valeurs pourclee de connexion
                    // La clee de connexion est définis depuis la classe TokenController
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtToken.SECRET_KEY))

                };
            });
            // services 
            services.AddDbContext<ArchiDbContext>(options =>
            { // clee de connexion
                options.UseSqlServer(Configuration.GetConnectionString("Archi"));
            });
            //versionning
            services.AddMvc();
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Authentification + Authorisation 
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    
    }
}

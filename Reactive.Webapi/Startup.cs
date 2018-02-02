using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reactive.DAL.CosmosDb;
using Reactive.DAL.Interfaces;
using Reactive.Models.DbModels;
using Reactive.UserIdentity;

namespace Reactive.Webapi
{
    public class Startup
    {
        private const string _endpointUri = "https://localhost:8081";
        private const string _primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private const string USERS_IDENTUTY_DB_NAME = "UserIdentity";
        private const string USERS_IDENTITY_COLLECTION_NAME = "users";

        private const string REACTIVE_DB_NAME = "Reactive";
        private const string RECIPES_COLLECTION_NAME = "recipes";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CosmosDbClient cosmosDbClient = new CosmosDbClient(_endpointUri, _primaryKey);
            cosmosDbClient.CreateDatabase(USERS_IDENTUTY_DB_NAME);
            cosmosDbClient.CreateCollection(USERS_IDENTUTY_DB_NAME, USERS_IDENTITY_COLLECTION_NAME);

            cosmosDbClient.CreateDatabase(REACTIVE_DB_NAME);
            cosmosDbClient.CreateCollection(REACTIVE_DB_NAME, RECIPES_COLLECTION_NAME);

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 2;
                options.User.RequireUniqueEmail = true;
            });

            // Add identity types
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();

            // services.AddScoped<RoleManager<IdentityRole>>();

            services.AddTransient<IUserQueries<ApplicationUser>>(provider => {
                return new UserQueries(cosmosDbClient.get(), USERS_IDENTUTY_DB_NAME, USERS_IDENTITY_COLLECTION_NAME);
            });

            services.AddTransient<IRecipeQueries>(provider => {
                return new RecipeQueries(cosmosDbClient.get(), REACTIVE_DB_NAME, RECIPES_COLLECTION_NAME);
            });

            // configure jwt authentication
            //var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes("123456789123456789123456789");// (appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                //x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "Reactive",
                    ValidAudience = "Reactive",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = false
                    //ValidateIssuer = false,
                    //ValidateAudience = false
                };
            });
            services.AddAuthorization();

            // Identity Services
            //services.AddSingleton<>();
            //services.AddSingleton<IUserStore<ApplicationUser>>(provider => {
            //    return new UserStore(provider.GetService<UserQueries<ApplicationUser>>());
            //});

            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add application services.
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

using System.Threading.Tasks;
using Azure.Cosmos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nora.Server.Auth;

namespace Nora.Server
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
      _webHostEnvironment = webHostEnvironment;
      _configuration = configuration;
    }

    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo {Title = "Nora.Server", Version = "v1"});
        c.EnableAnnotations();
      });

      services
        .AddIdentityServer()
        .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
        .AddInMemoryApiResources(IdentityServerConfig.Apis)
        .AddInMemoryClients(IdentityServerConfig.Clients)
        .AddInMemoryApiScopes(IdentityServerConfig.Scopes);


      services
        .AddAuthentication()
        .AddIdentityServerJwt()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
          options.Audience = "nora-api";
          // TODO: Audience for URL.
          options.Authority = "";
          options.TokenValidationParameters.ValidTypes = new []
          {
            ""
          };
        });

      services.AddSingleton<CosmosClient>(InitCosmosDbAsync().GetAwaiter().GetResult());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      if (_webHostEnvironment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nora.Server v1");
        });
      }

      app.UseHttpsRedirection();
      app.UseIdentityServer();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private async Task<CosmosClient> InitCosmosDbAsync()
    {
      var cosmosClient = new CosmosClient(_configuration["CosmosDbEndpointUrl"], _configuration["CosmosDbAuthorizationKey"]);

      var databaseId = $"{_webHostEnvironment.EnvironmentName}-NoraDb";
      var userContainerId = $"{_webHostEnvironment.EnvironmentName}-UserContainer";
      var infantContainerId = $"{_webHostEnvironment.EnvironmentName}-InfantContainer";
      var journalEntryContainerId = $"{_webHostEnvironment.EnvironmentName}-JournalEntryContainer";

      var databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
      await databaseResponse.Database.CreateContainerIfNotExistsAsync(userContainerId, "/Email");
      await databaseResponse.Database.CreateContainerIfNotExistsAsync(infantContainerId, "/id");
      await databaseResponse.Database.CreateContainerIfNotExistsAsync(journalEntryContainerId, "/InfantId");

      return cosmosClient;
    }
  }
}

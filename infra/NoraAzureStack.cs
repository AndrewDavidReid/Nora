using Pulumi;
using Pulumi.Azure.AppService;
using Pulumi.Azure.AppService.Inputs;
using Pulumi.Azure.Core;
using Pulumi.Azure.CosmosDB;

class NoraAzureStack : Stack
{
  public NoraAzureStack()
  {
    var resourceGroup = new ResourceGroup("nora");

    // Import existing app service plan.
    var appServicePlan = new Plan("home-projects-asp", new PlanArgs
    {
      Name = "home-projects-asp",
      ResourceGroupName = "Shared",
      Kind = "Linux",
      Sku = new PlanSkuArgs
      {
        Tier = "Basic",
        Size = "B1"
      },
      Reserved = true
    }, new CustomResourceOptions
    {
      ImportId = "/subscriptions/72fc2865-939d-4afc-80d4-ab60f14c099e/resourceGroups/Shared/providers/Microsoft.Web/serverfarms/home-projects-asp",
      Protect = true
    });

    // Import existing cosmosdb account.
    var cosmosDbAccount = new Account("home-projects-cosmosdb", new AccountArgs
    {
      Name = "home-projects-cosmosdb",
      ResourceGroupName = "Shared",
      EnableFreeTier = true,
      GeoLocations =
    }, new CustomResourceOptions
    {
      ImportId = "/subscriptions/72fc2865-939d-4afc-80d4-ab60f14c099e/resourceGroups/Shared/providers/Microsoft.DocumentDB/databaseAccounts/home-projects-cosmosdb",
      Protect = true
    });

    var webApp = new AppService("nora-web", new AppServiceArgs
    {
      ResourceGroupName = resourceGroup.Name,
      HttpsOnly = true,
      AppServicePlanId = appServicePlan.Id
    });

    // var hostNameBinding = new CustomHostnameBinding("nora-web-hostname", new CustomHostnameBindingArgs
    // {
    //   Hostname = "nora.momo-adew.com",
    //   AppServiceName = webApp.Name,
    //   ResourceGroupName = resourceGroup.Name
    // });

    // var sslCert = new ManagedCertificate("nora-web-cert", new ManagedCertificateArgs
    // {
    //   CustomHostnameBindingId = hostNameBinding.Id
    // });
  }

  [Output]
  public Output<string> ConnectionString { get; set; }
}

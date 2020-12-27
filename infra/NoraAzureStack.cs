using Pulumi;
using Pulumi.Azure.AppService;
using Pulumi.Azure.Core;
using Pulumi.Azure.CosmosDB;

class NoraAzureStack : Stack
{
  public NoraAzureStack()
  {
    var resourceGroup = new ResourceGroup("nora");

    // Reference existing app service plan.
    var appServicePlan = Plan.Get("home-projects-asp",
      "/subscriptions/72fc2865-939d-4afc-80d4-ab60f14c099e/resourceGroups/Shared/providers/Microsoft.Web/serverfarms/home-projects-asp");

    // Reference existing cosmosdb account.
    var cosmosDbAccount = Account.Get("home-projects-cosmosdb",
      "/subscriptions/72fc2865-939d-4afc-80d4-ab60f14c099e/resourceGroups/Shared/providers/Microsoft.DocumentDB/databaseAccounts/home-projects-cosmosdb");

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

using Pulumi;
using Pulumi.Azure.AppService;
using Pulumi.Azure.AppService.Inputs;
using Pulumi.Azure.Core;
using Pulumi.Azure.Storage;

class NoraAzureStack : Stack
{
  public NoraAzureStack()
  {
    var resourceGroup = new ResourceGroup("nora");

    // App Service Plan
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

    var webApp = new AppService("nora-web", new AppServiceArgs
    {
      ResourceGroupName = resourceGroup.Name,
      HttpsOnly = true,
      AppServicePlanId = appServicePlan.Id
    });

    var hostName = new CustomHostnameBinding("nora-web-app-host", new CustomHostnameBindingArgs
    {
      Hostname = "dev-nora.momo-adew.com",
      AppServiceName = webApp.Name,
      ResourceGroupName = resourceGroup.Name
    });
  }

  [Output]
  public Output<string> ConnectionString { get; set; }
}

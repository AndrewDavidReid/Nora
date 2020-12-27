using Pulumi;
using Pulumi.Azure.AppService;
using Pulumi.Azure.Core;
using Pulumi.Azure.CosmosDB;
using Pulumi.Cloudflare;

class NoraAzureStack : Stack
{
  public NoraAzureStack()
  {
    var resourceGroup = new ResourceGroup(AddEnvironmentPrefix("nora"));

    // Reference existing app service plan.
    var appServicePlan = Plan.Get("home-projects-asp",
      "/subscriptions/72fc2865-939d-4afc-80d4-ab60f14c099e/resourceGroups/Shared/providers/Microsoft.Web/serverfarms/home-projects-asp");

    // Reference existing cosmosdb account.
    var cosmosDbAccount = Account.Get("home-projects-cosmosdb",
      "/subscriptions/72fc2865-939d-4afc-80d4-ab60f14c099e/resourceGroups/Shared/providers/Microsoft.DocumentDB/databaseAccounts/home-projects-cosmosdb");

    // Reference dns zone from cloudflare
    var dnsZone = Zone.Get("momo-adew.com", "b9eb57d1d01a353e72276d2fa0565d81");

    var webApp = new AppService(AddEnvironmentPrefix("nora-web"), new AppServiceArgs
    {
      ResourceGroupName = resourceGroup.Name,
      HttpsOnly = true,
      AppServicePlanId = appServicePlan.Id
    });

    var customDomainName = AddEnvironmentPrefix("nora.momo-adew.com");
    var verificationDomainName = $"asuid.{customDomainName}";

    var verificationRecord = new Record(AddEnvironmentPrefix("nora-web-txt-record"), new RecordArgs
    {
      Name = verificationDomainName,
      ZoneId = dnsZone.Id,
      Type = "TXT",
      Value = webApp.CustomDomainVerificationId,
      Ttl = 300
    });

    var cnameRecord = new Record(AddEnvironmentPrefix("nora-web-cname-record"), new RecordArgs
    {
      Name = customDomainName,
      ZoneId = dnsZone.Id,
      Type = "CNAME",
      Value = webApp.DefaultSiteHostname,
      Ttl = 300
    });

    var hostNameBinding = new CustomHostnameBinding(AddEnvironmentPrefix("nora-web-hostname"), new CustomHostnameBindingArgs
    {
      Hostname = customDomainName,
      AppServiceName = webApp.Name,
      ResourceGroupName = resourceGroup.Name
    });

    var sslCert = new ManagedCertificate("nora-web-cert", new ManagedCertificateArgs
    {
      CustomHostnameBindingId = hostNameBinding.Id
    });

    var sslCertBinding = new CertificateBinding("nora-web-cert-binding", new CertificateBindingArgs
    {
      HostnameBindingId = hostNameBinding.Id,
      CertificateId = sslCert.Id,
      SslState = "SniEnabled"
    });
  }

  private string AddEnvironmentPrefix(string input)
  {
    var environmentName = Deployment.Instance.StackName;

    // For non-prod environments, prefix the deployment with the stack name.
    return environmentName == "production" ? input : $"{environmentName}-{input}";
  }

  [Output]
  public Output<string> ConnectionString { get; set; }
}

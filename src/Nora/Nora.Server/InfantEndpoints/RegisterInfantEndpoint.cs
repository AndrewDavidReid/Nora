using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Nora.Shared.Infant;

namespace Nora.Server.InfantEndpoints
{
  public class RegisterInfantEndpoint : BaseAsyncEndpoint<RegisterInfant.Response, RegisterInfant.Response>
  {
    [HttpPost("/api/infant")]
    public override async Task<ActionResult<RegisterInfant.Response>> HandleAsync(RegisterInfant.Response request, CancellationToken cancellationToken = new CancellationToken())
    {
      return Ok(new Infant());
    }
  }
}

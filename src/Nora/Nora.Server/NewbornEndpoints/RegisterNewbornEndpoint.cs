using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Nora.Shared.Newborn;

namespace Nora.Server.NewbornEndpoints
{
  public class RegisterNewbornEndpoint : BaseAsyncEndpoint<RegisterNewborn.Response, RegisterNewborn.Response>
  {
    [HttpPost("/api/newborn")]
    public override async Task<ActionResult<RegisterNewborn.Response>> HandleAsync(RegisterNewborn.Response request, CancellationToken cancellationToken = new CancellationToken())
    {
      return Ok(new Newborn());
    }
  }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Nora.Shared.Infant;

namespace Nora.Server.InfantEndpoints
{
  public class GetInfantsEndpoint : BaseAsyncEndpoint<IEnumerable<Infant>>
  {
    [HttpGet("/api/infants")]
    public override async Task<ActionResult<IEnumerable<Infant>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      return Ok(new []
      {
        new Infant()
      });
    }
  }
}

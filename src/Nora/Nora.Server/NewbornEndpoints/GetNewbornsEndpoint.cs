using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Nora.Shared.Identity;
using Nora.Shared.Newborn;

namespace Nora.Server.NewbornEndpoints
{
  public class GetNewbornsEndpoint : BaseAsyncEndpoint<IEnumerable<Newborn>>
  {
    [HttpGet("/api/newborns")]
    public override async Task<ActionResult<IEnumerable<Newborn>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      return Ok(new []
      {
        new Newborn()
      });
    }
  }
}

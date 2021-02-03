using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Nora.Shared.Infant;

namespace Nora.Shared.Caretaker
{
  public class Caretaker
  {
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime RegisteredOn { get; set; }

    public IEnumerable<InfantSummary> Infants { get; set; } = new List<InfantSummary>();
  }
}

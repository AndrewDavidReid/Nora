using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Nora.Shared.Caretaker;
using Nora.Shared.Scheduling;

namespace Nora.Shared.Infant
{
  public class Infant
  {
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }

    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public IEnumerable<CaretakerSummary> Caretakers { get; set; } = new List<CaretakerSummary>();

    public FeedingSchedule FeedingSchedule { get; set; }
  }
}

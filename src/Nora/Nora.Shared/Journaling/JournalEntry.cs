using System;
using System.Text.Json.Serialization;

namespace Nora.Shared.Journaling
{
  public class JournalEntry
  {
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    public Guid InfantId { get; set; }

    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }

    public DateTime Time { get; set; }
    public int FeedingAmount { get; set; }
    public bool Pee { get; set; }
    public bool Poop { get; set; }
    public string Notes { get; set; }
  }
}

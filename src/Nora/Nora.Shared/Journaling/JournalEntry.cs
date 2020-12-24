using System;

namespace Nora.Shared.Journaling
{
  public class JournalEntry
  {
    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }

    public DateTime Time { get; set; }
    public int FeedingAmount { get; set; }
    public bool Pee { get; set; }
    public bool Poop { get; set; }
    public string Notes { get; set; }
  }
}

using System;
using System.Collections.Generic;
using Nora.Shared.Journaling;
using Nora.Shared.Scheduling;

namespace Nora.Shared.Infant
{
  public class Infant
  {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }

    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }


    public IEnumerable<Guid> GuardianIds { get; set; } = new List<Guid>();
    public IEnumerable<Guid> CaregiverIds { get; set; } = new List<Guid>();

    public FeedingSchedule FeedingSchedule { get; set; }
    public IEnumerable<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
  }
}

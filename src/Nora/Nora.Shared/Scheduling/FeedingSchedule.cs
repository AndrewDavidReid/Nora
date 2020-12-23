using System;
using System.Collections.Generic;

namespace Nora.Shared.Scheduling
{
  public class FeedingSchedule
  {
    public Guid Id { get; set; }
    public Guid NewbornId { get; set; }
    public string TimeZone { get; set; }

    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; }

    public IEnumerable<FeedingScheduleTime> FeedingTimes { get; set; } = new List<FeedingScheduleTime>();
  }
}

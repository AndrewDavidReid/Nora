using System;

namespace Nora.Shared.Newborn
{
  public class Newborn
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public Guid GuardianId { get; set; }
    public DateTime CreatedOn { get; set; }
  }
}

using System;

namespace Nora.Shared.Identity
{
  public class User
  {
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime RegisteredOn { get; set; }
  }
}

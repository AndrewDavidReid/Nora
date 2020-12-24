using System;

namespace Nora.Shared.Infant
{
  public static class RegisterInfant
  {
    public class Request
    {
      public string FirstName { get; set; }
      public string MiddleName { get; set; }
      public string LastName { get; set; }
      public DateTime DateOfBirth { get; set; }
    }

    public class Response
    {
      public Guid InfantId { get; set; }
    }
  }
}

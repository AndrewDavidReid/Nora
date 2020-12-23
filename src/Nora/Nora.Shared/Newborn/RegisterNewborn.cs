using System;

namespace Nora.Shared.Newborn
{
  public static class RegisterNewborn
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
      public Guid NewbornId { get; set; }
    }
  }
}

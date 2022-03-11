using Postdb.DTOs;

namespace Postdb.Models;

public enum Gender
{
    Male = 1,
    Female = 2,
}

    public record Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public long Mobile { get; set; }
        public string Email { get; set; }

        public string Address{ get; set; }
        public Gender Gender { get; set; }
        public CustomerDTO asDto => new CustomerDTO
        {
          CustomerId = CustomerId,
          FirstName = FirstName,
          LastName = LastName,
          Mobile = Mobile,
          Email = Email,
          
          Gender = Gender.ToString().ToLower(),
        };
    }  

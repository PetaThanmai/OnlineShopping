
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Postdb.DTOs;
public record CustomerDTO
{
    [JsonPropertyName("customer_id")]
    public long CustomerId { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("mobile")]

    public long Mobile { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    [JsonPropertyName("customers")]
    
    public List<OrderDTO> MyOrders{ get; set; }  


}

public record CreateCustomerDTO
{


    [JsonPropertyName("first_name")]
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }
    [JsonPropertyName("email")]

    [MaxLength(255)]
    public string Email { get; set; }
    [JsonPropertyName("gender")]
    [Required]
    [MaxLength(6)]
    public string Gender { get; set; }

    [JsonPropertyName("address")]
    [Required]
    [MaxLength(12)]
    public string Address { get; set; }


}


public record CustomerUpdateDTO
{






    [JsonPropertyName("mobile")]

    public long? Mobile { get; set; } = null;
    [JsonPropertyName("email")]

    [MaxLength(255)]
    public string Email { get; set; }




}





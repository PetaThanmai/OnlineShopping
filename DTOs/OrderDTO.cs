using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Postdb.DTOs;
public record OrderDTO
{
    [JsonPropertyName("order_id")]
    public long OrderId { get; set; }

    [JsonPropertyName("order_no")]
    public long OrderNo { get; set; }
    [JsonPropertyName("quantity")]
    public string Quantity { get; set; }
    //     [JsonPropertyName("mobile")]

    //     public long Mobile { get; set; }
    //     [JsonPropertyName("email")]
    //     public string Email { get; set; }
        [JsonPropertyName("customer_id")]
        public long CustomerId { get; set; }
        [JsonPropertyName("product")]
    
    public List<ProductDTO> Product { get; set; }  


    
}

public record CreateOrderDTO
{


    [JsonPropertyName("order_id")]
    [Required]
    [Range(1, 50)]
    public long OrderId { get; set; }

    [JsonPropertyName("order_no")]
    [Required]
    [Range(1, 50)]
    public long OrderNo { get; set; }

    [JsonPropertyName("quantity")]
    [Required]
    public string Quantity { get; set; }

    // [JsonPropertyName("email")]

    // [MaxLength(255)]
    // public string Email { get; set; }
    // [JsonPropertyName("gender")]
    // [Required]
    // [MaxLength(6)]
    // public string Gender { get; set; }

    [JsonPropertyName("customer_id")]
    [Required]
    public long CustomerId { get; set; }

}


public record OrderUpdateDTO
{
    [JsonPropertyName("order_no")]
    public long? OrderNo { get; set; } = null;

    [JsonPropertyName("quantity")]
    [MaxLength(255)]
    public string Quantity { get; set; }

    
}

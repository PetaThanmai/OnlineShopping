
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Postdb.Models;

namespace Postdb.DTOs;
public record ProductDTO
{
    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }

    [JsonPropertyName("product_name")]
    public string ProductName { get; set; }
    // [JsonPropertyName("product_type")]
    // public string ProductType { get; set; }
    // [JsonPropertyName("product_size")]

    // public long ProductSize { get; set; }
    [JsonPropertyName("prodcut_brand")]
    public string ProductBrand { get; set; }
    // [JsonPropertyName("prodcut_prize")]
    // public long ProductPrize { get; set; }
     [JsonPropertyName("order")]
    
    public List<OrderDTO> Order{ get; set; }  
     [JsonPropertyName("tags")]

    public List<Tags> Tags{get;set;}
}

public record CreateProductDTO
{


    [JsonPropertyName("product_id")]
    [Required]
    [Range(1, 50)]
    public int ProductId { get; set; }
    [JsonPropertyName("product_name")]
    [Required]
    [MaxLength(50)]
    public string  ProductName{ get; set; }
    // [JsonPropertyName("product_type")]
    // [Required]
    // public long ProductType { get; set; }
    // [JsonPropertyName("ProductSize")]

    // [MaxLength(255)]
    // public string  ProductSize { get; set; }
    [JsonPropertyName("prodcut_brand")]
    [Required]
    [MaxLength(6)]
    public string ProductBrand { get; set; }

    // [JsonPropertyName("prodcut_prize")]
    // [Required]
    // [MaxLength(12)]
    // public string ProductPrize { get; set; }
    [JsonPropertyName("order_id")]
    // [Required]
    // [MaxLength(6)]
    public int OrderId { get; set; }
    [JsonPropertyName("customer_id")]
    // [Required]
    // [MaxLength(6)]
    public int CustomerId{ get; set; }

}


public record ProductUpdateDTO
{
    [JsonPropertyName("product_name")]
    [MaxLength(50)]

    public long? ProductName { get; set; } = null;
    [JsonPropertyName("product_brand")]

    [MaxLength(120)]
    public string ProductBrand { get; set; }




}






using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Postdb.DTOs;
public record TagsDTO
{
    [JsonPropertyName("tag_id")]
    public long TagId { get; set; }

    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }
    // [JsonPropertyName("Tags_type")]
    // public string TagsType { get; set; }
    // [JsonPropertyName("Tags_size")]

    // public long TagsSize { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
     [JsonPropertyName("colour")]
     public string Colour { get; set; }

     [JsonPropertyName("tag_size")]
     public string TagSize{ get; set; }
     
}

public record CreateTagsDTO
{
[JsonPropertyName("tag_id")]
    [Required]
    
    public long TagId { get; set; }
    [JsonPropertyName("product_id")]
    [Required]
    
    public long ProductId{ get; set; }
    // [JsonPropertyName("product_type")]
    // [Required]
    // public long ProductType { get; set; }
    // [JsonPropertyName("ProductSize")]

    // [MaxLength(255)]
    // public string  ProductSize { get; set; }
    [JsonPropertyName("title")]
    [Required]
    [MaxLength(6)]
    public string Title { get; set; }

    // [JsonPropertyName("prodcut_prize")]
    // [Required]
    // [MaxLength(12)]
    // public string ProductPrize { get; set; }
    [JsonPropertyName("colour")]
    // [Required]
    // [MaxLength(6)]
    public string Colour { get; set; }
    [JsonPropertyName("tag_size")]
    // [Required]
    // [MaxLength(6)]
    public string TagSize{ get; set; }



     


}


public record TagsUpdateDTO
{


    [JsonPropertyName("title")]
    public string Title { get; set; }
     [JsonPropertyName("colour")]
     public string Colour { get; set; }

     [JsonPropertyName("tag_size")]
     public string TagSize{ get; set; }
     


}





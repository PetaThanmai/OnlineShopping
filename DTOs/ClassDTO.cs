using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace School.DTOs;
public record ClassDTO
{
    [JsonPropertyName("class_id")]
    public long ClassId { get; set; }

    // [JsonPropertyName("class_no")]
    // public long ClassNo { get; set; }
    [JsonPropertyName("capaity")]
    public long Capacity { get; set; }
      public string Email { get; set; }
        [JsonPropertyName("room_type")]
        public int RoomType { get; set; }
        // [JsonPropertyName("product")]
    
    // public List<ProductDTO> Product { get; set; }
}

public record CreateClassDTO

{

    [JsonPropertyName("class_id")]
    [Required]
    // [Range(1, 50)]
    public long ClassId { get; set; }
}

   


public record ClassUpdateDTO
{
    
    [JsonPropertyName("capacity")]
    [MaxLength(255)]
    public string Capacity{ get; set; }

    
}

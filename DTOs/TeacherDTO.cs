
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace School.DTOs;
public record TeacherDTO
{
    [JsonPropertyName("teacher _id")]
    public int TeacherId { get; set; }

    [JsonPropertyName("teacher_name")]
    public string TeacherName { get; set; }
    [JsonPropertyName("teacher_sub")]
    public string TeacherSub { get; set; }
    [JsonPropertyName("mobile")]

    public long Mobile { get; set; }
    // [JsonPropertyName("email")]
    // public string Email { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    // [JsonPropertyName("teachers")]
    
    // public List<OrderDTO> MyOrders{ get; set; }  

    [JsonPropertyName("student")]

    public List<StudentDTO> Student { get; set; }

    [JsonPropertyName("subject")]

    public List<SubjectDTO> Subject { get; set; }


}

public record CreateTeacherDTO
{


    [JsonPropertyName("teacher_id")]
    [Required]
    // [MaxLength(50)]
    public int TeacherId { get; set; }
    [JsonPropertyName("teacher_name")]
    [Required]
    // [MaxLength(50)]
    public string TeacherName { get; set; }
    [JsonPropertyName("teacher_sub")]

    [MaxLength(255)]
    public string TeacherSub { get; set; }


    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }
    // [JsonPropertyName("email")]

    // [MaxLength(255)]
    // public string Email { get; set; }
    [JsonPropertyName("gender")]
    [Required]
    [MaxLength(6)]
    public string Gender { get; set; }

    // [JsonPropertyName("address")]
    // [Required]
    // [MaxLength(12)]
    // public string Address { get; set; }


}


public record TeacherUpdateDTO
{

 [JsonPropertyName("teacher_sub")]

    [MaxLength(255)]
    public string TeacherSub { get; set; }


    [JsonPropertyName("mobile")]

    public long? Mobile { get; set; } = null;
    // [JsonPropertyName("teacher_sub")]

    // [MaxLength(255)]
    // public string TeacherSub { get; set; }




}






using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using School.Models;

namespace School.DTOs;
public record StudentDTO
{
    [JsonPropertyName("student_id")]
    public int StudentId { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("date_of_birth")]

    public  DateTimeOffset DateOfBirth { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    // [JsonPropertyName("prodcut_prize")]
    // public long studentPrize { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
        [JsonPropertyName("class_id")]

    public  int ClassId { get; set; }
    [JsonPropertyName("teacher_id")]
         public int TeacherId { get; set; }

[JsonPropertyName("teacher")]

    public List<TeacherDTO> Teacher { get; set; }

    [JsonPropertyName("subject")]

    public List<SubjectDTO> Subject { get; set; }
    
     
}
public  record CreateStudentDTO
{
    [JsonPropertyName("student_id")]
    public int StudentId { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("date_of_birth")]

    public  DateTimeOffset DateOfBirth { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    // [JsonPropertyName("prodcut_prize")]
    // public long studentPrize { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("class_id")]

    public  int ClassId { get; set; }
    [JsonPropertyName("teacher_id")]
         public int TeacherId { get; set; }
         

    // public List<OrderDTO> Order { get; set; }
    // [JsonPropertyName("tags")]

    // public List<TagsDTO> Tags { get; set; }

}


public record StudentUpdateDTO
{
     [JsonPropertyName("date_of_birth")]

    public  DateTimeOffset DateOfBirth { get; set; }
     [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
}





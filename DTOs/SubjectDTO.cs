
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace School.DTOs;
public record SubjectDTO
{
    // [JsonPropertyName("tags_id")]
    // public long TagsId { get; set; }

    [JsonPropertyName("subject _id")]
    public int SubjectId { get; set; }
    // [JsonPropertyName("Tags_type")]
    // public string TagsType { get; set; }
    // [JsonPropertyName("Tags_size")]
    [JsonPropertyName("sub_name")]
    [Required]
    public string SubName{ get; set; }
    [JsonPropertyName("sub_teacher")]

    [MaxLength(255)]
    public string  SubTeacher { get; set; }
    [JsonPropertyName("teacher_id")]
    public int TeacherId { get; set; }


   
}

public record CreateSubjectDTO
{
    // [JsonPropertyName("tags_id")]
    // [Required]

    // public long TagsId { get; set; }
    [JsonPropertyName("subject_id")]
    [Required]

    public int SubjectId { get; set; }
    [JsonPropertyName("sub_name")]
    [Required]
    public string SubName{ get; set; }
    


    // [JsonPropertyName("title")]
    // [Required]
    // [MaxLength(6)]
    // // public string Title { get; set; }

    // // [JsonPropertyName("prodcut_prize")]
    // // [Required]
    // // [MaxLength(12)]
    // // public string subjectPrize { get; set; }
    // [JsonPropertyName("colour")]
    // // [Required]
    // // [MaxLength(6)]
    // public string Colour { get; set; }
    // [JsonPropertyName("tag_size")]
    // // [Required]
    // // [MaxLength(6)]
    // public string TagSize { get; set; }


}


public record SubjectUpdateDTO
{
 [JsonPropertyName("sub_name")]
    [Required]
    public string SubName{ get; set; }
}





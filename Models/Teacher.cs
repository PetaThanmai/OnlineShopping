// using Postdb.DTOs;
using School.DTOs;
// using SCHOOL.DTOs;

namespace School.Models;

public enum Gender
{
    Male = 1,
    Female = 2,
}

    public record Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSub { get; set; }

        public long Mobile { get; set; }
        // public string Email { get; set; }

        // public string Address{ get; set; }
        public string Gender { get; set; }
        public TeacherDTO asDto => new TeacherDTO
        {
          TeacherId = TeacherId,
          TeacherName = TeacherName,
          TeacherSub=TeacherSub,
          Mobile = Mobile,
          
          Gender = Gender,
        };
    }  

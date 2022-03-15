// using Postdb.DTOs;
using School.DTOs;
// using SCHOOL.DTOs;

namespace School.Models;

    public record Subject
    {
        public int SubjectId { get; set; }
      
        public string SubName { get; set; }

        // public string SubjectType { get; set; }
        // public string SubjectSize { get; set; }


        public SubjectDTO asDto => new SubjectDTO
        {
          SubjectId =SubjectId ,
          SubName=SubName,
        
      
    //       // SubjectBrand=SubjectBrand,
          
        }; 
    
    }

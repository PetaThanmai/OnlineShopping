// using Postdb.DTOs;
using School.DTOs;

namespace School.Models;


    public record Student
    {
        // public int StudentId { get; set; }
        // public string VendorNo { get; set; }
        // public string StudentName { get; set; }

        // public long StudentType { get; set; }
        // public string StudentSize { get; set; }

        // public string StudentBrand{ get; set; }
        // public long StudentPrize { get; set; }

        public int StudentId { get; set; }
        public string FirstName{get;set;}
        public string LastName  { get; set; }
        public DateTimeOffset DateOfBirth  {get;set; }
        
        public string Gender  {get;set; }
        public long Mobile  {get;set; }
        // public long DateOfBirth  {get;set; }
         public  int ClassId { get; set; }
         public int TeacherId { get; set; }
        

        public StudentDTO asDto => new StudentDTO
        {
          
          StudentId=StudentId,
          FirstName=FirstName,
          LastName=LastName,
          DateOfBirth=DateOfBirth,
          Gender=Gender,
          Mobile=Mobile,
          ClassId=ClassId,
          TeacherId=TeacherId,

          
        }; 
    
    }
// using Postdb.DTOs;
// using SCHOOL.DTOs;

using School.DTOs;

namespace School.Models;




    public record Class
    {
        public long ClassId { get; set; }
      
        public long Capacity { get; set; }

        public int RoomType { get; set; }
        // public string ClassSize { get; set; }

        // public string Colour{ get; set; }
        // public string ClassPrize { get; set; }

        // public string Classize { get; set; }
        // public long CustomerId { get; set; }
        public ClassDTO asDto => new ClassDTO
        {
          ClassId =ClassId ,
          
          Capacity=Capacity,
          RoomType=RoomType,

      
        //   ClassId=ClassId,
          // ProductId=ProductId,
          // Title=Title,
          // Colour=Colour,
          // Classize=Classize,
          
        }; 
    
    }

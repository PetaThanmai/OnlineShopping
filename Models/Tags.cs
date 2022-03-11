using Postdb.DTOs;

namespace Postdb.Models;




    public record Tags
    {
        public long TagId { get; set; }
      
        public long ProductId { get; set; }

        public string Title { get; set; }
        // public string TagsSize { get; set; }

        public string Colour{ get; set; }
        // public string TagsPrize { get; set; }

        public string TagSize { get; set; }
        // public long CustomerId { get; set; }
        public TagsDTO asDto => new TagsDTO
        {
          TagId =TagId ,
          
          // TagsType=TagsType,
      
        //   TagsId=TagsId,
          ProductId=ProductId,
          Title=Title,
          Colour=Colour,
          TagSize=TagSize,
          
        }; 
    
    }

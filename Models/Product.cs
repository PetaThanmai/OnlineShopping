using Postdb.DTOs;

namespace Postdb.Models;




    public record Product
    {
        public long ProductId { get; set; }
      
        public string ProductName { get; set; }

        public string ProductType { get; set; }
        // public string ProductSize { get; set; }

        public string ProductBrand{ get; set; }
        // public string ProductPrize { get; set; }

        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public ProductDTO asDto => new ProductDTO
        {
          ProductId =ProductId ,
          ProductName=ProductName,
          // ProductType=ProductType,
      
          ProductBrand=ProductBrand,
          
        }; 
    
    }

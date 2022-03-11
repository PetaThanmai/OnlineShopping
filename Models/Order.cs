using Postdb.DTOs;

namespace Postdb.Models;


    public record Order
    {
        // public int OrderId { get; set; }
        // public string VendorNo { get; set; }
        // public string OrderName { get; set; }

        // public long OrderType { get; set; }
        // public string OrderSize { get; set; }

        // public string OrderBrand{ get; set; }
        // public long OrderPrize { get; set; }

        public long OrderId { get; set; }
        public long OrderNo{get;set;}
        public string Quantity  { get; set; }
        public long CustomerId  {get;set; }
        public OrderDTO asDto => new OrderDTO
        {
          
          OrderId=OrderId,
          OrderNo=OrderNo,
          Quantity=Quantity,
          CustomerId=CustomerId,
        }; 
    
    }
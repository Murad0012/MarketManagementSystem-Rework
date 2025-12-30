using System;
using MarketSystem.Data.Common;

namespace MarketSystem.Data.Models
{
	public class SaleItem : BaseModel
	{
        private static int id { get; set; }

        public SaleItem()
		{
            ID = id;
            id++;
        }

        public Product Product { get; set; }
        public int Count { get; set; }
    }
}


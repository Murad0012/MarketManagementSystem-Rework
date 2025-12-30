using System;
using MarketSystem.Data.Common;

namespace MarketSystem.Data.Models
{
	public class Sale : BaseModel
	{
        private static int id;

        public Sale()
		{
            ID = id;
            id++;
        }

        public decimal Amount { get; set; }
        public List<SaleItem> SaleItems { get; set; }
        public DateTime Date { get; set; }
    }
}


using System;
using MarketSystem.Data.Common;
using MarketSystem.Data.Enums;

namespace MarketSystem.Data.Models
{
	public class Product : BaseModel
	{
		private static int id;

		public Product()
		{
			ID = id;
			id++;
		}

		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public Category Category { get; set; }
		public int Count { get; set; }
	}
}


using System;
using System.Collections;
using MarketSystem.Data.Enums;
using MarketSystem.Data.Models;

namespace MarketSystem.Services.Abstract
{
	public interface IMarketable
	{
		public int AddProduct(string name, decimal price, Category category, int count);
		public void UpdateProduct(int id, string name, decimal price, Category category, int count);
		public void DeleteProduct(int id);
		public List<Product> GetProducts();
        public List<Product> GetProductsByCategory(Category category);
        public List<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
        public List<Product> GetProductsByName(string name);

        public void AddSale(Dictionary<int, int> products); 
        public void ReturnProductFromSale(int saleID, Dictionary<int, int> products);
        public void DeleteSale(int id);
        public List<Sale> GetSales();
        public List<Sale> GetSalesByDateRange(DateTime minDate, DateTime maxDate);
        public List<Sale> GetSalesByAmountRange(decimal minAmount, decimal maxAmount);
        public List<Sale> GetSalesByDate(DateTime date);
        public Sale GetSale(int id);

    }
}


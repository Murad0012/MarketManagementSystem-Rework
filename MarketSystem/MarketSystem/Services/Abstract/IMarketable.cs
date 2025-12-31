using System;
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
        public List<Product> GetProductsByPriceRange(int minPrice, int maxPrice);
        public List<Product> GetProductsByName(string name);

        public void AddSale(); // method isn't ready
        public void ReturnProductFromSale(int saleId,int productId,int count);
        public void DeleteSale(int id);
        public List<Sale> GetSales();
        public List<Sale> GetSalesByDateRange(DateTime minDate, DateTime maxDate);
        public List<Sale> GetSalesByAmount(int minAmount, int maxAmount);
        public List<Sale> GetSalesByDate(DateTime date);
        public List<Sale> GetSale(int id);

    }
}


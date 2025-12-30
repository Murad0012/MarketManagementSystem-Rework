using System;
using MarketSystem.Data.Enums;
using MarketSystem.Data.Models;
using MarketSystem.Services.Abstract;

namespace MarketSystem.Services.Concrete
{
    public class MarketService : IMarketable
    {
        private List<Product> _products = new();

        public int AddProduct(string name, decimal price, Category category, int count)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name of product can't be empty!");

            if (price < 0)
                throw new Exception("Price of product can't be less than zero!");

            if(count < 0)
                throw new Exception("Count of product can't be less than zero!");


            var product = new Product()
            {
                Name = name,
                Price = price,
                Count = count,
                Category = category
            };

            if (_products.Any(p => p.Name == name && p.Category == category && p.Price == price))
            {
                var a = _products.FirstOrDefault(p => p.Name == name && p.Category == category && p.Price == price);

                a!.Count += count;

                return product.ID;

            }

            _products.Add(product);

            return product.ID;
        }

        public void AddSale()
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteSale(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByPriceRange(int minPrice, int maxPrice)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetSale(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetSales()
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetSalesByAmount(int minAmount, int maxAmount)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetSalesByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetSalesByDateRange(DateTime minDate, DateTime maxDate)
        {
            throw new NotImplementedException();
        }

        public void ReturnProductFromSale(int saleId, int productId, int count)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}


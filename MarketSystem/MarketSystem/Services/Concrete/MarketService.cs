using System;
using System.Diagnostics;
using MarketSystem.Data.Enums;
using MarketSystem.Data.Models;
using MarketSystem.Services.Abstract;

namespace MarketSystem.Services.Concrete
{
    public class MarketService : IMarketable
    {
        private List<Product> _products = new();
        private List<Sale> _sales = new();
        // Ready
        // Product methods
        public List<Product> GetProducts()
        {
            return _products;
        }

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

        public void UpdateProduct(int id, string name, decimal price, Category category, int count)
        {
            var product = _products.FirstOrDefault(p => p.ID == id);

            if (product == null)
                throw new Exception($"Product with ID:{id} was not found! ");

            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name of product can't be empty!");

            if (price < 0)
                throw new Exception("Price of product can't be less than zero!");

            if (count < 0)
                throw new Exception("Count of product can't be less than zero!");


            product!.Name = name;
            product.Category = category;
            product.Count = count;
            product.Price = price;

        }

        public void DeleteProduct(int id)
        {
            if (id < 0)
                throw new Exception("ID of product can't be less than zero!");

            var product = _products.FirstOrDefault(p => p.ID == id);

            _products.Remove(product!);
        }

        public List<Product> GetProductsByCategory(Category category)
        {
            var products = _products.Where(p => p.Category == category).ToList();

            return products;
        }

        public List<Product> GetProductsByPriceRange(int minPrice, int maxPrice)
        {
            if (minPrice > maxPrice)
                throw new Exception("Min price can't be higher than max price");

            if (minPrice < 0)
                throw new Exception("Min price can't be less than zero");

            if (maxPrice < 0)
                throw new Exception("Max price can't be less than zero");

            var products = _products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();

            return products;
        }

        public List<Product> GetProductsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name of product can't be empty!");

            var products = _products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();

            return products;
        }

        // Sale Methods
        public List<Sale> GetSales()
        {
            return _sales;
        }

        public Sale GetSale(int id)
        {
            if (id < 0)
                throw new Exception("ID of product can't be less than zero!");

            var sale = _sales.FirstOrDefault(s => s.ID == id);

            if (sale == null)
                throw new Exception("Sale was not found!");

            return sale!;
        }

        public List<Sale> GetSalesByAmountRange(int minAmount, int maxAmount)
        {
            if(minAmount > maxAmount)
                throw new Exception("Min amount can't be higher than max amount");

            if (minAmount < 0)
                throw new Exception("Min amount can't be less than zero");

            if (maxAmount < 0)
                throw new Exception("Max amount can't be less than zero");

            var sales = _sales.Where(s => s.Amount >= minAmount && s.Amount <= maxAmount).ToList();

            return sales;
        }

        public List<Sale> GetSalesByDate(DateTime date)
        {
            var sales = _sales.Where(s => s.Date == date).ToList();

            return sales;
        }

        public List<Sale> GetSalesByDateRange(DateTime minDate, DateTime maxDate)
        {
            if (minDate > maxDate)
                throw new Exception("Min date can't be higher than max date");

            var sales = _sales.Where(s => s.Date >= minDate && s.Date <= maxDate).ToList();

            return sales;
        }

        // Unready

        public void DeleteSale(int id)
        {
            throw new NotImplementedException();
        }
        
        public void AddSale()
        {
            throw new NotImplementedException();
        }

        public void ReturnProductFromSale(int saleId, int productId, int count)
        {
            throw new NotImplementedException();
        }

    }
}

